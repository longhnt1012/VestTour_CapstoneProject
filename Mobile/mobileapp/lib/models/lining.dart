class Lining {
  int? liningId;
  String? liningName;
  String? imageUrl;

  Lining({this.liningId, this.liningName, this.imageUrl});

  Lining.fromJson(Map<String, dynamic> json) {
    liningId = json['liningId'];
    liningName = json['liningName'];
    imageUrl = json['imageUrl'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['liningId'] = this.liningId;
    data['liningName'] = this.liningName;
    data['imageUrl'] = this.imageUrl;
    return data;
  }
}
