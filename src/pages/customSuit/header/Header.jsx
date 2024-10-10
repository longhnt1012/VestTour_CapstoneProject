import React from 'react'
import './Header.scss';
import { Link } from 'react-router-dom';


export const Header = () => {
  return (
    <header id="header">
  <div className="all">
    <div className="logo">
      <a href="https://adongsilk.com/" className="custom-logo-link" rel="home">
        <img
          width="306"
          height="81"
          src="https://adongsilk.com/wp-content/uploads/2024/03/cropped-Logo_ADS-03_2024-final2_edit1-1-1.png"
          className="custom-logo"
          alt="A DONG SILK I Hoi An Tailor"
          srcSet="https://adongsilk.com/wp-content/uploads/2024/03/cropped-Logo_ADS-03_2024-final2_edit1-1-1.png 306w, https://adongsilk.com/wp-content/uploads/2024/03/cropped-Logo_ADS-03_2024-final2_edit1-1-1-300x79.png 300w, https://adongsilk.com/wp-content/uploads/2024/03/cropped-Logo_ADS-03_2024-final2_edit1-1-1-216x57.png 216w"
          sizes="(max-width: 306px) 100vw, 306px"
        />
      </a>
    </div>

    <ul className="customMenu">
      <li>
        <Link to="/custom-suits/fabric" className="toggle-side-menu active mona-toggle-side-menu">FABRIC</Link>
      </li>
      <li><i className="fa fa-angle-right"></i></li>
      <li>
        <Link to="/custom-suits/style" className="toggle-side-menu mona-toggle-side-menu">STYLE</Link>
      </li>
      <li><i className="fa fa-angle-right"></i></li>
      <li>
        <Link to="/custom-suits/lining" className="toggle-side-menu mona-toggle-side-menu">LINING</Link>
      </li>
    </ul>
  </div>
</header>
  )
}
