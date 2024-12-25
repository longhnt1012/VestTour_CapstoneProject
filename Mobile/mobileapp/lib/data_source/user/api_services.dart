import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import '../../models/user.dart';


class ApiService {
  static const String baseUrl = "http://165.22.243.162:8080/api";

  // Get the auth token from SharedPreferences
  static Future<String?> getAuthToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString('auth_token');  // Returns the token if exists, else null
  }

  // Decode JWT token and extract userId
  static Future<int?> getUserIdFromToken(String token) async {
    try {
      // Decode the token and extract the userId
      Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
      return int.tryParse(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']?.toString() ?? '');
    } catch (e) {
      print("Error decoding token: $e");
      return null;  // Return null if token decoding fails
    }
  }

  // Function to get userId from SharedPreferences
  static Future<int?> getUserIdFromSharedPreferences() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final authToken = prefs.getString('auth_token');

    if (authToken != null) {
      return await getUserIdFromToken(authToken);
    }
    return null;
  }

  // Function to get user by userId from the API
  static Future<User?> getUserById() async {
    final userId = await getUserIdFromSharedPreferences();
    if (userId == null) {
      print("User ID not found");
      return null;
    }

    final authToken = await getAuthToken();
    if (authToken == null) {
      print("Auth Token not found");
      return null;
    }

    final response = await http.get(
      Uri.parse('$baseUrl/User/$userId'),
      headers: {
        'Authorization': 'Bearer $authToken',  // Add the token in headers for authorization
      },
    );

    if (response.statusCode == 200) {
      // If the server returns a 200 OK response, parse the user data
      final Map<String, dynamic> responseData = json.decode(response.body);
      return User.fromJson(responseData);
    } else {
      print('Failed to load user: ${response.statusCode}');
      return null;
    }
  }
}
