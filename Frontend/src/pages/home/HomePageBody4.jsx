
import '../home/HomePageBody4.scss';

// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import { faTripadvisor } from '@fortawesome/free-brands-svg-icons';

export const HomePageBody4 = () => {
  return (
    <>
    {/* link-info */}
    <section className='kpart sec k-sec'>
        <div className="decor-sec">
            <img src="https://adongsilk.com/template/images/review-decor.png" alt="" />
        </div>
        <div className="all">
            <div className="kpart-warp">
                <div className="kpart-main">
                    <div className='kpart-slide kpart-slide-js slick-initialized slick-slider' id='kpart-slide-js'>
                        <div className="slick-list draggable">
                            <div className="slick-track-banner">
                                <div className="slider-item slick-slide slick-current slick-active" data-slick-index="0" aria-hidden="false" style={{ width: '100%' }} tabIndex="0">
                                    <div className="kpart-item">
                                        <a href="https://www.trustpilot.com/review/adongsilk.com" target="_blank" rel="noopener noreferrer" tabIndex="0">
                                        <img src="https://adongsilk.com/wp-content/uploads/2024/03/ADS_Trustpilot.png" alt="" />
                                        </a>
                                    </div>
                                </div>
                                <div className="slider-item slick-slide slick-current slick-active" data-slick-index="0" aria-hidden="false" style={{ width: '100%' }} tabIndex="0">
                                    <div className="kpart-item">
                                        <a href="https://www.trustpilot.com/review/adongsilk.com" target="_blank" rel="noopener noreferrer" tabIndex="0">
                                        <img src="https://adongsilk.com/wp-content/uploads/2024/03/Tripadvisor.png" alt="" />
                                        </a>
                                    </div>
                                </div>
                                <div className="slider-item slick-slide slick-current slick-active" data-slick-index="0" aria-hidden="false" style={{ width: '100%' }} tabIndex="0">
                                    <div className="kpart-item">
                                        <a href="https://www.trustpilot.com/review/adongsilk.com" target="_blank" rel="noopener noreferrer" tabIndex="0">
                                        <img src="https://adongsilk.com/wp-content/uploads/2024/03/ADS_Google.png" alt="" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    {/* feedback */}
    <section className='fback sec'>
        <div className="decor-sec">
            <img src="https://adongsilk.com/template/images/review-decor.png" alt="" />
        </div>
        <div className="fback-content">
            <div className="all">
                <div className="fback-wrap">
                    <div className="kpart-top">
                        <div className="sec-title k-title">
                            <h2 className='tt-txt'>
                                <span className='tt-sub'>
                                    CUSTOMERS
                                </span>
                                FEEDBACk
                            </h2>
                        </div>
                    </div>
                    {/* feedback here */}
                    <div className="fback-main">
                        <div>FEEDBACK HERE</div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    </>
    
  )
}
