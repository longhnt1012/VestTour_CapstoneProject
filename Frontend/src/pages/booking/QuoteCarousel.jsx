import { useEffect, useState } from "react";
import "./QuoteCarousel.scss"; // Create this file for styling

const quotes = [
  {
    text: "The Atmosphere Feels Luxurious",
    author: "POPSUGAR",
  },
  {
    text: "An Unforgettable Experience",
    author: "VOGUE",
  },
  {
    text: "A Perfect Blend of Style and Comfort",
    author: "HARPER'S BAZAAR",
  },
  {
    text: "Where Elegance Meets Quality",
    author: "FORBES",
  },
];

const QuoteCarousel = () => {
  const [currentIndex, setCurrentIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentIndex((prevIndex) =>
        prevIndex === quotes.length - 1 ? 0 : prevIndex + 1
      );
    }, 3000); // Change quote every 3 seconds

    return () => clearInterval(interval); // Cleanup interval on unmount
  }, []);

  return (
    <div className="quote-carousel">
      <div className="quote">
        <p>{quotes[currentIndex].text}</p>
        <p className="author">- {quotes[currentIndex].author}</p>
      </div>
    </div>
  );
};

export default QuoteCarousel;
