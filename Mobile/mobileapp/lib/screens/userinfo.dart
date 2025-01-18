import 'package:flutter/material.dart';
import 'package:mobileapp/data_source/user/api_services.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../models/user.dart';

class UserDetailScreen extends StatefulWidget {
  const UserDetailScreen({Key? key}) : super(key: key);

  @override
  _UserDetailScreenState createState() => _UserDetailScreenState();
}

class _UserDetailScreenState extends State<UserDetailScreen> {
  int? userId;
  User? user;
  bool isEditing = false;
  final _formKey = GlobalKey<FormState>();

  final _nameController = TextEditingController();
  final _genderController = TextEditingController();
  final _emailController = TextEditingController();
  final _phoneController = TextEditingController();
  final _addressController = TextEditingController();
  final _dobController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _loadUserId();
  }

  Future<void> _loadUserId() async {
    final prefs = await SharedPreferences.getInstance();
    final loadedUserId = prefs.getInt('user_id') ?? 0;

    if (loadedUserId > 0) {
      setState(() {
        userId = loadedUserId;
      });
      await _fetchUserDetails();
    } else {
      setState(() {
        userId = null;
      });
    }
  }

  Future<void> _fetchUserDetails() async {
    if (userId != null) {
      final fetchedUser = await ApiService().getUserProfile(userId!);
      if (fetchedUser != null) {
        setState(() {
          user = fetchedUser;
          _populateFormFields();
        });
      }
    }
  }

  void _populateFormFields() {
    _nameController.text = user?.name ?? "";
    _genderController.text = user?.gender ?? "";
    _emailController.text = user?.email ?? "";
    _phoneController.text = user?.phone ?? "";
    _addressController.text = user?.address ?? "";
    _dobController.text = user?.dob ?? "";
  }

  Future<void> _saveUserDetails() async {
    if (_formKey.currentState!.validate()) {
      final updatedData = {
        "name": _nameController.text.trim(),
        "gender": _genderController.text.trim(),
        "address": _addressController.text.trim(),
        "dob": _dobController.text.trim(),
        "email": _emailController.text.trim(),
        "phone": _phoneController.text.trim(),
      };

      final success = await ApiService().updateUser(userId!, updatedData);
      if (success) {
        setState(() {
          isEditing = false;
        });
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text("User details updated successfully")),
        );
        await _fetchUserDetails();
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text("Failed to update user details")),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("User Details"),
        actions: [
          if (user != null)
            IconButton(
              icon: Icon(isEditing ? Icons.save : Icons.edit),
              onPressed: () {
                if (isEditing) {
                  _saveUserDetails();
                } else {
                  setState(() {
                    isEditing = true;
                  });
                }
              },
            ),
        ],
      ),
      body: user == null
          ? const Center(child: CircularProgressIndicator())
          : Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: ListView(
            children: [
              _buildTextField("Name", _nameController, isEditing),
              _buildTextField("Gender", _genderController, isEditing),
              _buildTextField("Email", _emailController, isEditing),
              _buildTextField("Phone", _phoneController, isEditing),
              _buildTextField("Address", _addressController, isEditing),
              _buildTextField("Date of Birth", _dobController, isEditing),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildTextField(String label, TextEditingController controller, bool editable) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: TextFormField(
        controller: controller,
        enabled: editable,
        decoration: InputDecoration(
          labelText: label,
          border: const OutlineInputBorder(),
        ),
        validator: (value) {
          if (value == null || value.isEmpty) {
            return "Please enter $label";
          }
          return null;
        },
      ),
    );
  }
}
