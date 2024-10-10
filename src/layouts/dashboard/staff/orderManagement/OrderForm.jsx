import React, { useState } from 'react';

const OrderForm = ({ onSubmit }) => {
  // ... rest of the code
};

OrderForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
};
  const [customerName, setCustomerName] = useState('');
  const [productName, setProductName] = useState('');
  const [fabric, setFabric] = useState('');
  const [measurement, setMeasurement] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit({ customerName, productName, fabric, measurement });
    // Reset form after submission
    setCustomerName('');
    setProductName('');
    setFabric('');
    setMeasurement('');
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Customer Name:
        <input
          type="text"
          value={customerName}
          onChange={(e) => setCustomerName(e.target.value)}
        />
      </label>
      <label>
        Product:
        <input
          type="text"
          value={productName}
          onChange={(e) => setProductName(e.target.value)}
        />
      </label>
      <label>
        Fabric:
        <input
          type="text"
          value={fabric}
          onChange={(e) => setFabric(e.target.value)}
        />
      </label>
      <label>
        Measurement:
        <input
          type="text"
          value={measurement}
          onChange={(e) => setMeasurement(e.target.value)}
        />
      </label>
      <button type="submit">Submit Order</button>
    </form>
  );
};

export default OrderForm;
