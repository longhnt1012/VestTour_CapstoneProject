import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/register/api_services.dart';


class OtpScreen extends StatefulWidget {
  final String email;

  OtpScreen({required this.email});

  @override
  _OtpScreenState createState() => _OtpScreenState();
}

class _OtpScreenState extends State<OtpScreen> {
  final TextEditingController _otpController = TextEditingController();
  final ApiServiceRegister _apiService = ApiServiceRegister();
  bool _isSubmitting = false;

  Future<void> _confirmOtp() async {
    if (_otpController.text.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Please enter OTP')),
      );
      return;
    }

    setState(() {
      _isSubmitting = true;
    });

    try {
      final response = await _apiService.confirmEmail(widget.email, _otpController.text);
      if (response.statusCode == 200) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Email confirmed successfully!')),
        );
        Navigator.popUntil(context, (route) => route.isFirst); // Navigate to the first screen
      } else {
        final errorResponse = jsonDecode(response.body);
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Confirmation failed: ${errorResponse['message']}')),
        );
      }
    } catch (error) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('An error occurred. Please try again.')),
      );
    } finally {
      setState(() {
        _isSubmitting = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Confirm Email'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            Text('A confirmation email has been sent to ${widget.email}. Please enter the OTP below.'),
            SizedBox(height: 16),
            TextFormField(
              controller: _otpController,
              decoration: InputDecoration(labelText: 'OTP'),
              keyboardType: TextInputType.number,
            ),
            SizedBox(height: 16),
            ElevatedButton(
              onPressed: _isSubmitting ? null : _confirmOtp,
              child: _isSubmitting
                  ? CircularProgressIndicator(color: Colors.white)
                  : Text('Confirm OTP'),
            ),
          ],
        ),
      ),
    );
  }
}
