import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthService {
  static const String baseUrl = 'http://165.22.243.162:8080/api/Login/login';

  // Login method to authenticate the user and retrieve the token
  Future<bool> login(String email, String password) async {
    final response = await http.post(
      Uri.parse(baseUrl),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'email': email,
        'password': password,
      }),
    );

    if (response.statusCode == 200) {
      final responseData = json.decode(response.body);
      final token = responseData['token'];  // Lấy token từ response

      // Lưu token vào SharedPreferences
      final prefs = await SharedPreferences.getInstance();
      await prefs.setString('auth_token', token);

      // Lấy userId từ token và lưu vào SharedPreferences
      final userId = _getUserIdFromToken(token);
      await prefs.setInt('user_id', userId);

      return true;
    } else {
      return false;
    }
  }

  // Phương thức để lấy userId từ token
  int _getUserIdFromToken(String token) {
    Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
    print("Decoded Token: $decodedToken");

    // Truy xuất userId từ claim đúng
    if (decodedToken.containsKey('http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name')) {
      var userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      print("User ID: $userId");
      return int.tryParse(userId.toString()) ?? 0; // Trả về userId dưới dạng số nguyên
    }

    // Trường hợp không tìm thấy userId trong token, trả về 0 hoặc xử lý khác nếu cần
    return 0;
  }

  // Method to get the saved token
  Future<String?> getToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString('auth_token');
  }

  // Method to check if the user is logged in by checking the token
  Future<bool> isLoggedIn() async {
    final token = await getToken();
    return token != null;
  }

  // Method to log out by clearing the stored token
  Future<void> logout() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('auth_token');
  }
}
