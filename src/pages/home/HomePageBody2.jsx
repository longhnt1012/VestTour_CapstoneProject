
import '../../assets/scss/HomePageBody2.scss';

export const HomePageBody2 = () => {
  return (
    <div>
      {/* Tailor House Section */}
      <section className="visit-tailor-area fix mt-margin">
        <div className="tailor-offers"></div>
        <div className="tailor-details">
          <h2>About our<br />tailor house</h2>
          <p className="pera-top">Introduction about the tailor house.</p>
          <p className="pera-bottom">More details about the tailor house.</p>
          <a href="#" className="btn">More About Us</a>
        </div>
      </section>

      {/* Wedding Collection Section */}
      <section className="kban sec k-sec">
        <div className="decor-sec">
          <img 
            src="https://adongsilk.com/template/images/review-decor.png" 
            alt="Decoration"
          />
        </div>
        <div className="all">
          <div className="kban-wrap">
            <div className="kban-flex">
              <div className="kban-left">
                <div className="sec-title k-title">
                  <h2 className="tt-txt">
                    <span className="tt-sub">WEDDING</span> COLLECTION
                  </h2>
                </div>
                <div className="kban-txt">
                  Elevate your wardrobe with the timeless allure and distinguished charm of the Classic Elegance Collection.
                </div>
                <div className="kban-btn mt-20">
                  <a href="https://adongsilk.com/lists/wedding/" className="btn primary-btn">
                    View full collection
                  </a>
                </div>
              </div>

              <div className="kban-right">
                <div className="kban-box">
                  <div className="kban-de">
                    <img 
                      src="https://adongsilk.com/wp-content/uploads/2024/04/kban-de.png" 
                      alt="Wedding Collection"
                      srcSet="
                        https://adongsilk.com/wp-content/uploads/2024/04/kban-de.png 699w, 
                        https://adongsilk.com/wp-content/uploads/2024/04/kban-de-300x200.png 300w, 
                        https://adongsilk.com/wp-content/uploads/2024/04/kban-de-488x326.png 488w, 
                        https://adongsilk.com/wp-content/uploads/2024/04/kban-de-216x144.png 216w, 
                        https://adongsilk.com/wp-content/uploads/2024/04/kban-de-600x401.png 600w"
                      sizes="(max-width: 699px) 100vw, 699px"
                    />
                  </div>
                  <div className="kban-bg">
                    <img 
                      src="https://adongsilk.com/template/images/kimg/kban-bg.png" 
                      alt="Background"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};
