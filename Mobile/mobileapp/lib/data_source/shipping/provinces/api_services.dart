import 'dart:convert';
import 'package:http/http.dart' as http;
import '../../../models/shipping.dart';


class ApiServiceProvinces {
  static const String baseUrl = "http://165.22.243.162:8080/api/Shipping/provinces";

  Future<List<Provinces>> fetchProvinces() async {
    try {
      final response = await http.get(Uri.parse(baseUrl));

      if (response.statusCode == 200) {
        // Parse JSON data
        List<dynamic> data = jsonDecode(response.body);
        return data.map((json) => Provinces.fromJson(json)).toList();
      } else {
        throw Exception('Failed to load provinces: ${response.reasonPhrase}');
      }
    } catch (e) {
      throw Exception('Error fetching provinces: $e');
    }
  }
}
