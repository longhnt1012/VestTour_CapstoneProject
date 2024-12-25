class StyleOption {
  int? styleOptionId;
  int? styleId;
  String? optionType;
  String? optionValue;
  double? price;

  StyleOption(
      {this.styleOptionId,
        this.styleId,
        this.optionType,
        this.optionValue,
        this.price});

  StyleOption.fromJson(Map<String, dynamic> json) {
    styleOptionId = json['styleOptionId'];
    styleId = json['styleId'];
    optionType = json['optionType'];
    optionValue = json['optionValue'];
    price = json['price'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['styleOptionId'] = this.styleOptionId;
    data['styleId'] = this.styleId;
    data['optionType'] = this.optionType;
    data['optionValue'] = this.optionValue;
    data['price'] = this.price;
    return data;
  }
}
