import 'bootstrap/dist/css/bootstrap.min.css';
import '../../../assets/scss/Footer.scss';

import logo from '../../../assets/img/logo/logo.png';
import footerBg from '../../../assets/img/gallery/footer-bg.png';

export const Footer = () => {
  return (
    <div className="footer-wrapper section-bg2 pl-100" style={{backgroundImage: `url(${footerBg})`}}>
  <div className="footer-area footer-padding">
    <div className="container">
      <div className="row justify-content-end">
        <div className="col-xl-3 col-lg-4 col-md-4 col-sm-6">
          <div className="single-footer-caption mb-50">
            <div className="single-footer-caption mb-30">
              {/* logo */}
              <div className="footer-logo mb-35">
                  <img src={logo} alt="footerLogo" />
              </div>
              <div className="footer-tittle">
                <div className="footer-pera">
                  <p>Lorem ipsum dolor sit amet, consectetur adipiscing.</p>
                </div>
                <ul className="mb-40">
                  <li className="number">
                    <a href="#">(80) 783 367-3904</a>
                  </li>
                  <li className="number2">
                    <a href="#">(80) 783 367-3904</a>
                  </li>
                </ul>
              </div>
              {/* social */}
              <div className="footer-social">
                <a href="#">
                  <i className="fab fa-twitter"></i>
                </a>
                <a href="https://bit.ly/sai4ull">
                  <i className="fab fa-facebook-f"></i>
                </a>
                <a href="#">
                  <i className="fab fa-pinterest-p"></i>
                </a>
              </div>
            </div>
          </div>
        </div>
        <div className="col-xl-2 col-lg-4 col-md-4 col-sm-6">
          <div className="single-footer-caption mb-50">
            <div className="footer-tittle">
              <h4>Our solutions</h4>
              <ul>
                <li>
                  <a href="#">Home</a>
                </li>
                <li>
                  <a href="#">About</a>
                </li>
                <li>
                  <a href="#">Services</a>
                </li>
                <li>
                  <a href="#">Blog</a>
                </li>
                <li>
                  <a href="#">Contact</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  {/* footer-bottom area */}
  <div className="footer-bottom-area">
    <div className="container">
      <div className="footer-border">
        <div className="row align-items-center">
          <div className="col-xl-12 ">
            <div className="footer-copy-right text-right">
              <p>
                Copyright &copy;
                {new Date().getFullYear()} <i className="fa fa-heart" aria-hidden="true"></i>{" "}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

  )
}
