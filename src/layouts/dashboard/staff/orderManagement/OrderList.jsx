import React from 'react';
import PropTypes from 'prop-types';

const OrderList = ({ orders }) => {
  // ... rest of the code
};

OrderList.propTypes = {
  orders: PropTypes.arrayOf(PropTypes.shape({
    id: PropTypes.number.isRequired,
    customerName: PropTypes.string.isRequired,
    productName: PropTypes.string.isRequired,
    status: PropTypes.string.isRequired,
  })).isRequired,
};


export default OrderList;
