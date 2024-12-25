
class User {
  int? userId;
  String? name;
  String? gender;
  String? address;
  String? dob;
  int? roleId;
  String? email;
  String? password;
  bool? isConfirmed;
  String? status;
  String? phone;
  dynamic refreshToken;
  dynamic refreshTokenExpiryTime;

  User({this.userId, this.name, this.gender, this.address, this.dob, this.roleId, this.email, this.password, this.isConfirmed, this.status, this.phone, this.refreshToken, this.refreshTokenExpiryTime});

  User.fromJson(Map<String, dynamic> json) {
    if(json["userId"] is int) {
      userId = json["userId"];
    }
    if(json["name"] is String) {
      name = json["name"];
    }
    if(json["gender"] is String) {
      gender = json["gender"];
    }
    if(json["address"] is String) {
      address = json["address"];
    }
    if(json["dob"] is String) {
      dob = json["dob"];
    }
    if(json["roleId"] is int) {
      roleId = json["roleId"];
    }
    if(json["email"] is String) {
      email = json["email"];
    }
    if(json["password"] is String) {
      password = json["password"];
    }
    if(json["isConfirmed"] is bool) {
      isConfirmed = json["isConfirmed"];
    }
    if(json["status"] is String) {
      status = json["status"];
    }
    if(json["phone"] is String) {
      phone = json["phone"];
    }
    refreshToken = json["refreshToken"];
    refreshTokenExpiryTime = json["refreshTokenExpiryTime"];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> _data = <String, dynamic>{};
    _data["userId"] = userId;
    _data["name"] = name;
    _data["gender"] = gender;
    _data["address"] = address;
    _data["dob"] = dob;
    _data["roleId"] = roleId;
    _data["email"] = email;
    _data["password"] = password;
    _data["isConfirmed"] = isConfirmed;
    _data["status"] = status;
    _data["phone"] = phone;
    _data["refreshToken"] = refreshToken;
    _data["refreshTokenExpiryTime"] = refreshTokenExpiryTime;
    return _data;
  }
}