using System.Text.RegularExpressions;

namespace VestTour.ValidationHelpers
{
    public class StringValidate
    {
        public static bool IsValidProductName(string productCode)
        {
            string productPattern = @"^PR\d{3}$";
            return Regex.IsMatch(productCode, productPattern);
        }

        public static bool IsValidVoucherCode(string voucherCode) {
            string voucherPattern = @"^(FREESHIP\d{2}|BIGSALE\d{2})$";
            return Regex.IsMatch(voucherCode, voucherPattern);
        }
    }
}
