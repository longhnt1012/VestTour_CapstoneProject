import 'dart:convert';
import 'package:http/http.dart' as http;
import '../../models/store.dart';


class ApiServiceStores {
   Future<List<Store>> getAllStores() async {
    final url = 'http://165.22.243.162:8080/api/Store';
    final response = await http.get(Uri.parse(url));

    if (response.statusCode == 200) {
      List<dynamic> data = jsonDecode(response.body);
      return data.map((store) => Store.fromJson(store)).toList();
    } else {
      return [];
    }
  }
}
