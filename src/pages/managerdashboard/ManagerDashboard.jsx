import React from "react";
import "./ManagerDashboard.scss";
import BasicArea from "./BasicArea"; // Ensure the path to BasicArea is correct

const ManagerDashboard = () => {
  return (
    <div className="dashboard">
      <aside className="sidebar">
        <div className="user-info">
          <img src="path_to_avatar_image" alt="User Avatar" />
          <span>Jaydon Frankie</span>
        </div>
        <nav className="nav">
          <ul>
            <li>Dashboard</li>
            <li>User</li>
            <li>Product</li>
            <li>Blog</li>
            <li>Login</li>
            <li>Not Found</li>
          </ul>
        </nav>
      </aside>

      <main className="main-content">
        <header className="dashboard-header">
          <h1>Hi, Welcome back</h1>
        </header>

        <div className="cards">
          <div className="card sales">
            <h2>714k</h2>
            <p>Weekly Sales</p>
          </div>
          <div className="card users">
            <h2>1.35m</h2>
            <p>New Users</p>
          </div>
          <div className="card orders">
            <h2>1.72m</h2>
            <p>Item Orders</p>
          </div>
          <div className="card reports">
            <h2>234</h2>
            <p>Bug Reports</p>
          </div>
        </div>

        <div className="visits">
          <h2>Website Visits</h2>
          <div className="chart">
            <BasicArea /> {/* MUI LineChart integrated here */}
          </div>
        </div>

        <div className="current-visits">
          <h2>Current Visits</h2>
          <div className="visit-data">
            <p>Data visualization here</p>
          </div>
        </div>
      </main>
    </div>
  );
};

export default ManagerDashboard;
