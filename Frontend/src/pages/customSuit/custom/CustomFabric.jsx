import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { addToCart, clearCustomizationCache } from "../../../utils/cartUtil";
import { toast } from "react-toastify";
import "./CustomFabric.scss";

import all_icon from "../../../assets/img/filter/icon-fabricFilter-all.jpg";
import new_icon from "../../../assets/img/filter/icon-fabricFilter-new.jpg";
import premium_icon from "../../../assets/img/filter/icon-fabricFilter-premium.jpg";
import sale_icon from "../../../assets/img/filter/icon-fabricFilter-sale.png";
import search_icon from "../../../assets/img/icon/search.png";
import unavailable_img from "../../../assets/img/icon/unavailable.jpg";

const CustomFabric = () => {
  const [fabrics, setFabrics] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedTag, setSelectedTag] = useState("All");
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedFabric, setSelectedFabric] = useState(null);
  const navigate = useNavigate();

  const tagImage = {
    All: all_icon,
    New: new_icon,
    Premium: premium_icon,
    Sale: sale_icon,
  };

  const fetchFabrics = async (tag = "") => {
    try {
      const response = await fetch(
        `https://vesttour.xyz/api/Fabrics${tag ? `/tag/${tag}` : ""}`
      );
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();
      setFabrics(data);
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const initializeFabric = async () => {
      try {
        const response = await fetch("https://vesttour.xyz/api/Fabrics");
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setFabrics(data);

        // Automatically select the first fabric if none is selected
        if (!localStorage.getItem("selectedFabricID") && data.length > 0) {
          handleFabricClick(data[0]);
        }
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    initializeFabric();
  }, []);

  const handleTagClick = (tag) => {
    setSelectedTag(tag);
    setLoading(true);
    fetchFabrics(tag === "All" ? "" : tag);
  };

  const handleFabricClick = (fabric) => {
    if (selectedFabric && selectedFabric.fabricID === fabric.fabricID) return;

    // Check if the fabric is unavailable
    if (fabric.status === "Unavailable") {
      toast.error("Product is not available!");
      return;
    }

    setSelectedFabric(fabric);
    localStorage.setItem("selectedFabricID", fabric.fabricID);
    addToCart({
      id: fabric.fabricID,
      name: fabric.fabricName,
      price: fabric.price,
      imageUrl: fabric.imageUrl,
      type: "fabric"
    });
  };

  // Filter fabrics by search term
  const filteredFabrics = fabrics.filter((fabric) =>
    fabric.fabricName.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleNextClick = () => {
    if (!selectedFabric) {
      toast.error("Please select a fabric first");
      return;
    }

    // Clear tất cả style data khi chuyển sang trang style
    clearCustomizationCache();
    
    // Clear các state liên quan đến style trong localStorage
    localStorage.removeItem("styleOptionId");
    localStorage.removeItem("selectedStyles");
    localStorage.removeItem("selectedImages");
    localStorage.removeItem("selectedOptionValues");
    localStorage.removeItem("selectedOptions");

    // Chuyển đến trang style
    navigate("/custom-suits/style");
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div className="custom-fabric-container">
      <div className="left-half">
        <div className="tag-buttons">
          {["All", "New", "Premium", "Sale"].map((tag) => (
            <li
              key={tag}
              className={selectedTag === tag ? "active" : ""}
              onClick={() => handleTagClick(tag)}
            >
              <a>
                <img
                  src={tagImage[tag]}
                  alt={`${tag} icon`}
                  className="tag-icon"
                />
                {tag}
              </a>
            </li>
          ))}
        </div>

        <div className="right-items-fabric">
          <div className="search-box">
            <input
              id="live-search"
              type="text"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
            <span className="icon">
              <img src={search_icon} alt="Search" />
            </span>
          </div>

          <ul className="list-fabric">
            {filteredFabrics.length > 0 ? (
              filteredFabrics.map((fabric) => (
                <li
                  key={fabric.fabricID}
                  onClick={() => handleFabricClick(fabric)}
                  className={`fabric-item ${selectedFabric && selectedFabric.fabricID === fabric.fabricID ? "selected" : ""}`}
                >
                  <div className="fabric-img">
                    {fabric.imageUrl ? (
                      <img src={fabric.imageUrl} alt={fabric.fabricName} />
                    ) : (
                      <img src={unavailable_img} alt="Unavailable" />
                    )}
                  </div>
                  <div className="fabric-price">{fabric.price} USD</div>
                  <div className="fabric-name">{fabric.fabricName}</div>
                </li>
              ))
            ) : (
              <li>No fabrics match your search.</li>
            )}
          </ul>
        </div>
      </div>

      <div className="right-half">
        {selectedFabric && (
          <div className="fabric-details">
            <div className="product-info" id="pd_info">
              <h1 className="pd-name">
                CUSTOM
                <span>SUIT</span>
              </h1>
              <p className="composition set">{selectedFabric.description}</p>
              <p className="price">{selectedFabric.price} USD</p>
              <div className="fabric-img">
                {selectedFabric.imageUrl ? (
                  <img
                    src={selectedFabric.imageUrl}
                    alt={selectedFabric.fabricName}
                  />
                ) : (
                  <img src={unavailable_img} alt="Unavailable" />
                )}
              </div>
            </div>
          </div>
        )}

        <div className="next-btn">
          <button
            className='navigation-button'
            onClick={handleNextClick}
          >
            Style
          </button>
        </div>
      </div>
    </div>
  );
};

export default CustomFabric;