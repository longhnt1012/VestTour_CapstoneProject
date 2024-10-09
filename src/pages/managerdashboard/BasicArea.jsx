import * as React from "react";
import { LineChart, BarChart, Title } from "@mui/x-charts";

export default function BasicArea() {
  const data = [
    { month: "Jan", teamA: 30, teamB: 40, teamC: 20 },
    { month: "Feb", teamA: 40, teamB: 45, teamC: 30 },
    { month: "Mar", teamA: 50, teamB: 30, teamC: 35 },
    { month: "Apr", teamA: 60, teamB: 55, teamC: 40 },
    { month: "May", teamA: 70, teamB: 65, teamC: 55 },
    { month: "Jun", teamA: 80, teamB: 70, teamC: 60 },
  ];

  return (
    <div
      style={{
        padding: "20px",
        background: "white",
        borderRadius: "8px",
        boxShadow: "0 2px 10px rgba(0, 0, 0, 0.1)",
      }}
    >
      <Title>Website Visits (+43% than last year)</Title>
      <LineChart
        data={data}
        series={[
          {
            id: "Team A",
            data: data.map((item) => item.teamA),
            color: "blue",
            area: true,
          },
          {
            id: "Team B",
            data: data.map((item) => item.teamB),
            color: "orange",
            area: true,
          },
          {
            id: "Team C",
            data: data.map((item) => item.teamC),
            color: "yellow",
            area: true,
          },
        ]}
        xAxis={[{ data: data.map((item) => item.month) }]}
        yAxis={[{ title: "Visits" }]}
        width={600}
        height={300}
      />
      <BarChart
        data={data}
        series={[
          {
            id: "Visits",
            data: data.map((item) => item.teamC),
            color: "lightblue",
          },
        ]}
        xAxis={[{ data: data.map((item) => item.month) }]}
        yAxis={[{ title: "Visits" }]}
        height={100}
      />
    </div>
  );
}
