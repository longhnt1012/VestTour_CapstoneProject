import 'dart:convert';
import 'package:flutter_paypal_payment/flutter_paypal_payment.dart';
import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/booking/request/api_services.dart';
import 'package:mobileapp/data_source/paypal/api_services.dart';
import 'package:mobileapp/data_source/store/api_services.dart';
import 'package:mobileapp/data_source/voucher/api_services.dart';
import 'package:mobileapp/screens/confirmorder_screen.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:url_launcher/url_launcher_string.dart';
import '../data_source/shipping/district/api_services.dart';
import '../data_source/shipping/provinces/api_services.dart';
import '../data_source/shipping/ward/api_services.dart'; // Add Ward API service
import '../models/shipping.dart';
import '../models/store.dart'; // Add Store model import
// Add Store API service
import 'package:http/http.dart' as http;

import '../models/voucher.dart';

class CheckoutScreen extends StatefulWidget {
  final double? cartTotal;
  CheckoutScreen({Key? key, this.cartTotal}) : super(key: key);
  @override
  _CheckoutScreenState createState() => _CheckoutScreenState();
}

class _CheckoutScreenState extends State<CheckoutScreen> {
  String deliveryOption = 'delivery'; // 'delivery' or 'pickup'
  String paymentOption = 'full'; // 'full' or 'deposit'

  // Input variables
  String? selectedCity;
  String? selectedDistrict;
  String? selectedWard;
  String? addressDetail;
  String? selectedStore;
  String? voucherCode; // Variable for voucher input
  int? userId;
  // Data from API
  List<Provinces> provinces = [];
  List<District> districts = [];
  List<Ward> wards = [];
  List<Store> stores = []; // List to store stores from the API
  bool isLoadingProvinces = true;
  bool isLoadingDistricts = false;
  bool isLoadingWards = false;
  bool isLoadingStores = false; // Add loading state for stores
  List<Voucher> _voucher= [];
  // API service instances
  final apiServiceProvinces = ApiServiceProvinces();
  final apiServiceDistricts = ApiServiceDistricts();
  final apiServiceWards = ApiServiceWard();
  final apiServiceStores = ApiServiceStores(); // Add API service for stores

  double totalProductCost = 0.0; // To hold the total product cost
  double shippingCost =
      0.0; // Shipping cost, which can change based on delivery option
  double discountOnShipping = 0.0; // Discount on shipping (if applicable)
  double totalPayment = 0.0; // Total payment after calculating discount
  @override
  void initState() {
    super.initState();
    fetchProvincesData();
    fetchStoresData(); // Fetch stores when the screen is initialized
    if (widget.cartTotal != null) {
      totalProductCost = widget.cartTotal!;
    }

    calculateTotalPayment();

    _loadUserId();
  }
  // Gọi API để tạo đơn hàng PayPal

  Future<void> _loadUserId() async {
    final _userId = await ApiServicePaypal.getUserIdFromSharedPreferences();
    setState(() {
      userId = _userId; // Assign userId value
    });
  }

