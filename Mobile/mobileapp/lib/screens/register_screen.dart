import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/register/api_services.dart';

import 'confirm_email_screen.dart';


class RegisterScreen extends StatefulWidget {
  @override
  _RegisterScreenState createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _nameController = TextEditingController();
  final TextEditingController _genderController = TextEditingController(text: "Male");
  final TextEditingController _addressController = TextEditingController();
  final TextEditingController _dobController = TextEditingController();
  final TextEditingController _phoneController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  final ApiServiceRegister _apiService = ApiServiceRegister();
  bool _isSubmitting = false;

  Future<void> _register() async {
    if (_formKey.currentState!.validate()) {
      setState(() {
        _isSubmitting = true;
      });

      final body = {
        "name": _nameController.text,
        "gender": _genderController.text,
        "address": _addressController.text,
        "dob": _dobController.text,
        "phone": _phoneController.text,
        "email": _emailController.text,
        "password": _passwordController.text,
      };

      try {
        final response = await _apiService.registerUser(body);
        if (response.statusCode == 200 || response.statusCode == 201) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text('Registration successful!')),
          );
          // Navigate to OTP Screen
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => OtpScreen(email: _emailController.text),
            ),
          );
        } else {
          final errorResponse = jsonDecode(response.body);
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text('Registration failed: ${errorResponse['message']}')),
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
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Register'),
        backgroundColor: Color(0xFF6F4F37),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: SingleChildScrollView(
            child: Column(
              children: [
                TextFormField(
                  controller: _nameController,
                  decoration: InputDecoration(labelText: 'Name'),
                  validator: (value) => value == null || value.isEmpty ? 'Please enter your name' : null,
                ),
                SizedBox(height: 16),
                DropdownButtonFormField<String>(
                  value: _genderController.text.isNotEmpty ? _genderController.text : "Male", // Default value
                  items: ["Male", "Female"]
                      .map((gender) => DropdownMenuItem(value: gender, child: Text(gender)))
                      .toList(),
                  onChanged: (value) {
                    if (value != null) {
                      _genderController.text = value; // Update the controller with the selected value
                    }
                  },
                  decoration: InputDecoration(labelText: 'Gender'),
                  validator: (value) => value == null || value.isEmpty ? 'Please select your gender' : null,
                ),

                SizedBox(height: 16),
                TextFormField(
                  controller: _addressController,
                  decoration: InputDecoration(labelText: 'Address'),
                  validator: (value) => value == null || value.isEmpty ? 'Please enter your address' : null,
                ),
                SizedBox(height: 16),
                TextFormField(
                  controller: _dobController,
                  decoration: InputDecoration(labelText: 'Date of Birth'),
                  onTap: () async {
                    FocusScope.of(context).requestFocus(FocusNode());
                    DateTime? pickedDate = await showDatePicker(
                      context: context,
                      initialDate: DateTime.now(),
                      firstDate: DateTime(1900),
                      lastDate: DateTime.now(),
                    );
                    if (pickedDate != null) {
                      _dobController.text = pickedDate.toIso8601String().split('T').first;
                    }
                  },
                  validator: (value) => value == null || value.isEmpty ? 'Please enter your date of birth' : null,
                ),
                SizedBox(height: 16),
                TextFormField(
                  controller: _phoneController,
                  decoration: InputDecoration(labelText: 'Phone'),
                  keyboardType: TextInputType.phone,
                  validator: (value) => value == null || value.isEmpty ? 'Please enter your phone number' : null,
                ),
                SizedBox(height: 16),
                TextFormField(
                  controller: _emailController,
                  decoration: InputDecoration(labelText: 'Email'),
                  keyboardType: TextInputType.emailAddress,
                  validator: (value) => value == null || value.isEmpty ? 'Please enter your email' : null,
                ),
                SizedBox(height: 16),
                TextFormField(
                  controller: _passwordController,
                  obscureText: true,
                  decoration: InputDecoration(labelText: 'Password'),
                  validator: (value) => value == null || value.isEmpty ? 'Please enter your password' : null,
                ),
                SizedBox(height: 16),
                ElevatedButton(
                  onPressed: _isSubmitting ? null : _register,
                  child: _isSubmitting
                      ? CircularProgressIndicator(color: Colors.white)
                      : Text('Register'),
                  style: ElevatedButton.styleFrom(
                    minimumSize: Size(double.infinity, 50),
                    backgroundColor: Color(0xFF6F4F37),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
