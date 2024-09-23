import { HomePage } from "../pages/home/HomePage";
import BookingPage from "../pages/booking/BookingPage";
import SignIn from "../pages/signIn/signIn";
import SignUp from "../pages/signUp/signUp";
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
  {
    path: "/signup",
    element: <SignUp />,
  },
];
