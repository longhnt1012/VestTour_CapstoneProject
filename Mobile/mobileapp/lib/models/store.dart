class Store {
  int? storeId;
  int? userId;
  String? name;
  String? address;
  String? contactNumber;
  int? storeCode;
  String? openTime;
  String? closeTime;
  String? staffIDs;
  int? districtID;
  String? imgUrl;
  String? status;

  Store(
      {this.storeId,
        this.userId,
        this.name,
        this.address,
        this.contactNumber,
        this.storeCode,
        this.openTime,
        this.closeTime,
        this.staffIDs,
        this.districtID,
        this.imgUrl,
        this.status,
        });

  Store.fromJson(Map<String, dynamic> json) {
    storeId = json['storeId'];
    userId = json['userId'];
    name = json['name'];
    address = json['address'];
    contactNumber = json['contactNumber'];
    storeCode = json['storeCode'];
    openTime = json['openTime'];
    closeTime = json['closeTime'];
    staffIDs = json['staffIDs'];
    districtID = json['districtID'];
    imgUrl = json['imgUrl'];
    status = json['status'];

  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['storeId'] = this.storeId;
    data['userId'] = this.userId;
    data['name'] = this.name;
    data['address'] = this.address;
    data['contactNumber'] = this.contactNumber;
    data['storeCode'] = this.storeCode;
    data['openTime'] = this.openTime;
    data['closeTime'] = this.closeTime;
    data['staffIDs'] = this.staffIDs;
    data['districtID'] = this.districtID;
    data['imgUrl'] = this.imgUrl;
    data['status'] = this.status;
    return data;
  }
}