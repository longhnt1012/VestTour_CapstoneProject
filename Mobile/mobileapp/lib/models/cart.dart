import 'package:mobileapp/models/product.dart';

class Cart {
  int? userId;
  bool? isCustom;
  int? productId;
  CustomProduct? customProduct;
  Product? product;  // Added field for regular products
  int? cartTotal;    // Added cartTotal field

  Cart({this.userId, this.isCustom, this.productId, this.customProduct, this.product, this.cartTotal});

  Cart.fromJson(Map<String, dynamic> json) {
    userId = json['userId'];
    isCustom = json['isCustom'];
    productId = json['productId'];
    customProduct = json['customProduct'] != null
        ? new CustomProduct.fromJson(json['customProduct'])
        : null;
    product = json['product'] != null
        ? new Product.fromJson(json['product'])
        : null; // Handling the regular product
    cartTotal = json['cartTotal'];  // Handle cartTotal field
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['userId'] = this.userId;
    data['isCustom'] = this.isCustom;
    data['productId'] = this.productId;
    if (this.customProduct != null) {
      data['customProduct'] = this.customProduct!.toJson();
    }
    if (this.product != null) {
      data['product'] = this.product!.toJson(); // Add regular product to JSON
    }
    data['cartTotal'] = this.cartTotal;  // Handle cartTotal field
    return data;
  }
}

class CustomProduct {
  String? productCode;
  int? categoryID;
  int? fabricID;
  int? liningID;
  int? measurementID;
  List<PickedStyleOptions>? pickedStyleOptions;

  CustomProduct({this.productCode, this.categoryID, this.fabricID, this.liningID, this.measurementID, this.pickedStyleOptions});

  CustomProduct.fromJson(Map<String, dynamic> json) {
    productCode = json['productCode'];
    categoryID = json['categoryID'];
    fabricID = json['fabricID'];
    liningID = json['liningID'];
    measurementID = json['measurementID'];
    if (json['pickedStyleOptions'] != null) {
      pickedStyleOptions = <PickedStyleOptions>[];
      json['pickedStyleOptions'].forEach((v) {
        pickedStyleOptions!.add(new PickedStyleOptions.fromJson(v));
      });
    }
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['productCode'] = this.productCode;
    data['categoryID'] = this.categoryID;
    data['fabricID'] = this.fabricID;
    data['liningID'] = this.liningID;
    data['measurementID'] = this.measurementID;
    if (this.pickedStyleOptions != null) {
      data['pickedStyleOptions'] = this.pickedStyleOptions!.map((v) => v.toJson()).toList();
    }
    return data;
  }
}

class PickedStyleOptions {
  int? styleOptionID;

  PickedStyleOptions({this.styleOptionID});

  PickedStyleOptions.fromJson(Map<String, dynamic> json) {
    styleOptionID = json['styleOptionID'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['styleOptionID'] = this.styleOptionID;
    return data;
  }
}