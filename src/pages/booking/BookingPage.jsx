import { useState, useEffect } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import "./BookingPage.scss";
import QuoteCarousel from "./QuoteCarousel";
import ReviewSection from "./ReviewSection";
import { Navigation } from "../../layouts/components/navigation/Navigation";
import { Footer } from "../../layouts/components/footer/Footer";

const BookingPage = () => {
  const [date, setDate] = useState(new Date());
  const [availableTimes, setAvailableTimes] = useState([]);
  const [selectedTime, setSelectedTime] = useState("");
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    zipCode: "",
    description: "",
    pieces: "",
  });

  useEffect(() => {
    if (date) {
      updateAvailableTimes(date);
    }
  }, [date]);

  const updateAvailableTimes = (selectedDate) => {
    const times = [
      "10:00 AM",
      "11:00 AM",
      "11:30 AM",
      "12:00 PM",
      "12:30 PM",
      "3:00 PM",
      "3:30 PM",
      "4:00 PM",
      "4:30 PM",
      "5:00 PM",
      "5:30 PM",
    ];
    setAvailableTimes(times);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Form submitted:", { ...formData, date, selectedTime });
    // Add your form submission logic here
  };

  return (
    <div className="booking-page">
      <Navigation />
      <div className="content-container">
        {/* Left Column */}
        <div className="left-column">
          {/* Image and Address */}
          <div className="location-info">
            <img
              src="https://lh3.googleusercontent.com/proxy/eJH-Ha07sbxp8lnnmrypHAlDtp1FdWuypD9Gx1p---KXhOet2hnOjex3JP9Bn8v_xecQSEVUTS-_WA"
              alt="Court Street Location"
              className="location-image"
            />
            <div className="address-info">
              <h2>357 Court St,</h2>
              <h2>Brooklyn, NY 11231</h2>
              <button className="bridal-appointments">
                Click here for Bridal Appointments
              </button>
            </div>
          </div>

          {/* About Section */}
          <div className="about-section">
            <div className="header-box">
              <h2>About Us</h2>
            </div>
            <p>
              Our team at Alts (Alteration Specialists) Court St offers standard
              services, bridal services, and specialty services. Conveniently
              located between President St and Union St.
            </p>
          </div>

          {/* Contact Information */}
          <div className="contact-info">
            <h3>Contact Us</h3>
            <p>
              <strong>(347) 223-4905</strong>
            </p>
            <p>
              Email:{" "}
              <a href="mailto:courts@alterationspecialists.com">
                courts@alterationspecialists.com
              </a>
            </p>
          </div>

          {/* Business Hours */}
          <div className="hours-section">
            <h3>Business Hours</h3>
            <ul>
              <li>
                <span>Monday:</span> Closed
              </li>
              <li>
                <span>Tuesday - Friday:</span> 10:00 AM – 7:00 PM
              </li>
              <li>
                <span>Saturday:</span> 10:00 AM – 6:00 PM
              </li>
              <li>
                <span>Sunday:</span> Closed
              </li>
            </ul>
          </div>

          {/* Subway Access */}
          <div className="subway-access">
            <h3>Subway Access</h3>
            <div className="subway-icons">
              <span className="subway-icon f">F</span>
              <span className="subway-icon g">G</span>
            </div>
          </div>

          {/* Google Map */}
          <div className="map-section">
            <iframe
              src="https://www.google.com/maps/embed?pb=..."
              width="100%"
              height="300"
              style={{ border: 0 }}
              allowFullScreen=""
              loading="lazy"
              referrerPolicy="no-referrer-when-downgrade"
              title="Google Map Location"
            ></iframe>
          </div>
        </div>

        {/* Right Column */}
        <div className="right-column">
          <div className="header-box">
            <h2>Book an Appointment</h2>
          </div>
          <select className="studio-select">
            <option>Change Studio</option>
          </select>

          <div className="appointment-info">
            <h3>Alts Court Street</h3>
            <p>Duration: 30 minutes</p>
            <p>Select a time slot below to book your appointment.</p>
          </div>

          {/* Calendar */}
          <Calendar
            onChange={setDate}
            value={date}
            className="custom-calendar"
          />

          {/* Available Times */}
          <div className="available-times">
            <h3>
              {date.toLocaleDateString("en-US", {
                weekday: "long",
                year: "numeric",
                month: "long",
                day: "numeric",
              })}
            </h3>
            {availableTimes.map((time, index) => (
              <button
                key={index}
                className={`time-slot ${
                  selectedTime === time ? "selected" : ""
                }`}
                onClick={() => setSelectedTime(time)}
              >
                {time}
              </button>
            ))}
          </div>

          {/* Booking Form */}
          {selectedTime && (
            <form onSubmit={handleSubmit} className="booking-form">
              <div className="form-group">
                <label htmlFor="firstName">First Name (Required)</label>
                <input
                  type="text"
                  id="firstName"
                  name="firstName"
                  value={formData.firstName}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div className="form-group">
                <label htmlFor="lastName">Last Name (Required)</label>
                <input
                  type="text"
                  id="lastName"
                  name="lastName"
                  value={formData.lastName}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Email (Required)</label>
                <input
                  type="email"
                  id="email"
                  name="email"
                  value={formData.email}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div className="form-group">
                <label htmlFor="phone">Phone Number (Required)</label>
                <input
                  type="tel"
                  id="phone"
                  name="phone"
                  value={formData.phone}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div className="form-group">
                <label htmlFor="zipCode">Zip Code (Required)</label>
                <input
                  type="text"
                  id="zipCode"
                  name="zipCode"
                  value={formData.zipCode}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div className="form-group">
                <label htmlFor="description">
                  Briefly describe what you are looking to accomplish with us.
                  (Required)
                </label>
                <textarea
                  id="description"
                  name="description"
                  value={formData.description}
                  onChange={handleInputChange}
                  required
                ></textarea>
              </div>
              <div className="form-group">
                <label htmlFor="pieces">
                  How many pieces are you getting altered? (Required)
                </label>
                <input
                  type="number"
                  id="pieces"
                  name="pieces"
                  value={formData.pieces}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <button type="submit">Confirm Booking</button>
            </form>
          )}
        </div>
      </div>

      {/* Quote Carousel and Review Section */}
      <section className="quote-carousel-section">
        <QuoteCarousel />
        <ReviewSection />
      </section>
      <Footer />
    </div>
  );
};

export default BookingPage;
