import 'package:firebase_messaging/firebase_messaging.dart';

class FirebaseApi{
  final _firebaseMessageing= FirebaseMessaging.instance;

  Future<void> initNotifications() async{
    await _firebaseMessageing.requestPermission();
    final fCMToken= await _firebaseMessageing.getToken();
    print("Token: $fCMToken");
    NotificationSettings settings = await FirebaseMessaging.instance.requestPermission();
    if (settings.authorizationStatus == AuthorizationStatus.authorized) {
      print("Đã cho phép thông báo.");
    }
    FirebaseMessaging.onBackgroundMessage(handleBackgroundMessaging);
  }

  Future<void> handleBackgroundMessaging(RemoteMessage message) async {
      print('Title: ${message.notification?.title}');
      print('Body: ${message.notification?.body}');
      print('Payload: ${message.data}');

  }
}