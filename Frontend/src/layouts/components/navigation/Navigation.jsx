import '../navigation/Navigation.scss';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo from '../../../assets/img/logo/logo.png';

export const Navigation = () => {
  return (
    <div className='header-area header_area'>
        <div className='main-header'>
            <div className='header-bottom header-sticky sticky-bar'>
                <div className='logo'>
                    <img src={logo} alt="logo" />
                </div>

                <div className="header-left  d-flex f-right align-items-center">
                    <div className="main-menu f-right d-none d-lg-block">
                        <nav> 
                            <ul id="navigation">                                                                                                                                     
                            <li><a href="">SUITS</a>
                                    <ul className="submenu">
                                        <li><a href="">DESIGN YOUR SUITS</a></li>
                                        <li><a href="">COLLECTIONS</a></li>
                                    </ul>
                                </li>
                                <li><a href="">BLAZERS</a>
                                    <ul className="submenu">
                                        <li><a href="">DESIGN YOUR BLAZERS</a></li>
                                        <li><a href="">BLAZERS COLLECTION</a></li>
                                    </ul>
                                </li>
                                <li><a href="">SHIRTS</a>
                                    <ul className="submenu">
                                        <li><a href="">DESIGN YOUR SHIRTS</a></li>
                                        <li><a href="">COLLECTIONS</a></li>
                                    </ul>
                                </li>
                                <li><a href="">PANTS</a>
                                    <ul className="submenu">
                                        <li><a href="">DESIGN YOUR PANTS</a></li>
                                        <li><a href="">COLLECTIONS</a></li>
                                    </ul>
                                </li>
                                <li><a href="">COAT</a>
                                </li>
                                <li><a href="">WOMEN</a>
                                    <ul className="submenu">
                                        <li><a href="">SUITS</a></li>
                                        <li><a href="">PANTS</a></li>
                                        <li><a href="">BLAZERS</a></li>
                                        <li><a href="">DRESS</a></li>
                                        <li><a href="">SKIRT</a></li>
                                        <li><a href="">TOP & BLOUSE</a></li>
                                        <li><a href="">OVERCOAT</a></li>
                                        <li><a href="">BUSINESS DRESS</a></li>
                                        <li><a href="">BUSINESS SHIRTS</a></li>
                                    </ul>
                                </li>
                                <li><a href="">WEDDING</a>
                                    <ul className="submenu">
                                        <li><a href="">GROOM</a></li>
                                        <li><a href="">BRIDESMAID</a></li>
                                        <li><a href="">BRIDESMAID</a></li>
                                    </ul>
                                </li>
                                <li><a href="">ACCESSORIES</a>
                                    <ul className="submenu">
                                        <li><a href="">TIES</a></li>
                                        <li><a href="">BOW TIES</a></li>
                                        <li><a href="">MASKS</a></li>
                                    </ul>
                                </li>
                                <li><a href="">SHIRTS</a>
                                    <ul className="submenu">
                                        <li><a href="">DESIGN YOUR SHIRTS</a></li>
                                        <li><a href="">COLLECTIONS</a></li>
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