class Provinces {
  int? provinceID;
  String? provinceName;
  int? countryID;
  String? code;
  List<String>? nameExtension;
  bool? isEnable;
  int? regionID;
  String? updatedAt;

  Provinces(
      {this.provinceID,
        this.provinceName,
        this.countryID,
        this.code,
        this.nameExtension,
        this.isEnable,
        this.regionID,
        this.updatedAt});

  Provinces.fromJson(Map<String, dynamic> json) {
    provinceID = json['provinceID'];
    provinceName = json['provinceName'];
    countryID = json['countryID'];
    code = json['code'];
    nameExtension = json['nameExtension'].cast<String>();
    isEnable = json['isEnable'];
    regionID = json['regionID'];
    updatedAt = json['updatedAt'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['provinceID'] = this.provinceID;
    data['provinceName'] = this.provinceName;
    data['countryID'] = this.countryID;
    data['code'] = this.code;
    data['nameExtension'] = this.nameExtension;
    data['isEnable'] = this.isEnable;
    data['regionID'] = this.regionID;
    data['updatedAt'] = this.updatedAt;
    return data;
  }
}

class District {
  int? districtID;
  int? provinceID;
  String? districtName;
  String? code;
  int? type;
  int? supportType;
  List<String>? nameExtension;

  District(
      {this.districtID,
        this.provinceID,
        this.districtName,
        this.code,
        this.type,
        this.supportType,
        this.nameExtension});

  District.fromJson(Map<String, dynamic> json) {
    districtID = json['districtID'];
    provinceID = json['provinceID'];
    districtName = json['districtName'];
    code = json['code'];
    type = json['type'];
    supportType = json['supportType'];
    nameExtension = json['nameExtension'].cast<String>();
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['districtID'] = this.districtID;
    data['provinceID'] = this.provinceID;
    data['districtName'] = this.districtName;
    data['code'] = this.code;
    data['type'] = this.type;
    data['supportType'] = this.supportType;
    data['nameExtension'] = this.nameExtension;
    return data;
  }
}

class Ward {
  String? wardCode;
  int? districtID;
  String? wardName;
  List<String>? nameExtension;
  int? isEnable;
  bool? canUpdateCOD;
  int? supportType;
  int? pickType;
  int? deliverType;

  Ward(
      {this.wardCode,
        this.districtID,
        this.wardName,
        this.nameExtension,
        this.isEnable,
        this.canUpdateCOD,
        this.supportType,
        this.pickType,
        this.deliverType});

  Ward.fromJson(Map<String, dynamic> json) {
    wardCode = json['wardCode'];
    districtID = json['districtID'];
    wardName = json['wardName'];
    nameExtension = json['nameExtension'].cast<String>();
    isEnable = json['isEnable'];
    canUpdateCOD = json['canUpdateCOD'];
    supportType = json['supportType'];
    pickType = json['pickType'];
    deliverType = json['deliverType'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['wardCode'] = this.wardCode;
    data['districtID'] = this.districtID;
    data['wardName'] = this.wardName;
    data['nameExtension'] = this.nameExtension;
    data['isEnable'] = this.isEnable;
    data['canUpdateCOD'] = this.canUpdateCOD;
    data['supportType'] = this.supportType;
    data['pickType'] = this.pickType;
    data['deliverType'] = this.deliverType;
    return data;
  }
}
class Shipping {
  int? shopId;
  int? fromDistrict;
  int? toDistrict;

  Shipping({this.shopId, this.fromDistrict, this.toDistrict});

  Shipping.fromJson(Map<String, dynamic> json) {
    shopId = json['shopId'];
    fromDistrict = json['fromDistrict'];
    toDistrict = json['toDistrict'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['shopId'] = this.shopId;
    data['fromDistrict'] = this.fromDistrict;
    data['toDistrict'] = this.toDistrict;
    return data;
  }
}
class ShippingFee {
  int? serviceId;
  int? insuranceValue;
  String? coupon;
  String? toWardCode;
  int? toDistrictId;
  int? fromDistrictId;
  int? weight;
  int? length;
  int? width;
  int? height;
  int? shopCode;

  ShippingFee(
      {this.serviceId,
        this.insuranceValue,
        this.coupon,
        this.toWardCode,
        this.toDistrictId,
        this.fromDistrictId,
        this.weight,
        this.length,
        this.width,
        this.height,
        this.shopCode});

  ShippingFee.fromJson(Map<String, dynamic> json) {
    serviceId = json['serviceId'];
    insuranceValue = json['insuranceValue'];
    coupon = json['coupon'];
    toWardCode = json['toWardCode'];
    toDistrictId = json['toDistrictId'];
    fromDistrictId = json['fromDistrictId'];
    weight = json['weight'];
    length = json['length'];
    width = json['width'];
    height = json['height'];
    shopCode = json['shopCode'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['serviceId'] = this.serviceId;
    data['insuranceValue'] = this.insuranceValue;
    data['coupon'] = this.coupon;
    data['toWardCode'] = this.toWardCode;
    data['toDistrictId'] = this.toDistrictId;
    data['fromDistrictId'] = this.fromDistrictId;
    data['weight'] = this.weight;
    data['length'] = this.length;
    data['width'] = this.width;
    data['height'] = this.height;
    data['shopCode'] = this.shopCode;
    return data;
  }
}
