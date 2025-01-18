import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../../models/payment.dart';
 // Import the Payment class

class ApiServicePayment {
  static String baseUrl = 'https://vesttour.xyz/api/Payments';

  // Lấy auth_token từ SharedPreferences
  static Future<String?> getAuthToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString('auth_token');  // Trả về token nếu có, nếu không trả về null
  }
  
  // Giải mã JWT token và lấy userId
  static Future<int?> getUserIdFromToken(String token) async {
    try {
      // Giải mã token và lấy userId
      Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
      return int.tryParse(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']?.toString() ?? '');
    } catch (e) {
      print("Lỗi giải mã token: $e");
      return null;  // Trả về null nếu giải mã thất bại
    }
  }

  // Hàm lấy userId từ SharedPreferences
  static Future<int?> getUserIdFromSharedPreferences() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final authToken = prefs.getString('auth_token');

    if (authToken != null) {
      return await getUserIdFromToken(authToken);
    }
    return null;
  }

  static Future<bool> postPayment(Payment payment) async {
    try {
      // Convert the payment object to JSON
      final String requestBody = json.encode(payment.toJson());
      String? authToken = await getAuthToken();
      // Log the headers and request body
      print("Request Headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ${await getAuthToken()}' }");
      print("Request Body: $requestBody");

      // Send the HTTP POST request
      final response = await http.post(
        Uri.parse(baseUrl),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $authToken',
        },
        body: requestBody,
      );

      // Log the response details
      print("Response Status Code: ${response.statusCode}");
      print("Response Body: ${response.body}");

      if (response.statusCode == 200 ||response.statusCode == 201) {
        print('Payment successful: ${response.body}');
        return true;
      } else {
        print('Failed to post payment: ${response.statusCode}');
        print('Response Error: ${response.body}');
        return false;
      }
    } catch (e) {
      // Catch and log any exceptions
      print("Exception occurred during payment: $e");
      return false;
    }
  }
}
