import 'package:flutter/material.dart';
import '../data_source/fabrics/api_services.dart';
import '../models/fabric.dart';
import 'style_screen.dart'; // Import your StyleScreen

class FabricListScreen extends StatefulWidget {
  @override
  _FabricListScreenState createState() => _FabricListScreenState();
}

class _FabricListScreenState extends State<FabricListScreen> {
  late Future<List<Fabric>> futureFabrics;
  int? selectedFabricId;
  bool isLongPressed = false; // Track if the long press is active
  Fabric? selectedFabric;
  int? fabricIdForColorChange; // Track selected fabric's ID for color change

  @override
  void initState() {
    super.initState();
    futureFabrics = ApiServicesFabric().getFabrics();
  }

  // Function to handle fabric selection
  void selectFabric(int? fabricId) {
    setState(() {
      // If the same fabric is clicked again, unselect it
      if (fabricIdForColorChange == fabricId) {
        fabricIdForColorChange = null;
      } else {
        fabricIdForColorChange = fabricId; // Set the fabric ID for color change
      }
      selectedFabricId = fabricId;
    });
  }

  // Function to handle navigation to StyleScreen
  void navigateToStyleScreen() {
    if (selectedFabricId != null) {
      Navigator.push(
        context,
        MaterialPageRoute(
          builder: (context) => StyleScreen(fabricId: selectedFabricId!),
        ),
      );
    } else {
      // Optionally show an alert if no fabric is selected
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Please select a fabric')));
    }
  }

  // Function to show a long-pressed fabric's details
  void onLongPress(Fabric fabric) {
    setState(() {
      isLongPressed = true;
      selectedFabric = fabric;
    });
  }

  // Function to close the long press details
  void closeLongPress() {
    setState(() {
      isLongPressed = false;
      selectedFabric = null;
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Fabrics List'),
      ),
      body: FutureBuilder<List<Fabric>>(
        future: futureFabrics,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Center(child: Text('No fabrics available.'));
          } else {
            List<Fabric> fabrics = snapshot.data!;
            return Stack(
              children: [
                // Main grid view without SingleChildScrollView
                GridView.builder(
                  gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                    crossAxisCount: 3, // 3 fabrics per row
                    crossAxisSpacing: 10, // Space between columns
                    mainAxisSpacing: 10, // Space between rows
                    childAspectRatio: 0.75, // Aspect ratio for each grid item (height/width)
                  ),
                  itemCount: fabrics.length,
                  itemBuilder: (context, index) {
                    Fabric fabric = fabrics[index];
                    bool isSelected = selectedFabricId == fabric.fabricID;
                    return GestureDetector(
                      onTap: () => selectFabric(fabric.fabricID), // Set the selected fabricId
                      onLongPress: () => onLongPress(fabric), // Show details on long press
                      child: Card(
                        color: isSelected ? Color(0xFFD3B69F) : Colors.white,
                        elevation: 5,
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            fabric.imageUrl == null || fabric.imageUrl!.isEmpty
                                ? Image.asset(
                              'assets/images/unavailable.png',
                              width: 100,
                              height: 100,
                              fit: BoxFit.fitWidth,
                            )
                                : Image.network(
                              fabric.imageUrl ?? '',
                              width: 100,
                              height: 100,
                              fit: BoxFit.cover,
                            ),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Text(
                                fabric.fabricName ?? 'No Name',
                                textAlign: TextAlign.center,
                                style: TextStyle(fontWeight: FontWeight.bold),
                              ),
                            ),
                            Padding(
                              padding: const EdgeInsets.symmetric(horizontal: 8.0),
                              child: Text(
                                'Price: \$${fabric.price}',
                                style: TextStyle(color: Colors.grey),
                              ),
                            ),
                          ],
                        ),
                      ),
                    );
                  },
                ),
                // If a long press occurs, show the floating widget
                if (isLongPressed && selectedFabric != null)
                  GestureDetector(
                    onTap: closeLongPress, // Close the details on tap outside
                    child: Container(
                      color: Colors.black54, // Semi-transparent background
                      child: Center(
                        child: Material(
                          color: Colors.transparent,
                          child: Container(
                            width: 300,
                            height: 400,
                            decoration: BoxDecoration(
                              color: Colors.white,
                              borderRadius: BorderRadius.circular(10),
                            ),
                            child: Column(
                              children: [
                                // Title section
                                Padding(
                                  padding: const EdgeInsets.all(8.0),
                                  child: Text(
                                    'Fabrics Detail',
                                    style: TextStyle(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                ),
                                SizedBox(height: 10), // Space between title and image
                                selectedFabric!.imageUrl == null || selectedFabric!.imageUrl!.isEmpty
                                    ? Image.asset(
                                  'assets/images/unavailable.png',
                                  width: 250,
                                  height: 250,
                                  fit: BoxFit.cover,
                                ):
                                // Image section
                                Image.network(
                                  selectedFabric!.imageUrl ?? '',
                                  width: 250,
                                  height: 250,
                                  fit: BoxFit.cover,
                                ),

                                // Description section
                                Padding(
                                  padding: const EdgeInsets.all(10.0),
                                  child: Row(
                                    children: [
                                      // Left side: Description text
                                      Expanded(
                                        child: Text(
                                          selectedFabric!.description ?? 'No description available.',
                                          style: TextStyle(
                                            fontSize: 16,
                                            color: Colors.black,
                                          ),
                                          textAlign: TextAlign.left,
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                    ),
                  ),
              ],
            );
          }
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: navigateToStyleScreen, // Navigate when pressed
        child: Icon(Icons.arrow_forward),
      ),
    );
  }

}
