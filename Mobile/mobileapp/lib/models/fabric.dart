class Fabric {
  int? fabricID;
  String? fabricName;
  double? price;
  String? description;
  String? imageUrl;
  int? tag;

  Fabric(
      {this.fabricID,
        this.fabricName,
        this.price,
        this.description,
        this.imageUrl,
        this.tag});

  Fabric.fromJson(Map<String, dynamic> json) {
    fabricID = json['fabricID'];
    fabricName = json['fabricName'];
    price = json['price'];
    description = json['description'];
    imageUrl = json['imageUrl'];
    tag = json['tag'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['fabricID'] = this.fabricID;
    data['fabricName'] = this.fabricName;
    data['price'] = this.price;
    data['description'] = this.description;
    data['imageUrl'] = this.imageUrl;
    data['tag'] = this.tag;
    return data;
  }
}
