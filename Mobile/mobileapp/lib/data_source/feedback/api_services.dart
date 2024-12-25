import 'dart:convert';
import 'package:http/http.dart' as http;


import '../../models/feedback.dart'; // Replace with your actual import path

class ApiServiceFeedback {
  final String baseUrl = "http://165.22.243.162:8080/api/Feedback";

  // Fetch feedbacks for a specific product by productId

  // Add a new feedback for a specific product
  Future<bool> addFeedbackProduct(FeedbackforProduct feedback) async {
    final url = Uri.parse("$baseUrl/feedbackforproduct"); // URL to send the feedback

    try {
      feedback.dateSubmitted = DateTime.now().toIso8601String().split('T')[0];
      final feedbackJson = feedback.toJson(); // Convert feedback to JSON
      print('JSON being posted: ${json.encode(feedbackJson)}'); // Log JSON

      final response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: json.encode(feedback.toJson()), // Encode feedback to JSON
      );

      if (response.statusCode == 201|| response.statusCode==200) {
        return true; // Feedback successfully added
      } else {
        throw Exception('Failed to add feedback');
      }
    } catch (e) {
      throw Exception('Error: $e');
    }
  }
  /// Fetch feedbacks for a specific product by productId
  Future<List<FeedbackforProduct>> getFeedbacksByProductId(int productId) async {
    final url = Uri.parse('$baseUrl/product/$productId');

    try {
      final response = await http.get(
          url,
          headers: {
        'Content-Type': 'application/json',
      });

      if (response.statusCode == 200) {
        // Decode JSON response
        final List<dynamic> feedbacksJson = jsonDecode(response.body);
        // Map JSON objects to Feedback model
        return feedbacksJson.map((json) => FeedbackforProduct.fromJson(json)).toList();
      } else {
        throw Exception(
            'Failed to load feedbacks. Status code: ${response.statusCode}');
      }
    } catch (error) {
      throw Exception('Error fetching feedbacks: $error');
    }
  }
}
