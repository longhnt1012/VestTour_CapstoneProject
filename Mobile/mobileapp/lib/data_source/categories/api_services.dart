import 'dart:convert';
import 'package:http/http.dart' as http;
import '../../models/category.dart'; // Import model Category

class ApiServiceCategories {
  static const String baseUrl = "http://165.22.243.162:8080/api/Category";

  // Lấy danh sách tất cả các Category
  static Future<List<Category>> getAllCategories() async {
    try {
      final url = Uri.parse(baseUrl);
      final response = await http.get(
        url,
        headers: {
          'Content-Type': 'application/json',
        },
      );

      if (response.statusCode == 200) {
        List<dynamic> data = json.decode(response.body);
        print("Dữ liệu danh sách Category: $data");
        return data.map((item) => Category.fromJson(item)).toList();
      } else {
        print("Lỗi: Không thể tải danh sách Category. Mã lỗi: ${response.statusCode}");
        throw Exception('Failed to load categories');
      }
    } catch (e) {
      print("Lỗi trong quá trình lấy danh sách Category: $e");
      throw Exception('Error fetching categories');
    }
  }

  // Lấy thông tin một Category dựa trên ID
  static Future<Category?> getCategoryById(int? id) async {
    try {
      print("Fetching Category with ID: $id");
      final url = Uri.parse("$baseUrl/$id");
      final response = await http.get(
        url,
        headers: {
          'Content-Type': 'application/json',
        },
      );

      if (response.statusCode == 200) {
        Map<String, dynamic> responseBody = json.decode(response.body);

        // Extract the actual data object from the response
        if (responseBody.containsKey('data') && responseBody['data'] != null) {
          Map<String, dynamic> data = responseBody['data'];

          print("Dữ liệu Category với ID $id: $data");

          // Validate and convert the category data
          if (data.containsKey('categoryId') && data['categoryId'] != null) {
            return Category.fromJson(data);
          } else {
            print("Dữ liệu không hợp lệ: $data");
            return null;
          }
        } else {
          print("Dữ liệu không hợp lệ hoặc thiếu 'data': $responseBody");
          return null;
        }
      } else {
        print("Lỗi: Không thể tải Category ID $id. Mã lỗi: ${response.statusCode}");
        return null;
      }
    } catch (e) {
      print("Lỗi trong quá trình lấy Category ID $id: $e");
      return null;
    }
  }

}
