import 'package:mobileapp/models/voucher.dart';

import '../../models/feedback.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;
class ApiServiceVoucher {
  static const String baseUrl = "http://165.22.243.162:8080/api";

  /// Lấy danh sách voucher
  static Future<List<Voucher>> getAllVouchers() async{
  final url= Uri.parse('$baseUrl/Voucher');

  try{
    final response = await http.get(url
    ,headers: {
          'Content-Type': 'application/json',
        }
    );

    if(response.statusCode==200){
      final List<dynamic> voucherJson= jsonDecode(response.body);
      return voucherJson.map((json) => Voucher.fromJson(json)).toList();
    }else{
      throw Exception(
          'Failed to load feedbacks. Status code: ${response.statusCode}');

    }
  }catch(e){
    throw Exception('Error fetching feedbacks: $e');
  }
}

}