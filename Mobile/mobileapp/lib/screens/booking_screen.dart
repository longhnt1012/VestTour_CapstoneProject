import 'package:flutter/material.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:intl/intl.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../data_source/booking/request/api_services.dart';
import '../models/store.dart';
import 'confirmbooking_screen.dart';

class BookingScreen extends StatefulWidget {
  @override
  _BookingScreenState createState() => _BookingScreenState();
}

class _BookingScreenState extends State<BookingScreen> {
  int userId = 0;
  List<Store> _stores = [];
  Store? _selectedStore;
  DateTime _selectedDate = DateTime.now();
  TimeOfDay _selectedTime = TimeOfDay.now();
  String _service = 'Tailor'; // Default service is 'tailor'

  List<String> _imageList = [
    'assets/images/storehcm.jpg', // Replace with actual image paths
    'assets/images/storehanoi.jpg', // Replace with actual image paths
    'assets/images/storedanang.jpg', // Replace with actual image paths
  ];
  List<String> _services = ['Tailor', 'Exchange', 'Fix']; // List of services

  @override
  void initState() {
    super.initState();
    _fetchStores();
    _loadUserId();
  }

  _loadUserId() async {
    final prefs = await SharedPreferences.getInstance();
    setState(() {
      userId = prefs.getInt('user_id') ?? 0; // Load userId from SharedPreferences
    });
  }

  Future<void> _fetchStores() async {
    List<Store> stores = await ApiServices.getAllStores();
    setState(() {
      _stores = stores.where((store) => store.name != null && store.status =="Active").toList();
    });
  }

  void _onBookAppointment() async {
    if (_selectedStore != null &&  _selectedStore!.storeId !=null && userId != 0) { // Ensure user is authenticated
      bool success = await ApiServices.postBooking(
        storeId: _selectedStore!.storeId!,
        date: _selectedDate,
        time: _selectedTime,
        service: _service,
        userId: userId,
        status: 'on-going',
      );
      if (success) {

            Navigator.push(
                context,
                MaterialPageRoute(
                builder: (context) => BookingConfirmationScreen(
            storeId: _selectedStore!.storeId!,
            storeName: _selectedStore!.name!,
                  storeAddress: _selectedStore!.address!,
            bookingDate: _selectedDate,
            bookingTime: _selectedTime,
            service: _service, ),
                ),
            );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text('Booking failed!')));
      }
    } else if (_selectedStore == null) {
      ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Please select a store!')));
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('User not authenticated!')));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Book an Appointment', style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold)),
        backgroundColor: Color(0xFFD2B48C),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // Image Slider
              SizedBox(
                height: 200,
                child: CarouselSlider(
                  options: CarouselOptions(
                    autoPlay: true,
                    enlargeCenterPage: true,
                    aspectRatio: 16 / 9,
                    viewportFraction: 0.9,
                  ),
                  items: _imageList.map((imagePath) {
                    return Builder(
                      builder: (BuildContext context) {
                        return Container(
                          decoration: BoxDecoration(
                            image: DecorationImage(
                              image: AssetImage(imagePath),
                              fit: BoxFit.cover,
                            ),
                            borderRadius: BorderRadius.circular(8),
                          ),
                        );
                      },
                    );
                  }).toList(),
                ),
              ),
              SizedBox(height: 20),

              // Store selection section
              Text(
                'Choose a Store',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: Color(0xFFD2B48C)),
              ),
              SizedBox(height: 8),
              _stores.isEmpty
                  ? CircularProgressIndicator()
                  : DropdownButton<Store>(
                value: _selectedStore,
                hint: Text('Select Store'),
                onChanged: (Store? store) {
                  setState(() {
                    _selectedStore = store;
                  });
                },
                items: _stores.map((store) {
                  return DropdownMenuItem<Store>(
                    value: store,
                    child: Text(store.name!),
                  );
                }).toList(),
              ),
              SizedBox(height: 16),

              // Service selection section
              Text(
                'Select Service',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: Color(0xFFD2B48C)),
              ),
              SizedBox(height: 8),
              DropdownButton<String>(
                value: _service,
                onChanged: (String? newService) {
                  setState(() {
                    _service = newService!;
                  });
                },
                items: _services.map((service) {
                  return DropdownMenuItem<String>(
                    value: service,
                    child: Text(service),
                  );
                }).toList(),
              ),
              SizedBox(height: 16),

              // Date selection section
              Text(
                'Select Date',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: Color(0xFFD2B48C)),
              ),
              SizedBox(height: 8),
              ElevatedButton(
                onPressed: () async {
                  DateTime? pickedDate = await showDatePicker(
                    context: context,
                    initialDate: _selectedDate,
                    firstDate: DateTime(2020),
                    lastDate: DateTime(2101),
                  );
                  if (pickedDate != null) {
                    setState(() {
                      _selectedDate = pickedDate;
                    });
                  }
                },
                child: Text('Pick Date', style: TextStyle(fontSize: 16)),
              ),
              SizedBox(height: 8),
              Text(
                'Selected Date: ${DateFormat('dd/MM/yyyy').format(_selectedDate)}',
                style: TextStyle(fontSize: 16, color: Colors.teal),
              ),
              SizedBox(height: 16),

              // Time selection section
              Text(
                'Select Time',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: Color(0xFFD2B48C)),
              ),
              SizedBox(height: 8),
              ElevatedButton(
                onPressed: () async {
                  TimeOfDay? pickedTime = await showTimePicker(
                    context: context,
                    initialTime: _selectedTime,
                  );
                  if (pickedTime != null) {
                    setState(() {
                      _selectedTime = pickedTime;
                    });
                  }
                },
                child: Text('Pick Time', style: TextStyle(fontSize: 16)),
              ),
              SizedBox(height: 8),
              Text(
                'Selected Time: ${_selectedTime.format(context)}',
                style: TextStyle(fontSize: 16, color: Colors.teal),
              ),
              SizedBox(height: 16),

              // Book Appointment button
              Center(
                child: ElevatedButton(
                  onPressed: _onBookAppointment,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Color(0xFFD2B48C), // Button color
                    padding: EdgeInsets.symmetric(vertical: 15, horizontal: 30),
                  ),
                  child: Text('Book Appointment', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

