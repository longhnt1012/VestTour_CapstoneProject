namespace VestTour.Repository.Models
{
    public class MeasurementModel
    {
        public int MeasurementId { get; set; }

        public int? UserId { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Height { get; set; }

        public decimal? Neck { get; set; }

        public decimal? Hip { get; set; }

        public decimal? Waist { get; set; }

        public decimal? Armhole { get; set; }

        public decimal? Biceps { get; set; }

        public decimal? PantsWaist { get; set; }

        public decimal? Crotch { get; set; }

        public decimal? Thigh { get; set; }

        public decimal? PantsLength { get; set; }

    }
}
