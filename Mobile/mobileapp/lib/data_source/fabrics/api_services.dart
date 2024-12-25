import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../models/fabric.dart';


class ApiServicesFabric {
  // Define the base URL for the API
  static const String _baseUrl = 'http://165.22.243.162:8080/api/Fabrics';

  // Method to fetch the list of fabrics
  Future<List<Fabric>> getFabrics() async {
    try {
      // Send a GET request to the API
      final response = await http.get(Uri.parse(_baseUrl));

      // Check if the response is successful (status code 200)
      if (response.statusCode == 200) {
        // Decode the JSON response
        List<dynamic> responseData = json.decode(response.body);

        // Convert the list of JSON objects into a list of Fabric objects
        List<Fabric> fabrics = responseData.map((fabricJson) => Fabric.fromJson(fabricJson)).toList();

        // Return the list of fabrics
        return fabrics;
      } else {
        // Handle the case when the API returns an error
        throw Exception('Failed to load fabrics');
      }
    } catch (e) {
      // Handle any exceptions
      print('Error: $e');
      throw Exception('Failed to load fabrics');
    }
  }
  Future<Fabric> getFabricById(int fabricId) async {
    try {
      // Construct the URL with the fabric ID
      final url = '$_baseUrl/$fabricId';

      // Send a GET request to the API
      final response = await http.get(Uri.parse(url));

      // Check if the response is successful (status code 200)
      if (response.statusCode == 200) {
        // Decode the JSON response and convert it to a Fabric object
        Map<String, dynamic> responseData = json.decode(response.body);
        Fabric fabric = Fabric.fromJson(responseData);
        if (fabric.fabricID == null) {
          throw Exception('Fabric ID là null');
        }
        // Return the Fabric object
        return fabric;
      } else {
        // Handle the case when the API returns an error
        throw Exception('Failed to load fabric with ID $fabricId');
      }
    } catch (e) {
      // Handle any exceptions
      print('Error: $e');
      throw Exception('Failed to load fabric with ID $fabricId');
    }
  }
}

