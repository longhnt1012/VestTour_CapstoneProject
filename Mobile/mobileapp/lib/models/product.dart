class Product {
  int? productID;
  String? productCode;
  int? measurementID;
  int? categoryID;
  int? fabricID;
  int? liningID;
  String? size;
  bool? isCustom;
  String? imgURL;
  double? price;

  Product(
      {this.productID,
        this.productCode,
        this.measurementID,
        this.categoryID,
        this.fabricID,
        this.liningID,
        this.size,
        this.isCustom,
        this.imgURL,
        this.price});

  Product.fromJson(Map<String, dynamic> json) {
    productID = json['productID'];
    productCode = json['productCode'];
    measurementID = json['measurementID'];
    categoryID = json['categoryID'];
    fabricID = json['fabricID'];
    liningID = json['liningID'];
    size = json['size'];
    isCustom = json['isCustom'];
    imgURL = json['imgURL'];
    price = json['price'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['productID'] = this.productID;
    data['productCode'] = this.productCode;
    data['measurementID'] = this.measurementID;
    data['categoryID'] = this.categoryID;
    data['fabricID'] = this.fabricID;
    data['liningID'] = this.liningID;
    data['size'] = this.size;
    data['isCustom'] = this.isCustom;
    data['imgURL'] = this.imgURL;
    data['price'] = this.price;
    return data;
  }
}
