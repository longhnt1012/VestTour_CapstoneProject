import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:jwt_decode/jwt_decode.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:mobileapp/data_source/login/api_services.dart';
import 'package:mobileapp/models/store.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ApiServices {
  // Define the getAuthToken method here

  static Future<String?> getAuthToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString('auth_token');  // Returns the token if exists, else null
  }
  static Future<int?> getUserIdFromToken(String token) async {
    try {
      // Decode the token and extract userId
      Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
      return int.tryParse(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'].toString());
    } catch (e) {
      print("Error decoding token: $e");
      return null;  // Return null if token decoding fails
    }
  }



  // Post booking API
  static Future<bool> postBooking({

    required int storeId,
    required DateTime date,
    required TimeOfDay time,
    required String service,
    required int userId,
    required String status

  }) async {

      // Get the auth token from shared preferences
      String? authToken = await getAuthToken();

      if (authToken == null) {
        return false; // Return false if no token found (user not authenticated)
      }

      // Get the userId from the token
      int? userId = await getUserIdFromToken(authToken);

      if (userId == null) {
        return false; // Return false if userId is not found in token
      }
    // Prepare time as a string in the correct format (HH:mm:ss)
      String timeString = '${time.hour.toString().padLeft(2, '0')}:${time.minute.toString().padLeft(2, '0')}:00';  // Format: HH:mm:ss

    final url = 'http://165.22.243.162:8080/api/Booking/loggedin-user-booking';
    final response = await http.post(

      Uri.parse(url),
      headers: {
        'Authorization': 'Bearer $authToken',  // Add token in Authorization header
        'Content-Type': 'application/json',
      },
      body: jsonEncode({

        'storeId': storeId,
        'bookingDate': date.toIso8601String().split('T')[0],  // Send only the date part (YYYY-MM-DD)
        'time': timeString,
        'service': service,
        'userId': userId, // Use null since userId should be inferred from the token
        'status': 'on-going',
      }),
    );
      print('storeId: $storeId');
      print('bookingDate: ${date.toIso8601String().split('T')[0]}');
      print('time: $timeString');
      print('service: $service');
      print('userId: $userId');
      print('status: on-going');
    if (response.statusCode == 200) {
      return true; // Successfully booked
    } else {
      return false; // Failed to book
    }
  }

  static Future<List<Store>> getAllStores() async {
    final url = 'http://165.22.243.162:8080/api/Store';
    final response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      List<dynamic> data = jsonDecode(response.body);
      return data.map((store) => Store.fromJson(store)).toList();
    } else {
      return [];
    }
  }
}

