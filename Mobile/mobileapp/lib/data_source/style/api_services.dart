import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../models/StyleOption.dart';
import '../../models/fabric.dart';
import '../../models/style.dart';
// File chứa class Style và StyleOption

class ApiServiceStyle {
  final String styleUrl = "http://165.22.243.162:8080/api/Style";
  final  String  styleOptionUrl = "http://165.22.243.162:8080/api/StyleOption";

  Future<List<Style>> fetchStyles() async {
    final response = await http.get(Uri.parse(styleUrl));
    if (response.statusCode == 200) {
      Iterable jsonData = json.decode(response.body);
      return jsonData.map((item) => Style.fromJson(item)).toList();
    } else {
      throw Exception('Failed to load styles');
    }
  }

  Future<List<StyleOption>> fetchStyleOptions() async {
    final response = await http.get(Uri.parse(styleOptionUrl));
    if (response.statusCode == 200) {
      Iterable jsonData = json.decode(response.body);
      return jsonData.map((item) => StyleOption.fromJson(item)).toList();
    } else {
      throw Exception('Failed to load style options');
    }
  }
  Future<List<StyleOption>> fetchStyleOptionsByStyleId(int styleId) async {
    final response = await http.get(Uri.parse(styleOptionUrl));
    if (response.statusCode == 200) {
      Iterable jsonData = json.decode(response.body);
      return jsonData
          .map((item) => StyleOption.fromJson(item))
          .where((option) => option.styleId == styleId)
          .toList();
    } else {
      throw Exception('Failed to load style options');
    }
  }
  Future<Map<String, List<StyleOption>>> fetchGroupedStyleOptionsByStyleId(
      int styleId) async {
    final response = await http.get(Uri.parse(styleOptionUrl));
    if (response.statusCode == 200) {
      Iterable jsonData = json.decode(response.body);
      List<StyleOption> options = jsonData
          .map((item) => StyleOption.fromJson(item))
          .where((option) => option.styleId == styleId)
          .toList();

      // Nhóm theo optionType
      Map<String, List<StyleOption>> groupedOptions = {};
      for (var option in options) {
        groupedOptions.putIfAbsent(option.optionType!, () => []);
        groupedOptions[option.optionType!]!.add(option);
      }
      return groupedOptions;
    } else {
      throw Exception('Failed to load style options');
    }
  }
  static Future<StyleOption> getStyleOptionById(int id) async {
    try {
      print("Fetching StyleOption with ID: $id");
      final url = Uri.parse("http://165.22.243.162:8080/api/StyleOption/$id");
      final response = await http.get(
        url,
        headers: {
          'Content-Type': 'application/json',
        },
      );

      if (response.statusCode == 200) {
        Map<String, dynamic> data = json.decode(response.body);
        print("Dữ liệu StyleOption với ID $id: $data");
        return StyleOption.fromJson(data);
      } else {
        print("Lỗi: Không thể tải StyleOption ID $id. Mã lỗi: ${response.statusCode}");
        throw Exception('Failed to load style option');
      }
    } catch (e) {
      print("Lỗi trong quá trình lấy StyleOption ID $id: $e");
      throw Exception('Error fetching style option by ID');
    }
  }

  Future<Fabric> getFabricById(int fabricId) async {
    try {
      // Construct the URL with the fabric ID
      final url = 'http://165.22.243.162:8080/api/Fabrics/$fabricId';

      // Send a GET request to the API
      final response = await http.get(Uri.parse(url));

      // Check if the response is successful (status code 200)
      if (response.statusCode == 200) {
        // Decode the JSON response and convert it to a Fabric object
        Map<String, dynamic> responseData = json.decode(response.body);
        Fabric fabric = Fabric.fromJson(responseData);

        // Return the Fabric object
        return fabric;
      } else {
        // Handle the case when the API returns an error
        throw Exception('Failed to load fabric with ID $fabricId');
      }
    } catch (e) {
      // Handle any exceptions
      print('Error: $e');
      throw Exception('Failed to load fabric with ID $fabricId');
    }
  }
}
