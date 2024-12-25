import 'dart:convert';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;
import '../../../models/booking.dart';
import '../../../models/store.dart';

class ApiServices {
  static const String _baseUrl = 'http://165.22.243.162:8080/api';

  // Fetch auth token from SharedPreferences
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

  // Fetch all bookings for a user
  static Future<List<Booking>> getUserBookings(int userId) async {
    final response = await http.get(Uri.parse('$_baseUrl/Booking/user-booking?userId=$userId'));

    if (response.statusCode == 200) {
      // Parse the response body to get the bookings
      List<dynamic> data = json.decode(response.body);
      return data.map((booking) => Booking.fromJson(booking)).toList();
    } else {
      throw Exception('Failed to load bookings');
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
  static Future<Store?> getStoreById(int storeId) async {
    final url = '$_baseUrl/Store/$storeId'; // Adjust the API endpoint to fetch a single store by its ID
    final response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      // If the response is successful, parse the JSON and return a single Store object
      Map<String, dynamic> data = jsonDecode(response.body);
      return Store.fromJson(data); // Assuming Store.fromJson handles the map correctly
    } else {
      return null; // Return null if the request fails or store is not found
    }
  }

}
