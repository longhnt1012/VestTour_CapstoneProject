
import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/cart/api_services.dart';
import 'package:mobileapp/data_source/lining/api_services.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../data_source/fabrics/api_services.dart';
import '../models/fabric.dart';
import '../models/lining.dart';
import 'cart_screen.dart'; // Import CartScreen to route there

class LiningScreen extends StatefulWidget {
  final int fabricId; // Fabric ID passed from the previous screen
  final List<int> selectedOptionIds; // Style option IDs passed from the previous screen

  LiningScreen({required this.fabricId, required this.selectedOptionIds});

  @override
  _LiningScreenState createState() => _LiningScreenState();
}

class _LiningScreenState extends State<LiningScreen> {
  late Future<List<Lining>> futureLinings;
  late Future<Fabric> futureFabric;
  bool _isLoading = false;
  String _message = '';
  int? selectedLiningId;
  int? userId;
  int? measurementId;

  @override
  void initState() {
    super.initState();
    futureLinings = ApiServiceLining().getAllLinings(); // Fetch linings
    futureFabric = ApiServicesFabric().getFabricById(widget.fabricId); // Fetch fabric details
    _loadMeasurementId();
    _loadUserId();
  }

  Future<void> _loadMeasurementId() async {
    final prefs = await SharedPreferences.getInstance();
    setState(() {
      measurementId = prefs.getInt('measurementId');
    });
  }

  Future<void> _loadUserId() async {
    final _userId = await ApiServicesCart.getUserIdFromSharedPreferences();
    setState(() {
      userId = _userId; // Assign userId value
    });
  }

  void selectLining(int? liningId) {
    setState(() {
      selectedLiningId = liningId;
    });
  }

  void _addToCart() async {
    if (selectedLiningId == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Please select a lining')),
      );
      return;
    }

    setState(() {
      _isLoading = true;
      _message = ''; // Clear previous messages
    });

    final success = await ApiServicesCart.createCart(
      userId: userId!,
      fabricId: widget.fabricId,
      liningId: selectedLiningId!,
      measurementId: measurementId!,
      styleOptionIds: widget.selectedOptionIds,
    );

    setState(() {
      _isLoading = false;
      _message = success ? 'Cart added successfully!' : 'Failed to add to cart.';
    });

    if (success) {
      Navigator.push(
        context,
        MaterialPageRoute(builder: (context) => CartScreen()),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Lining Options'),
        actions: [
          IconButton(
            icon: const Icon(Icons.shopping_cart),
            onPressed: _addToCart,
          ),
        ],
      ),
      body: Column(
        children: [
          Expanded(
            child: FutureBuilder<List<Lining>>(
              future: futureLinings,
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return const Center(child: CircularProgressIndicator());
                } else if (snapshot.hasError) {
                  return Center(child: Text('Error: ${snapshot.error}'));
                } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
                  return const Center(child: Text('No linings available.'));
                } else {
                  List<Lining> linings = snapshot.data!;
                  return GridView.builder(
                    gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                      crossAxisCount: 3,
                      crossAxisSpacing: 10,
                      mainAxisSpacing: 10,
                      childAspectRatio: 0.75,
                    ),
                    itemCount: linings.length,
                    itemBuilder: (context, index) {
                      Lining lining = linings[index];
                      bool isSelected = selectedLiningId == lining.liningId;
                      return GestureDetector(
                        onTap: () => selectLining(lining.liningId),
                        child: Card(
                          color: isSelected ? const Color(0xFFD3B69F) : Colors.white,
                          elevation: 5,
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.center,
                            children: [
                              lining.imageUrl == null || lining.imageUrl!.isEmpty
                                  ? Image.asset(
                                'assets/images/unavailable.png',
                                width: 100,
                                height: 100,
                                fit: BoxFit.fitWidth,
                              )
                                  : Image.network(
                                lining.imageUrl ?? '',
                                width: 100,
                                height: 100,
                                fit: BoxFit.cover,
                              ),
                              Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Text(
                                  lining.liningName ?? 'No Name',
                                  textAlign: TextAlign.center,
                                  style: const TextStyle(fontWeight: FontWeight.bold),
                                ),
                              ),
                            ],
                          ),
                        ),
                      );
                    },
                  );
                }
              },
            ),
          ),
          Align(
            alignment: Alignment.bottomCenter,
            child: FutureBuilder<Fabric>(
              future: futureFabric,
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return Container(
                    color: Colors.white,
                    padding: const EdgeInsets.all(15),
                    child: const Center(child: CircularProgressIndicator()),
                  );
                } else if (snapshot.hasError) {
                  return Container(
                    color: Colors.white,
                    padding: const EdgeInsets.all(15),
                    child: Center(child: Text("Error: ${snapshot.error}")),
                  );
                } else if (!snapshot.hasData) {
                  return Container(
                    color: Colors.white,
                    padding: const EdgeInsets.all(15),
                    child: const Center(child: Text("Fabric not found.")),
                  );
                } else {
                  return Container(
                    color: const Color(0xFFD2B48C),
                    padding: const EdgeInsets.all(15),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        const Text(
                          "Total Price:",
                          style: TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.bold,
                            fontSize: 16,
                          ),
                        ),
                        Text(
                          "${snapshot.data!.price} USD",
                          style: const TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.bold,
                            fontSize: 16,
                          ),
                        ),
                      ],
                    ),
                  );
                }
              },
            ),
          ),
        ],
      ),
    );
  }
}

