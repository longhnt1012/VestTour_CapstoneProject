
import 'dart:convert';

import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

import '../../models/cart.dart';
class ApiServicesCart {
  static const String baseUrl = "http://165.22.243.162:8080/api/AddCart";
  // Define the getAuthToken method here
  static Future<String?> getAuthToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString(
        'auth_token'); // Returns the token if exists, else null
  }

  static Future<int?> getUserIdFromToken(String token) async {
    try {
      // Decode the token and extract userId
      Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
      return int.tryParse(
          decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
              .toString());
    } catch (e) {
      print("Error decoding token: $e");
      return null; // Return null if token decoding fails
    }
  }
  static Future<int?> getUserIdFromSharedPreferences() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final authToken = prefs.getString('auth_token');

    if (authToken != null) {
      return await getUserIdFromToken(authToken);
    }
    return null;
  }
  static Future<bool> createCart({
    required int userId,
     required int fabricId,
    required int liningId,
    required int measurementId,
    required List<int> styleOptionIds,
    int categoryId = 5,
    bool isCustom = true,
  }) async {
    try {
      String? authToken = await getAuthToken();
      final url = Uri.parse("$baseUrl/addtocart");
      int? userId = await getUserIdFromToken(authToken!);
      // Tạo object `Cart`
      final cart = {
        "userId": userId,
        "isCustom": isCustom,
        // Vì sản phẩm là custom nên để null
        "customProduct": {
          "categoryID": categoryId,
          "fabricID": fabricId,
          "liningID": liningId,
          "measurementID": measurementId,
          "pickedStyleOptions": styleOptionIds.map((id) => {"styleOptionID": id}).toList(),
        },
      };
      print("Request Cart Data: ${jsonEncode(cart)}");

      // Gửi POST request
      final response = await http.post(
        url,
        headers: {
            'Authorization': 'Bearer $authToken',  // Add token in Authorization header
            'Content-Type': 'application/json',
        },
        body: jsonEncode(cart),
      );


      // Kiểm tra trạng thái phản hồi
      if (response.statusCode == 200) {
        print("Cart created successfully!");
        return true;
      } else {
        print("Failed to create cart: ${response.body}");
        return false;
      }
    } catch (e) {
      print("Error occurred while creating cart: $e");
      return false;
    }
  }

  static Future<bool> createCartProduct(int userId, int productId) async {
    // Tạo đối tượng Cart
    String? authToken = await getAuthToken();
    int? userId = await getUserIdFromToken(authToken!);
    var customProduct = {
      "categoryID": 0,
      "fabricID": 0,
      "liningID": 0,
      "measurementID": 0,
      "pickedStyleOptions": [
        {
          "styleOptionID": 0
        }
      ]
    };
    var cart = {
      "userId": userId, // Gán userId từ tham số đầu vào
      "isCustom": false, // Gán isCustom mặc định là false
      "productId": productId, // Gán productId từ tham số đầu vào
      "customProduct": customProduct // Gán customProduct với dữ liệu
    };
    print("Sending data to API: ${jsonEncode(cart)}");
    try {
      final url = Uri.parse('$baseUrl/addtocart');

      // Gửi POST request với dữ liệu JSON
      final response = await http.post(
        url,
        headers: {
          'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json', // Đảm bảo rằng header là JSON
        },
        body: jsonEncode(cart), // Chuyển đối tượng Cart thành JSON
      );

      // Kiểm tra phản hồi
      if (response.statusCode == 200) {
        print("Cart added successfully!");
        return true;
      } else {
        print("Failed to add cart: ${response.body}");
        return false;
      }
    } catch (e) {
      print("Error occurred while adding cart: $e");
      return false;
    }
  }


  // Retrieve the list of carts
  static Future<Map<String, dynamic>> getCartList() async {
    try {
      String? authToken = await getAuthToken();
      final url = Uri.parse("$baseUrl/mycart");

      final response = await http.get(
        url,
        headers: {
          'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json',
        },
      );


      if (response.statusCode == 200) {
        // Phân tích dữ liệu JSON từ phản hồi
        Map<String, dynamic> data = json.decode(response.body);
        print("Dữ liệu trả về từ API: $data");
        return data; // Trả về dữ liệu giỏ hàng
      } else {
        // Xử lý lỗi HTTP
        throw Exception('Failed to load cart. Status code: ${response.statusCode}');
      }
    } catch (e) {
      // Xử lý ngoại lệ khi gọi API
      print('Error fetching cart: $e');
      throw Exception('Error fetching cart');
    }
  }


  static Future<bool> confirmOrder(Cart cart) async {
    String? authToken = await getAuthToken();
    final String url = "$baseUrl/confirmorder";

    try {
      final response = await http.post(
        Uri.parse(url),
        headers: {
          'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json',
        },
        body: jsonEncode(cart.toJson()),
      );

      if (response.statusCode == 200) {
        // Parse response to check success
        final data = jsonDecode(response.body);
        return data['success'] ?? false; // Check for a 'success' key in API response
      } else {
        print('Failed to confirm order: ${response.statusCode}');
        return false;
      }
    } catch (e) {
      print('Error confirming order: $e');
      return false;
    }
  }
  static Future<bool> deleteProductFromCart(String authToken, String productCode, int userId) async {
    try {
      final url = Uri.parse("$baseUrl/remove/$productCode?userId=$userId");
      final response = await http.delete(
        url,
        headers: {
          'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json',
        },
      );

      if (response.statusCode == 200) {
        return true;
      } else {
        print('Failed to delete product: ${response.body}');
        return false;
      }
    } catch (e) {
      print('Error: $e');
      return false;
    }
  }
  static Future<bool> increaseQuantity(String productCode) async{
    String? authToken = await getAuthToken();
    final String url= "$baseUrl/increase/$productCode";
    try{
      final response= await http.post(Uri.parse(url),
      headers: {
        "Content-Type": "application/json",
        'Authorization': 'Bearer $authToken',
      },
      );
      if (response.statusCode == 200) {
        // Successfully increased the quantity
        print("Quantity increased successfully");
        return true;
      } else {
        // Error from the server
        print("Failed to increase quantity. Status Code: ${response.statusCode}");
        return false;
      }
    }catch (e){
      print("Error increasing quantity: $e");
      return false;
    }
  }
  static Future<bool> decreaseQuantity(String productCode) async{
    String? authToken = await getAuthToken();
    final String url= "$baseUrl/decrease/$productCode";
    try{
      final response= await http.post(Uri.parse(url),
        headers: {
          "Content-Type": "application/json",
          'Authorization': 'Bearer $authToken',
        },
      );
      if (response.statusCode == 200) {
        // Successfully increased the quantity
        print("Quantity increased successfully");
        return true;
      } else {
        // Error from the server
        print("Failed to increase quantity. Status Code: ${response.statusCode}");
        return false;
      }
    }catch (e){
      print("Error increasing quantity: $e");
      return false;
    }
  }

}