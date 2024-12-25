
class Booking {
  int? bookingId;
  dynamic userId;
  String? bookingDate;
  String? time;
  String? note;
  String? status;
  int? storeId;
  String? guestName;
  String? guestEmail;
  String? guestPhone;

  String? service;

  Booking({this.bookingId, this.userId, this.bookingDate, this.time, this.note, this.status, this.storeId, this.guestName, this.guestEmail, this.guestPhone,  this.service});

  Booking.fromJson(Map<String, dynamic> json) {
    if(json["bookingId"] is int) {
      bookingId = json["bookingId"];
    }
    userId = json["userId"];
    if(json["bookingDate"] is String) {
      bookingDate = json["bookingDate"];
    }
    if(json["time"] is String) {
      time = json["time"];
    }
    if(json["note"] is String) {
      note = json["note"];
    }
    if(json["status"] is String) {
      status = json["status"];
    }
    if(json["storeId"] is int) {
      storeId = json["storeId"];
    }
    if(json["guestName"] is String) {
      guestName = json["guestName"];
    }
    if(json["guestEmail"] is String) {
      guestEmail = json["guestEmail"];
    }
    if(json["guestPhone"] is String) {
      guestPhone = json["guestPhone"];
    }
    if(json["service"] is String) {
      service = json["service"];
    }

  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> _data = <String, dynamic>{};
    _data["bookingId"] = bookingId;
    _data["userId"] = userId;
    _data["bookingDate"] = bookingDate;
    _data["time"] = time;
    _data["note"] = note;
    _data["status"] = status;
    _data["storeId"] = storeId;
    _data["guestName"] = guestName;
    _data["guestEmail"] = guestEmail;
    _data["guestPhone"] = guestPhone;
    _data["service"] = service;
    return _data;
  }
}
