import 'package:flutter/material.dart';

class CustomVestScreen extends StatefulWidget {
  @override
  _CustomVestScreenState createState() => _CustomVestScreenState();
}

class _CustomVestScreenState extends State<CustomVestScreen> {
  int _currentIndex = 1;

  // Handle bottom navigation bar tap
  void _onBottomNavTap(int index) {
    setState(() {
      _currentIndex = index;
    });
    if (index == 0) {
      Navigator.pushNamed(context, '/home');
    } else if (index == 2) {
      Navigator.pushNamed(context, '/cart');
    } else if (index == 3) {
      Navigator.pushNamed(context, '/profile');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Customize Your Suit"),
        backgroundColor:Color(0xFFD2B48C),
      ),
      body: SingleChildScrollView(  // Wrap the content with SingleChildScrollView
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Step 1: Choose Fabric
            Text(
              'Step 1: Choose Fabric',
              style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold, color:Color(0xFFD2B48C)),
            ),
            SizedBox(height: 10),
            // Add an Image for Fabric selection and description
            Image.asset('assets/images/fabric_selection.jpg', height: 150, width: double.infinity, fit: BoxFit.cover),
            SizedBox(height: 10),
            Text(
              'Select a fabric that matches your style and comfort. Choose from premium, elegant, or luxury fabric options.',
              style: TextStyle(fontSize: 16, color: Colors.grey[700]),
            ),
            SizedBox(height: 20),

            // Fabric selection widget goes here...

            // Step 2: Choose Your Style
            Text(
              'Step 2: Choose Your Style',
              style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold, color:Color(0xFFD2B48C)),
            ),
            SizedBox(height: 10),
            // Add an Image for Style selection
            Image.asset('assets/images/vest_style.jpg', height: 150, width: double.infinity, fit: BoxFit.cover),
            SizedBox(height: 10),
            Text(
              'Select the style of your vest. Whether it\'s formal, casual, or trendy, we have the perfect fit for you.',
              style: TextStyle(fontSize: 16, color: Colors.grey[700]),
            ),
            SizedBox(height: 20),

            // Style selection widget goes here...

            // Step 3: Select Lining
            Text(
              'Step 3: Select Lining',
              style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold, color: Color(0xFFD2B48C)),
            ),
            SizedBox(height: 10),
            // Add an Image for Lining selection
            Image.asset('assets/images/lining_selection.jpg', height: 150, width: double.infinity, fit: BoxFit.cover),
            SizedBox(height: 10),
            Text(
              'Choose the lining that complements your vest. A good lining can improve comfort and durability.',
              style: TextStyle(fontSize: 16, color: Colors.grey[700]),
            ),
            SizedBox(height: 20),

            // Lining selection widget goes here...

            // Next button to proceed
            ElevatedButton(
              onPressed: () {
                Navigator.pushNamed(context, '/fabric');
              },
              style: ElevatedButton.styleFrom(
                backgroundColor: Color(0xFFD2B48C), // Button color
                padding: EdgeInsets.symmetric(vertical: 15, horizontal: 30),
              ),
              child: Text(
                'Next',
                style: TextStyle(fontSize: 18, color: Colors.white),
              ),
            ),
          ],
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _currentIndex,
        selectedItemColor: Colors.blueAccent,
        unselectedItemColor: Colors.grey,
        onTap: _onBottomNavTap,
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(icon: Icon(Icons.design_services), label: 'Customize'),
          BottomNavigationBarItem(
            icon: Icon(Icons.shopping_bag), label: 'Bag',
          ),
          BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Profile'),
        ],
      ),
    );
  }
}
