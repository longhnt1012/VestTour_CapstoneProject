import { BookingPage } from "../pages/booking/BookingPage";
import { ProductPage } from "../pages/product/productPage";
import { HomePage } from "../pages/home/homePage";

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
    }
]
