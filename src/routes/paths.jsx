import BookingPage from "../pages/booking/BookingPage";
import ProductPage from "../pages/product/ProductPage";
import HomePage from "../pages/home/HomePage"; 
import SignIn from "../pages/signIn/signIn";
import SignUp from "../pages/signUp/signUp";
import Checkout from "../pages/checkout/Checkout";
import Fabric from "../pages/fabric/Fabric";
import CustomSuit from "../pages/customsuit/CustomSuit";
import CustomFabric from "../pages/customSuit/custom/CustomFabric";
import CustomStyle from "../pages/customSuit/custom/CustomStyle";
import CustomLining from "../pages/customSuit/custom/CustomLining";

export const routes = [
    {
        path: "/",
        element: <HomePage />
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
        path: "/checkout",
        element: <Checkout />
    },
    {
        path: "/fabric",
        element: <Fabric />
    },
    {
        path:"/signin",
        element: <SignIn />
    },
    {
        path:"/signup",
        element: <SignUp />
    },
    {
        path: "/custom-suits",
        element: <CustomSuit />,
        children: [
            {
                path: "fabric",
                element: <CustomFabric />
            },
            {
                path: "style",
                element: <CustomStyle />
            },
            {
                path: "lining",
                element: <CustomLining />
            }
        ]
    }
];
