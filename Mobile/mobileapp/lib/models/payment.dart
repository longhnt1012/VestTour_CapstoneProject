class Payment {
  int? paymentId;
  int? orderId;
  int? userId;
  String? method;
  String? paymentDate;
  String? paymentDetails;
  String? status;
  double? amount;

  Payment(
      {this.paymentId,
        this.orderId,
        this.userId,
        this.method,
        this.paymentDate,
        this.paymentDetails,
        this.status,
        this.amount});

  Payment.fromJson(Map<String, dynamic> json) {
    paymentId = json['paymentId'];
    orderId = json['orderId'];
    userId = json['userId'];
    method = json['method'];
    paymentDate = json['paymentDate'];
    paymentDetails = json['paymentDetails'];
    status = json['status'];
    amount = json['amount'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['paymentId'] = this.paymentId;
    data['orderId'] = this.orderId;
    data['userId'] = this.userId;
    data['method'] = this.method;
    data['paymentDate'] = this.paymentDate;
    data['paymentDetails'] = this.paymentDetails;
    data['status'] = this.status;
    data['amount'] = this.amount;
    return data;
  }
}
