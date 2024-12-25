class ProductInStore {
  int? storeID;
  int? productID;
  int? quantity;

  ProductInStore({this.storeID, this.productID, this.quantity});

  ProductInStore.fromJson(Map<String, dynamic> json) {
    storeID = json['storeID'];
    productID = json['productID'];
    quantity = json['quantity'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['storeID'] = this.storeID;
    data['productID'] = this.productID;
    data['quantity'] = this.quantity;
    return data;
  }
}
