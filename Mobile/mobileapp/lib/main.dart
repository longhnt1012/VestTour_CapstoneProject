import 'package:flutter/material.dart';
import 'package:mobileapp/models/cart.dart';
import 'package:mobileapp/screens/cart_screen.dart';
import 'package:mobileapp/screens/customize_screen.dart';
import 'package:mobileapp/screens/fabrics_screen.dart';
import 'package:mobileapp/screens/managebooking_screen.dart';
import 'package:mobileapp/screens/measurement_screen.dart';
import 'package:mobileapp/screens/order_screen.dart';
import 'package:mobileapp/screens/product_screen.dart';
import 'package:mobileapp/screens/user_screen.dart';
import 'screens/userprofile_screen.dart';
import 'screens/login_screen.dart';
import 'screens/homepage_screen.dart';
import 'screens/booking_screen.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Vest Tour',
      routes: {
        '/': (context) => LoginScreen(),
        '/home': (context) => HomeScreen(),
        '/booking': (context) => BookingScreen(),
        '/profile': (context) => UserProfileScreen(),
        '/measurement': (context) => MeasurementScreen(),
        '/orders':(context) =>OrderListScreen(),
        '/managebooking': (context)=> ManageBookingsScreen(),
        '/customize': (context) => CustomVestScreen(),
        '/fabric': (context)=> FabricListScreen(),
        '/cart':(context) => CartScreen(),
        '/products':(context)=> ProductScreen(),
         // New ProductScreen route
      },
      initialRoute: '/',
    );
  }
}
