import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/cart/api_services.dart';
import 'package:mobileapp/data_source/feedback/api_services.dart';
import 'package:mobileapp/models/feedback.dart' as CustomFeedback;
import 'package:shared_preferences/shared_preferences.dart';
import 'package:flutter_rating_bar/flutter_rating_bar.dart';
import '../data_source/categories/api_services.dart';
import '../data_source/user/api_services.dart';
import '../models/category.dart';
import '../models/product.dart';
import 'cart_screen.dart';

class ProductDetailScreen extends StatelessWidget {
  final Product product;
  final Color pastelBrown = const Color(0xFFD2B48C);
  final TextEditingController _commentController = TextEditingController();
  bool _isSubmitting = false;
  double _rating = 0.0;

  ProductDetailScreen({Key? key, required this.product}) : super(key: key);

  Future<String?> _fetchCategoryName(int? categoryId) async {
    if (categoryId == null) return null;
    try {
      final Category? category = await ApiServiceCategories.getCategoryById(categoryId);
      return category?.name;
    } catch (e) {
      print('Error fetching category name: $e');
      return null;
    }
  }
  Future<String?> getAuthToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString('auth_token');
  }
  Future<int> _getUserId() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getInt('userId') ?? 0;
  }
  Future<int?> getUserIdFromToken(String authToken) async {
    return await ApiServicesCart.getUserIdFromToken(authToken);
  }
  Future<List<CustomFeedback.FeedbackforProduct>> _fetchFeedbacks(int productId) async {
    try {
      final feedbacks = await ApiServiceFeedback().getFeedbacksByProductId(productId);
      return feedbacks;
    } catch (error) {
      print('Error fetching feedbacks: $error');
      throw Exception('Failed to load feedbacks.');
    }
  }
  Future<String?> _fetchUserName(int userId) async {
    try {
      final user = await ApiService.getUserById(); // Gọi API để lấy thông tin người dùng
      return user?.name; // Giả sử `user` có thuộc tính `name`
    } catch (e) {
      print('Error fetching user name: $e');
      return null;
    }
  }


  Future<void> _submitFeedback(BuildContext context) async {
    if (_commentController.text.isEmpty || _rating == 0.0) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Please provide a comment and rating.')),
      );
      return;
    }

    try {
      _isSubmitting = true;
      String? authToken = await getAuthToken();
      final userId = await getUserIdFromToken(authToken!);
      final success = await ApiServiceFeedback().addFeedbackProduct(
        CustomFeedback.FeedbackforProduct(
          feedbackId: 0,
          productId: product.productID,
          userId: userId,
          comment: _commentController.text,
          rating: _rating.toInt(),
          dateSubmitted: DateTime.now().toIso8601String(),
        ),
      );

      if (success) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Feedback submitted successfully.')),
        );
        _commentController.clear();
        _rating = 0.0;
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Failed to submit feedback.')),
        );
      }
    } catch (e) {
      print('Error submitting feedback: $e');
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('An error occurred while submitting feedback.')),
      );
    } finally {
      _isSubmitting = false;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(product.productCode ?? 'Product Details'),
        centerTitle: true,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () {
            Navigator.pop(context);
          },
        ),
      ),
      body: SafeArea(
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              if (product.imgURL != null)
                Container(
                  height: 300,
                  width: double.infinity,
                  child: AspectRatio(
                    aspectRatio: 1.5,
                    child: Image.network(
                      product.imgURL!,
                      fit: BoxFit.contain,
                    ),
                  ),
                )
              else
                const Icon(Icons.image_not_supported, size: 100),
              const SizedBox(height: 16),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      product.productCode ?? 'N/A',
                      style: const TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
                    ),

                    const SizedBox(height: 8),

                    FutureBuilder<String?>(
                      future: _fetchCategoryName(product.categoryID),
                      builder: (context, snapshot) {
                        if (snapshot.connectionState == ConnectionState.waiting) {
                          return const CircularProgressIndicator();
                        } else if (snapshot.hasError) {
                          return const Text('Error loading category');
                        } else if (snapshot.hasData) {
                          return Text(
                            snapshot.data ?? 'Unknown Category',
                            style: const TextStyle(fontSize: 16, color: Colors.grey),
                          );
                        } else {
                          return const Text('Unknown Category');
                        }
                      },
                    ),
                    const SizedBox(height: 8),
                    Text(
                      '${product.price?.toStringAsFixed(2) ?? 'N/A'} USD (Tax Incl.)',
                      style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                    ),
                    Wrap(
                      spacing: 8.0, // Khoảng cách giữa các ô size
                      children: ['S', 'M', 'L', 'XL'].map(
                            (size) {
                          final isAvailable = product.size?.contains(size) ?? false;
                          return GestureDetector(
                            onTap: isAvailable
                                ? () {
                              // Xử lý sự kiện khi size khả dụng được chọn
                              print('Selected size: $size');
                            }
                                : null, // Không làm gì khi size không khả dụng
                            child: Container(
                              padding: const EdgeInsets.symmetric(vertical: 8.0, horizontal: 16.0),
                              decoration: BoxDecoration(
                                color: Colors.white, // Màu nền chung
                                borderRadius: BorderRadius.circular(8.0),
                              ),
                              child: Text(
                                size,
                                style: TextStyle(
                                  color: isAvailable ? Colors.black : Colors.grey, // Màu chữ
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16,
                                  decoration: isAvailable
                                      ? TextDecoration.none // Không gạch khi có
                                      : TextDecoration.lineThrough, // Gạch khi không có
                                ),
                              ),
                            ),
                          );
                        },
                      ).toList(),
                    ),
                  ],
                ),
              ),
              const SizedBox(height: 16),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: ElevatedButton(
                  onPressed: () async {
                    int userId = await _getUserId();

                    bool success = await ApiServicesCart.createCartProduct(userId, product.productID ?? 0);
                    if (success) {
                      ScaffoldMessenger.of(context).showSnackBar(
                        SnackBar(content: Text('${product.productCode} added to bag')),
                      );
                      Future.delayed(const Duration(seconds: 2), () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(builder: (context) => CartScreen()),
                        );
                      });
                    } else {
                      ScaffoldMessenger.of(context).showSnackBar(
                        const SnackBar(content: Text('Failed to add product to bag')),
                      );
                    }
                  },
                  style: ElevatedButton.styleFrom(
                    minimumSize: const Size.fromHeight(50),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(8),
                    ),
                    backgroundColor: pastelBrown,
                  ),
                  child: const Text(
                    'Add to Bag',
                    style: TextStyle(fontSize: 18),
                  ),
                ),
              ),
              const SizedBox(height: 16),
              const Divider(),
              const Padding(
                padding: EdgeInsets.all(16.0),
                child: Text(
                  'Customer Feedbacks',
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                ),
              ),
              FutureBuilder<List<CustomFeedback.FeedbackforProduct>>(
                future: _fetchFeedbacks(product.productID ?? 0),
                builder: (context, snapshot) {
                  List<Widget> feedbackWidgets = [];

                  // Thêm Form Phản Hồi (Luôn Hiển Thị)
                  feedbackWidgets.add(
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 16.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            'Add your feedback:',
                            style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(height: 8),
                          TextField(
                            controller: _commentController,
                            decoration: const InputDecoration(
                              hintText: 'Write a comment...',
                              border: OutlineInputBorder(),
                            ),
                            maxLines: 3,
                          ),
                          const SizedBox(height: 8),
                          RatingBar.builder(
                            initialRating: 0,
                            minRating: 1,
                            direction: Axis.horizontal,
                            itemCount: 5,
                            itemSize: 20.0,
                            itemPadding: const EdgeInsets.symmetric(horizontal: 4.0),
                            itemBuilder: (context, _) => const Icon(
                              Icons.star,
                              color: Colors.amber,
                            ),
                            onRatingUpdate: (rating) {
                              _rating = rating;
                            },
                          ),
                          const SizedBox(height: 16),
                          ElevatedButton(
                            onPressed: () => _submitFeedback(context),
                            child: _isSubmitting
                                ? const CircularProgressIndicator(color: Colors.white)
                                : const Text('Submit Feedback'),
                          ),
                        ],
                      ),
                    ),
                  );

                  // Hiển Thị Feedbacks Nếu Có
                  if (snapshot.connectionState == ConnectionState.waiting) {
                    feedbackWidgets.add(const Center(child: CircularProgressIndicator()));
                  } else if (snapshot.hasError) {
                    feedbackWidgets.add(const Center(child: Text('Failed to load feedbacks.')));
                  } else if (snapshot.hasData && snapshot.data!.isNotEmpty) {
                    feedbackWidgets.addAll(snapshot.data!.map((feedback) {
                      return Card(
                        margin: const EdgeInsets.symmetric(vertical: 8.0, horizontal: 16.0),
                        child: ListTile(
                          title:
                          FutureBuilder<String?>(
                            future: _fetchUserName(feedback.userId ?? 0), // Gọi phương thức để lấy tên người dùng từ userId
                            builder: (context, snapshot) {
                              if (snapshot.connectionState == ConnectionState.waiting) {
                                return const CircularProgressIndicator();
                              } else if (snapshot.hasError) {
                                return const Text('Error fetching user name');
                              } else if (snapshot.hasData) {
                                return Text(
                                  '${snapshot.data ?? 'N/A'}',
                                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                                );
                              } else {
                                return const Text('User Name: N/A');
                              }
                            },
                          ),
                          subtitle: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Row(
                                children: List.generate(5, (index) {
                                  if (index < (feedback.rating?.round() ?? 0)) {
                                    // Hiển thị ngôi sao đầy (filled)
                                    return Icon(Icons.star, color: Colors.yellow, size: 16);
                                  } else {
                                    // Hiển thị ngôi sao rỗng (empty)
                                    return Icon(Icons.star_border, color: Colors.grey, size: 16);
                                  }
                                }),
                              ),


                              Text('Date: ${feedback.dateSubmitted ?? 'N/A'}'),
                              Text(
                                feedback.comment ?? 'No Comment',
                                style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                              ),
                            ],
                          ),

                        ),
                      );
                    }).toList());
                  } else {
                    feedbackWidgets.add(const Padding(
                      padding: EdgeInsets.symmetric(horizontal: 16.0),
                      child: Text('No feedbacks available.'),
                    ));
                  }

                  return Column(children: feedbackWidgets);
                },
              ),

            ],
          ),
        ),
      ),
    );
  }
}
