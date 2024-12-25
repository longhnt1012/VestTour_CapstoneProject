import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../models/product.dart';

class ApiServicesProduct {
  // Define the base URL for the API
  static const String _baseUrl = 'http://165.22.243.162:8080/api/Product';

  // Method to fetch the list of products with isCustom = false
  Future<List<Product>> getProductsCustomFalse() async {
    try {
      // Send a GET request to the API with the custom-false filter
      final response = await http.get(Uri.parse('$_baseUrl/products/custom-false'));

      // Check if the response is successful (status code 200)
      if (response.statusCode == 200) {
        // Decode the JSON response
        List<dynamic> responseData = json.decode(response.body);

        // Convert the list of JSON objects into a list of Product objects
        List<Product> products = responseData
            .map((productJson) => Product.fromJson(productJson))
            .toList();

        // Return the list of products
        return products;
      } else {
        // Handle the case when the API returns an error
        throw Exception('Failed to load products');
      }
    } catch (e) {
      // Handle any exceptions
      print('Error: $e');
      throw Exception('Failed to load products');
    }
  }
  Future<String?> getProductCode(int productId) async {
    final String url = '$_baseUrl/basic/$productId'; // Tạo URL với productId

    try {
      final response = await http.get(Uri.parse(url));

      // Kiểm tra nếu phản hồi thành công
      if (response.statusCode == 200) {
        final Map<String, dynamic> json = jsonDecode(response.body);

        // Kiểm tra nếu có 'productCode' trong phản hồi
        if (json.containsKey('productCode')) {
          return json['productCode'];
        } else {
          print('productCode not found in the response');
          return null;
        }
      } else {
        print('Failed to load product: ${response.statusCode}');
        return null;
      }
    } catch (e) {
      print('Error occurred: $e');
      return null;
    }
  }
}
