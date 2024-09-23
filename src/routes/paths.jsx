import { HomePage } from "../pages/home/HomePage";
import BookingPage from "../pages/booking/BookingPage";

export const routes = [
  {
    path: "/",
    element: <HomePage />,
    exact: true,
  },
  {
    path: "/booking",
    element: <BookingPage />,
  },
];
