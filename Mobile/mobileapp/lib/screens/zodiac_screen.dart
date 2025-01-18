import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/user/api_services.dart';


class ZodiacScreen extends StatefulWidget {
  @override
  _ZodiacScreenState createState() => _ZodiacScreenState();
}

class _ZodiacScreenState extends State<ZodiacScreen> {
  final ApiService _apiService = ApiService();

  // Các controller để lưu giá trị được chọn
  String? _selectedMonth;
  String? _selectedDay;
  String? _selectedYear;

  String? _zodiacSign = '';
  List<String> _suggestedColors = [];
  // Danh sách tháng, ngày và năm
  List<String> months = List.generate(12, (index) => (index + 1).toString().padLeft(2, '0'));
  List<String> days = List.generate(31, (index) => (index + 1).toString().padLeft(2, '0'));
  List<String> years = List.generate(2024 - 1900, (index) => (2024 - index).toString());

  // Hàm gọi API để lấy cung hoàng đạo
  Future<void> fetchZodiac(String dateOfBirth) async {
    final result = await _apiService.getZodiac(dateOfBirth);

    setState(() {
      if (result != null) {
        _zodiacSign = result['zodiacSign'] ?? 'Zodiac not found';
        _suggestedColors = List<String>.from(result['suggestedColors'] ?? []);
      } else {
        _zodiacSign = 'Error fetching data';
        _suggestedColors = [];
      }
    });
  }

  // Hàm gộp giá trị tháng, ngày, năm thành định dạng MM-DD-YYYY
  String getFormattedDate() {
    return "${_selectedMonth ?? ''}-${_selectedDay ?? ''}-${_selectedYear ?? ''}";
  }

  // Hàm xác thực nếu không nhập đủ thông tin
  bool validateInputs() {
    if (_selectedMonth == null || _selectedDay == null || _selectedYear == null) {
      return false;
    }
    return true;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Find Your Color by Zodiac"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          //mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                "Zodiac Color",
                style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
              ),
            ),Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: Text(
                "Colors based on your zodiac sign are believed to enhance positive energy and align with your personality traits. According to astrology, certain colors can bring good luck, improve your mood, and increase harmony in your life.",
                style: TextStyle(fontSize: 16),
                textAlign: TextAlign.left,
              ),
            ),
            SizedBox(height: 10),
            // Các ô vuông bo tròn cho tháng, ngày, năm với DropdownButton
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                _buildDateDropdown(
                    value: _selectedDay,
                    hintText: "Day",
                    items: days,
                    onChanged: (value) => setState(() {
                      _selectedDay = value;
                    })),
                SizedBox(width: 15), // Thêm khoảng cách giữa các ô
                _buildDateDropdown(
                    value: _selectedMonth,
                    hintText: "Month",
                    items: months,
                    onChanged: (value) => setState(() {
                      _selectedMonth = value;
                    })),
                SizedBox(width: 15), // Thêm khoảng cách giữa các ô
                _buildDateDropdown(
                    value: _selectedYear,
                    hintText: "Year",
                    items: years,
                    onChanged: (value) => setState(() {
                      _selectedYear = value;
                    })),
              ],
            ),
            SizedBox(height: 20),

            // Nút để gọi hàm fetchZodiac
            ElevatedButton(
              onPressed: () {
                if (!validateInputs()) {
                  // Hiển thị thông báo lỗi nếu chưa nhập đầy đủ
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(content: Text("Please select all fields: month, day, and year")),
                  );
                } else {
                  String dateOfBirth = getFormattedDate();
                  fetchZodiac(dateOfBirth);
                }
              },
              child: Text("Find Zodiac"),
            ),
            SizedBox(height: 20),

            // Hiển thị kết quả cung hoàng đạo
            if (_zodiacSign != null) ...[
              Text(
                "Your Zodiac Sign is: $_zodiacSign",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              SizedBox(height: 10),
              Wrap(
                children: _suggestedColors.map((color) {
                  return Padding(
                    padding: const EdgeInsets.all(4.0),
                    child: Chip(
                      label: Text(color),
                      backgroundColor: _getColorFromString(color),
                      labelStyle: TextStyle(color: Colors.white),
                    ),
                  );
                }).toList(),
              ),
            ],
          ],
        ),
      ),
    );
  }

  // Widget xây dựng ô dropdown cho tháng, ngày, năm
  Widget _buildDateDropdown({
    required String? value,
    required String hintText,
    required List<String> items,
    required Function(String?) onChanged,
  }) {
    return Container(
      width: 110, // Tăng width để các ô rộng hơn
      height: 60, // Tăng height để các ô cao hơn
      padding: EdgeInsets.symmetric(horizontal: 12.0, vertical: 8.0), // Tăng padding
      decoration: BoxDecoration(
        color: Colors.grey[200],
        borderRadius: BorderRadius.circular(12),
      ),
      child: DropdownButton<String>(
        value: value,
        hint: Text(hintText),
        onChanged: onChanged,
        isExpanded: true,
        underline: Container(),
        items: items.map<DropdownMenuItem<String>>((String value) {
          return DropdownMenuItem<String>(
            value: value,
            child: Text(value, textAlign: TextAlign.center),
          );
        }).toList(),
      ),
    );
  }
}
Color _getColorFromString(String colorName) {
  switch (colorName.toLowerCase()) {
    case 'red':
      return Colors.red;
    case 'green':
      return Colors.green;
    case 'yellow':
      return Colors.yellow;
    case 'orange':
      return Colors.orange;
    case 'purple':
      return Colors.purple;
    case 'blue':
      return Colors.blue;
    case 'pink':
      return Colors.pink;
    case 'white':
      return Colors.white;
    case 'black':
      return Colors.black;
    case 'silver':
      return Colors.grey.shade400; // Dùng màu xám sáng cho bạc
    case 'pearl blue':
      return Colors.blue.shade200; // Dùng màu xanh dương nhạt cho ngọc trai
    case 'emerald':
      return Colors.green.shade800; // Dùng màu xanh lá đậm cho ngọc lục bảo
    case 'coral':
      return Colors.orange.shade200; // Dùng màu san hô nhạt
    case 'dark purple':
      return Colors.purple.shade800; // Dùng màu tím đậm
    case 'blue-green':
      return Colors.teal; // Dùng màu xanh lá cây biển (teal)
    default:
      return Colors.grey; // Màu mặc định nếu không khớp
  }
}

