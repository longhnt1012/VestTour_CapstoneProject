import { useState, useEffect } from 'react';

import '../../assets/scss/SlideShow.scss';
import '../../assets/scss/HomePageBody.scss';
import a from '../../assets/img/elements/a.jpg';
import a2 from '../../assets/img/elements/a2.jpg';
import d from '../../assets/img/elements/d.jpg';
export function HomePageBody() {
  const [slideIndex, setSlideIndex] = useState(0);
  const slides = [
    { src: a},
    { src: a2},
    { src: d}
  ];

  // Function to handle next/prev buttons
  const plusSlides = (n) => {
    setSlideIndex((prevIndex) => (prevIndex + n + slides.length) % slides.length);
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      setSlideIndex((prevIndex) => (prevIndex + 1) % slides.length);
    }, 3000); // Auto-slide every 3 seconds
    return () => clearTimeout(timer); // Clean up the timer
  }, [slideIndex, slides.length]);

  return (
    <div className="slider-area position-relative">
      <div className="slider-active">
      <div className="slideshow-container section-padding40">
      {slides.map((slide, index) => (
        <div
          key={index}
          className="mySlides"
          style={{ display: index === slideIndex ? 'block' : 'none' }}
        >
          <div className="numbertext">{index + 1} / {slides.length}</div>
          <img src={slide.src} alt={`Slide ${index + 1}`} style={{ width: '100%' }} />
          <div className="text">{slide.caption}</div>
        </div>
      ))}

      {/* Next and previous buttons */}
      <a className="prev" onClick={() => plusSlides(-1)}>&#10094;</a>
      <a className="next" onClick={() => plusSlides(1)}>&#10095;</a>
    </div>

        {/* Clients Area */}
        <div className="container">
    <div className="row">
      {/* Three equally sized divs */}
      <div className="col-lg-4 col-md-4 col-sm-4">
        <div className="client-div">
          <a href="#">
            <img src="https://adongsilk.com/wp-content/uploads/2024/03/wedding-suits-in-hoi-an.jpg" alt="" style={{width: '100%', display: 'block'}}/>
          </a>
        </div>
      </div>
      <div className="col-lg-4 col-md-4 col-sm-4">
        <div className="client-div">
          <a href="#">
            <img src="https://adongsilk.com/wp-content/uploads/2024/03/wedding-suits-in-hoi-an.jpg" alt="" style={{width: '100%', display: 'block'}}/>
          </a>
        </div>
      </div>
      <div className="col-lg-4 col-md-4 col-sm-4">
        <div className="client-div">
        <a href="#">
            <img src="https://adongsilk.com/wp-content/uploads/2024/03/wedding-suits-in-hoi-an.jpg" alt="" style={{width: '100%', display: 'block'}}/>
          </a>
        </div>
      </div>
    </div>
  </div>
    </div>
  </div>
  );
}
