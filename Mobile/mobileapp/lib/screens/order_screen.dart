import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';
import 'package:mobileapp/models/orderDetail.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../../models/order.dart';
import '../data_source/order/api_services.dart';

class OrderListScreen extends StatefulWidget {
  @override
  _OrderListScreenState createState() => _OrderListScreenState();
}

class _OrderListScreenState extends State<OrderListScreen> {
  List<Order> _orders = [];
  bool _isLoading = true;
  String _errorMessage = "";
  int _currentIndex = 3;

  @override
  void initState() {
    super.initState();
    _fetchOrders();
  }

  Future<void> _fetchOrders() async {
    try {
      setState(() {
        _isLoading = true;
        _errorMessage = "";
      });

      final userId = await ApiServicesOrder.getUserIdFromSharedPreferences();
      if (userId == null) {
        setState(() {
          _errorMessage = "User not logged in.";
        });
        return;
      }

      final orders = await ApiServicesOrder.showOrdersByUser(userId);
      setState(() {
        _orders = orders;
      });

    } catch (e) {
      setState(() {
        _errorMessage = "An error occurred while fetching orders.";
      });
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }



  Future<String> _getUserFirebaseToken(Order order) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? firebaseToken = prefs.getString('firebase_token');

    if (firebaseToken != null) {
      return firebaseToken;  // Trả về Firebase token từ SharedPreferences
    } else {
      // Nếu không tìm thấy token trong SharedPreferences, có thể gọi lại API để lấy token
      throw Exception("Không tìm thấy Firebase token");
    }
  }

  // Handle bottom navigation bar tap
  void _onBottomNavTap(int index) {
    setState(() {
      _currentIndex = index;
    });
    if (index == 0) {
      Navigator.pushNamed(context, '/home');
    } else if (index == 1) {
      Navigator.pushNamed(context, '/customize');
    } else if (index == 2) {
      Navigator.pushNamed(context, '/cart');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("My Orders"),
      ),
      body: _isLoading
          ? Center(child: CircularProgressIndicator())
          : _errorMessage.isNotEmpty
              ? Center(child: Text(_errorMessage))
              : _orders.isEmpty
                  ? Center(child: Text("No orders found."))
                  : ListView.builder(
                      itemCount: _orders.length,
                      itemBuilder: (context, index) {
                        final order = _orders[index];

                        return Card(
                          margin: EdgeInsets.all(10.0),
                          child: ListTile(
                            leading: Image.asset(
                              'assets/images/unavailable.png',
                              width: 100,
                              height: 150,
                              fit: BoxFit.fitWidth,
                            ),
                            title: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Row(
                                  children: [
                                    if (order.deliveryMethod != null)
                                      Text(
                                        "${order.deliveryMethod}",
                                        style: TextStyle(
                                          fontWeight: FontWeight.normal,
                                          color: Colors.grey[600],
                                        ),
                                      ),
                                    if (order.shipStatus != null)
                                      Padding(
                                        padding:
                                            const EdgeInsets.only(left: 10.0),
                                        child: Container(
                                          padding: EdgeInsets.symmetric(
                                              horizontal: 8.0, vertical: 4.0),
                                          decoration: BoxDecoration(
                                            color: _getShipStatusColor(
                                                order.shipStatus),
                                            borderRadius:
                                                BorderRadius.circular(12.0),
                                          ),
                                          child: Text(
                                            "${order.shipStatus}",
                                            style: TextStyle(
                                              fontWeight: FontWeight.normal,
                                              color: Colors.white,
                                            ),
                                          ),
                                        ),
                                      ),
                                  ],
                                ),
                                Text(
                                  "Order #${order.orderId}",
                                  style: TextStyle(
                                    fontWeight: FontWeight.bold,
                                    color: Colors.black,
                                  ),
                                ),
                              ],
                            ),
                            subtitle: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                if (order.orderDate != null)
                                  Text("Order Date: ${order.orderDate}"),
                                if (order.shippedDate != null)
                                  Text("Shipped Date: ${order.shippedDate}"),
                                if (order.totalPrice != null)
                                  Text("Total: \$${order.totalPrice}"),
                              ],
                            ),
                            trailing: Icon(Icons.arrow_forward_ios),
                            onTap: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (context) => OrderDetailsScreen(
                                    order: order,
                                  ),
                                ),
                              );
                            },
                          ),
                        );
                      },
                    ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _currentIndex,
        selectedItemColor: Colors.black,
        unselectedItemColor: Colors.grey,
        onTap: _onBottomNavTap,
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(icon: Icon(Icons.build), label: 'Customize'),
          BottomNavigationBarItem(
            icon: Icon(Icons.shopping_bag),
            label: 'Bag',
          ),
          BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Profile'),
        ],
      ),
    );
  }

// Function to determine background color based on shipStatus
  Color _getShipStatusColor(String? status) {
    switch (status) {
      case 'Confirming':
        return Colors.pink; // Pink for confirming
      case 'Tailoring':
        return Colors.blue; // Blue for tailoring
      case 'Shipping':
        return Colors.yellow; // Yellow for shipping
      case 'Ready':
        return Colors.green; // Green for ready
      case 'Finished':
        return Colors.orange; // Orange for finished
      default:
        return Colors.grey; // Default grey for unknown status
    }
  }
}

class OrderDetailsScreen extends StatelessWidget {
  final Order order;

