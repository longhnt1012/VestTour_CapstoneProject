class Style {
  int? styleId;
  String? styleName;

  Style({this.styleId, this.styleName});

  Style.fromJson(Map<String, dynamic> json) {
    styleId = json['styleId'];
    styleName = json['styleName'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['styleId'] = this.styleId;
    data['styleName'] = this.styleName;
    return data;
  }
}
