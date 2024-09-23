import React, { useState, useEffect } from "react";
import Calendar from "react-calendar";
import QuoteCarousel from "./QuoteCarousel";
import ReviewSection from "./ReviewSection";
import "./BookingPage.scss";
import ScrollToTopOnMount from "../../layouts/components/navigation/BackToTop";
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
    firstTime: "",
    referral: "",
    promoCode: "",
  });
  const [showConfirmation, setShowConfirmation] = useState(false);

  useEffect(() => {
    if (date) {
      updateAvailableTimes(date);
    }
  }, [date]);

  const updateAvailableTimes = (selectedDate) => {
    const times = [
      "3:00 PM",
      "3:30 PM",
      "4:00 PM",
      "4:30 PM",
      "5:00 PM",
      "5:30 PM",
    ];
    setAvailableTimes(times);
  };

  const formatDate = (date) => {
    const options = {
      weekday: "long",
      year: "numeric",
      month: "long",
      day: "numeric",
    };
    return date
      .toLocaleDateString(undefined, options)
      .replace(/(\d+)/, (match) => `${match}${getOrdinalSuffix(match)}`);
  };

  const getOrdinalSuffix = (n) => {
    const suffixes = ["th", "st", "nd", "rd"];
    const value = n % 100;
    return suffixes[(value - 20) % 10] || suffixes[value] || suffixes[0];
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
    console.log("Booking confirmed:", { date, selectedTime, formData });
  };

  return (
    <div className="booking-page">
      <ScrollToTopOnMount />
      <Navigation /> {/* Header */}
      <div className="overlay"></div>
      <div className="content-container">
        <div className="info-section">
          <img
            src="https://dynamic-media-cdn.tripadvisor.com/media/photo-o/1a/f3/2f/27/inside.jpg?w=1200&h=1200&s=1"
            alt="Inside the store"
            className="about-image"
          />
          <div className="header-box about-header">
            <h2>About</h2>
          </div>
          <p>
            Our team at Alts (Alteration Specialists) Court St offers standard
            services, bridal services, and specialty services. The studio is
            conveniently located on Court Street between President St and Union
            St. <a href="/tour">Tour our studio.</a>
          </p>

          <div className="contact-info">
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

          <h3 style={{ marginLeft: "260px", marginTop: "40px" }}>Hours</h3>
          <ul className="business-hours">
            <li>Monday: Closed</li>
            <li>Tuesday: 10:00 am – 7:00 pm</li>
            <li>Wednesday: 10:00 am – 7:00 pm</li>
            <li>Thursday: 10:00 am – 7:00 pm</li>
            <li>Friday: 10:00 am – 7:00 pm</li>
            <li>Saturday: 10:00 am – 6:00 pm</li>
            <li>Sunday: Closed</li>
          </ul>

          <h2>Address</h2>
          <div className="map">
            <iframe
              src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3151.8354345094056!2d144.9537353153153!3d-37.817209979751!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x6ad642af0f11e7d7%3A0x5045675218ceed30!2s357%20Court%20St%2C%20Brooklyn%2C%20NY%2011231%2C%20USA!5e0!3m2!1sen!2sin!4v1618232999980!5m2!1sen!2sin"
              width="100%"
              height="400"
              style={{ border: 0 }}
              allowFullScreen=""
              loading="lazy"
            ></iframe>
          </div>
        </div>

        <div className="appointment-section">
          <div className="header-box appointment-header">
            <h2>Book an Appointment</h2>
          </div>
          <div className="selected-info">
            <label>Selected Date:</label>
            <p>{formatDate(date)}</p>
            <label>Selected Time:</label>
            <p>{selectedTime || "No time selected"}</p>
          </div>

          <Calendar
            onChange={setDate}
            value={date}
            className="custom-calendar"
          />

          <div className="available-times">
            <h3>Available Times</h3>
            {availableTimes.length > 0 ? (
              <ul>
                {availableTimes.map((time, index) => (
                  <li key={index}>
                    <button
                      type="button"
                      className={selectedTime === time ? "selected" : ""}
                      onClick={() => {
                        setSelectedTime(time);
                        setShowConfirmation(true);
                      }}
                    >
                      {time}
                    </button>
                  </li>
                ))}
              </ul>
            ) : (
              <p>No available times for this date.</p>
            )}
          </div>

          {showConfirmation && (
            <form onSubmit={handleSubmit}>
              <label>
                First Name (Required)
                <input
                  type="text"
                  name="firstName"
                  value={formData.firstName}
                  onChange={handleInputChange}
                  required
                />
              </label>

              <label>
                Last Name (Required)
                <input
                  type="text"
                  name="lastName"
                  value={formData.lastName}
                  onChange={handleInputChange}
                  required
                />
              </label>

              <label>
                Email (Required)
                <input
                  type="email"
                  name="email"
                  value={formData.email}
                  onChange={handleInputChange}
                  required
                />
              </label>

              <label>
                Phone Number (Required)
                <input
                  type="tel"
                  name="phone"
                  value={formData.phone}
                  onChange={handleInputChange}
                  required
                />
              </label>

              <label>
                Zip Code (Required)
                <input
                  type="text"
                  name="zipCode"
                  value={formData.zipCode}
                  onChange={handleInputChange}
                  required
                />
              </label>

              <label>
                Briefly describe what you are looking to accomplish with us.
                (Required)
                <textarea
                  name="description"
                  value={formData.description}
                  onChange={handleInputChange}
                  required
                />
              </label>

              <label>
                How many pieces are you getting altered?
                <input
                  type="number"
                  name="pieces"
                  value={formData.pieces}
                  onChange={handleInputChange}
                />
              </label>

              <label>
                Is this your first time with us? (Required)
                <select
                  name="firstTime"
                  value={formData.firstTime}
                  onChange={handleInputChange}
                  required
                >
                  <option value="">Please choose</option>
                  <option value="yes">Yes</option>
                  <option value="no">No</option>
                </select>
              </label>

              <label>
                How did you hear about us? (Required)
                <select
                  name="referral"
                  value={formData.referral}
                  onChange={handleInputChange}
                  required
                >
                  <option value="">Please choose</option>
                  <option value="online">Online</option>
                  <option value="friend">Friend</option>
                  <option value="advertisement">Advertisement</option>
                </select>
              </label>

              <label>
                Please specify Influencer / Stylist / Brand Referral name
                <input
                  type="text"
                  name="promoCode"
                  value={formData.promoCode}
                  onChange={handleInputChange}
                />
              </label>

              <button type="submit">Confirm Booking</button>
            </form>
          )}
        </div>
      </div>
      <QuoteCarousel />
      <ReviewSection />
      <Footer /> {/* Footer */}
    </div>
  );
};

export default BookingPage;
