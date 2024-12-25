import 'dart:convert';
import 'package:http/http.dart' as http;
import '../../../models/shipping.dart'; // Đảm bảo bạn import đúng model

class ApiServiceWard {
  // URL gốc
  final String baseUrl = "http://165.22.243.162:8080/api/Shipping";

  // Hàm gọi API lấy danh sách wards
  Future<List<Ward>> fetchWards(int districtId) async {
    final url = Uri.parse('$baseUrl/wards?districtId=$districtId');

    try {
      // Thực hiện GET request
      final response = await http.get(url);

      // Kiểm tra nếu response trả về lỗi
      if (response.statusCode == 200) {
        // Nếu thành công, parse danh sách wards
        List<dynamic> data = json.decode(response.body);

        // Chuyển đổi dữ liệu từ JSON thành đối tượng Ward
        return data.map((item) => Ward.fromJson(item)).toList();
      } else {
        throw Exception('Failed to load wards');
      }
    } catch (e) {
      // Xử lý lỗi
      print('Error fetching wards: $e');
      throw e;
    }
  }
}
