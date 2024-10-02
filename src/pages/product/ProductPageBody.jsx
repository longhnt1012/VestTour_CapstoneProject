import '../product/ProductPageBody.scss';


export const ProductPageBody = () => {
  return (
    <>
    {/* Header promotion */}
    <div className="header-promotion">
  <div className="header-promotion__slide slick-initialized slick-slider slick-vertical">
    <div className="slick-list draggable">
      <div className="slick-track">
        <div className="slick-slide slick-current slick-active" data-slick-index="0" aria-hidden="false">
          <div>
            <div className="header-promotion__slide-item">
              <p>
                VEST COLLECTION - 
                <span>
                  <strong>
                    <a href="#">BUY NOW</a>
                  </strong>
                </span>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>


    <div className="banner-container">
  <img src="https://owen.vn/media/catalog/category/veston_2.jpg" className="banner-image" alt="Áo Vest Nam" />
  <div className="banner-category">
    <h1 className="banner-title">
      Áo Vest Nam
    </h1>
    <div className="banner-description">
      Áo vest nam đẹp, cập nhật phong cách theo xu hướng mới nhất, được sản xuất từ những chất liệu cao cấp của OWEN mang đến cho các quý ông một phong cách lịch lãm
    </div>
  </div>
</div>

    {/* main container */}
    <div id="maincontent" className="page-main">
      <span id="contentarea" tabIndex={-1}></span>
      <div className="page messages">
        <div data-placeholder="messages"></div>
        <div data-bind="scope: 'messages'"></div>
      </div>
      <div className="columns">
        <div className="row-3 sidebar-catalog">
  <nav className="navigation-left">
    <ul>
      <li className="level0 nav-1 category-item first level-top">
        <a href="#" className="level-top">
          <span>NEW ARRIVALS</span>
        </a>
      </li>
      <li className="level0 nav-2 category-item level-top parent">
        <a href="#" className="level-top">
          <span>COLLECTIONS</span>
        </a>
        <ul className="level0 submenu">
          <li className="level1 nav-2-1 category-item first">
            <a href="#">
              <span>KAIZEN</span>
            </a>
          </li>
          <li className="level1 nav-2-2 category-item">
            <a href="#">
              <span>BE CONFIDENT</span>
            </a>
          </li>
          <li className="level1 nav-2-3 category-item">
            <a href="#">
              <span>INNER RISE</span>
            </a>
          </li>
          <li className="level1 nav-2-4 category-item">
            <a href="#">
              <span>INSPIRED BY THE CITY</span>
            </a>
          </li>
          <li className="level1 nav-2-5 category-item">
            <a href="#">
              <span>JOY THE BEAT</span>
            </a>
          </li>
          <li className="level1 nav-2-6 category-item">
            <a href="#">
              <span>BAC HA COLLECTION</span>
            </a>
          </li>
          <li className="level1 nav-2-7 category-item last">
            <a href="#">
              <span>RENU</span>
            </a>
          </li>
        </ul>
      </li>
      <li className="level0 nav-3 category-item level-top">
        <a href="#" className="level-top">
          <span>RETRO VACATION COLLECTION</span>
        </a>
      </li>
      <li className="level0 nav-4 category-item level-top parent">
        <a href="#" className="level-top">
          <span>GOOD PRICE</span>
        </a>
        <ul className="level0 submenu">
          <li className="level1 nav-4-1 category-item first">
            <a href="#">
              <span>Polo - Tshirt</span>
            </a>
          </li>
          <li className="level1 nav-4-2 category-item">
            <a href="#">
              <span>Good Price Pants</span>
            </a>
          </li>
          <li className="level1 nav-4-3 category-item last">
            <a href="#">
              <span>Shirts</span>
            </a>
          </li>
        </ul>
      </li>
      <li className="level0 nav-5 category-item level-top">
        <a href="#" className="level-top">
          <span>Coffee Polo Shirt</span>
        </a>
      </li>
      <li className="level0 nav-6 category-item has-active level-top parent">
        <a href="#" className="level-top">
          <span>SHIRTS</span>
        </a>
        <ul className="level0 submenu">
          <li className="level1 nav-6-1 category-item first">
            <a href="#">
              <span>Polo Shirt</span>
            </a>
          </li>
          <li className="level1 nav-6-2 category-item">
            <a href="#">
              <span>Shirts</span>
            </a>
          </li>
          <li className="level1 nav-6-3 category-item">
            <a href="#">
              <span>T-Shirt</span>
            </a>
          </li>
          <li className="level1 nav-6-4 category-item">
            <a href="#">
              <span>Blazer</span>
            </a>
          </li>
          <li className="level1 nav-6-5 category-item active">
            <a href="#">
              <span>Veston</span>
            </a>
          </li>
          <li className="level1 nav-6-6 category-item">
            <a href="#">
              <span>Jacket</span>
            </a>
          </li>
          <li className="level1 nav-6-7 category-item">
            <a href="#">
              <span>Sweaters</span>
            </a>
          </li>
          <li className="level1 nav-6-8 category-item">
            <a href="#">
              <span>Premium Shirts</span>
            </a>
          </li>
          <li className="level1 nav-6-9 category-item parent">
            <a href="#">
              <span>Hoodies</span>
            </a>
            <ul className="level1 submenu">
              <li className="level2 nav-6-9-1 category-item first last">
                <a href="#">
                  <span>T-Shirts</span>
                </a>
              </li>
            </ul>
          </li>
          <li className="level1 nav-6-10 category-item last">
            <a href="#">
              <span>Blazer</span>
            </a>
          </li>
        </ul>
      </li>
      <li className="level0 nav-7 category-item level-top parent">
        <a href="#" className="level-top">
          <span>PANTS</span>
        </a>
        <ul className="level0 submenu">
          <li className="level1 nav-7-1 category-item first">
            <a href="#">
              <span>Shorts</span>
            </a>
          </li>
          <li className="level1 nav-7-2 category-item">
            <a href="#">
              <span>Trousers</span>
            </a>
          </li>
          <li className="level1 nav-7-3 category-item">
            <a href="#">
              <span>Khaki Pants</span>
            </a>
          </li>
          <li className="level1 nav-7-4 category-item">
            <a href="#">
              <span>Jeans</span>
            </a>
          </li>
          <li className="level1 nav-7-5 category-item last">
            <a href="#">
              <span>Joggers</span>
            </a>
          </li>
        </ul>
      </li>
      <li className="level0 nav-8 category-item level-top parent">
        <a href="#" className="level-top">
          <span>ACCESSORIES</span>
        </a>
        <ul className="level0 submenu">
          <li className="level1 nav-8-1 category-item first">
            <a href="#">
              <span>Underwear</span>
            </a>
          </li>
          <li className="level1 nav-8-2 category-item">
            <a href="#">
              <span>Socks</span>
            </a>
          </li>
          <li className="level1 nav-8-3 category-item">
            <a href="#">
              <span>Belts</span>
            </a>
          </li>
          <li className="level1 nav-8-4 category-item">
            <a href="#">
              <span>Ties</span>
            </a>
          </li>
          <li className="level1 nav-8-5 category-item">
            <a href="#">
              <span>Hats</span>
            </a>
          </li>
          <li className="level1 nav-8-6 category-item last">
            <a href="#">
              <span>Handkerchiefs</span>
            </a>
          </li>
        </ul>
      </li>
    </ul>
  </nav>
</div>
<div className="row-9 main">
          {/* item */}
          {/* sidebar sidebar-main */}
          <div id="amasty-shopby-product-list">
            {/* <div className="toolbar toolbar-products"></div> */}
            {/* script */}
            <div className="products wrapper grid products-grid">
              <ol className="products list items products-items">
                <li className="item product product-item">
                  <div className="product-item-info" id="product-item-info_414643"
                  data-container="product-grid">
                    <a href="#" className='product photo product-item-photo' tabIndex={-1}>
                      <span className="product-image-container product-image-container-41463">
                        <span className="product-image-wrapper">
                          <img src="https://owen.cdn.vccloud.vn/media/catalog/product/cache/01755127bd64f5dde3182fd2f139143a/v/e/ves231494._40.jpg" alt="Bộ Veston - VES231494" className="product-image-photo" />
                        </span>
                      </span>
                    </a>
                    <div className="product details product-item-details">
                      <div data-role="add-to-links" className="actions-secondary">
                        <a href="#" className="action towishlist" title="Yêu thích"
                        aria-label='Yêu thích' 
                        data-post="{&quot;action&quot;:&quot;https:\/\/owen.vn\/wishlist\/index\/add\/&quot;,&quot;data&quot;:{&quot;product&quot;:41463,&quot;uenc&quot;:&quot;aHR0cHM6Ly9vd2VuLnZuL2FvL3Zlc3Rvbi5odG1s&quot;}}"
                        data-action="add-to-wishlist" role='button'>
                          <span>Yêu thích</span>
                        </a>
                      </div>
                      <strong className="product name product-item-name">
                        <a href="#" className="product-item-link">Veston - VES231494</a>
                      </strong>
                      <div className="price-box price-final_price"
                      data-role="priceBox" data-product-id="41463" data-price-box="product-id-41463">
                        <span className="normal-price">
                          <span className="price-container price-final_price tax weee">
                            <span className="price-label">As low as</span>
                            <span id="product-price-41463"
                            data-price-amount="3500000" data-price-type="finalPrice"
                            className='price-wrapper'>
                              <span className="price">3.500.000&nbsp;₫</span>
                            </span>
                          </span>
                        </span>
                        <div className="product-item-percent product-item-percent--listing"></div>
                      </div>
                      <div className="swatch-opt-41463" data-role="swatch-option-41463" data-rendered="true"></div>
                    </div>
                  </div>
                </li>
              </ol>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    </>
  )
}
