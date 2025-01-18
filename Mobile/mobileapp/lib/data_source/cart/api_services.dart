
import 'dart:convert';

import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

import '../../models/cart.dart';
class ApiServicesCart {
  static const String baseUrl = "https://vesttour.xyz/api/AddCart";
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
  Future<Map<String, dynamic>> fetchCartList() async {
    try {
      String? authToken = await getAuthToken(); // Lấy token
      final url = Uri.parse("https://vesttour.xyz/api/AddCart/mycart");

      final response = await http.get(
        url,
        headers: {
          'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json',
        },
      );
      print('Response body: ${response.body}');
      if (response.statusCode == 200) {
        return json.decode(response.body); // Parse JSON
      } else {
        throw Exception('Failed to load cart. Status code: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Error fetching cart: $e');
    }
  }


  static Future<Map<String, dynamic>> confirmOrder({
    String? userName,
    String? userEmail,
    String? userPhone,
    String? address,
    required double depositFee,
    required double shippingFee,
    required String deliveryMethod, // "Pick up" or "Delivery"
    required int storeId,
  }) async {
    try {
      // Get the auth token if needed
      String? authToken = await getAuthToken();
      String encodedUserName = Uri.encodeComponent(userName!);
      String encodedUserEmail = Uri.encodeComponent(userEmail!);
      String encodedAddress = Uri.encodeComponent(address!);
      String encodedMethod = Uri.encodeComponent(deliveryMethod);

      // Build the URL with parameters
      final String url =
          "https://vesttour.xyz/api/AddCart/confirmorder?guestName=$userName&guestEmail=$userEmail&guestAddress=$address&guestPhone=$userPhone&deposit=$depositFee&shippingfee=$shippingFee&deliverymethod=$deliveryMethod&storeId=$storeId";

      // Send the POST request
      final response = await http.post(
        Uri.parse(url),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $authToken",
        },
      );
      print('Response Status: ${response.statusCode}');
      print('Response Body: ${response.body}');
      // Check the response from the API
      if (response.statusCode == 200) {
        // Successfully confirmed the order
        print("Order confirmed successfully: ${response.body}");

        // Parse the response body
        final Map<String, dynamic> responseData = json.decode(response.body);

        // Extract orderId from the response
        if (responseData['orderId'] != null) {
          int orderId = responseData['orderId'];
          print("Order ID: $orderId");

          // Return a map containing success status and the orderId
          return {
            'success': true,
            'orderId': orderId,
          };
        } else {
          // If orderId is missing from the response
          return {'success': false, 'orderId': null};
        }
      } else {
        // Failed to confirm the order
        print("Failed to confirm order: ${response.statusCode}");
        return {'success': false, 'orderId': null};
      }
    } catch (e) {
      // Handle error
      print("Error confirming order: $e");
      return {'success': false, 'orderId': null};
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