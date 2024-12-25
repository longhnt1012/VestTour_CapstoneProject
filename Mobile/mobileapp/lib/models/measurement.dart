class Measurement {
  int? measurementId;
  int? userId;
  double? weight;
  double? height;
  double? neck;
  double? hip;
  double? waist;
  double? armhole;
  double? biceps;
  double? pantsWaist;
  double? crotch;
  double? thigh;
  double? pantsLength;
  dynamic age;
  dynamic chest;
  dynamic shoulder;
  dynamic sleeveLength;
  dynamic jacketLength;

  Measurement({
    this.measurementId,
    this.userId,
    this.weight,
    this.height,
    this.neck,
    this.hip,
    this.waist,
    this.armhole,
    this.biceps,
    this.pantsWaist,
    this.crotch,
    this.thigh,
    this.pantsLength,
    this.age,
    this.chest,
    this.shoulder,
    this.sleeveLength,
    this.jacketLength,
  });

  Measurement.fromJson(Map<String, dynamic> json) {
    measurementId = json["measurementId"];
    userId = json["userId"];
    weight = json["weight"]?.toDouble();
    height = json["height"]?.toDouble();
    neck = json["neck"]?.toDouble();
    hip = json["hip"]?.toDouble();
    waist = json["waist"]?.toDouble();
    armhole = json["armhole"]?.toDouble();
    biceps = json["biceps"]?.toDouble();
    pantsWaist = json["pantsWaist"]?.toDouble();
    crotch = json["crotch"]?.toDouble();
    thigh = json["thigh"]?.toDouble();
    pantsLength = json["pantsLength"]?.toDouble();
    age = json["age"];
    chest = json["chest"];
    shoulder = json["shoulder"];
    sleeveLength = json["sleeveLength"];
    jacketLength = json["jacketLength"];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data["measurementId"] = measurementId;
    data["userId"] = userId;
    data["weight"] = weight;
    data["height"] = height;
    data["neck"] = neck;
    data["hip"] = hip;
    data["waist"] = waist;
    data["armhole"] = armhole;
    data["biceps"] = biceps;
    data["pantsWaist"] = pantsWaist;
    data["crotch"] = crotch;
    data["thigh"] = thigh;
    data["pantsLength"] = pantsLength;
    data["age"] = age;
    data["chest"] = chest;
    data["shoulder"] = shoulder;
    data["sleeveLength"] = sleeveLength;
    data["jacketLength"] = jacketLength;
    return data;
  }
}
