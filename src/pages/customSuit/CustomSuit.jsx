import React from 'react';
import { Outlet, Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import './CustomSuit.scss';

import all_icon from '../../assets/img/filter/icon-fabricFilter-all.jpg';
import elegant_icon from '../../assets/img/filter/elegant.jpg'
import new_icon from '../../assets/img/filter/icon-fabricFilter-new.jpg';
import premium_icon from '../../assets/img/filter/icon-fabricFilter-premium.jpg';
import luxury_icon from '../../assets/img/filter/luxury.jpg';
import sale_icon from '../../assets/img/filter/images-150x150.png';

import search_icon from '../../assets/img/icon/search.png';

const allProducts = {
    title: "All Products",
    products: [
        {
            id: 1,
            name: 'Product 1',
            price: 100,
            image: 'https://adongsilk.com/wp-content/uploads/2024/10/1176.270-black-herringbone-tailored-suits-in-hoi-an-150x150.jpg'
        },
        {
            id: 2,
            name: 'Product 2',
            price: 150,
            image: 'https://adongsilk.com/wp-content/uploads/2024/10/1176.270-black-herringbone-tailored-suits-in-hoi-an-150x150.jpg'
        },
        {
            id: 3,
            name: 'Product 3',
            price: 200,
            image: 'https://adongsilk.com/wp-content/uploads/2024/10/1176.270-black-herringbone-tailored-suits-in-hoi-an-150x150.jpg'
        },
        {
            id: 4,
            name: 'Product 4',
            price: 250,
            image: 'https://adongsilk.com/wp-content/uploads/2024/10/1176.270-black-herringbone-tailored-suits-in-hoi-an-150x150.jpg'
        },
    ],
}

const AllProductItems = ({ allProduct }) => {
    return (
        <div>
            {allProduct.products.map(product => (
                <div key={product.id}>
                    <img src={product.image} alt={product.name} />
                    <h3>{product.name}</h3>
                    <p>${product.price}</p>
                </div>
            ))}
        </div>
    );
}

// Updated component name in PropTypes
AllProductItems.propTypes = {
    allProduct: PropTypes.shape({
        title: PropTypes.string.isRequired,
        products: PropTypes.arrayOf(PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string.isRequired,
            price: PropTypes.number.isRequired,
            image: PropTypes.string.isRequired,
        })).isRequired,
    }).isRequired,
};

const CustomSuit = () => {
    return (
        <>

<nav>
        <Link to="/custom-suits/fabric">Fabric </Link>
        <Link to="/custom-suits/style">Style </Link>
        <Link to="/custom-suits/lining">Lining</Link>
      </nav>

        <div className="fabric-mix-sec">
            <div className="all-center">
                <div className="right-info">
                <div className="suit-smr-info">
  <div className="product-info-box" id="pd_info_box">
    <h1 className="pd-name">CUSTOM <span>SUIT</span></h1>
    <p className="composition set" data-price="270">
      <a href="https://adongsilk.com/list/60-wool-20-cashmere-20-synthetic/" rel="tag">
        60% Wool . 20% Cashmere . 20% Synthetic
      </a>
    </p>
    <p className="price"><span className="num">270</span> USD</p>
    <div className="frow" style={{ marginTop: '20px' }}>
      <a href="scustom-suit/style" className="btn primary-btn border-btn toggle-side-menu" id="mona-compleate-step">
        NEXT
      </a>
    </div>
  </div>
  <label className="radiolb">
    <input type="radio" name="viewdirection" value="front" defaultChecked />
    <span className="ip-avata"></span>
    <span>Front</span>
  </label>
  <label className="radiolb">
    <input type="radio" name="viewdirection" value="back" />
    <span className="ip-avata"></span>
    <span>Back</span>
  </label>
  <br />
  <br />
  <a href="javascript:;" className="toggle-hide-vest btn border-btn">Hide Vest</a>
</div>

                </div>
                <div className="left-cont">
                    <div className="side-menu menu-fabric" id='MnFabric'>
                        <div className="left-filter">
                        <ul className="filter-nenu">
  <li className="active" data-tog="e-all">
    <a href="javascript:;">
      <img src={all_icon} alt="all-filter" />
      ALL
    </a>
  </li>
  <li data-tog="mona-term-121">
    <a href="javascript:;">
      <img
        width="50"
        height="50"
        src={elegant_icon}
        className="attachment-thumbnail size-thumbnail"
        alt="elegant-filter"
      />
      ELEGANT
    </a>
  </li>
  <li data-tog="mona-term-122">
    <a href="javascript:;">
      <img
        width="50"
        height="50"
        src={luxury_icon}
        className="attachment-thumbnail size-thumbnail"
        alt="luxury-filter"
      />
      LUXURY
    </a>
  </li>
  <li data-tog="mona-term-40">
    <a href="javascript:;">
      <img
        width="50"
        height="50"
        src={new_icon}
        className="attachment-thumbnail size-thumbnail"
        alt="new-filter"
      />
      NEW
    </a>
  </li>
  <li data-tog="mona-term-42">
    <a href="javascript:;">
      <img
        width="50"
        height="50"
        src={premium_icon}
        className="attachment-thumbnail size-thumbnail"
        alt="premium-filter"
      />
      PREMIUM
    </a>
  </li>
  <li data-tog="mona-term-250">
    <a href="javascript:;">
      <img
        width="150"
        height="150"
        src={sale_icon}
        className="attachment-thumbnail size-thumbnail"
        alt="sale-filter"
        srcSet="https://adongsilk.com/wp-content/uploads/2020/01/images-150x150.png 150w, https://adongsilk.com/wp-content/uploads/2020/01/images-100x100.png 100w"
        sizes="(max-width: 150px) 100vw, 150px"
      />
      SALE
    </a>
  </li>
</ul>
                        </div>
                        <div className="right-menu">
                        <div className="opts-fabric" style={{ display: 'none' }}>
    <label className="radiolb">
        <input type="radio" name="mixtype" value="set" defaultChecked />
        <span className="ip-avata"></span> Set
    </label>
    <label className="radiolb">
        <input type="radio" name="mixtype" value="pant" />
        <span className="ip-avata"></span> Pant
    </label>
    <label className="radiolb">
        <input type="radio" name="mixtype" value="vest" />
        <span className="ip-avata"></span> Vest
    </label>
                        </div>
                        <div className="box-live-srch">
    <input type="text" id="live-search" />
    <span className="icon">
        <img src={search_icon} alt="Search icon" />
    </span>
                        </div>
                        <div className="list-fabric" style={{position: 'relative', height: '7464.8px'}}>
                            <div className="mona-term-122" style={{position: 'absolute', left: '0px', top: '0px'}}>
                                <div className="fabric-thumbnail toggle-vest active" data-source>
                                <div className='img mona-custom-thumbnail-wrap'>
                                    <AllProductItems allProduct={allProducts} />
                                </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            

            <div>
                <Outlet />
            </div>
        </>
    );
}

export default CustomSuit;
