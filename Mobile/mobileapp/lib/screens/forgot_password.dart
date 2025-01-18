import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/user/api_services.dart';
import 'package:mobileapp/screens/reset_password.dart';
 // Import API Service

class ForgotPasswordScreen extends StatefulWidget {
  @override
  _ForgotPasswordScreenState createState() => _ForgotPasswordScreenState();
}

class _ForgotPasswordScreenState extends State<ForgotPasswordScreen> {
  final TextEditingController _emailController = TextEditingController();
  final ApiService _apiService = ApiService();
  String? _message;
  String? _error;

  void _submit() async {
    final email = _emailController.text.trim();
    if (email.isEmpty) {
      setState(() {
        _error = 'Please enter your email';
      });
      return;
    }

    setState(() {
      _message = null;
      _error = null;
    });

    try {
      final response = await _apiService.forgotPassword(email);
      setState(() {
        _message = response['message'];
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (context) => ResetPasswordScreen(email: email),
          ),
        );// Assume the API returns a message
      });
    } catch (e) {
      setState(() {
        _error = 'Failed to send request';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Forgot Password'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            TextField(
              controller: _emailController,
              decoration: InputDecoration(
                labelText: 'Enter your email',
                errorText: _error,
              ),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: _submit,
              child: Text('Submit'),
            ),
            SizedBox(height: 20),
            if (_message != null)
              Text(
                _message!,
                style: TextStyle(color: Colors.green),
              ),
          ],
        ),
      ),
    );
  }
}
