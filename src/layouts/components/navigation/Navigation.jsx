import '../../../assets/scss/Navigation.scss';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo from '../../../assets/img/logo/logo.png';

export const Navigation = () => {
  return (
    <div className='header-area header_area'>
        <div className='main-header'>
            <div className='header-bottom header-sticky'>
                <div className='logo'>
                    <img src={logo} alt="logo" />
                </div>

                <div className="header-left  d-flex f-right align-items-center">
                    <div className="main-menu f-right d-none d-lg-block">
                        <nav> 
                            <ul id="navigation">                                                                                                                                     
                                <li><a href="index.html">Home</a></li>
                                <li><a href="about.html">About</a></li>
                                <li><a href="services.html">Services</a></li>
                                <li><a href="blog.html">Blog</a>
                                    <ul className="submenu">
                                        <li><a href="blog.html">Blog</a></li>
                                        <li><a href="blog_details.html">Blog Details</a></li>
                                        <li><a href="elements.html">Elements</a></li>
                                    </ul>
                                </li>
                                <li><a href="contact.html">Contact</a></li>
                            </ul>
                        </nav>
                    </div>
                    <div className="header-right-btn f-right d-none d-lg-block  ml-30">
                        <a href="#" className="header-btn">Visit Us</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
  );
};
