import React, { useState } from 'react';
import './Fabric.scss'; // Assuming you have a SCSS file for styling
import { Navigation } from "../../layouts/components/navigation/Navigation";
import { Footer } from "../../layouts/components/footer/Footer";

const fabricCollections = {
  justSanded: [
    { name: 'Just Sanded', image: 'https://fabriccollection.com.au/cdn/shop/files/linen-bubblegum-fabric-ella-collection_1800x1800.jpg?v=1690353098', price: '$45.99/m' },
    { name: 'Vibrant Pink', image: 'https://fabriccollection.com.au/cdn/shop/files/linen-jade-fabric-ella-collection_540x.jpg?v=1690353252', price: '$39.99/m' },
    { name: 'Pastel Blue', image: 'https://fabriccollection.com.au/cdn/shop/files/linen-frappe-fabric-ella-collection_1080x.jpg?v=1690350706', price: '$42.99/m' },
    { name: 'Sober', image: 'https://fabriccollection.com.au/cdn/shop/files/linen-cyber-lime-fabric-ella-collection_1080x.jpg?v=1690353209', price: '$47.99/m' },
  ],
  trending: [
    { name: 'Silk Floral', image: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-1_540x.jpg?v=1682633455', detailImage: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-2_540x.jpg?v=1682633460', price: '$55.99/m' },
    { name: 'Silk Floral', image: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-1_540x.jpg?v=1682633455', detailImage: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-2_540x.jpg?v=1682633460', price: '$55.99/m' },
    { name: 'Silk Floral', image: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-1_540x.jpg?v=1682633455', detailImage: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-2_540x.jpg?v=1682633460', price: '$55.99/m' },
    { name: 'Cotton Blend', image: 'https://fabriccollection.com.au/cdn/shop/products/foglia-printed-italian-linen_1080x.jpg?v=1674598292', detailImage: 'https://fabriccollection.com.au/cdn/shop/products/foglia-italian-printed-linen-2_540x.jpg?v=1674598292', price: '$49.99/m' },
  ],
  bambooKnit: [
    { name: 'Bamboo Knit 1', image: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink1_540x.jpg?v=1642830190', detailImage: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink2_540x.jpg?v=1642830191', price: '$35.99/m' },
    { name: 'Bamboo Knit 2', image: 'https://fabriccollection.com.au/cdn/shop/products/bamboo-jersey-navy-1_540x.jpg?v=1679698370', detailImage: 'https://fabriccollection.com.au/cdn/shop/products/bamboo-jersey-navy-2_540x.jpg?v=1679698374', price: '$37.99/m' },
    { name: 'Silk Floral', image: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-1_540x.jpg?v=1682633455', detailImage: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-2_540x.jpg?v=1682633460', price: '$55.99/m' },
    { name: 'Silk Floral', image: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-1_540x.jpg?v=1682633455', detailImage: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-2_540x.jpg?v=1682633460', price: '$55.99/m' },
  ],
  incredible: [
    { name: 'Italian Fabric', image: 'https://fabriccollection.com.au/cdn/shop/files/linen-cyber-lime-fabric-ella-collection_1080x.jpg?v=1690353209', detailImage: 'https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-1_540x.jpg?v=1682633455', price: '$47.99/m' },
    { name: 'Bamboo Knit 1', image: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink1_540x.jpg?v=1642830190', detailImage: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink2_540x.jpg?v=1642830191', price: '$35.99/m' },
    { name: 'Bamboo Knit 1', image: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink1_540x.jpg?v=1642830190', detailImage: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink2_540x.jpg?v=1642830191', price: '$35.99/m' },
    { name: 'Bamboo Knit 1', image: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink1_540x.jpg?v=1642830190', detailImage: 'https://fabriccollection.com.au/cdn/shop/products/BambooJerseyhotpink2_540x.jpg?v=1642830191', price: '$35.99/m' },
  ],
};

const Fabric = () => {
  return (
    <div className="fabric-collection">
      <header className="header-image animate-header">
        <h1>Luxury Linen Fabrics</h1>
        <p>For Fashion Designers, Modern Dressmakers, and Sewing Enthusiasts</p>
      </header>
      <div className="headline-container">

<h2 className="headline">Welcome to Our Fabric Collection</h2>

<p className="subheadline">Discover the finest fabrics for your next sewing project</p>

</div>
      <Section title="Just Landed" fabrics={fabricCollections.justSanded} isHoverDisabled={true} />
      <Section title="TRENDING FABRICS PICKED FOR YOU" fabrics={fabricCollections.trending} />
      <Section title="For Our Bamboo Knit Lovers" fabrics={fabricCollections.bambooKnit} />
      <Section title="Incredible Fabrics for Incredible Sewists" fabrics={fabricCollections.incredible} />
      
      <FeaturedSection 
        title="SANDWASHED PURE LINEN" 
        subtitle="At Fabric Collection, linen is in our DNA."
        description="Our range of beautiful sandwashed linens are pre-washed to create softness with a relaxed, unique texture finish. This eco-friendly and versatile fabric is perfect for creating bespoke garments, whether dresses, pants, shorts, skirts, jumpsuits, tops, jackets or even some lightweight home decor. Sandwashed linen becomes softer after each wash, so there's no need to iron!"
        imageSrc="https://fabriccollection.com.au/cdn/shop/files/linen-bubblegum-fabric-ella-collection_1800x1800.jpg?v=1690353098"
        buttonText="SHOP NOW"
      />
      
      <FeaturedSection 
        title="PURE PRINTED LINEN" 
        subtitle="At Fabric Collection, we love linen!"
        description="Our wide-ranging yet carefully curated, high-quality Linen Collection is hand-picked every season, offering the style-savvy sewist an intriguing palette of colours in every shade you could imagine. Whatever your sewing project, we have an enormous array of linen prints and coordinating plains to match – all available at our beautiful Summer store in Brisbane and online fabric store – ready for quick delivery Australia-wide."
        imageSrc="https://fabriccollection.com.au/cdn/shop/files/printed-linen-fabric-caruso-1_540x.jpg?v=1682633455"
        buttonText="SHOP NOW"
        imageOnRight={true}
      />
    </div>
  );
};

const Section = ({ title, fabrics, isHoverDisabled }) => {
  return (
    <div className={`section animate-section`}>
      <h2>{title}</h2>
      <div className="fabric-grid">
        {fabrics.map((fabric, index) => (
          <FabricItem key={index} fabric={fabric} isHoverDisabled={isHoverDisabled} />
        ))}
      </div>
    </div>
  );
};

const FabricItem = ({ fabric, isHoverDisabled }) => {
  const [isHovered, setIsHovered] = useState(false);

  return (
    <div
      className="fabric-item"
      onMouseEnter={() => !isHoverDisabled && setIsHovered(true)}
      onMouseLeave={() => !isHoverDisabled && setIsHovered(false)}
    >
      <img src={isHovered && fabric.detailImage ? fabric.detailImage : fabric.image} alt={fabric.name} />
      <p>{fabric.name}</p>
      <p className="price">{fabric.price}</p>
    </div>
  );
};

const FeaturedSection = ({ title, subtitle, description, imageSrc, buttonText, imageOnRight }) => {
  return (
    <div className={`featured-section ${imageOnRight ? 'image-right' : 'image-left'}`}>
      <div className="content">
        <h2>{title}</h2>
        <h3>{subtitle}</h3>
        <p>{description}</p>
        <button>{buttonText}</button>
      </div>
      <div className="image">
        <img src={imageSrc} alt={title} />
      </div>
    </div>
  );
};

export default Fabric;