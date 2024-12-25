import 'package:flutter/material.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

import 'package:mobileapp/data_source/categories/api_services.dart';
import 'package:mobileapp/data_source/fabrics/api_services.dart';
import 'package:mobileapp/data_source/lining/api_services.dart';
import 'package:mobileapp/data_source/product/api_services.dart';
import 'package:mobileapp/data_source/style/api_services.dart';
import 'package:mobileapp/screens/checkout_screen.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../data_source/cart/api_services.dart';
import '../models/StyleOption.dart';
import '../models/cart.dart';
import '../models/category.dart';
import '../models/fabric.dart';
import '../models/lining.dart';

class CartScreen extends StatefulWidget {
  @override
  _CartScreenState createState() => _CartScreenState();
}

class _CartScreenState extends State<CartScreen> {
  int _currentIndex = 2;
  Future<Map<String, dynamic>> fetchCartList() async {
    try {
      String? authToken = await getAuthToken(); // Lấy token
      final url = Uri.parse("http://165.22.243.162:8080/api/AddCart/mycart");

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
  void _onBottomNavTap(int index) {
    setState(() {
      _currentIndex = index;
    });
    if (index == 0) {
      Navigator.pushNamed(context, '/home');
    } else if (index == 1) {
      Navigator.pushNamed(context, '/customize');
    } else if (index == 3) {
      Navigator.pushNamed(context, '/profile');
    }
  }
  Future<Lining?> getLining(int? liningId) => liningId != null ? ApiServiceLining().getLiningById(liningId) : Future.value(null);
  Future<Fabric?> getFabric(int? fabricId) => fabricId != null ? ApiServicesFabric().getFabricById(fabricId) : Future.value(null);
  Future<Category?> getCategory(int? categoryId) async {
    try {
      final category = await ApiServiceCategories.getCategoryById(categoryId);
      return category;
    } catch (e) {
      return null;
    }
  }

  Future<List<StyleOption>?> getStyleOptions(List<int>? styleOptionIds) async {
    try {
      if (styleOptionIds == null || styleOptionIds.isEmpty) return [];

      // Create a list of futures to get each StyleOption by its ID
      final futures = styleOptionIds.map((id) async {
        var styleOptionMap = await ApiServiceStyle.getStyleOptionById(id);
        if (styleOptionMap != null) {
          return StyleOption.fromJson(styleOptionMap as Map<String, dynamic>);  // Ensure casting
        }
        return null;
      }).toList();

      // Use Future.wait to execute all futures concurrently and return the results
      final styleOptions = await Future.wait(futures);

      // Filter out null results and cast to List<StyleOption>
      return styleOptions.where((option) => option != null).cast<StyleOption>().toList();
    } catch (e) {
      // Handle exceptions gracefully and return an empty list if something goes wrong
      return [];
    }
  }

  Future<void> removeProductFromCart(Cart cart) async {
    try {
      String? authToken = await getAuthToken();
      int? userId = await getUserIdFromToken(authToken!);
      String? productCode;

      if (cart.isCustom == false ) {
        productCode = cart.product?.productCode;
        print('Non-Custom Product Code: $productCode');
        // Get productCode for non-custom products

      } else {
        // Get productCode for custom products

        productCode = cart.customProduct?.productCode;
        print('Custom Product Code: $productCode');
      }
      bool success = await ApiServicesCart.deleteProductFromCart(authToken, productCode!, userId!);

      if (success) {
        setState(() {
          // Remove the product from the cart list
        });
      } else {
        print('Failed to remove product from cart');
      }
    } catch (e) {
      print('Error: $e');
    }
  }
  Future<void> increaseProductQuantity(Cart cart) async {
    try {
      String? authToken = await getAuthToken();
      String? productCode;
      // Check if it's a custom product or not

      if (cart.isCustom == false ) {
        productCode = cart.product?.productCode;
        print('Non-Custom Product Code: $productCode');
        // Get productCode for non-custom products

      } else {
        // Get productCode for custom products

        productCode = cart.customProduct?.productCode;
        print('Custom Product Code: $productCode');
      }
      // Convert the Cart object to JSON
      if (productCode != null) {
        // Convert the Cart object to JSON
        final String jsonCart = jsonEncode(cart.toJson());

        // Send the POST request to increase quantity
        final response = await http.post(
          Uri.parse("http://165.22.243.162:8080/api/AddCart/increase/$productCode"),
          headers: {
            'Authorization': 'Bearer $authToken',
            'Content-Type': 'application/json',
          },
          body: jsonCart, // Send the JSON-encoded Cart object
        );

        // Handle the response
        if (response.statusCode == 200) {
          print('Product quantity increased successfully');
          setState(() {}); // Update the UI after increasing quantity
        } else {
          print('Failed to increase quantity: ${response.body}');
        }
      } else {
        print('Product code is null');
      }
    } catch (e) {
      // Handle any exceptions that occur during the request
      print('Error: $e');
    }
  }
  Future<int?> getUserIdFromToken(String authToken) async {
    return await ApiServicesCart.getUserIdFromToken(authToken);
  }
  Future<void> decreaseProductQuantity(Cart cart) async {
    try {
      String? authToken = await getAuthToken();

      String? productCode;
      // Check if it's a custom product or not

      if (cart.isCustom == false ) {
        productCode = cart.product?.productCode;
        print('Non-Custom Product Code: $productCode');
        // Get productCode for non-custom products

      } else {
        // Get productCode for custom products

        productCode = cart.customProduct?.productCode;
        print('Custom Product Code: $productCode');
      }

      // Send the POST request to decrease the quantity
      final response = await http.post(
        Uri.parse("http://165.22.243.162:8080/api/AddCart/decrease/${productCode}"),
        headers: {
          'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json',
        },
        body: jsonEncode(cart.toJson()), // Send the JSON-encoded Cart object
      );

      // Handle the response
      if (response.statusCode == 200) {
        // Success
        print('Product quantity decreased successfully');
        setState(() {}); // Cập nhật lại giao diện sau khi giảm số lượng
      } else {
        // Handle error response
        print('Failed to decrease quantity: ${response.body}');
      }
    } catch (e) {
      // Handle any exceptions that occur during the request
      print('Error: $e');
    }
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Your Cart'),
      ),
      body: FutureBuilder<Map<String, dynamic>>(
        future: fetchCartList(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (snapshot.hasData) {
            final cartItems = snapshot.data!['cartItems'] as List;
            final cartTotal = snapshot.data!['cartTotal'];

            return Stack(
              children: [
                Column(
                  children: [
                    Expanded(
                      child: ListView.builder(
                        itemCount: cartItems.length,
                        itemBuilder: (context, index) {
                          final item = cartItems[index];
                          final isCustom = item['isCustom'] as bool;

                          return Container(
                            margin: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                            decoration: BoxDecoration(
                              border: Border.all(color: Colors.black),
                              borderRadius: BorderRadius.circular(10),
                            ),
                            child: Padding(
                              padding: EdgeInsets.all(10),
                              child: Row(
                                children: [
                                  Image(
                                    image: item['product'] != null &&
                                        item['product']['imgURL'] != null &&
                                        item['product']['imgURL']!.isNotEmpty
                                        ? NetworkImage(item['product']['imgURL'])
                                        : AssetImage('assets/images/unavailable.png') as ImageProvider,
                                    height: 100,
                                    width: 100,
                                    fit: BoxFit.cover,
                                  ),
                                  SizedBox(width: 10),
                                  Expanded(
                                    child: Column(
                                      crossAxisAlignment: CrossAxisAlignment.start,
                                      children: [
                                        Text(
                                          isCustom ? 'Customize Product' : 'Shopping Product',
                                          style: TextStyle(
                                            fontWeight: FontWeight.bold,
                                            fontSize: 16,
                                          ),
                                        ),
                                        SizedBox(height: 5),
                                        if (isCustom == false && item['customProduct'] == null) ...[
                                          // If it's a shopping product, display all the details
                                          Text('Product Code: ${item['product']["productCode"] ?? 'N/A'}'),
                                          FutureBuilder<Category?>(
                                            future: getCategory(item['product']["categoryID"]),
                                            builder: (context, snapshot) {
                                              if (snapshot.connectionState == ConnectionState.waiting) {
                                                return CircularProgressIndicator();
                                              } else if (snapshot.hasData) {
                                                return Text('Category: ${snapshot.data?.name ?? 'Not found'}');
                                              } else {
                                                return Text('No Category');
                                              }
                                            },
                                          ),
                                          Text('Price: \$${item['price'] ?? 'N/A'}'),
                                          Text('Size: ${item['product']['size'] ?? 'N/A'}'),
                                        ],
                                        if (isCustom && item['customProduct'] != null) ...[
                                          Text('ProductCode: ${item['customProduct']['productCode'] ?? 'N/A'}'),
                                          FutureBuilder<Lining?>(
                                            future: getLining(item['customProduct']['liningID']),
                                            builder: (context, snapshot) {
                                              if (snapshot.connectionState == ConnectionState.waiting) {
                                                return CircularProgressIndicator();
                                              } else if (snapshot.hasData) {
                                                return Text('Lining: ${snapshot.data?.liningName ?? 'Not found'}');
                                              } else {
                                                return Text('No Lining');
                                              }
                                            },
                                          ),
                                          FutureBuilder<Fabric?>(
                                            future: getFabric(item['customProduct']['fabricID']),
                                            builder: (context, snapshot) {
                                              if (snapshot.connectionState == ConnectionState.waiting) {
                                                return CircularProgressIndicator();
                                              } else if (snapshot.hasData) {
                                                return Text('Fabric: ${snapshot.data?.fabricName ?? 'Not found'}');
                                              } else {
                                                return Text('No Fabric');
                                              }
                                            },
                                          ),
                                         /* FutureBuilder<List<StyleOption>?>(
                                            future: getStyleOptions(
                                              item['customProduct']['pickedStyleOptions']
                                                  .map<int>((option) => option['styleOptionID'])
                                                  .toList(),
                                            ),
                                            builder: (context, snapshot) {
                                              if (snapshot.connectionState == ConnectionState.waiting) {
                                                return CircularProgressIndicator();
                                              } else if (snapshot.hasError) {
                                                return Text('Error: ${snapshot.error}');
                                              } else if (snapshot.hasData) {
                                                final styleOptions = snapshot.data!;
                                                return ListView.builder(
                                                  itemCount: styleOptions.length,
                                                  itemBuilder: (context, index) {
                                                    final styleOption = styleOptions[index];
                                                    return ListTile(
                                                      title: Text(styleOption.optionType!),
                                                      subtitle: Text(styleOption.optionValue!),
                                                    );
                                                  },
                                                );
                                              } else {
                                                return Text('No style options found');
                                              }
                                            },
                                          )*/
                                        ],
                                        Text('Price: \$${item['price'] ?? 'N/A'}'),
                                        Row(
                                          children: [
                                            IconButton(
                                              icon: Icon(Icons.remove),
                                              onPressed: () => decreaseProductQuantity(Cart.fromJson(item)),
                                            ),
                                            Text('${item['quantity'] ?? 0}'),
                                            IconButton(
                                              icon: Icon(Icons.add),
                                              onPressed: () => increaseProductQuantity(Cart.fromJson(item)),
                                            ),
                                            IconButton(
                                              icon: Icon(Icons.delete),
                                              onPressed: () => removeProductFromCart(Cart.fromJson(item)),
                                            ),
                                          ],
                                        ),
                                      ],
                                    ),
                                  ),
                                ],
                              ),
                            ),
                          );
                        },
                      ),
                    ),
                  ],
                ),
                Positioned(
                  left: 0,
                  right: 0,
                  bottom: 20,
                  child: Padding(
                    padding: EdgeInsets.symmetric(horizontal: 20),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Text(
                          'Total Price: \$${cartTotal ?? 'N/A'}',
                          style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        ElevatedButton(
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => CheckoutScreen(cartTotal: cartTotal)),
                            );
                          },
                          child: Text('Checkout'),
                        ),
                      ],
                    ),
                  ),
                ),

              ],
            );

          } else {
            return Center(child: Text('Cart is empty'));
          }
        },
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _currentIndex,
        selectedItemColor: Colors.black,
        unselectedItemColor: Colors.grey,
        onTap: _onBottomNavTap,
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(icon: Icon(Icons.design_services), label: 'Customize'),
          BottomNavigationBarItem(
              icon: Icon(Icons.shopping_bag), label: 'Bag'), // New Bag item
          BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Profile'),
        ],
      ),
    );
  }

  Future<String?> getAuthToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString('auth_token');
  }
}