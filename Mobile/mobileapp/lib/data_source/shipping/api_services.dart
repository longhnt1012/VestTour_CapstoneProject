import 'dart:convert';
import 'package:http/http.dart' as http;

class ApiServiceShipping {
  static const String baseUrl = "http://165.22.243.162:8080/api";

  /// Calculates the shipping fee by sending a POST request with the required parameters
  static Future<Map<String, dynamic>> calculateFee({
    required int serviceId,
    required int insuranceValue,
    required String coupon,
    required String toWardCode,
    required int toDistrictId,
    required int fromDistrictId,
    required int weight,
    required int length,
    required int width,
    required int height,
    required int shopCode,
  }) async {
    final url = Uri.parse('$baseUrl/Shipping/calculate-fee');

    final body = jsonEncode({
      "serviceId": serviceId,
      "insuranceValue": insuranceValue,
      "coupon": coupon,
      "toWardCode": toWardCode,
      "toDistrictId": toDistrictId,
      "fromDistrictId": fromDistrictId,
      "weight": weight,
      "length": length,
      "width": width,
      "height": height,
      "shopCode": shopCode,
    });

    try {
      final response = await http.post(
        url,
        headers: {
          'Content-Type': 'application/json',
        },
        body: body,
      );

      if (response.statusCode == 200) {
        return jsonDecode(response.body);
      } else {
        throw Exception(
            'Failed to calculate shipping fee. Status code: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Error calculating shipping fee: $e');
    }
  }
}
