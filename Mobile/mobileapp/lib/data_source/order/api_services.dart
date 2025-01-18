import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:mobileapp/models/orderDetail.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../models/order.dart';

class ApiServicesOrder {
  static const String baseUrl = "https://vesttour.xyz/api/Orders/user";

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

  // Function to get userId from SharedPreferences
  static Future<int?> getUserIdFromSharedPreferences() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final authToken = prefs.getString('auth_token');

    if (authToken != null) {
      return await getUserIdFromToken(authToken);
    }
    return null;
  }
  static Future<List<Order>> showOrdersByUser(int userId) async {
    final String url = "$baseUrl/$userId";

    try {
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        // Parse the JSON response
        print("Response body: ${response.body}");
        List<dynamic> responseData = json.decode(response.body);
        return responseData.map((order) => Order.fromJson(order)).toList();
      } else {
        print("Failed to fetch orders. Status code: ${response.statusCode}");
        return [];
      }
    } catch (e) {
      print("An error occurred while fetching orders: $e");
      return [];
    }
  }
  static Future<List<OrderDetails>?> getOrderDetails(int? orderId) async {
    final url = Uri.parse('https://vesttour.xyz/api/Orders/$orderId/details');

    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        if (data['orderDetails'] != null) {
          return (data['orderDetails'] as List)
              .map((item) => OrderDetails.fromJson(item))
              .toList();
        } else {
          return [];
        }
      } else {
        throw Exception('Failed to load order details');
      }
    } catch (error) {
      // Handle any errors that occur during the request
      throw Exception('Failed to load order details: $error');
    }
  }
}
