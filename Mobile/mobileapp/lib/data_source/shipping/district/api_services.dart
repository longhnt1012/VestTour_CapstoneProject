import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../../models/shipping.dart';

class ApiServiceDistricts {
  static const String baseUrl = "http://165.22.243.162:8080/api/Shipping/districts";

  Future<List<District>> fetchDistricts(int provinceId) async {
    try {
      final response = await http.get(Uri.parse("$baseUrl?provinceId=$provinceId"));

      if (response.statusCode == 200) {
        // Parse JSON data
        List<dynamic> data = jsonDecode(response.body);
        return data.map((json) => District.fromJson(json)).toList();
      } else {
        throw Exception('Failed to load districts: ${response.reasonPhrase}');
      }
    } catch (e) {
      throw Exception('Error fetching districts: $e');
    }
  }
}
