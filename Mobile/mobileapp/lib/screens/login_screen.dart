import 'package:flutter/material.dart';
import 'package:mobileapp/screens/forgot_password.dart';
import 'package:mobileapp/screens/register_screen.dart';

import '../data_source/login/api_services.dart';

class LoginScreen extends StatefulWidget {
  @override
  _LoginScreenState createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final _formKey = GlobalKey<FormState>();

  bool _isPasswordVisible = false;

  // Simulated login function
  void _login() async {
    if (_formKey.currentState!.validate()) {
      String email = _emailController.text;
      String password = _passwordController.text;

      // Call your AuthService to log in
      bool success = await AuthService().login(email, password);

      if (success) {
        // Login was successful, navigate to the homepage
        Navigator.pushReplacementNamed(context, '/home');
      } else {
        // If login failed, show an error message
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Login Failed! Please try again.')),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,  // White background

      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Center(
          child: SingleChildScrollView(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                // Logo or image
                Image.asset(
                  'assets/images/logotailor.png', // Use your logo image here
                  height: 250,
                ),
                SizedBox(height: 20),

                // Form for email and password
                Form(
                  key: _formKey,
                  child: Column(
                    children: [
                      // Email Text Field
                      TextFormField(
                        controller: _emailController,
                        decoration: InputDecoration(
                          labelText: 'Email',
                          labelStyle: TextStyle(color: Color(0xFF6F4F37)),  // Brown label text
                          border: OutlineInputBorder(),
                          focusedBorder: OutlineInputBorder(
                            borderSide: BorderSide(color: Color(0xFF6F4F37)), // Brown border on focus
                          ),
                          prefixIcon: Icon(Icons.email, color: Color(0xFF6F4F37)), // Brown icon
                        ),
                        keyboardType: TextInputType.emailAddress,
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return 'Please enter your email';
                          }
                          return null;
                        },
                      ),
                      SizedBox(height: 16),

                      // Password Text Field
                      TextFormField(
                        controller: _passwordController,
                        obscureText: !_isPasswordVisible,
                        decoration: InputDecoration(
                          labelText: 'Password',
                          labelStyle: TextStyle(color: Color(0xFF6F4F37)),  // Brown label text
                          border: OutlineInputBorder(),
                          focusedBorder: OutlineInputBorder(
                            borderSide: BorderSide(color: Color(0xFF6F4F37)), // Brown border on focus
                          ),
                          prefixIcon: Icon(Icons.lock, color: Color(0xFF6F4F37)), // Brown icon
                          suffixIcon: IconButton(
                            icon: Icon(
                              _isPasswordVisible
                                  ? Icons.visibility
                                  : Icons.visibility_off,
                              color: Color(0xFF6F4F37), // Brown color for visibility icon
                            ),
                            onPressed: () {
                              setState(() {
                                _isPasswordVisible = !_isPasswordVisible;
                              });
                            },
                          ),
                        ),
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return 'Please enter your password';
                          }
                          return null;
                        },
                      ),
                      SizedBox(height: 16),

                      // Login Button
                      ElevatedButton(
                        onPressed: _login,
                        child: Text('Login'),
                        style: ElevatedButton.styleFrom(
                          minimumSize: Size(double.infinity, 50), backgroundColor: Colors.white, // Brown button
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(8),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),

                // Forgot Password and Sign Up Links
                SizedBox(height: 20),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    // Forgot Password Link
                    TextButton(
                      onPressed: () {
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                            builder: (context) => ForgotPasswordScreen(),
                            )
                        );
                        // Navigate to Forgot Password screen
                        print("Forgot Password tapped");
                      },
                      child: Text(
                        'Forgot Password?',
                        style: TextStyle(color: Color(0xFFD2B48C)), // Brown color for text
                      ),
                    ),

                    TextButton(
                      onPressed: () {
                        // Navigate to Sign Up screen
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => RegisterScreen(),
                          ),
                        );
                      },
                      child: Text(
                        'Sign Up',
                        style: TextStyle(color: Color(0xFFD2B48C)), // Brown color for text
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
