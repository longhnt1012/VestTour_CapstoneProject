import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/measurement/api_services.dart';

import '../models/measurement.dart';

class MeasurementScreen extends StatefulWidget {
  const MeasurementScreen({Key? key}) : super(key: key);

  @override
  State<MeasurementScreen> createState() => _MeasurementScreenState();
}

class _MeasurementScreenState extends State<MeasurementScreen> {
  Measurement? _measurement;
  final _formKey = GlobalKey<FormState>();
  int? userId = 0;
  bool isUpdating = false;

  // Form fields
  final TextEditingController _weightController = TextEditingController();
  final TextEditingController _heightController = TextEditingController();
  final TextEditingController _neckController = TextEditingController();
  final TextEditingController _waistController = TextEditingController();
  final TextEditingController _hipController = TextEditingController();
  final TextEditingController _armholeController = TextEditingController();
  final TextEditingController _chestController = TextEditingController();
  final TextEditingController _shoulderController = TextEditingController();
  final TextEditingController _sleeveLengthController = TextEditingController();
  final TextEditingController _jacketLengthController = TextEditingController();
  final TextEditingController _pantsWaistController = TextEditingController();
  final TextEditingController _crotchController = TextEditingController();
  final TextEditingController _thighController = TextEditingController();
  final TextEditingController _bicepsController = TextEditingController();
  final TextEditingController _pantsLengthController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _fetchMeasurement();
    _loadUserId();
  }

  Future<void> _loadUserId() async {
    final _userId = await ApiService.getUserIdFromSharedPreferences();
    setState(() {
      userId = _userId; // Assign userId value
    });
  }

  Future<void> _fetchMeasurement() async {
    final measurement = await ApiService.getMeasurementForUser();
    setState(() {
      _measurement = measurement;
      if (measurement != null) {
        // Populate the form fields
        _weightController.text = measurement.weight?.toString() ?? '';
        _heightController.text = measurement.height?.toString() ?? '';
        _neckController.text = measurement.neck?.toString() ?? '';
        _waistController.text = measurement.waist?.toString() ?? '';
        _hipController.text = measurement.hip?.toString() ?? '';
        _armholeController.text = measurement.armhole?.toString() ?? '';
        _chestController.text = measurement.chest?.toString() ?? '';
        _shoulderController.text = measurement.shoulder?.toString() ?? '';
        _sleeveLengthController.text = measurement.sleeveLength?.toString() ?? '';
        _jacketLengthController.text = measurement.jacketLength?.toString() ?? '';
        _pantsWaistController.text = measurement.pantsWaist?.toString() ?? '';
        _crotchController.text = measurement.crotch?.toString() ?? '';
        _thighController.text = measurement.thigh?.toString() ?? '';
        _bicepsController.text = measurement.biceps?.toString() ?? '';
        _pantsLengthController.text = measurement.pantsLength?.toString() ?? '';
      }
    });
  }
  Future<void> _submitMeasurement() async {
    if (_formKey.currentState!.validate()) {
      final measurementData = {
        "userId": userId,
        "weight": double.tryParse(_weightController.text),
        "height": double.tryParse(_heightController.text),
        "neck": double.tryParse(_neckController.text),
        "waist": double.tryParse(_waistController.text),
        "hip": double.tryParse(_hipController.text),
        "armhole": double.tryParse(_armholeController.text),
        "chest": double.tryParse(_chestController.text),
        "shoulder": double.tryParse(_shoulderController.text),
        "sleeveLength": double.tryParse(_sleeveLengthController.text),
        "jacketLength": double.tryParse(_jacketLengthController.text),
        "pantsWaist": double.tryParse(_pantsWaistController.text),
        "crotch": double.tryParse(_crotchController.text),
        "thigh": double.tryParse(_thighController.text),
        "biceps": double.tryParse(_bicepsController.text),
        "pantsLength": double.tryParse(_pantsLengthController.text),
        "measurementId" : _measurement!.measurementId!,
      };
      print("POST Data: ${measurementData.toString()}");
      try {
        if (isUpdating) {

          await ApiService.updateMeasurement(_measurement!.measurementId!, measurementData);; // Gọi API cập nhật
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text('Measurement updated successfully!')),
          );
        } else {
          await ApiService.postMeasurement(measurementData); // Gọi API tạo mới
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text('Measurement created successfully!')),
          );
        }
        await _fetchMeasurement(); // Tải lại dữ liệu mới
        setState(() {
          isUpdating = false; // Thoát khỏi chế độ cập nhật
        });
      } catch (error) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Error: $error')),
        );
      }
    }
  }


  Widget _buildMeasurementForm() {
    List<Map<String, dynamic>> fields = [
      {"controller": _weightController, "label": "Weight (kg)"},
      {"controller": _heightController, "label": "Height (cm)"},
      {"controller": _neckController, "label": "Neck (cm)"},
      {"controller": _waistController, "label": "Waist (cm)"},
      {"controller": _hipController, "label": "Hip (cm)"},
      {"controller": _armholeController, "label": "Armhole (cm)"},
      {"controller": _chestController, "label": "Chest (cm)"},
      {"controller": _shoulderController, "label": "Shoulder (cm)"},
      {"controller": _sleeveLengthController, "label": "Sleeve Length (cm)"},
      {"controller": _jacketLengthController, "label": "Jacket Length (cm)"},
      {"controller": _pantsWaistController, "label": "Pants Waist (cm)"},
      {"controller": _crotchController, "label": "Crotch (cm)"},
      {"controller": _thighController, "label": "Thigh (cm)"},
      {"controller": _bicepsController, "label": "Biceps (cm)"},
      {"controller": _pantsLengthController, "label": "Pant Length (cm)"},
    ];

    return Form(
      key: _formKey,
      child: Column(
        children: [
          ...fields.map((field) => TextFormField(
            controller: field["controller"],
            keyboardType: TextInputType.number,
            decoration: InputDecoration(labelText: field["label"]),
            validator: (value) {
              if (value == null || value.isEmpty) {
                return 'Please enter ${field["label"].toLowerCase()}';
              }
              return null;
            },
          )),
          const SizedBox(height: 20),
          ElevatedButton(
            onPressed: _submitMeasurement,
            child: const Text('Save'),
          ),
        ],
      ),
    );
  }
  Widget _buildMeasurementDisplay() {
    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          _buildSection(
            title: "Upper Body",
            items: [
              "Weight: ${_measurement?.weight ?? ''} kg",
              "Height: ${_measurement?.height ?? ''} cm",
              "Neck: ${_measurement?.neck ?? ''} cm",
              "Waist: ${_measurement?.waist ?? ''} cm",
              "Hip: ${_measurement?.hip ?? ''} cm",
              "Armhole: ${_measurement?.armhole ?? ''} cm",
              "Chest: ${_measurement?.chest ?? ''} cm",
              "Shoulder: ${_measurement?.shoulder ?? ''} cm",
            ],
          ),
          const SizedBox(height: 20),
          _buildSection(
            title: "Lower Body",
            items: [
              "Sleeve Length: ${_measurement?.sleeveLength ?? ''} cm",
              "Jacket Length: ${_measurement?.jacketLength ?? ''} cm",
              "Pants Waist: ${_measurement?.pantsWaist ?? ''} cm",
              "Crotch: ${_measurement?.crotch ?? ''} cm",
              "Thigh: ${_measurement?.thigh ?? ''} cm",
              "Biceps: ${_measurement?.biceps ?? ''} cm",
              "Pant Length: ${_measurement?.pantsLength ?? ''} cm",
            ],
          ),
          const SizedBox(height: 20),
          Center(
            child: ElevatedButton(
              onPressed: () {
                setState(() {
                  isUpdating = true;
                  // Chuyển sang chế độ cập nhật // Xóa dữ liệu hiện tại để hiển thị form
                });
              },
              style: ElevatedButton.styleFrom(
                padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 12),
                textStyle: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
              ),
              child: const Text('Edit Measurement'),
            ),
          ),

        ],
      ),
    );
  }

  Widget _buildSection({required String title, required List<String> items}) {
    return Card(
      elevation: 4,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8.0),
      ),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              title,
              style: const TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
                color: Color(0xFFD2B48C),
              ),
            ),
            const Divider(color: Colors.grey),
            ...items.map((item) => Padding(
              padding: const EdgeInsets.symmetric(vertical: 4.0),
              child: Text(
                item,
                style: const TextStyle(fontSize: 16),
              ),
            )),
          ],
        ),
      ),
    );
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Measurement')),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: _measurement == null || isUpdating==true ? _buildMeasurementForm() : _buildMeasurementDisplay(),
        ),
      ),
    );
  }
}
