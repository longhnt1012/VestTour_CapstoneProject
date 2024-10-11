import React, { useState } from "react";
import {
  TextField,
  Button,
  MenuItem,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  InputAdornment,
} from "@mui/material";
import SearchIcon from "@mui/icons-material/Search"; // Import the Search icon
import "./StaffManagement.scss";

const initialStaff = [
  { id: 1, name: "John Doe", email: "john@example.com", status: "Active" },
  { id: 2, name: "Jane Smith", email: "jane@example.com", status: "Active" },
  {
    id: 3,
    name: "Alice Johnson",
    email: "alice@example.com",
    status: "Inactive",
  },
];

const StaffManagement = () => {
  const [staff, setStaff] = useState(initialStaff);
  const [newStaff, setNewStaff] = useState({
    name: "",
    email: "",
    status: "Active",
  });
  const [editIndex, setEditIndex] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNewStaff({ ...newStaff, [name]: value });
  };

  const handleAdd = () => {
    setStaff([...staff, { ...newStaff, id: staff.length + 1 }]);
    setNewStaff({ name: "", email: "", status: "Active" });
  };

  const handleEdit = (index) => {
    setNewStaff(staff[index]);
    setEditIndex(index);
  };

  const handleUpdate = () => {
    const updatedStaff = staff.map((s, index) =>
      index === editIndex ? newStaff : s
    );
    setStaff(updatedStaff);
    setNewStaff({ name: "", email: "", status: "Active" });
    setEditIndex(null);
  };

  const handleDelete = (id) => {
    setStaff(staff.filter((s) => s.id !== id));
  };

  const handleSearchChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const filteredStaff = staff.filter((s) =>
    s.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="staff-management">
      <h2>Staff Management</h2>
      <div className="header">
        <div className="form">
          <TextField
            label="Name"
            name="name"
            value={newStaff.name}
            onChange={handleChange}
            variant="outlined"
            style={{ marginRight: "1rem" }}
          />
          <TextField
            label="Email"
            name="email"
            value={newStaff.email}
            onChange={handleChange}
            variant="outlined"
            style={{ marginRight: "1rem" }}
          />
          <TextField
            select
            label="Status"
            name="status"
            value={newStaff.status}
            onChange={handleChange}
            variant="outlined"
            style={{ marginRight: "1rem" }}
          >
            <MenuItem value="Active">Active</MenuItem>
            <MenuItem value="Inactive">Inactive</MenuItem>
          </TextField>

          {editIndex !== null ? (
            <Button variant="contained" color="primary" onClick={handleUpdate}>
              Update
            </Button>
          ) : (
            <Button variant="contained" color="secondary" onClick={handleAdd}>
              Add Staff
            </Button>
          )}
        </div>

        <TextField
          label="Search by Name"
          variant="outlined"
          value={searchTerm}
          onChange={handleSearchChange}
          style={{ margin: "1rem 0", marginLeft: "auto" }} // Align to the right
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <SearchIcon />
              </InputAdornment>
            ),
          }}
        />
      </div>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Email</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {filteredStaff.map((s, index) => (
              <TableRow key={s.id}>
                <TableCell>{s.name}</TableCell>
                <TableCell>{s.email}</TableCell>
                <TableCell>{s.status}</TableCell>
                <TableCell>
                  <Button
                    variant="outlined"
                    color="primary"
                    onClick={() => handleEdit(index)}
                    style={{ marginRight: "0.5rem" }}
                  >
                    Edit
                  </Button>
                  <Button
                    variant="outlined"
                    color="secondary"
                    onClick={() => handleDelete(s.id)}
                  >
                    Delete
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default StaffManagement;
