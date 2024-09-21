import '../../assets/scss/HomePageBody.scss';
import h1_hero from '../../assets/img/hero/h1_hero.png';
import hero_icon from '../../assets/img/hero/hero-icon.png';
import h2_hero2 from '../../assets/img/hero/h2_hero2.png';
export function HomePageBody() {
  return (
    <div className="slider-area position-relative">
    <div className="slider-active">
        <div className="single-slider position-relative hero-overly slider-height  d-flex align-items-center slick-slide slick-current slick-active" style={{ backgroundImage: `url(${h1_hero})`, marginTop: '80px' }}>
            <div className="container">
             <div className="row">
                 <div className="col-xl-6 col-lg-6">
                    <div className="hero-caption hero-caption2">
                        <img src={hero_icon} alt="" data-animation="zoomIn" data-delay="1s"/>
                        <h2 data-animation="fadeInLeft" data-delay=".4s">About us</h2>
                    </div>
                </div>
            </div>
        </div>
        <div className="hero-img hero-img2">
            <img src={h2_hero2} alt=""  data-animation="fadeInRight" data-transition-duration="5s"/>
        </div>
    </div>
</div>
</div>
  )
}

