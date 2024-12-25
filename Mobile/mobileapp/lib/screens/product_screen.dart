import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/categories/api_services.dart';
import '../../models/product.dart';
import '../../models/category.dart';
import '../data_source/product/api_services.dart';
 // Assuming this is the file where getCategoryById is implemented.
import 'product_detail_screen.dart';

class ProductScreen extends StatefulWidget {
  const ProductScreen({Key? key}) : super(key: key);

  @override
  State<ProductScreen> createState() => _ProductScreenState();
}

class _ProductScreenState extends State<ProductScreen> {
  final ApiServicesProduct _apiServicesProduct = ApiServicesProduct();
  int _currentIndex = 0;
  late Future<List<Product>> _productsFuture;
  final Color pastelBrown = Color(0xFFD2B48C);
  @override
  void initState() {
    super.initState();
    _productsFuture = _apiServicesProduct.getProductsCustomFalse();
  }

  Future<String?> _fetchCategoryName(int? categoryId) async {
    if (categoryId == null) return null;
    final Category? category = await ApiServiceCategories.getCategoryById(categoryId);
    return category?.name; // Return categoryName if available
  }

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
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Shop All'),
        centerTitle: true,
        actions: [
          IconButton(
            icon: const Icon(Icons.filter_alt),
            onPressed: () {
              // Implement filter functionality
            },
          ),
          IconButton(
            icon: const Icon(Icons.search),
            onPressed: () {
              // Implement search functionality
            },
          ),
        ],
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
          BottomNavigationBarItem(icon: Icon(Icons.design_services), label: 'Customize'),
          BottomNavigationBarItem(
              icon: Icon(Icons.shopping_bag), label: 'Bag'), // New Bag item
          BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Profile'),
        ],
      ),
      body: FutureBuilder<List<Product>>(
        future: _productsFuture,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return const Center(child: Text('No products found'));
          }

          List<Product> products = snapshot.data!;
          return GridView.builder(
            padding: const EdgeInsets.all(8.0),
            gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 2,
              crossAxisSpacing: 8.0,
              mainAxisSpacing: 8.0,
              childAspectRatio: 0.7,
            ),
            itemCount: products.length,
            itemBuilder: (context, index) {
              final product = products[index];
              return GestureDetector(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          ProductDetailScreen(product: product),
                    ),
                  );
                },
                child: Card(
                  elevation: 2,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(8.0),
                  ),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      // Product Image
                      Expanded(
                        child: ClipRRect(
                          borderRadius: const BorderRadius.vertical(
                            top: Radius.circular(8.0),
                          ),
                          child: product.imgURL != null
                              ? Image.network(
                            product.imgURL!,
                            fit: BoxFit.cover,
                            width: double.infinity,
                          )
                              : const Icon(
                            Icons.image_not_supported,
                            size: 50,
                          ),
                        ),
                      ),
                      // Product Info
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            const Text(
                              'Just In',
                              style: TextStyle(
                                color: Colors.orange,
                                fontWeight: FontWeight.bold,
                                fontSize: 12,
                              ),
                            ),
                            const SizedBox(height: 4),
                            Text(
                              product.productCode ?? 'N/A',
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.bold,
                              ),
                              maxLines: 1,
                              overflow: TextOverflow.ellipsis,
                            ),
                            const SizedBox(height: 4),
                            FutureBuilder<String?>(
                              future:
                              _fetchCategoryName(product.categoryID), // Fetch categoryName
                              builder: (context, snapshot) {
                                if (snapshot.connectionState ==
                                    ConnectionState.waiting) {
                                  return const Text(
                                    'Loading category...',
                                    style: TextStyle(
                                      fontSize: 12,
                                      color: Colors.grey,
                                    ),
                                  );
                                } else if (snapshot.hasError ||
                                    !snapshot.hasData) {
                                  return const Text(
                                    'Unknown category',
                                    style: TextStyle(
                                      fontSize: 12,
                                      color: Colors.grey,
                                    ),
                                  );
                                } else {
                                  return Text(
                                    snapshot.data!,
                                    style: const TextStyle(
                                      fontSize: 12,
                                      color: Colors.grey,
                                    ),
                                    maxLines: 1,
                                    overflow: TextOverflow.ellipsis,
                                  );
                                }
                              },
                            ),


                            const SizedBox(height: 8),
                            Text(
                              '${product.price?.toStringAsFixed(2)  ?? 'N/A'} USD',
                              style: const TextStyle(
                                fontWeight: FontWeight.bold,
                                fontSize: 14,
                              ),
                            ),

                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}
