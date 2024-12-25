import 'dart:convert';
import 'package:http/http.dart' as http;

class ApiServiceRegister {
  final String baseUrl = 'http://165.22.243.162:8080/api';

  /// Register a new user
  /// [data] contains user registration details.
  /// Example: {
  ///   "name": "John Doe",
  ///   "gender": "Male",
  ///   "address": "123 Street",
  ///   "dob": "2000-01-01",
  ///   "phone": "1234567890",
  ///   "email": "john.doe@example.com",
  ///   "password": "password123"
  /// }
  Future<http.Response> registerUser(Map<String, dynamic> data) async {
    final url = Uri.parse('$baseUrl/Register');
    try {
      final response = await http.post(
        url,
        headers: {"Content-Type": "application/json"},
        body: jsonEncode(data),
      );
      return response;
    } catch (error) {
      throw Exception('Error registering user: $error');
    }
  }

  /// Confirm the email using OTP
  /// [email] is the user's email address.
  /// [otp] is the OTP code sent to the email.
  Future<http.Response> confirmEmail(String email, String otp) async {
    final url = Uri.parse('$baseUrl/Register/confirm-email');
    final body = {
      "email": email,
      "otp": otp,
    };
    try {
      final response = await http.post(
        url,
        headers: {"Content-Type": "application/json"},
        body: jsonEncode(body),
      );
      return response;
    } catch (error) {
      throw Exception('Error confirming email: $error');
    }
  }
}
