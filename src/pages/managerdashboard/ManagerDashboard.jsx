import React from "react";
import "./ManagerDashboard.scss";

const ManagerDashboard = () => {
  const notificationCount = 5; // Example notification count

  return (
    <div className="manager-dashboard">
      <div className="flex">
        {/* Sidebar */}
        <div className="sidebar">
          <div className="logo">
            <img
              alt="Logo"
              src="https://storage.googleapis.com/a1aa/image/gimjwrHlSw4UP9hcEjhGo9Z6muOrLa7CNlrjRKBB9eDhgjyJA.jpg"
            />
            <span className="title">M.</span>
          </div>
          <div className="user-info">
            <div className="user-avatar">
              <img
                alt="User Avatar"
                src="https://storage.googleapis.com/a1aa/image/BCLG9m5sUnK8F5cPgFxdMVxgheb4LPh5b79gVeD1ZZyGBHlTA.jpg"
              />
              <p className="user-name">Jaydon Frankie</p>
            </div>
            <ul className="menu">
              <li>
                <a className="active" href="#">
                  <i className="fas fa-tachometer-alt"></i> Dashboard
                </a>
              </li>
              <li>
                <a href="#">
                  <i className="fas fa-user"></i> User
                </a>
              </li>
              <li>
                <a href="#">
                  <i className="fas fa-box"></i> Product
                </a>
              </li>
              <li>
                <a href="#">
                  <i className="fas fa-blog"></i> Blog
                </a>
              </li>
              <li>
                <a href="#">
                  <i className="fas fa-sign-in-alt"></i> Login
                </a>
              </li>
              <li>
                <a href="#">
                  <i className="fas fa-exclamation-circle"></i> Not Found
                </a>
              </li>
            </ul>
          </div>
        </div>
        {/* Main Content */}
        <div className="main-content">
          <div className="header">
            <h1>Hi, Welcome back</h1>
            <div className="header-icons">
              {/* Language Change Icon (Globe) */}
              <img
                src="https://cdn-icons-png.flaticon.com/512/61/61027.png"
                alt="Globe Icon"
                style={{
                  width: "24px",
                  height: "24px",
                  cursor: "pointer",
                  marginRight: "1rem",
                }}
              />

              {/* Notifications Icon (Updated Bell) */}
              <button
                style={{
                  background: "none",
                  border: "none",
                  cursor: "pointer",
                  position: "relative",
                  marginRight: "1rem",
                }}
                aria-label="Notifications"
              >
                <img
                  src="https://cdn-icons-png.flaticon.com/512/60/60753.png"
                  alt="Bell Icon"
                  style={{ width: "24px", height: "24px" }}
                />
                {notificationCount > 0 && (
                  <span
                    style={{
                      position: "absolute",
                      top: "-5px",
                      right: "-10px",
                      background: "red",
                      color: "white",
                      borderRadius: "50%",
                      padding: "2px 6px",
                      fontSize: "12px",
                    }}
                  >
                    {notificationCount}
                  </span>
                )}
              </button>

              {/* User Avatar */}
              <img
                alt="User Avatar"
                src="https://storage.googleapis.com/a1aa/image/BCLG9m5sUnK8F5cPgFxdMVxgheb4LPh5b79gVeD1ZZyGBHlTA.jpg"
                style={{
                  borderRadius: "50%",
                  width: "40px",
                  height: "40px",
                  marginLeft: "1rem",
                }}
              />
            </div>
          </div>
          <div className="stats">
            <div className="stat-item blue">
              <i className="fab fa-android"></i>
              <p className="stat-value">714k</p>
              <p className="stat-label">Weekly Sales</p>
            </div>
            <div className="stat-item blue-light">
              <i className="fab fa-apple"></i>
              <p className="stat-value">1.35m</p>
              <p className="stat-label">New Users</p>
            </div>
            <div className="stat-item yellow">
              <i className="fab fa-windows"></i>
              <p className="stat-value">1.72m</p>
              <p className="stat-label">Item Orders</p>
            </div>
            <div className="stat-item red">
              <i className="fas fa-bug"></i>
              <p className="stat-value">234</p>
              <p className="stat-label">Bug Reports</p>
            </div>
          </div>
          <div className="content">
            <div className="visits">
              <h2>Website Visits</h2>
              <p>(+43%) than last year</p>
              <img
                className="graph"
                alt="Graph showing website visits"
                src="https://storage.googleapis.com/a1aa/image/YSljUhYQmp6PNhCir0KXcn4sKmXS6rf3dAPmBfCfeEwdEcUOB.jpg"
              />
            </div>
            <div className="current-visits">
              <h2>Current Visits</h2>
              <div className="pie-chart">
                <div className="label label-1">27.7%</div>
                <div className="label label-2">34.7%</div>
                <div className="label label-3">28.4%</div>
                <div className="label label-4">9.2%</div>
              </div>
              <div className="legend">
                <div className="legend-item">
                  <div className="color blue"></div>
                  <p>America</p>
                </div>
                <div className="legend-item">
                  <div className="color yellow"></div>
                  <p>Asia</p>
                </div>
                <div className="legend-item">
                  <div className="color red"></div>
                  <p>Europe</p>
                </div>
                <div className="legend-item">
                  <div className="color green"></div>
                  <p>Africa</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ManagerDashboard;
