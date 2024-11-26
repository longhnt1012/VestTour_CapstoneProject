import React from "react";
import { PieChart } from "@mui/x-charts";

const CurrentVisits = () => {
  const data = [
    { country: "America", visits: 284 },
    { country: "Asia", visits: 277 },
    { country: "Europe", visits: 92 },
    { country: "Africa", visits: 347 },
  ];

  const totalVisits = data.reduce((sum, item) => sum + item.visits, 0);

  const chartData = data.map((item) => ({
    id: item.country,
    value: item.visits,
    percentage: ((item.visits / totalVisits) * 100).toFixed(1),
  }));

  return (
    <div className="current-visits-chart">
      <PieChart
        data={chartData}
        series={[{ id: "visits", data: chartData }]}
        colors={["#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0"]}
        width={300}
        height={300}
        label={({ datum }) => `${datum.percentage}%`} // Display percentage on each segment
        labelPosition="middle" // Ensure labels are positioned in the middle of segments
      />
      <div className="legends">
        {data.map((item, index) => (
          <div key={index} style={{ display: "flex", alignItems: "center" }}>
            <div
              style={{
                width: "12px",
                height: "12px",
                backgroundColor: ["#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0"][
                  index
                ],
                marginRight: "5px",
              }}
            />
            <span>
              {item.country}: {((item.visits / totalVisits) * 100).toFixed(1)}%
            </span>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CurrentVisits;