  // Hàm mở URL PayPal trong trình duyệt
  Future<void> fetchVoucher() async {
    try {
      // Fetch all available vouchers
      final vouchers = await ApiServiceVoucher.getAllVouchers();
      setState(() {
        setState(() {
          _voucher = vouchers.where((voucher) => voucher.voucherCode != null).toList();
        });
      });
    } catch (e) {
      print("Error fetching voucher: $e");
    }
  }
  void applyVoucher(String code) {
    // Example logic to find the voucher and calculate discount
    final matchedVoucher = _voucher.firstWhere(
          (voucher) => voucher.voucherCode == code,
      orElse: () => Voucher(voucherCode: '', discountNumber: 0),
    );

    if (matchedVoucher != null) {
      setState(() {
        voucherCode = code;
        discountOnShipping = matchedVoucher.discountNumber!; // Apply discount
        calculateTotalPayment(); // Recalculate payment
      });
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Voucher applied successfully!'),
          duration: Duration(seconds: 2),
        ),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Invalid voucher code!'),
          duration: Duration(seconds: 2),
        ),
      );
    }
  }

  Future<void> fetchProvincesData() async {
    try {
      final data = await apiServiceProvinces.fetchProvinces();
      setState(() {
        provinces = data;
        isLoadingProvinces = false;
      });
    } catch (e) {
      setState(() {
        isLoadingProvinces = false;
      });
      print('Error fetching provinces: $e');
    }
  }

  void calculateTotalPayment() {
    // For simplicity, shipping cost is fixed. You can modify this logic
    shippingCost = 50.0; // Example fixed shipping cost
    discountOnShipping = voucherCode != null && voucherCode!.isNotEmpty
        ? 10.0
        : 0.0; // Example discount logic
// Nếu phương thức thanh toán là Deposit, chia tiền hàng đôi
    double productPayment = totalProductCost;
    if (paymentOption == 'deposit') {
      productPayment = totalProductCost / 2; // Chia đôi số tiền hàng
      totalPayment = productPayment + shippingCost - discountOnShipping;
    } else {
      totalPayment = totalProductCost + shippingCost - discountOnShipping;
    }
    // Total payment calculation (product cost + shipping - discount)
  }
  Future<void> confirmOrder() async {
    try {
      String? authToken = await getAuthToken();
      final payload = {

      };
      final response = await http.post(
        Uri.parse('http://157.245.50.125:8080/api/AddCart/confirmorder'),
        headers: {'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json',},
        body: jsonEncode(payload),
      );
      if (response.statusCode == 200) {
        // Xử lý khi API trả về thành công
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Thank you for ordering!!'),
            duration: Duration(seconds: 2),
          ),
        );

        // Điều hướng đến màn hình ConfirmOrderScreen
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => ConfirmOrderScreen()),
        );
      } else {
        // Xử lý lỗi từ API
        print('Error: ${response.body}');
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Confirm Order Failed')),
        );
      }
    } catch (e) {
      // Xử lý lỗi khi gửi yêu cầu
      print('Error: $e');
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error')),
      );
    }
  }
  Future<void> fetchDistrictsData(int provinceId) async {
    setState(() {
      isLoadingDistricts = true;
    });
    try {
      final data = await apiServiceDistricts.fetchDistricts(provinceId);
      setState(() {
        districts = data;
        isLoadingDistricts = false;
      });
    } catch (e) {
      setState(() {
        isLoadingDistricts = false;
      });
      print('Error fetching districts: $e');
    }
  }

  Future<void> fetchWardsData(int districtId) async {
    setState(() {
      isLoadingWards = true;
    });
    try {
      final data = await apiServiceWards.fetchWards(districtId);
      setState(() {
        wards = data;
        isLoadingWards = false;
      });
    } catch (e) {
      setState(() {
        isLoadingWards = false;
      });
      print('Error fetching wards: $e');
    }
  }

  Future<void> fetchStoresData() async {
    setState(() {
      isLoadingStores = true; // Set loading state to true
    });
    try {
      final data =
          await apiServiceStores.getAllStores(); // Fetch stores from API
      setState(() {
        stores = data;
        isLoadingStores = false; // Set loading state to false
      });
    } catch (e) {
      setState(() {
        isLoadingStores = false; // Set loading state to false
      });
      print('Error fetching stores: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Checkout'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
            Text(
              'Delivery Method',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            ListTile(
              title: Text('Delivery'),
              leading: Radio(
                value: 'delivery',
                groupValue: deliveryOption,
                onChanged: (value) {
                  setState(() {
                    deliveryOption = value.toString();
                    selectedStore =
                        null; // Reset cửa hàng khi chuyển về delivery
                    selectedCity =
                        null; // Reset thành phố khi chuyển về delivery
                    selectedDistrict =
                        null; // Reset quận khi chuyển về delivery
                    selectedWard = null; // Reset phường khi chuyển về delivery
                    addressDetail =
                        null; // Reset địa chỉ chi tiết khi chuyển về delivery
                  });
                },
              ),
            ),
            if (deliveryOption == 'delivery') ...[
              isLoadingProvinces
                  ? Center(child: CircularProgressIndicator())
                  : DropdownButtonFormField<String>(
                      decoration: InputDecoration(labelText: 'Choose City'),
                      items: provinces.map((province) {
                        return DropdownMenuItem(
                          value: province.provinceName,
                          child: Text(province.provinceName ?? ''),
                        );
                      }).toList(),
                      onChanged: (value) {
                        setState(() {
                          selectedCity = value;
                          addressDetail = null;
                          selectedDistrict = null; // Reset district
                          selectedWard = null;
                          selectedStore = null; // Reset ward
                        });
                        final selectedProvince = provinces.firstWhere(
                            (province) => province.provinceName == value);
                        fetchDistrictsData(selectedProvince.provinceID!);
                      },
                    ),
              isLoadingDistricts
                  ? Center(child: CircularProgressIndicator())
                  : DropdownButtonFormField<String>(
                decoration: InputDecoration(labelText: 'Choose District'),
                value: selectedDistrict, // Ensure this matches an item in the list
                items: districts.map((district) {
                  return DropdownMenuItem(
                    value: district.districtName, // Use a unique value here
                    child: Text(district.districtName ?? ''),
                  );
                }).toList(),
                onChanged: (value) {
                  setState(() {
                    selectedDistrict = value;
                    selectedWard = null; // Reset ward
                  });
                  if (value != null) {
                    final selectedDistrictObj = districts.firstWhere(
                            (district) => district.districtName == value);
                    fetchWardsData(selectedDistrictObj.districtID!);
                    if (selectedDistrictObj != null) {
                      fetchWardsData(selectedDistrictObj.districtID!);
                    }
                  }
                },
              ),

              isLoadingWards
                  ? Center(child: CircularProgressIndicator())
                  : DropdownButtonFormField<String>(
                      decoration: InputDecoration(labelText: 'Choose Ward'),
                      value: selectedWard,
                      items: wards.map((ward) {
                        return DropdownMenuItem(
                          value: ward.wardName,
                          child: Text(ward.wardName ?? ''),
                        );
                      }).toList(),
                      onChanged: (value) {
                        setState(() {
                          selectedWard = value;
                          selectedStore = null;
                        });

                      },
                    ),
              TextField(
                decoration:
                    InputDecoration(labelText: 'Addres Number'),
                onChanged: (value) {
                  setState(() {
                    addressDetail = value;
                    selectedStore = null;
                  });
                },
              ),
            ],
            ListTile(
              title: Text('Pick Up'),
              leading: Radio(
                value: 'pickup',
                groupValue: deliveryOption,
                onChanged: (value) {
                  setState(() {
                    deliveryOption = value.toString();
                    // Reset các giá trị khi chuyển qua pickup
                    selectedCity = null;
                    selectedDistrict = null;
                    selectedWard = null;
                    addressDetail = null;
                    selectedStore =
                        null; // Reset cửa hàng khi chuyển qua pickup
                  });
                },
              ),
            ),
            if (deliveryOption == 'pickup') ...[
              isLoadingStores
                  ? Center(child: CircularProgressIndicator())
                  : DropdownButtonFormField<String>(
                      decoration: InputDecoration(labelText: 'Choose Store'),
                      value: selectedStore,
                      items: stores.map((store) {
                        return DropdownMenuItem(
                          value: store.name, // Display the store name
                          child: Text(store.name ?? ''),
                        );
                      }).toList(),
                      onChanged: (value) {
                        setState(() {
                          selectedStore = value;
                          selectedCity = null;
                          selectedDistrict = null;
                          selectedWard = null;
                          addressDetail = null;
                        });
                      },
                    ),
            ],
            SizedBox(height: 20),
            Text(
              'Do you need to deposit??',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            ListTile(
              title: Text('Full Banking'),
              leading: Radio(
                value: 'full',
                groupValue: paymentOption,
                onChanged: (value) {
                  setState(() {
                    paymentOption = value.toString();
                  });
                  calculateTotalPayment();
                },
              ),
            ),
            ListTile(
              title: Text('Deposit Order'),
              leading: Radio(
                value: 'deposit',
                groupValue: paymentOption,
                onChanged: (value) {
                  setState(() {
                    paymentOption = value.toString();
                  });
                  calculateTotalPayment();
                },
              ),
            ),
            // Voucher code input
            TextField(
              decoration: InputDecoration(labelText: 'Enter Voucher Code'),
              onChanged: (value) {
                voucherCode = value;
              },
            ),
            ElevatedButton(
              onPressed: () {
                if (voucherCode != null && voucherCode!.isNotEmpty) {
                  applyVoucher(voucherCode!);
                } else {
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(
                      content: Text('Please enter a voucher code!'),
                      duration: Duration(seconds: 2),
                    ),
                  );
                }
              },
              child: Text('Apply Voucher'),
            ),

            SizedBox(height: 20),
            // Displaying order summary
            Text(
              'Total Product: ${totalProductCost.toStringAsFixed(2)} VND',
              style: TextStyle(fontSize: 16),
            ),
            Text(
              'Shipping Fee: ${shippingCost.toStringAsFixed(2)} VND',
              style: TextStyle(fontSize: 16),
            ),
            Text(
              'Discount: ${discountOnShipping.toStringAsFixed(2)} VND',
              style: TextStyle(fontSize: 16),
            ),
            Text(
              'Total Order: ${totalPayment.toStringAsFixed(2)} VND',
              style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
            ),

            // Hiển thị các nút thanh toán (PayPal và Credit Card)
            SizedBox(height: 30),
            Text("Payment Method:",
                style: TextStyle(fontSize: 18)),
            SizedBox(height: 10),

            // Nút PayPal
            // Nút PayPal (Chỉ hiển thị khi có URL)
            // PayPal button
            SizedBox(height: 20),
            ElevatedButton(
              style: ElevatedButton.styleFrom(
                padding: EdgeInsets.symmetric(vertical: 14.0),
                backgroundColor: Colors.blue, // Color của nền nút
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(30), // Cạnh bo tròn
                ),
              ),
              onPressed: () {
                // Mở PayPal hoặc xử lý hành động thanh toán
                Navigator.of(context).push(MaterialPageRoute(
                  builder: (BuildContext context) => PaypalCheckoutView(
                    sandboxMode: true,
                    clientId: "ATdkTnFIC-GlMsObso9tQfAH70vHA_CDapFj7mMaEvntQ28nGsdvZx6HQ235AIOOxOIr6CHDRFKtWPXt",
                    secretKey: "EFnhwjq-AGwtRXwVYwRVXz8HXq6UJBka16AdazBiW3WqjiAZ-HmC5w_mKt5_fs51E-dM2Sa5VSm7kF0n",
                    transactions: const [
                      {
                        "amount": {
                          "total": '70',
                          "currency": "USD",
                          "details": {
                            "subtotal": '70',
                            "shipping": '0',
                            "shipping_discount": 0
                          }
                        },
                        "description": "The payment transaction description.",
                        // "payment_options": {
                        //   "allowed_payment_method":
                        //       "INSTANT_FUNDING_SOURCE"
                        // },
                        "item_list": {
                          "items": [
                            {
                              "name": "Apple",
                              "quantity": 4,
                              "price": '5',
                              "currency": "USD"
                            },
                            {
                              "name": "Pineapple",
                              "quantity": 5,
                              "price": '10',
                              "currency": "USD"
                            }
                          ],

                          // shipping address is not required though
                          //   "shipping_address": {
                          //     "recipient_name": "tharwat",
                          //     "line1": "Alexandria",
                          //     "line2": "",
                          //     "city": "Alexandria",
                          //     "country_code": "EG",
                          //     "postal_code": "21505",
                          //     "phone": "+00000000",
                          //     "state": "Alexandria"
                          //  },
                        }
                      }
                    ],
                    note: "Contact us for any questions on your order.",
                    onSuccess: (Map params) async {
                      print("onSuccess: $params");
                      Navigator.pop(context, params);
                    },
                    onError: (error) {
                      print("onError: $error");
                      Navigator.pop(context);
                    },
                    onCancel: () {
                      print('cancelled:');
                    },
                  ),
                ));
              },
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Image.asset(
                    'assets/images/paypal_logo.png', // Thêm logo PayPal ở đây
                    height: 24, // Chiều cao logo
                  ),
                  SizedBox(width: 10), // Khoảng cách giữa logo và text
                  Text(
                    'Payment by PayPal',
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 18,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ],
              ),
            ),

            SizedBox(height: 20),

          ],
        ),
      ),
    );
  }
}

// Hàm xử lý thanh toán qua Credit Card
void _handleCreditCardPayment() {
  // Thực hiện logic thanh toán qua Credit Card ở đây
  print("Credit Card");
  // Có thể gọi API thanh toán thẻ tín dụng hoặc mở trang nhập thông tin thẻ
}

Future<String?> getAuthToken() async {
  final SharedPreferences prefs = await SharedPreferences.getInstance();
  return prefs.getString('auth_token');
}
