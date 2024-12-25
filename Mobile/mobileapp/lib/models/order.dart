import 'package:mobileapp/models/product.dart';

import 'orderDetail.dart';

class Order {
  int? orderId;
  int? userID;
  int? storeId;
  int? voucherId;
  int? shipperPartnerId;
  String? orderDate;
  String? shippedDate;
  String? note;
  bool? paid;
  String? status;
  String? guestName;
  String? guestEmail;
  String? guestAddress;
  List<Product>? products;
  double? totalPrice;
  double? deposit;
  double? shippingFee;
  String? deliveryMethod;
  List<OrderDetails>? orderDetails;

  Order(
      {this.orderId,
        this.userID,
        this.storeId,
        this.voucherId,
        this.shipperPartnerId,
        this.orderDate,
        this.shippedDate,
        this.note,
        this.paid,
        this.status,
        this.guestName,
        this.guestEmail,
        this.guestAddress,
        this.products,
        this.totalPrice,
        this.deposit,
        this.shippingFee,
        this.deliveryMethod,
        this.orderDetails});

  Order.fromJson(Map<String, dynamic> json) {
    orderId = json['orderId'];
    userID = json['userID'];
    storeId = json['storeId'];
    voucherId = json['voucherId'];
    shipperPartnerId = json['shipperPartnerId'];
    orderDate = json['orderDate'];
    shippedDate = json['shippedDate'];
    note = json['note'];
    paid = json['paid'];
    status = json['status'];
    guestName = json['guestName'];
    guestEmail = json['guestEmail'];
    guestAddress = json['guestAddress'];
    if (json['products'] != null) {
      products = <Product>[];
      json['products'].forEach((v) {
        products!.add(new Product.fromJson(v));
      });
    }
    totalPrice = json['totalPrice'];
    deposit = json['deposit'];
    shippingFee = json['shippingFee'];
    deliveryMethod = json['deliveryMethod'];
    if (json['orderDetails'] != null) {
      orderDetails = <OrderDetails>[];
      json['orderDetails'].forEach((v) {
        orderDetails!.add(new OrderDetails.fromJson(v));
      });
    }
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['orderId'] = this.orderId;
    data['userID'] = this.userID;
    data['storeId'] = this.storeId;
    data['voucherId'] = this.voucherId;
    data['shipperPartnerId'] = this.shipperPartnerId;
    data['orderDate'] = this.orderDate;
    data['shippedDate'] = this.shippedDate;
    data['note'] = this.note;
    data['paid'] = this.paid;
    data['status'] = this.status;
    data['guestName'] = this.guestName;
    data['guestEmail'] = this.guestEmail;
    data['guestAddress'] = this.guestAddress;
    if (this.products != null) {
      data['products'] = this.products!.map((v) => v.toJson()).toList();
    }
    data['totalPrice'] = this.totalPrice;
    data['deposit'] = this.deposit;
    data['shippingFee'] = this.shippingFee;
    data['deliveryMethod'] = this.deliveryMethod;
    if (this.orderDetails != null) {
      data['orderDetails'] = this.orderDetails!.map((v) => v.toJson()).toList();
    }
    return data;
  }
}