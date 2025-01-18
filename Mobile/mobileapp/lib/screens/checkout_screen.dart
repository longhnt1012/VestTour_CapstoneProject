import 'dart:convert';
import 'package:flutter_paypal_payment/flutter_paypal_payment.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'package:mobileapp/data_source/cart/api_services.dart';
import 'package:mobileapp/data_source/payment/api_services.dart';
import 'package:mobileapp/data_source/shipping/api_services.dart';
import 'package:mobileapp/data_source/store/api_services.dart';
import 'package:mobileapp/data_source/user/api_services.dart';
import 'package:mobileapp/data_source/voucher/api_services.dart';
import 'package:mobileapp/screens/confirmorder_screen.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../data_source/shipping/district/api_services.dart';
import '../data_source/shipping/provinces/api_services.dart';
import '../data_source/shipping/ward/api_services.dart'; // Add Ward API service
import '../models/cart.dart';
import '../models/payment.dart';
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
  final _formKey = GlobalKey<FormState>();
  String deliveryOption = 'delivery'; // 'delivery' or 'pickup'
  String paymentOption = 'full'; // 'full' or 'deposit'
  // You can update this based on the user's choice
  String? userName;
  String? userEmail;
  String? userPhone;

  // Input variables
  String? selectedCity;
  String? selectedDistrict;
  String? selectedWard;
  String? addressDetail;
  String? selectedStore;
  String? voucherCode; // Variable for voucher input
  int? userId;
  int? shopCode;
  int? selectedDistrictId;
  String? selectedWardCode;
  double? discountNumer = 0.0;
  int? storeId;
  // Data from API
  List<Provinces> provinces = [];
  List<District> districts = [];
  List<Ward> wards = [];
  int? orderID;
  List<Store> stores = []; // List to store stores from the API
  bool isLoadingProvinces = true;
  bool isLoadingDistricts = false;
  bool isLoadingWards = false;
  bool isLoadingStores = false;
  bool isLoadingFee = false;
  // Add loading state for stores
  List<Voucher> _voucher = [];
  // API service instances
  final apiServiceProvinces = ApiServiceProvinces();
  final apiServiceDistricts = ApiServiceDistricts();
  final apiServiceWards = ApiServiceWard();
  final apiServiceStores = ApiServiceStores(); // Add API service for stores
  Voucher? _selectedVoucher;
  double totalProductCost = 0.0;
  int shippingFeeTotal = 0; // To hold the total product cost
  double shippingCost =
      0.0; // Shipping cost, which can change based on delivery option
  double discountOnShipping = 0.0; // Discount on shipping (if applicable)
  double totalPayment = 0.0; // Total payment after calculating discount
  @override
  void initState() {
    super.initState();
    fetchProvincesData();
    fetchVoucher();
    fetchStoresData(); // Fetch stores when the screen is initialized
    if (widget.cartTotal != null) {
      totalProductCost = widget.cartTotal!;
    }

    calculateTotalPayment();

    _loadUser();
  }
  // Gọi API để tạo đơn hàng PayPal

  _loadUser() async {
    final user = await ApiService.getUserById();
    if (user != null) {
      setState(() {
        userId = user.userId ?? 0;
        userName = user.name ?? 'No name available';
        userEmail = user.email ?? 'No Email available';
        userPhone = user.phone ??' Enter Phone number in your profile';
      });
    }
  }bool isDeliveryInfoComplete() {
    if (deliveryOption == 'delivery') {
      return selectedCity != null &&
          selectedDistrict != null &&
          selectedWard != null &&
          addressDetail != null && addressDetail!.isNotEmpty &&
          selectedStore != null;
    } else if (deliveryOption == 'pick up') {
      return selectedStore != null;
    }
    return false; // Trả về false nếu không chọn delivery hoặc pick up
  }
  Future<List<Map<String, dynamic>>> fetchCartList() async {
    try {
      String? authToken = await getAuthToken(); // Get the token
      final url = Uri.parse("http://165.22.243.162:8080/api/AddCart/mycart");

      final response = await http.get(
        url,
        headers: {
          'Authorization': 'Bearer $authToken',
          'Content-Type': 'application/json',
        },
      );

      print('Response body: ${response.body}');

      if (response.statusCode == 200) {
        // Parse the JSON response
        final cartData = json.decode(response.body);
        List<dynamic> cartItems =
            cartData['cartItems']; // Get the list of items

        // Map cart items to extract productCode, price, and quantity
        List<Map<String, dynamic>> itemList = cartItems.map((item) {
          String? productCode;
          if (item['product'] != null) {
            // If the item has a product, use productCode from product
            productCode = item['product']['productCode'];
          } else if (item['customProduct'] != null) {
            // If the item has a customProduct, use productCode from customProduct
            productCode = item['customProduct']['productCode'];
          }

          return {
            'productCode': productCode,
            'quantity': item['quantity'],
            'price': item['price'],
          };
        }).toList();

        return itemList; // Return the processed list
      } else {
        throw Exception(
            'Failed to load cart. Status code: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Error fetching cart: $e');
    }
  }

  // Hàm mở URL PayPal trong trình duyệt
  Future<void> fetchVoucher() async {
    try {
      // Fetch all available vouchers
      final vouchers = await ApiServiceVoucher.getAllVouchers();
      setState(() {
        setState(() {
          _voucher =
              vouchers.where((voucher) => voucher.voucherCode != null).toList();
        });
      });
    } catch (e) {
      print("Error fetching voucher: $e");
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

  Future<void> calculateShippingFee() async {
    if (selectedDistrictId == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Please select a district.')),
      );
      return;
    }

    setState(() {
      isLoadingFee = true;
    });

    try {
      final result = await ApiServiceShipping.calculateFee(
        serviceId: 0, // Example data
        insuranceValue: 0,
        coupon: "",
        toWardCode: "$selectedWardCode",
        toDistrictId: selectedDistrictId!,
        fromDistrictId: 203,
        weight: 0,
        length: 0,
        width: 0,
        height: 0,
        shopCode: 3253720,
      );

      setState(() {
        shippingFeeTotal =
            result['total'] ?? 0; // Update shipping fee from response
      });
      calculateTotalPayment();
    } catch (e) {
      setState(() {
        shippingFeeTotal = 35000; // Default shipping fee in case of error
      });
    } finally {
      setState(() {
        isLoadingFee = false;
      });
    }
  }

  void calculateTotalPayment() {
    // Để đơn giản, phí vận chuyển là cố định. Bạn có thể thay đổi logic này.
    shippingCost = shippingFeeTotal / 25000; // Ví dụ phí vận chuyển cố định
    discountOnShipping = discountNumer != null && shippingCost != null
        ? discountOnShipping = discountNumer! * shippingCost
        : 0.0; // Logic giảm giá nếu có mã giảm giá

    bool isDeposit =
        paymentOption == 'deposit'; // Kiểm tra phương thức thanh toán

    double productPayment = totalProductCost;

    // Nếu phương thức thanh toán là 'deposit', chia đôi số tiền sản phẩm
    if (isDeposit) {
      productPayment =
          totalProductCost / 2; // Chia đôi số tiền sản phẩm cho khoản đặt cọc
    }

    // Tính toán tổng tiền thanh toán
    totalPayment = productPayment + shippingCost - discountOnShipping;

    // Gán payAmount bằng totalPayment (dùng cho quá trình thanh toán)
  }
  Future<void> createPayment() async {
    String payDetail;
    double deppositFee = 0;
    bool isDeposit = paymentOption == 'deposit';
    if (isDeposit) {
      deppositFee = totalPayment;
      payDetail="Pay deposit";
    }else {
      deppositFee= totalPayment;
      payDetail="Pay Full Order";
    }
    // Create a new Payment object
    Payment payment = Payment(
      paymentId: 0,
      orderId: orderID, // Replace with your actual order ID
      userId: userId,  // Replace with the actual user ID
      method: 'PayPal', // Payment method (e.g., 'PayPal', 'Credit Card', etc.)
      paymentDate: DateFormat('yyyy-MM-dd').format(DateTime.now()), // Current date and time
      paymentDetails: payDetail, // Payment details (can be customized)
      status: 'Success', // Payment status (e.g., 'Pending', 'Completed')
      amount:double.parse(deppositFee.toStringAsFixed(2)), // Total amount to be paid
    );

    // Call the postPayment function to send payment data to the server
    bool success = await ApiServicePayment.postPayment(payment);
 
    if (success) {
      // Handle successful payment
      print('Payment was successful');
    } else {
      // Handle failed payment
      print('Payment failed');
    }
  }
  Future<void> handleConfirmOrder() async {
    double deppositFee = 0;
    bool isDeposit = paymentOption == 'deposit';
    deppositFee = totalPayment;

    if (deliveryOption == 'delivery') {
      deliveryOption = "Delivery";
    } else {
      deliveryOption = "Pick up";
    }

    String address = "$addressDetail,$selectedWard,$selectedDistrict,$selectedCity";

    // Call the confirmOrder API
    Map<String, dynamic> result = await ApiServicesCart.confirmOrder(
      userName: userName,
      userEmail: userEmail,
      userPhone: userPhone,
      address: address,
      depositFee: deppositFee, // Deposit fee
      shippingFee: shippingCost, // Shipping fee
      deliveryMethod: deliveryOption, // Delivery or Pickup
      storeId: 5, // Store ID
    );

    if (result['success']) {
       orderID = result['orderId'];
      if (orderID != null) {
        print("Order confirmed with order ID: $orderID");
        // Handle order confirmation success (e.g., navigate to another screen)
      } else {
        print("Order confirmed but no order ID received.");
      }
    } else {
      print("Order confirmation failed.");
      // Handle failure (e.g., show error message)
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
              'User Information',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            // Customer Name
            Row(
              children: [
                Text(
                  'Customer Name: ',
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                ),
                Text(
                  '$userName',
                  style: TextStyle(fontSize: 16),
                ),
              ],
            ),
            SizedBox(height: 8), // Space between lines

            // Email
            Row(
              children: [
                Text(
                  'Email: ',
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                ),
                Text(
                  '$userEmail',
                  style: TextStyle(fontSize: 16),
                ),
              ],
            ),
            SizedBox(height: 8), // Space between lines

            // Phone
            Row(
              children: [
                Text(
                  'Phone: ',
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                ),
                userPhone != null
                    ? Text(
                  '$userPhone',
                  style: TextStyle(fontSize: 16),
                )
                    : Expanded(
                  child: TextField(
                    keyboardType: TextInputType.phone,
                    decoration: InputDecoration(
                      hintText: 'Enter your phone number',
                      border: OutlineInputBorder(),
                    ),
                    onChanged: (value) {
                      setState(() {
                        userPhone = value; // Update phone number
                      });
                    },
                  ),
                ),
              ],
            ), SizedBox(height: 15),
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
                      value:
                          selectedDistrict, // Ensure this matches an item in the list
                      items: districts.map((district) {
                        return DropdownMenuItem(
                          value:
                              district.districtName, // Use a unique value here
                          child: Text(district.districtName ?? ''),
                        );
                      }).toList(),
                      onChanged: (value) {
                        setState(() {
                          selectedDistrict = value;
                          selectedWard = null; // Reset ward
                          selectedDistrictId = districts
                              .firstWhere(
                                  (district) => district.districtName == value)
                              .districtID;
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
                          selectedWardCode = wards
                              .firstWhere((ward) => ward.wardName == value)
                              .wardCode;
                        });

                      },
                    ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Address Number'),
                onChanged: (value) {
                  setState(() {
                    addressDetail = value;
                    selectedStore = null;
                  });
                  calculateShippingFee();
                  calculateTotalPayment();
                },
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return 'Address Number is required'; // Custom validation message
                  }
                  return null; // Return null if the value is valid
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
                  shippingFeeTotal=0;
                  calculateTotalPayment();
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
                    // Reset các giá trị liên quan đến địa chỉ khi thay đổi cửa hàng
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
            Text(
              "Available Vouchers",
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 10),
            DropdownButton<Voucher>(
              isExpanded: true,
              hint: Text("Select a Voucher"),
              value: _selectedVoucher,
              items: _voucher.map((voucher) {
                return DropdownMenuItem<Voucher>(
                  value: voucher,
                  child: Text(voucher.voucherCode ?? "No Code"),
                );
              }).toList(),
              onChanged: (newVoucher) {
                setState(() {
                  _selectedVoucher = newVoucher;
                  //discountNumer= _voucher.firstWhere((voucher) => voucher.voucherCode== newVoucher ).discountNumber;
                  discountNumer = _selectedVoucher!.discountNumber;
                  calculateTotalPayment();
                });
              },
            ),

            SizedBox(height: 20),
            // Displaying order summary
            Text(
              'Total Product: ${totalProductCost.toStringAsFixed(2)} USD',
              style: TextStyle(fontSize: 16),
            ),
            Text(
              'Shipping Fee: ${shippingCost.toStringAsFixed(2)} USD',
              style: TextStyle(fontSize: 16),
            ),
            Text(
              'Discount: ${discountOnShipping.toStringAsFixed(2)} USD',
              style: TextStyle(fontSize: 16),
            ),
            Text(
              'Total Order: ${totalPayment.toStringAsFixed(2)} USD',
              style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
            ),

            // Hiển thị các nút thanh toán (PayPal và Credit Card)
            SizedBox(height: 30),
            Text("Payment Method:", style: TextStyle(fontSize: 18)),
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
              onPressed: () async {
                if (deliveryOption == 'delivery' && (selectedCity == null || selectedDistrict == null || selectedWard == null || addressDetail == null)) {
                  // If delivery option is selected, ensure all address details are provided
                  ScaffoldMessenger.of(context).showSnackBar(
                      SnackBar(content: Text("Please complete your delivery address!"))
                  );
                  return; // Prevent further action
                }

                if (deliveryOption == 'pickup' && selectedStore == null) {
                  // If pickup option is selected, ensure a store is selected
                  ScaffoldMessenger.of(context).showSnackBar(
                      SnackBar(content: Text("Please select a store for pickup!"))
                  );
                  return; // Prevent further action
                }
                try {
                  // Lấy danh sách sản phẩm từ giỏ hàng
                  List<Map<String, dynamic>> itemList = await fetchCartList();  // Hàm fetchCartList()

                  // Tạo danh sách các sản phẩm theo định dạng mà PayPal yêu cầu
                  List<Map<String, dynamic>> paypalItems = itemList.map((item) {
                    try {
                      return {
                        "name": item['productCode'] ?? item['customProduct']['productCode'], // Dùng productCode của sản phẩm hoặc customProduct
                        "quantity": item['quantity'],
                        "price": item['price'].toString(),
                        "currency": "USD", // Hoặc đơn vị tiền tệ bạn cần
                      };
                    } catch (e) {
                      print("Error processing item: $item. Error: $e");
                      rethrow; // Đưa lỗi lên để tiếp tục xử lý ngoài try-catch
                    }
                  }).toList();

                  // Kiểm tra nếu danh sách sản phẩm không trống trước khi tiếp tục
                  if (paypalItems.isEmpty) {
                    print("No items in cart to proceed with PayPal payment.");
                    return; // Dừng lại nếu không có sản phẩm
                  }
                  print("=== Logging PayPal Payment Details ===");
                  print("Total Payment: ${totalPayment.toStringAsFixed(2)}");
                  print("Total Product Cost: ${totalProductCost.toStringAsFixed(2)}");
                  print("Shipping Cost: ${shippingCost.toStringAsFixed(2)}");
                  print("Discount on Shipping: ${discountOnShipping.toStringAsFixed(2)}");
                  print("PayPal Items: $paypalItems");

                  // Mở PayPal hoặc xử lý hành động thanh toán
                  Navigator.of(context).push(MaterialPageRoute(
                    builder: (BuildContext context) => PaypalCheckoutView(
                      sandboxMode: true,
                      clientId:
                      "ATdkTnFIC-GlMsObso9tQfAH70vHA_CDapFj7mMaEvntQ28nGsdvZx6HQ235AIOOxOIr6CHDRFKtWPXt",
                      secretKey:
                      "EFnhwjq-AGwtRXwVYwRVXz8HXq6UJBka16AdazBiW3WqjiAZ-HmC5w_mKt5_fs51E-dM2Sa5VSm7kF0n",
                      transactions: [
                        {
                          "amount": {
                            "total":  totalPayment.toStringAsFixed(2),
                            "currency": "USD",
                            "details": {
                              "subtotal": totalProductCost.toStringAsFixed(2),
                              "shipping": shippingCost.toStringAsFixed(2),
                                "shipping_discount": discountOnShipping.toStringAsFixed(2)
                            }
                          },
                          "description": "The payment transaction description.",
                          "item_list": {
                            "items": paypalItems, // Truyền đúng danh sách sản phẩm vào đây
                          }
                        }
                      ],
                      note: "Contact us for any questions on your order.",
                      onSuccess: (Map params) async {
                        print("onSuccess: $params");
                        await handleConfirmOrder();
                        await createPayment();

                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => ConfirmOrderScreen(),
                          ),
                        );
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
                } catch (e) {
                  print("Error creating paypalItems: $e");
                  // Bạn có thể thêm xử lý lỗi ở đây như thông báo cho người dùng nếu cần
                }
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

