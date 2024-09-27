import BookingPage from "../pages/booking/BookingPage";
import { ProductPage } from "../pages/product/productPage";
import { HomePage } from "../pages/home/homePage";
import SignIn from "../pages/signIn/signIn";
import SignUp from "../pages/signUp/signUp";

export const routes = [
    {
        path: "/",
        element: <HomePage />,
        exact: true
    },
    {
        path: "/booking",
        element: <BookingPage />
    },
    {
        path: "/products",
        element: <ProductPage />
    },
    {
        path:"/signin",
        element: <SignIn />
    },
    {
        path:"/signup",
        element: <SignUp />
    }
]
