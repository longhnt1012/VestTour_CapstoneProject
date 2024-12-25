import 'package:flutter/material.dart';
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

      final userId = await ApiServices.getUserIdFromSharedPreferences();
      if (userId == null) {
        setState(() {
          _errorMessage = "User not logged in.";
        });
        return;
      }

      final orders = await ApiServices.showOrdersByUser(userId);
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
                  if (order.deliveryMethod != null)
                    Text(
                      "${_mapStatus(order.deliveryMethod)}",
                      style: TextStyle(
                        fontWeight: FontWeight.normal,
                        color: Colors.grey[600],
                      ),
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

  String _mapStatus(String? status) {
    switch (status) {
      case 'picked_up':
        return "Picked up";
      case 'in_transit':
        return "In Transit";
      case 'delivered':
        return "Delivered";
      case 'Pending':
        return "Pending";
      case 'Shipment':
        return "Shipment";
      default:
        return "Status Unknown";
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
            Text(
              "Order #${order.orderId}",
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18.0),
            ),
            SizedBox(height: 10.0),
            if (order.orderDate != null)
              Text("Order Date: ${order.orderDate}"),
            if (order.shippedDate != null)
              Text("Shipped Date: ${order.shippedDate}"),
            Text("Status: ${_mapStatus(order.status)}"),
            if (order.totalPrice != null)
              Text("Total Price: \$${order.totalPrice}"),
            if (order.guestName != null) Text("Guest Name: ${order.guestName}"),
            if (order.guestAddress != null)
              Text("Guest Address: ${order.guestAddress}"),
            SizedBox(height: 20.0),
            Text(
              "Order Details",
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 16.0),
            ),
            if (order.orderDetails != null && order.orderDetails!.isNotEmpty)
              ...order.orderDetails!.map((detail) {
                return ListTile(
                  title: Text("Product ID: ${detail.productId}"),
                  subtitle: Text("Quantity: ${detail.quantity}"),
                  trailing: Text("\$${detail.price}"),
                );
              }).toList(),
            if (order.orderDetails == null || order.orderDetails!.isEmpty)
              Text("No order details available."),
          ],
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: 3, // You can modify this based on the desired screen
        selectedItemColor: Colors.black,
        unselectedItemColor: Colors.grey,
        onTap: (index) {
          // You can navigate to other screens here if needed
          if (index == 0) {
            Navigator.pushNamed(context, '/home');
          } else if (index == 1) {
            Navigator.pushNamed(context, '/customize');
          } else if (index == 2) {
            Navigator.pushNamed(context, '/cart');
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

  String _mapStatus(String? status) {
    switch (status) {
      case 'picked_up':
        return "Picked up";
      case 'in_transit':
        return "In Transit";
      case 'delivered':
        return "Delivered";
      default:
        return "Status Unknown";
    }
  }
}


