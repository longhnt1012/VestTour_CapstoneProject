class Lining {
  int? liningId;
  String? liningName;
  String? imageUrl;
  String? status;

  Lining({this.liningId, this.liningName, this.imageUrl, this.status});

  Lining.fromJson(Map<String, dynamic> json) {
    liningId = json['liningId'];
    liningName = json['liningName'];
    imageUrl = json['imageUrl'];
    status = json['status'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['liningId'] = this.liningId;
    data['liningName'] = this.liningName;
    data['imageUrl'] = this.imageUrl;
    data['status'] = this.status;
    return data;
  }
}