  const OrderDetailsScreen({Key? key, required this.order}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Order Details"),
      ),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: ListView(
          children: [
            Container(
              padding: EdgeInsets.all(8.0), // Add some padding around the text
              decoration: BoxDecoration(
                color: Color.fromARGB(124, 62, 156, 88), // Set the background color to green
                border: Border.all(
                  color: Colors.green.shade700, // Set the border color (you can change this to your preferred color)
                  width: 2.0, // Set the border width
                ),
                borderRadius: BorderRadius.circular(10.0), // Optional: to make the corners rounded
              ),
              child: Text(
                _getShipStatusMessage(order.shipStatus),
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.white, // Set the text color to red
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
            SizedBox(height: 10.0),
            Text(
              "Order #${order.orderId}",
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18.0),
            ),
            SizedBox(height: 10.0),
            if (order.orderDate != null) Text("Order Date: ${order.orderDate}"),
            if (order.shippedDate != null)
              Text("Shipped Date: ${order.shippedDate}"),
            Text("Delivery Method: ${order.deliveryMethod}"),
            if (order.totalPrice != null)
              Text("Total Price: \$${order.totalPrice}"),
            if (order.guestName != null)
              Text("Customer Name: ${order.guestName}"),
            if (order.guestAddress != null)
              Text("Ship at Address: ${order.guestAddress}"),
            SizedBox(height: 20.0),
            Text(
              "Order Details",
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 16.0),
            ),
            // Fetch order details using FutureBuilder
            FutureBuilder<List<OrderDetails>?>(
              future: ApiServicesOrder.getOrderDetails(order.orderId!),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return Center(child: CircularProgressIndicator());
                } else if (snapshot.hasError) {
                  return Center(child: Text('Error: ${snapshot.error}'));
                } else if (snapshot.hasData &&
                    snapshot.data != null &&
                    snapshot.data!.isNotEmpty) {
                  final orderDetails = snapshot.data!;
                  return SingleChildScrollView(
                    child: Column(
                      children: orderDetails.map((detail) {
                        return Padding(
                          padding: const EdgeInsets.symmetric(vertical: 10.0),
                          child: Card(
                            margin: EdgeInsets.all(10),
                            elevation: 8,
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(15),
                            ),
                            color:
                                Color(0xFFf9f9f9), // Sử dụng màu nền nhẹ nhàng
                            shadowColor: Colors.black.withOpacity(0.2),
                            child: Container(
                              padding: EdgeInsets.all(20),
                              decoration: BoxDecoration(
                                gradient: LinearGradient(
                                  colors: [
                                    Colors.blue.shade50,
                                    Colors.blue.shade200
                                  ],
                                  begin: Alignment.topLeft,
                                  end: Alignment.bottomRight,
                                ),
                                borderRadius: BorderRadius.circular(15),
                              ),
                                child: Column(
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: [
                                    Row(
                                      mainAxisAlignment: MainAxisAlignment.spaceBetween, // Spreads content across the available width
                                      children: [
                                        Text(
                                          "Product ID: ${detail.productId}",
                                          style: TextStyle(
                                            fontSize: 18,
                                            fontWeight: FontWeight.bold,
                                            color: Colors.blue.shade900,
                                          ),
                                        ),
                                        Text(
                                          "Quantity: ${detail.quantity}",
                                          style: TextStyle(
                                            fontSize: 16,
                                            color: Colors.blue.shade600,
                                          ),
                                        ),
                                      ],
                                    ),
                                    SizedBox(height: 10),
                                    Row(
                                      mainAxisAlignment: MainAxisAlignment.spaceBetween, // Spreads content across the available width
                                      children: [
                                        Text(
                                          "Price: \$${detail.price?.toStringAsFixed(2)}",
                                          style: TextStyle(
                                            fontSize: 16,
                                            fontWeight: FontWeight.w500,
                                            color: Colors.green.shade700,
                                          ),
                                        ),
                                        // You can add other text or widgets here if needed
                                      ],
                                    ),
                                    SizedBox(height: 15),
                                  ],
                                )

                            ),
                          ),
                        );
                      }).toList(),
                    ),
                  );
                } else {
                  return Center(child: Text("No order details available."));
                }
              },
            )
          ],
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: 3, // Change this based on the current screen if needed
        selectedItemColor: Colors.black,
        unselectedItemColor: Colors.grey,
        onTap: (index) {
          // Navigate based on index
          if (index == 0) {
            Navigator.pushNamed(context, '/home');
          } else if (index == 1) {
            Navigator.pushNamed(context, '/customize');
          } else if (index == 2) {
            Navigator.pushNamed(context, '/cart');
          } else if (index == 3) {
            Navigator.pushNamed(context, '/profile');
          }
        },
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(icon: Icon(Icons.build), label: 'Customize'),
          BottomNavigationBarItem(
            icon: Icon(Icons.shopping_bag),
            label: 'Bag',
          ),
          BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Profile'),
        ],
      ),
    );
  }
}
String _getShipStatusMessage(String? shipStatus) {
  switch (shipStatus) {
    case "Finished":
      return "Your order is shipped completely";
    case "Tailoring":
      return "Your order is sewing by tailor";
    case "Confirming":
      return "Your order is confirming by store manager";
    case "Ready":
      return "Ready for pick up";
    default:
      return "Status: $shipStatus";
  }
}
