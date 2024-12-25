class Voucher {
  int? voucherId;
  String? status;
  String? voucherCode;
  String? description;
  double? discountNumber;
  String? dateStart;
  String? dateEnd;

  Voucher(
      {this.voucherId,
        this.status,
        this.voucherCode,
        this.description,
        this.discountNumber,
        this.dateStart,
        this.dateEnd});

  Voucher.fromJson(Map<String, dynamic> json) {
    voucherId = json['voucherId'];
    status = json['status'];
    voucherCode = json['voucherCode'];
    description = json['description'];
    discountNumber = json['discountNumber'];
    dateStart = json['dateStart'];
    dateEnd = json['dateEnd'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['voucherId'] = this.voucherId;
    data['status'] = this.status;
    data['voucherCode'] = this.voucherCode;
    data['description'] = this.description;
    data['discountNumber'] = this.discountNumber;
    data['dateStart'] = this.dateStart;
    data['dateEnd'] = this.dateEnd;
    return data;
  }
}
