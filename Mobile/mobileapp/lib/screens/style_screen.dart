import 'package:flutter/material.dart';
import '../data_source/style/api_services.dart';
import '../models/StyleOption.dart';
import '../models/fabric.dart';
import '../models/style.dart';
import 'lining_screen.dart';


class StyleScreen extends StatefulWidget {
  StyleScreen({required this.fabricId});
  final int fabricId;
  @override
  _StyleScreenState createState() => _StyleScreenState();
}
class _StyleScreenState extends State<StyleScreen> {
  final ApiServiceStyle _apiService = ApiServiceStyle();
  late Future<List<Style>> _styles;
  late Future<Fabric> _fabric;
  Map<String, List<StyleOption>> _groupedOptions = {};
  int? _selectedStyleId;
  Map<String, int?> _selectedOptionIds = {}; // Sử dụng Map để lưu trữ chỉ một option cho mỗi optionType

  @override
  void initState() {
    super.initState();
    _styles = _apiService.fetchStyles();
    _fabric = _apiService.getFabricById(widget.fabricId);

    // Load options for the first style
    _styles.then((styles) {
      if (styles.isNotEmpty) {
        _fetchStyleOptions(styles.first.styleId!);
      }
    });
  }

  Future<void> _fetchStyleOptions(int styleId) async {
    final options = await _apiService.fetchGroupedStyleOptionsByStyleId(styleId);
    setState(() {
      _groupedOptions = options;
      _selectedStyleId = styleId;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Style & Options"),
      ),
      body: Stack(
        children: [
          SingleChildScrollView(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                FutureBuilder<List<Style>>(
                  future: _styles,
                  builder: (context, snapshot) {
                    if (snapshot.connectionState == ConnectionState.waiting) {
                      return const Center(child: CircularProgressIndicator());
                    } else if (snapshot.hasError) {
                      return Center(child: Text("Error: ${snapshot.error}"));
                    } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
                      return const Center(child: Text("No styles found."));
                    } else {
                      return SingleChildScrollView(
                        scrollDirection: Axis.horizontal,
                        child: Row(
                          children: snapshot.data!.map((style) {
                            return GestureDetector(
                              onTap: () {
                                _fetchStyleOptions(style.styleId!);
                              },
                              child: Container(
                                padding: const EdgeInsets.all(10),
                                margin: const EdgeInsets.all(5),
                                decoration: BoxDecoration(
                                  color: _selectedStyleId == style.styleId
                                      ? Color(0xFFD2B48C)
                                      : Colors.grey[300],
                                  borderRadius: BorderRadius.circular(8),
                                ),
                                child: Text(
                                  style.styleName ?? "Unnamed",
                                  style: TextStyle(
                                    color: _selectedStyleId == style.styleId
                                        ? Colors.white
                                        : Colors.black,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),
                              ),
                            );
                          }).toList(),
                        ),
                      );
                    }
                  },
                ),
                const SizedBox(height: 10),
                if (_selectedStyleId != null)
                  Column(
                    children: _groupedOptions.entries.map((entry) {
                      String optionType = entry.key;
                      List<StyleOption> options = entry.value;
                      bool isStyleType = optionType == "Style";
                      return ExpansionTile(
                        title: Text(
                          optionType,
                          style: const TextStyle(fontWeight: FontWeight.bold),
                        ),
                        initiallyExpanded: isStyleType,
                        children: options.map((option) {
                          int? selectedOptionId = _selectedOptionIds[optionType];
                          bool isSelected = selectedOptionId == option.styleOptionId;

                          return GestureDetector(
                            onTap: () {
                              setState(() {
                                if (isSelected) {
                                  // Deselect if already selected
                                  _selectedOptionIds[optionType] = null;
                                } else {
                                  // Select only this option for the current optionType
                                  _selectedOptionIds[optionType] = option.styleOptionId;
                                }
                              });
                            },
                            child: Container(
                              color: isSelected ? Colors.brown[200] : Colors.white,
                              child: ListTile(
                                title: Text(option.optionValue ?? "Unknown Option"),
                              ),
                            ),
                          );
                        }).toList(),
                      );
                    }).toList(),
                  ),
              ],
            ),
          ),
          Align(
            alignment: Alignment.bottomCenter,
            child: FutureBuilder<Fabric>(
              future: _fabric,
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
                    color: Color(0xFFD2B48C),
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
          Positioned(
            bottom: 60,
            right: 10,
            child: GestureDetector(
              onTap: _selectedOptionIds.isNotEmpty
                  ? () {
                // Navigate to LiningScreen and pass selected option IDs
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => LiningScreen(
                      fabricId: widget.fabricId,
                      selectedOptionIds: _selectedOptionIds.values.where((id) => id != null).cast<int>().toList(),
                    ),
                  ),
                );
              }
                  : null,
              child: Container(
                padding: const EdgeInsets.all(10),
                decoration: BoxDecoration(
                  color: Color(0xFFD2B48C),
                  shape: BoxShape.circle,
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.3),
                      spreadRadius: 2,
                      blurRadius: 4,
                    ),
                  ],
                ),
                child: Icon(
                  Icons.arrow_forward,
                  color: Colors.white,
                  size: 30,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}





