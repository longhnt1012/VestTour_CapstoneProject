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




  /// Lấy zodiac dựa trên ngày sinh
  Future<Map<String, dynamic>?> getZodiac(String dateOfBirth) async {
    final String endpoint =
        "https://vesttour.xyz/api/User/get-zodiac?birthDate=$dateOfBirth";

    try {
      final response = await http.get(Uri.parse(endpoint));

      if (response.statusCode == 200) {
        // Parse JSON response
        final Map<String, dynamic> data = json.decode(response.body);
        print('$data');

        // Trả về cả "zodiacSign" và "suggestedColors"
        return {
          'zodiacSign': data['zodiacSign'],
          'suggestedColors': data['suggestedColors']
        };
      } else {
        print("Failed to fetch zodiac. Status code: ${response.statusCode}");
        return null;
      }
    } catch (e) {
      print("Error while fetching zodiac: $e");
      return null;
    }
  }
  Future<Map<String, dynamic>> forgotPassword(String email) async {
    String url="https://vesttour.xyz/api/User/forgot-password";
    final response = await http.post(
      Uri.parse(url),
      headers: {
        'Content-Type': 'application/json',
      },
      body: jsonEncode({
        'email': email,
      }),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body); // Trả về dữ liệu nhận được
    } else {
      throw Exception('Failed to send request');
    }
  }
  Future<Map<String, dynamic>> resetPassword(String token, String newPassword) async {
    String url="https://vesttour.xyz/api/User/reset-password";
    final response = await http.post(
      Uri.parse(url),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'token': token, 'newPassword': newPassword}),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);  // Dữ liệu trả về sau khi reset mật khẩu thành công
    } else {
      throw Exception('Failed to reset password');
    }
  }
  Future<User?> getUserProfile(int userId) async {
    final userId = await getUserIdFromSharedPreferences();
    final url = Uri.parse("$baseUrl/User/$userId");
    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        // Parse the JSON data into a User object
        final Map<String, dynamic> data = json.decode(response.body);
        return User.fromJson(data);
      } else {
        // Handle other status codes
        print("Failed to fetch user. Status code: ${response.statusCode}");
        return null;
      }
    } catch (error) {
      // Handle connection or parsing errors
      print("Error fetching user by ID: $error");
      return null;
    }
  }
  Future<bool> updateUser(int userId, Map<String, dynamic> updatedData) async {
    final url = Uri.parse('https://vesttour.xyz/api/User/$userId');
    try {

      final authToken = await getAuthToken();
      final response = await http.put(
        url,
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $authToken', // Nếu cần
        },
        body: jsonEncode(updatedData),
      );

      if (response.statusCode == 204) {
        return true; // Update thành công
      } else {
        print('Error: ${response.statusCode}, ${response.body}');
        return false; // Lỗi từ server
      }
    } catch (e) {
      print('Error: $e');
      return false; // Lỗi từ client
    }
  }

}
