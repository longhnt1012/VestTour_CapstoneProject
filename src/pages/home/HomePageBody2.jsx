import '../../assets/scss/HomePageBody2.scss'; 

import services1 from '../../assets/img/icon/services1.svg'; 
import services2 from '../../assets/img/icon/services2.svg';
import services3 from '../../assets/img/icon/services3.svg';
import services4 from '../../assets/img/icon/services4.svg';
export const HomePageBody2 = () => {
  return (
        //tailor services
        <section className="categories-area section-padding40">
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-xl-6 col-lg-6 col-md-7 col-sm-9">
                    <div className="section-tittle text-center mb-60">
                        <h2>Our service?</h2>
                        <p>Gioi thieu services.</p>
                    </div>
                </div>
            </div>
            <div className="row">
                <div className="col-lg-3 col-md-6 col-sm-6">
                    <div className="single-cat mb-50 wow fadeInUp" data-wow-duration="1s" data-wow-delay=".2s">
                        <div className="cat-icon">
                            <img src={services1}alt=""/>
                        </div>
                        <div className="cat-cap">
                            <h5>Service 1</h5>
                            <p>Service 1 details.</p>
                        </div>
                    </div>
                </div>
                <div className="col-lg-3 col-md-6 col-sm-6">
                    <div className="single-cat mb-50 wow fadeInUp" data-wow-duration="1s" data-wow-delay=".2s">
                        <div className="cat-icon">
                            <img src={services2} alt=""/>
                        </div>
                        <div className="cat-cap">
                            <h5>Service 2</h5>
                            <p>Service 2 details.</p>
                        </div>
                    </div>
                </div>
                <div className="col-lg-3 col-md-6 col-sm-6">
                    <div className="single-cat mb-50 wow fadeInUp" data-wow-duration="1s" data-wow-delay=".4s">
                        <div className="cat-icon">
                            <img src={services3} alt=""/>
                        </div>
                        <div className="cat-cap">
                            <h5>Service 3</h5>
                            <p>Service 3 details.</p>
                        </div>
                    </div>
                </div>
                <div className="col-lg-3 col-md-6 col-sm-6">
                    <div className="single-cat mb-50 wow fadeInUp" data-wow-duration="1s" data-wow-delay=".5s">
                        <div className="cat-icon">
                            <img src={services4} alt=""/>
                        </div>
                        <div className="cat-cap">
                            <h5>Service 4</h5>
                            <p>Service 4 details.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
  )
}
