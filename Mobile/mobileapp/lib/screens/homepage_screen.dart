import 'package:flutter/material.dart';
import 'package:flutter_staggered_grid_view/flutter_staggered_grid_view.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../data_source/user/api_services.dart';
import '../models/user.dart';

class HomeScreen extends StatefulWidget {
  @override
  _HomeScreenState createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  int userId = 0;
  int _currentIndex = 0;
  int _currentImageIndex = 0; // For the image slider

  String userName = 'Loading...';
  final Color pastelBrown = Color(0xFFD2B48C);

  final List<String> _sliderImages = [
    'assets/images/vest_banner_slide_2.jpg',
    'assets/images/vest_banner_slide.jpg',
    'assets/images/vest_banner_slide_3.jpg',
  ];

  void _onBottomNavTap(int index) {
    setState(() {
      _currentIndex = index;
    });
    if (index == 1) {
      Navigator.pushNamed(context, '/customize');
    } else if (index == 2) {
      Navigator.pushNamed(context, '/cart');
    } else if (index == 3) {
      Navigator.pushNamed(context, '/profile');
    }
  }

  @override
  void initState() {
    super.initState();
    _loadUser();
  }

  // Load user from the API service
  _loadUser() async {
    final user = await ApiService.getUserById();
    if (user != null) {
      setState(() {
        userId = user.userId ?? 0;
        userName = user.name ?? 'No name available';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        backgroundColor: pastelBrown,
        elevation: 0,
        title: Image.asset('assets/images/logo_only.png',
            width: 100, height: 100, fit: BoxFit.cover),
        centerTitle: true,
      ),
      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment
              .start, // Căn trái toàn bộ các widget trong Column
          children: [
            Padding(
              padding: const EdgeInsets.all(16.0),
              child: Text(
                'Hello, $userName',
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                  color: Colors.black87,
                ),
              ),
            ),
            // Slider Images Section
            _buildImageSlider(),
            Container(
              margin: const EdgeInsets.only(
                  top: 15.0,
                  bottom: 15.0,
                  left: 10.0), // Thêm margin top, bottom và left
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Text(
                  'Services',
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                    color: Colors.black87,
                  ),
                ),
              ),
            ),
            SizedBox(height: 8),
            _buildCategoryList(),
            SizedBox(height: 20),
            Container(
              margin: const EdgeInsets.only(
                  top: 15.0,
                  bottom: 15.0,
                  left: 10.0), // Thêm margin top, bottom và left
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Text(
                  'Categories',
                  style: TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      color: Colors.black87),
                ),
              ),
            ),
            SizedBox(height: 8),
            _buildProductTypes(), // Updated section
            SizedBox(height: 20),
            Container(
              margin: const EdgeInsets.only(
                  top: 15.0,
                  bottom: 15.0,
                  left: 10.0), // Thêm margin top, bottom và left
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Text(
                  'Recommend for You',
                  style: TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      color: Colors.black87),
                ),
              ),
            ),
            SizedBox(height: 8),
