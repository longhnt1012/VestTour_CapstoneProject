import 'package:flutter/material.dart';

import '../data_source/booking/manage/api_services.dart';
import '../models/booking.dart';
import '../models/store.dart';

class ManageBookingsScreen extends StatefulWidget {
  @override
  _BookingsScreenState createState() => _BookingsScreenState();
}

class _BookingsScreenState extends State<ManageBookingsScreen> {
  List<Booking> _bookings = [];
  bool _isLoading = true;
  String _errorMessage = '';

  @override
  void initState() {
    super.initState();
    _loadUserBookings();
  }

  // Hàm tải tất cả bookings của người dùng
  _loadUserBookings() async {
    try {
      final userId = await ApiServices.getUserIdFromSharedPreferences();
      if (userId != null) {
        List<Booking> bookings = await ApiServices.getUserBookings(userId);
        setState(() {
          _bookings = bookings;
          _isLoading = false;
        });
      } else {
        setState(() {
          _errorMessage = 'User ID not found!';
          _isLoading = false;
        });
      }
    } catch (e) {
      setState(() {
        _errorMessage = 'Failed to load bookings';
        _isLoading = false;
      });
    }
  }

  // Hàm lấy thông tin cửa hàng từ storeId, trả về một Store object
  Future<Store?> _getStoreDetails(int storeId) async {
    try {
      var storeDetails = await ApiServices.getStoreById(storeId); // Get store by ID
      return storeDetails;
    } catch (e) {
      return null; // Return null if any error occurs
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Your Bookings'),
      ),
      body: _isLoading
          ? Center(child: CircularProgressIndicator())
          : _errorMessage.isNotEmpty
          ? Center(child: Text(_errorMessage))
          : ListView.builder(
        itemCount: _bookings.length,
        itemBuilder: (context, index) {
          final booking = _bookings[index];

          return FutureBuilder<Store?>(
            future: _getStoreDetails(booking.storeId!), // Lấy thông tin cửa hàng từ storeId
            builder: (context, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Center(child: CircularProgressIndicator());
              } else if (snapshot.hasError) {
                return Center(child: Text('Error loading store details'));
              } else if (snapshot.hasData) {
                var store = snapshot.data;
                String storeName = store?.name ?? 'Unknown Store';
                String storeAddress = store?.address ?? 'Unknown Address';

                return Card(
                  child: ListTile(
                    title: Text(storeName),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Address: $storeAddress'),
                        Text('Date: ${booking.bookingDate ?? 'No date'}'),
                        Text('Time: ${booking.time ?? 'No time'}'),
                        Text('Service: ${booking.service ?? 'No service'}'),
                      ],
                    ),
                  ),
                );
              } else {
                return Center(child: Text('No store details available'));
              }
            },
          );
        },
      ),
    );
  }
}
