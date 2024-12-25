  class Store {
    int? storeId;
    int? userId;
    String? name;
    String? address;
    String? contactNumber;
    int? storeCode;

    Store(
        {this.storeId,
          this.userId,
          this.name,
          this.address,
          this.contactNumber,
          this.storeCode});

    Store.fromJson(Map<String, dynamic> json) {
      storeId = json['storeId'];
      userId = json['userId'];
      name = json['name'];
      address = json['address'];
      contactNumber = json['contactNumber'];
      storeCode = json['storeCode'];
    }

    Map<String, dynamic> toJson() {
      final Map<String, dynamic> data = new Map<String, dynamic>();
      data['storeId'] = this.storeId;
      data['userId'] = this.userId;
      data['name'] = this.name;
      data['address'] = this.address;
      data['contactNumber'] = this.contactNumber;
      data['storeCode'] = this.storeCode;
      return data;
    }

  }
