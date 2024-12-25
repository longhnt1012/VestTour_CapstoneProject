import 'dart:convert';
import 'package:http/http.dart' as http;


import '../../models/lining.dart'; // Đảm bảo đường dẫn đúng với nơi bạn lưu model Lining

class ApiServiceLining{
  final String _baseUrl = 'http://165.22.243.162:8080/api/Linings';

  // Lấy danh sách tất cả Linings
  Future<List<Lining>> getAllLinings() async {
    try {
      final response = await http.get(Uri.parse(_baseUrl));

      if (response.statusCode == 200) {
        // Nếu thành công, parse JSON và trả về danh sách Linings
        List<dynamic> responseData = json.decode(response.body);
        return responseData.map((data) => Lining.fromJson(data)).toList();
      } else {
        throw Exception('Failed to load linings');
      }
    } catch (e) {
      print('Error: $e');
      throw Exception('Failed to load linings');
    }
  }

  // Lấy Lining theo liningId
  Future<Lining> getLiningById(int liningId) async {
    try {
      final url = '$_baseUrl/$liningId'; // Đường dẫn API để lấy Lining theo ID
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        // Nếu thành công, parse JSON và trả về Lining
        Map<String, dynamic> responseData = json.decode(response.body);

        return Lining.fromJson(responseData);
      } else {
        throw Exception('Failed to load lining with ID $liningId');
      }
    } catch (e) {
      print('Error: $e');
      throw Exception('Failed to load lining with ID $liningId');
    }
  }
}
