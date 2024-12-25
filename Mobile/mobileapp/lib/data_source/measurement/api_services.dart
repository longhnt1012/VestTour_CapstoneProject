import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

import '../../models/measurement.dart';

class ApiService {
  static const String baseUrl = "http://165.22.243.162:8080/api/Measurement/user";

  /// Get the auth token from SharedPreferences
  static Future<String?> getAuthToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString('auth_token');
  }

  /// Decode the JWT token and extract the userId
  static Future<int?> getUserIdFromToken(String token) async {
    try {
      Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
      return int.tryParse(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']?.toString() ?? '');
    } catch (e) {
      print("Error decoding token: $e");
      return null;
    }
  }

  /// Get userId from SharedPreferences
  static Future<int?> getUserIdFromSharedPreferences() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final authToken = prefs.getString('auth_token');
    if (authToken != null) {
      return await getUserIdFromToken(authToken);
    }
    return null;
  }

  /// Fetch measurements for the user
  static Future<Measurement?> getMeasurementForUser() async {
    try {
      final int? userId = await getUserIdFromSharedPreferences();

      if (userId == null) {
        print("User ID is null.");
        return null;
      }

      final String url = "$baseUrl/$userId";
      final response = await http.get(
        Uri.parse(url),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer ${await getAuthToken()}",
        },
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        return Measurement.fromJson(data);
      } else {
        print("Failed to fetch measurement. Status Code: ${response.statusCode}");
        print("Response Body: ${response.body}");
        return null;
      }
    } catch (e) {
      print("Error fetching measurement: $e");
      return null;
    }

  }
  static Future<http.Response> postMeasurement(Map<String, dynamic> measurementData) async {
    final String url = 'http://165.22.243.162:8080/api/Measurement';
    final int? userId = await getUserIdFromSharedPreferences();

    if (userId == null) {
      print("User ID is null.");

    }
    try {

      final response = await http.post(
        Uri.parse(url),
        headers: { "Content-Type": "application/json",
          "Authorization": "Bearer ${await getAuthToken()}"},
        body: jsonEncode(measurementData),
      );

      if (response.statusCode == 200 || response.statusCode == 201) {
        print('Measurement posted successfully: ${response.body}');
      } else {
        print('Failed to post measurement: ${response.statusCode} - ${response.body}');
      }

      return response;
    } catch (e) {
      print('Error posting measurement: $e');
      rethrow;
    }
  }

  static Future<bool> updateMeasurement(int measurementId, Map<String, dynamic> data) async {
    final url = Uri.parse("http://165.22.243.162:8080/api/Measurement/$measurementId");

    try {
      final response = await http.put(
        url,
        headers: {
          'Content-Type': 'application/json',
        },
        body: jsonEncode(data),
      );

      if (response.statusCode == 200 || response.statusCode==204) {
        // Thành công
        print("Measurement updated successfully.");
        return true;
      } else {
        // Lỗi từ server
        print("Failed to update measurement: ${response.statusCode} - ${response.body}");
        return false;
      }
    } catch (e) {
      // Lỗi ngoại lệ
      print("Error updating measurement: $e");
      return false;
    }
  }

}