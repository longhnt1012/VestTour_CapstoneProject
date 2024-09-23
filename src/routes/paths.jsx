import { HomePage } from "../pages/home/HomePage";
import BookingPage from "../pages/booking/BookingPage";
import SignIn from "../pages/signIn/signIn";
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
  {
    path: "/signin",
    element: <SignIn />,
  },
];
