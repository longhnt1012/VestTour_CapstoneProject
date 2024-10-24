﻿namespace VestTour.Repository.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public int? MeasurementID { get; set; }
        public int? CategoryID { get; set; }
        public int? FabricID { get; set; }
        public int? LiningID { get; set; }
        public int? OrderID { get; set; }

        public bool IsCustom { get; set; }
        public string? ImgURL { get; set; }
        public decimal? Price { get; set; }
    }

}