// Gọi grid gợi ý
            _buildRecommendationGrid(),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.pushNamed(context, '/booking');
        },
        child: Icon(Icons.calendar_today),
        backgroundColor: pastelBrown,
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _currentIndex,
        selectedItemColor: Colors.black,
        unselectedItemColor: Colors.grey,
        onTap: _onBottomNavTap,
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(
              icon: Icon(Icons.design_services), label: 'Customize'),
          BottomNavigationBarItem(
              icon: Icon(Icons.shopping_bag), label: 'Bag'), // New Bag item
          BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Profile'),
        ],
      ),
    );
  }

  Widget _buildImageSlider() {
    return Container(
      height: 200,
      child: PageView.builder(
        itemCount: _sliderImages.length,
        onPageChanged: (index) {
          setState(() {
            _currentImageIndex = index;
          });
        },
        itemBuilder: (context, index) {
          return Image.asset(
            _sliderImages[index],
            fit: BoxFit.cover,
          );
        },
      ),
    );
  }

  Widget _buildCategoryList() {
    final categories = [
      {'title': 'Booking Now', 'icon': Icons.book_online},
      {'title': 'Find your color', 'icon': Icons.color_lens},
      {'title': 'Scan your skin', 'icon': Icons.add_a_photo},
      {'title': 'Customize', 'icon': Icons.design_services}, // New category
      {'title': 'Shopping', 'icon': Icons.shopping_cart}, // New category
    ];

    return SizedBox(
      height: 120,
      child: ListView.builder(
        scrollDirection: Axis.horizontal,
        itemCount: categories.length,
        itemBuilder: (context, index) {
          return _buildCategoryItem(
            categories[index]['title'] as String,
            categories[index]['icon'] as IconData,
          );
        },
      ),
    );
  }

  Widget _buildCategoryItem(String title, IconData icon) {
    return GestureDetector(
      onTap: () {
        if (title == 'Booking Now') {
          Navigator.pushNamed(context, '/booking');
        } else if (title == 'Find your color') {
          Navigator.pushNamed(context, '/birthday');
        } else if (title == 'Scan your skin') {
          Navigator.pushNamed(context, '/scanSkin');
        } else if (title == 'Customize') {
          Navigator.pushNamed(
              context, '/customize'); // Navigate to customize page
        } else if (title == 'Shopping') {
          Navigator.pushNamed(
              context, '/shopping'); // Navigate to shopping page
        }
      },
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 8.0),
        child: Column(
          children: [
            CircleAvatar(
              radius: 30,
              backgroundColor: Color(0xFFD2B48C),
              child: Icon(icon, size: 30, color: Colors.black),
            ),
            SizedBox(height: 8),
            Text(title, style: TextStyle(color: Colors.black87)),
          ],
        ),
      ),
    );
  }

  // Function to display product types (e.g., Vest, Shirt, Pants, etc.)
  Widget _buildProductTypes() {
    final productTypes = [
      {'name': 'Vests', 'image': 'assets/images/suit_type.jpg'},
      {'name': 'Shirts', 'image': 'assets/images/somi.jpg'},
      {'name': 'Pants', 'image': 'assets/images/pant.jpg'},
      {'name': 'Accessories', 'image': 'assets/images/ties.jpg'}
    ];

    return SizedBox(
      height: 120,
      child: ListView.builder(
        scrollDirection: Axis.horizontal,
        itemCount: productTypes.length,
        itemBuilder: (context, index) {
          return _buildProductTypeItem(
            productTypes[index]['name'] as String,
            productTypes[index]['image'] as String,
          );
        },
      ),
    );
  }

  // Function to build each product type item (with image)
  Widget _buildProductTypeItem(String name, String image) {
    return GestureDetector(
      onTap: () {
        Navigator.pushNamed(
          context,
          '/products',
          // Passing the product type as an argument
        );
        // Handle tap (e.g., navigate to a specific product list page)
      },
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 8.0),
        child: Column(
          children: [
            Container(
              height: 80,
              width: 80,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(8.0),
                image: DecorationImage(
                  image: AssetImage(image),
                  fit: BoxFit.cover,
                ),
              ),
            ),
            SizedBox(height: 8),
            Text(name, style: TextStyle(color: Colors.black87)),
          ],
        ),
      ),
    );
  }
}
Widget _buildRecommendationGrid() {
  final List<Map<String, String>> recommendedImages = [
    {'image': 'assets/images/vest_banner_slide_2.jpg', 'name': 'Elegant Vest'},
    {'image': 'assets/images/somi.jpg', 'name': 'Stylish Shirt'},
    {'image': 'assets/images/pant.jpg', 'name': 'Classic Pants'},
    {'image': 'assets/images/ties.jpg', 'name': 'Modern Accessories'},
    {'image': 'assets/images/vest_banner_slide.jpg', 'name': 'Luxury Vest'},
    {'image': 'assets/images/vest_banner_slide_3.jpg', 'name': 'Premium Vest'},
  ];

  return Padding(
    padding: const EdgeInsets.symmetric(horizontal: 8.0),
    child: MasonryGridView.count(
      crossAxisCount: 2, // Số cột trong lưới
      mainAxisSpacing: 8, // Khoảng cách giữa các item theo chiều dọc
      crossAxisSpacing: 8, // Khoảng cách giữa các item theo chiều ngang
      shrinkWrap: true,
      physics: NeverScrollableScrollPhysics(), // Để không xung đột với cuộn chính
      itemCount: recommendedImages.length,
      itemBuilder: (context, index) {
        final item = recommendedImages[index];
        return GestureDetector(
          onTap: () {
            // Xử lý khi người dùng nhấn vào ảnh
            Navigator.pushNamed(context, '/productDetails', arguments: item);
          },
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              ClipRRect(
                borderRadius: BorderRadius.circular(8.0),
                child: Image.asset(
                  item['image']!,
                  fit: BoxFit.cover,
                  height: index.isEven ? 150 : 200, // Chiều cao không đồng nhất
                  width: double.infinity,
                ),
              ),
              SizedBox(height: 8),
              Text(
                item['name']!,
                style: TextStyle(fontSize: 16, color: Colors.black87),
              ),
            ],
          ),
        );
      },
    ),
  );
}
