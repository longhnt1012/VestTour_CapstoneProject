class Category {
  int? categoryId;
  int? categoryParentId;
  String? name;
  String? imageUrl;
  String? description;

  Category(
      {this.categoryId,
        this.categoryParentId,
        this.name,
        this.imageUrl,
        this.description});

  Category.fromJson(Map<String, dynamic> json) {
    categoryId = json['categoryId'];
    categoryParentId = json['categoryParentId'];
    name = json['name'];
    imageUrl = json['imageUrl'];
    description = json['description'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['categoryId'] = this.categoryId;
    data['categoryParentId'] = this.categoryParentId;
    data['name'] = this.name;
    data['imageUrl'] = this.imageUrl;
    data['description'] = this.description;
    return data;
  }
}
