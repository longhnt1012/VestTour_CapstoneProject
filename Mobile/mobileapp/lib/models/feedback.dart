class FeedbackforOrder {
  int? feedbackId;
  String? comment;
  int? rating;
  String? dateSubmitted;
  int? userId;
  int? orderId;
  int? productId;

  FeedbackforOrder(
      {this.feedbackId,
        this.comment,
        this.rating,
        this.dateSubmitted,
        this.userId,
        this.orderId,
        this.productId});

  FeedbackforOrder.fromJson(Map<String, dynamic> json) {
    feedbackId = json['feedbackId'];
    comment = json['comment'];
    rating = json['rating'];
    dateSubmitted = json['dateSubmitted'];
    userId = json['userId'];
    orderId = json['orderId'];
    productId = json['productId'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['feedbackId'] = this.feedbackId;
    data['comment'] = this.comment;
    data['rating'] = this.rating;
    data['dateSubmitted'] = this.dateSubmitted;
    data['userId'] = this.userId;
    data['orderId'] = this.orderId;
    data['productId'] = this.productId;
    return data;
  }
}
class FeedbackforProduct {
  int? feedbackId;
  String? comment;
  int? rating;
  String? dateSubmitted;
  int? userId;
  int? productId;

  FeedbackforProduct(
      {this.feedbackId,
        this.comment,
        this.rating,
        this.dateSubmitted,
        this.userId,
        this.productId});

  FeedbackforProduct.fromJson(Map<String, dynamic> json) {
    feedbackId = json['feedbackId'];
    comment = json['comment'];
    rating = json['rating'];
    dateSubmitted = json['dateSubmitted'];
    userId = json['userId'];
    productId = json['productId'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['feedbackId'] = this.feedbackId;
    data['comment'] = this.comment;
    data['rating'] = this.rating;
    data['dateSubmitted'] = this.dateSubmitted;
    data['userId'] = this.userId;
    data['productId'] = this.productId;
    return data;
  }
}

