using System.Text.RegularExpressions;

namespace VestTour.ValidationHelpers
{
    public class StringValidate
    {
        
        public static bool IsValidVoucherCode(string voucherCode) {
            string voucherPattern = @"^(FREESHIP\d{2}|BIGSALE\d{2})$";
            return Regex.IsMatch(voucherCode, voucherPattern);
        }
    }
}
