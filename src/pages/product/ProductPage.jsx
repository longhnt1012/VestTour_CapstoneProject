import { Navigation } from "../../layouts/components/navigation/Navigation"
import { Footer } from "../../layouts/components/footer/Footer"
import { ProductPageBody } from "./ProductPageBody"

export const ProductPage = () => {
  return (
    <>
      <Navigation />
      <ProductPageBody/>
      <Footer />
    </>
  )
}
