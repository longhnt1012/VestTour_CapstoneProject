import { useState } from "react";
import "./ReviewSection.scss"; // Ensure this file is linked

const reviews = [
  {
    name: "Stuart G.",
    rating: 5,
    text: "Very happy with my experience at Alteration Specialists. It is worth every cent. You get what you pay for, which is top-notch tailoring and quality. Highly recommended.",
    videoId: "your_vimeo_video_id_1", // Replace with actual Vimeo video ID
  },
  {
    name: "Jane D.",
    rating: 4,
    text: "The service was excellent! My dress fit perfectly after the alterations.",
    videoId: "your_vimeo_video_id_2", // Replace with actual Vimeo video ID
  },
  {
    name: "Michael S.",
    rating: 5,
    text: "Absolutely fantastic work! I will definitely return for future alterations.",
    videoId: "your_vimeo_video_id_3", // Replace with actual Vimeo video ID
  },
];

const ReviewSection = () => {
  const [currentIndex, setCurrentIndex] = useState(0);

  const handlePrev = () => {
    setCurrentIndex((prevIndex) =>
      prevIndex === 0 ? reviews.length - 1 : prevIndex - 1
    );
  };

  const handleNext = () => {
    setCurrentIndex((prevIndex) =>
      prevIndex === reviews.length - 1 ? 0 : prevIndex + 1
    );
  };

  const { name, rating, text, videoId } = reviews[currentIndex];

  return (
    <div className="review-section">
      <h2>Our Clients Love Us</h2>
      <div className="review">
        <div className="review-content">
          <div className="video-container">
            <iframe
              src={`https://player.vimeo.com/video/${732772142}`}
              width="100%"
              height="300"
              frameBorder="0"
              allow="autoplay; fullscreen; picture-in-picture"
              allowFullScreen
              title="Client Review Video"
            ></iframe>
          </div>
          <div className="text-content">
            <h3>{name}</h3>
            <div className="rating">{`⭐️`.repeat(rating)}</div>
            <p>{text}</p>
          </div>
        </div>
      </div>

      <div className="navigation-buttons">
        <button onClick={handlePrev}>&lt;</button>
        <button onClick={handleNext}>&gt;</button>
      </div>
    </div>
  );
};

export default ReviewSection;
