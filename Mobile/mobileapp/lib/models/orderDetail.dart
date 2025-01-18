class OrderDetails {
  int? orderId;
  int? productId;
  int? quantity;
  double? price;

  OrderDetails({this.orderId, this.productId, this.quantity, this.price});

  OrderDetails.fromJson(Map<String, dynamic> json) {
    orderId = json['orderId'];
    productId = json['productId'];
    quantity = json['quantity'];
    price = json['price'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['orderId'] = this.orderId;
    data['productId'] = this.productId;
    data['quantity'] = this.quantity;
    data['price'] = this.price;
    return data;
  }
}