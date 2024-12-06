using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Service.Services
{
    public class ZodiacService
    {
        public (string ZodiacSign, List<string> Colors) GetZodiacSignAndColors(DateOnly birthDate)
        {
            var zodiacSigns = new List<(string Sign, DateOnly StartDate, DateOnly EndDate, List<string> Colors)>
        {
            ("Capricorn", new DateOnly(1, 12, 22), new DateOnly(1, 1, 19), new List<string> { "Yellow", "Green", "Red", "White", "Black" }),
            ("Aquarius", new DateOnly(1, 1, 20), new DateOnly(1, 2, 18), new List<string> { "Blue", "Pink", "Yellow", "White" }),
            ("Pisces", new DateOnly(1, 2, 19), new DateOnly(1, 3, 20), new List<string> { "Green", "Yellow" }),
            ("Aries", new DateOnly(1, 3, 21), new DateOnly(1, 4, 20), new List<string> { "Red", "Pink", "Coral" }),
            ("Taurus", new DateOnly(1, 4, 21), new DateOnly(1, 5, 20), new List<string> { "Pink", "Green", "Yellow", "White" }),
            ("Gemini", new DateOnly(1, 5, 21), new DateOnly(1, 6, 21), new List<string> { "White", "Yellow", "Green" }),
            ("Cancer", new DateOnly(1, 6, 22), new DateOnly(1, 7, 22), new List<string> { "Silver", "Pearl Blue", "Emerald" }),
            ("Leo", new DateOnly(1, 7, 23), new DateOnly(1, 8, 22), new List<string> { "Pink", "Red" }),
            ("Virgo", new DateOnly(1, 8, 23), new DateOnly(1, 9, 22), new List<string> { "White", "Blue" }),
            ("Libra", new DateOnly(1, 9, 23), new DateOnly(1, 10, 23), new List<string> { "Pink", "Purple", "Blue" }),
            ("Scorpio", new DateOnly(1, 10, 24), new DateOnly(1, 11, 21), new List<string> { "Yellow", "Black", "Dark Purple", "Blue-Green" }),
            ("Sagittarius", new DateOnly(1, 11, 22), new DateOnly(1, 12, 21), new List<string> { "Red", "Green", "Yellow", "Orange", "Purple" })
        };

            foreach (var zodiac in zodiacSigns)
            {
                var adjustedStart = new DateOnly(birthDate.Year, zodiac.StartDate.Month, zodiac.StartDate.Day);
                var adjustedEnd = new DateOnly(birthDate.Year, zodiac.EndDate.Month, zodiac.EndDate.Day);

                if (zodiac.StartDate.Month > zodiac.EndDate.Month) // Handle signs spanning across years
                {
                    adjustedEnd = adjustedEnd.AddYears(1);
                }

                if (birthDate >= adjustedStart && birthDate <= adjustedEnd)
                {
                    return (zodiac.Sign, zodiac.Colors);
                }
            }

            throw new Exception("Could not determine zodiac sign.");
        }
    }

}
