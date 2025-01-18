import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/user/api_services.dart';
import 'package:mobileapp/screens/login_screen.dart';
 // Import API Service

class ResetPasswordScreen extends StatefulWidget {
  final String email; // Email từ màn hình trước

  ResetPasswordScreen({required this.email});

  @override
  _ResetPasswordScreenState createState() => _ResetPasswordScreenState();
}

class _ResetPasswordScreenState extends State<ResetPasswordScreen> {
  final ApiService _apiService = ApiService();
  final TextEditingController _tokenController = TextEditingController();
  final TextEditingController _newPasswordController = TextEditingController();
  final TextEditingController _confirmNewPasswordController = TextEditingController();
  String? _errorMessage;
  String? _successMessage;

  // Hàm reset mật khẩu
  void _resetPassword() async {
    final token = _tokenController.text.trim();
    final newPassword = _newPasswordController.text.trim();
    final confirmNewPassword = _confirmNewPasswordController.text.trim();

    if (token.isEmpty || newPassword.isEmpty || confirmNewPassword.isEmpty) {
      setState(() {
        _errorMessage = 'Please fill in all fields';
      });
      return;
    }

    if (newPassword != confirmNewPassword) {
      setState(() {
        _errorMessage = 'Passwords do not match';
      });
      return;
    }

    setState(() {
      _errorMessage = null;
      _successMessage = null;
    });

    try {
      // Gửi yêu cầu reset password
      final response = await _apiService.resetPassword(token, newPassword);
      setState(() {
        _successMessage = 'Password has been successfully reset!';
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => LoginScreen()),  // Chuyển qua LoginScreen
        );
      });
    } catch (e) {
      setState(() {
        _errorMessage = 'Failed to reset password. Please try again.';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Reset Password'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            TextField(
              controller: _tokenController,
              decoration: InputDecoration(
                labelText: 'Enter OTP (token)',
                errorText: _errorMessage,
              ),
            ),
            SizedBox(height: 20),
            TextField(
              controller: _newPasswordController,
              decoration: InputDecoration(
                labelText: 'Enter new password',
                errorText: _errorMessage,
              ),
              obscureText: true,
            ),
            SizedBox(height: 20),
            TextField(
              controller: _confirmNewPasswordController,
              decoration: InputDecoration(
                labelText: 'Confirm new password',
                errorText: _errorMessage,
              ),
              obscureText: true,
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: _resetPassword,
              child: Text('Reset Password'),
            ),
            SizedBox(height: 20),
            if (_successMessage != null)
              Text(
                _successMessage!,
                style: TextStyle(color: Colors.green),
              ),
            if (_errorMessage != null)
              Text(
                _errorMessage!,
                style: TextStyle(color: Colors.red),
              ),
          ],
        ),
      ),
    );
  }
}
