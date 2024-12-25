import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:shared_preferences/shared_preferences.dart';

// Dịch vụ API
class ApiServicePaypal {
  // URL của API để tạo đơn hàng PayPal
  static const String _baseUrl = 'http://157.245.50.125:8080/Cart/create-paypal-order';

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

  // Hàm tạo đơn hàng PayPal
  Future<String?> createPaypalOrder() async {
    try {
      // Lấy auth_token từ SharedPreferences
      final authToken = await getAuthToken();

      if (authToken == null) {
        throw 'Vui lòng đăng nhập trước khi thanh toán';
      }

      // Gửi yêu cầu POST tới API với header chứa auth_token
      final response = await http.post(
        Uri.parse(_baseUrl),
        headers: {
          'Authorization': 'Bearer $authToken',  // Thêm token vào header Authorization
          'Content-Type': 'application/json',  // Đảm bảo gửi đúng Content-Type
        },
      );

      // Kiểm tra trạng thái trả về
      if (response.statusCode == 200) {
        final data = json.decode(response.body); // Phân tích dữ liệu JSON trả về

        // Kiểm tra và lấy URL thanh toán từ phản hồi API
        if (data['links'] != null && data['links'].isNotEmpty) {
          // Trả về URL PayPal (để mở thanh toán)
          return data['links'][1]['href'];
        } else {
          throw 'Không có liên kết thanh toán PayPal';
        }
      } else {
        throw 'Không thể tạo đơn hàng PayPal';
      }
    } catch (error) {
      rethrow; // Ném lại lỗi để xử lý ở nơi gọi API
    }
  }
}
