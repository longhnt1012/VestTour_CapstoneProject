import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'homepage_screen.dart';

class BookingConfirmationScreen extends StatelessWidget {
  final int storeId;
  final String storeName;
  final String storeAddress;
  final DateTime bookingDate;
  final TimeOfDay bookingTime;
  final String service;

  const BookingConfirmationScreen({
    Key? key,
    required this.storeId,
    required this.storeName,
    required this.storeAddress,
    required this.bookingDate,
    required this.bookingTime,
    required this.service,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Confirm Booking'),
        backgroundColor: Color(0xFFD2B48C),
        centerTitle: true,
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20.0, vertical: 16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Tiêu đề chính
            Center(
              child: Text(
                'Thank You for Your Booking!',
                style: TextStyle(
                  fontSize: 26,
                  fontWeight: FontWeight.bold,
                ),
                textAlign: TextAlign.center,
              ),
            ),
            SizedBox(height: 24), // Tạo khoảng cách lớn hơn

            // Tiêu đề thông tin booking
            Text(
              'Booking Details:',
              style: TextStyle(
                fontSize: 22,
                fontWeight: FontWeight.bold,
              ),
            ),
            SizedBox(height: 16),

            // Thông tin chi tiết
            Text(
              'Store: $storeName',
              style: TextStyle(fontSize: 20),
            ),
            SizedBox(height: 8),
            Text(
              'Address: $storeAddress',
              style: TextStyle(fontSize: 20),
            ),
            SizedBox(height: 16),

            Text(
              'Date: ${DateFormat('dd/MM/yyyy').format(bookingDate)}',
              style: TextStyle(fontSize: 20),
            ),
            SizedBox(height: 8),
            Text(
              'Time: ${bookingTime.format(context)}',
              style: TextStyle(fontSize: 20),
            ),
            SizedBox(height: 8),
            Text(
              'Service: $service',
              style: TextStyle(fontSize: 20 ),
            ),
            SizedBox(height: 32), // Tạo khoảng cách lớn hơn trước nút

            // Nút Back to Home
            Center(
              child: ElevatedButton(
                onPressed: () {
                  // Điều hướng về trang HomePageScreen
                  Navigator.pushReplacement(
                    context,
                    MaterialPageRoute(builder: (context) => HomeScreen()),
                  );
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Color(0xFFD2B48C),
                  padding: EdgeInsets.symmetric(vertical: 15, horizontal: 40),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: Text(
                  'Back to Home',
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
