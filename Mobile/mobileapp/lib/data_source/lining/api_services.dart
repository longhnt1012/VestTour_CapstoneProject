import 'dart:convert';
import 'package:http/http.dart' as http;


import '../../models/lining.dart'; // Đảm bảo đường dẫn đúng với nơi bạn lưu model Lining

class ApiServiceLining{
  final String _baseUrl = 'https://vesttour.xyz/api/Linings';

  // Lấy danh sách tất cả Linings
  Future<List<Lining>> getAllLinings() async {
    try {
      final response = await http.get(Uri.parse(_baseUrl));

      if (response.statusCode == 200) {
        // Parse the JSON response.
        final Map<String, dynamic> responseData = json.decode(response.body);

        // Assuming that the 'data' key contains the list of linings.
        if (responseData.containsKey('data')) {
          List<dynamic> liningsData = responseData['data'];
          return liningsData.map((data) => Lining.fromJson(data)).toList();
        } else {
          throw Exception('No "data" field in the response');
        }
      } else {
        throw Exception('Failed to load linings');
      }
    } catch (e) {
      print('Error: $e');
      throw Exception('Failed to load linings');
    }
  }


  Future<Lining> getLiningById(int liningId) async {
    try {
      final url = '$_baseUrl/$liningId'; // Đường dẫn API để lấy Lining theo ID
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        // Nếu thành công, parse JSON và trả về Lining
        final Map<String, dynamic> responseData = json.decode(response.body);

        // If the response is wrapped in a key (e.g., "data"), extract it.
        if (responseData.containsKey('data')) {
          return Lining.fromJson(responseData['data']);
        } else {
          return Lining.fromJson(responseData); // Return directly if no wrapping key.
        }
      } else {
        throw Exception('Failed to load lining with ID $liningId');
      }
    } catch (e) {
      print('Error: $e');
      throw Exception('Failed to load lining with ID $liningId');
    }
  }

}
