import 'package:flutter/material.dart';
import 'package:mobileapp/screens/booking_screen.dart';
import 'package:mobileapp/screens/managebooking_screen.dart';
import 'package:mobileapp/screens/measurement_screen.dart';
import 'package:mobileapp/screens/order_screen.dart';
import 'package:mobileapp/screens/userinfo.dart';
import 'package:shared_preferences/shared_preferences.dart';

class UserProfileScreen extends StatefulWidget {
  @override
  _UserProfileScreenState createState() => _UserProfileScreenState();
}

class _UserProfileScreenState extends State<UserProfileScreen> {
  int _currentIndex = 3; // Start with Profile as the selected index
  Future<void> logout() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('auth_token');
    // Điều hướng về trang đăng nhập
    Navigator.pushReplacementNamed(context, '/');
  }
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
        backgroundColor: Colors.white,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            SizedBox(height: 16),
            CircleAvatar(
              radius: 40,
              backgroundColor: Colors.grey[300],
              child: Icon(Icons.camera_alt, size: 40, color: Colors.grey[700]),
            ),
            SizedBox(height: 8),
            Text(
              'Thành Long',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 8),
            ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const UserDetailScreen(),
                  ),
                );

              },
              style: ElevatedButton.styleFrom(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(20),
                  side: BorderSide(color: Colors.grey),
                ),
                backgroundColor: Colors.white,
              ),
              child: Text(
                'Edit Profile',
                style: TextStyle(color: Colors.black),
              ),
            ),
            SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                _buildIconWithLabel(context, Icons.shopping_bag, 'Orders', () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => OrderListScreen()),
                  );
                }),
                _buildIconWithLabel(context, Icons.qr_code, 'Booking', () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => ManageBookingsScreen()),
                  );
                }),
                _buildIconWithLabel(context, Icons.event, 'Measurement', () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => MeasurementScreen()),
                  );
                }),
                _buildIconWithLabel(context, Icons.settings, 'Settings', () {}),
              ],

            ),
            Divider(height: 40),
            _buildListTile('Inbox', 'View messages', trailing: Text('9+', style: TextStyle(color: Colors.red))),
            Divider(),
            _buildListTile('Your Member Rewards', 'No Unlocks Yet'),
            Divider(),
            _buildListTile('Following (5)', '', trailing: Text('Edit')),
            SizedBox(height: 8),

            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: ElevatedButton(
                onPressed: () async {
                  await logout();
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.red, // Màu đỏ
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: Text(
                  'Log Out',
                  style: TextStyle(color: Colors.white, fontSize: 16),
                ),
              ),
            ),
            SizedBox(height: 16),
          ],
        ),
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

  Widget _buildIconWithLabel(BuildContext context, IconData icon, String label, VoidCallback onTap) {
    return GestureDetector(
      onTap: onTap,
      child: Column(
        children: [
          Icon(icon, size: 28),
          SizedBox(height: 4),
          Text(label, style: TextStyle(fontSize: 12)),
        ],
      ),
    );
  }

  Widget _buildListTile(String title, String subtitle, {Widget? trailing}) {
    return ListTile(
      title: Text(title, style: TextStyle(fontWeight: FontWeight.bold)),
      subtitle: subtitle.isNotEmpty ? Text(subtitle) : null,
      trailing: trailing,
    );
  }

  Widget _buildFollowingImage(String url) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 4.0),
      child: ClipRRect(
        borderRadius: BorderRadius.circular(8.0),
        child: Image.network(
          url,
          width: 80,
          height: 80,
          fit: BoxFit.cover,
        ),
      ),
    );
  }
}
