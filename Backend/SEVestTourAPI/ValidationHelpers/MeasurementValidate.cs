using System;

namespace SEVestTourAPI.ValidationHelpers
{
    public static class MeasurementValidate
    {
        public static bool IsValidWeight(double weight)
        {
            return weight > 0 && weight <= 300; 
        }

        public static bool IsValidHeight(double height)
        {
            return height > 0 && height <= 250; 
        }

        public static bool IsValidNeck(double neck)
        {
            return neck > 0 && neck <= 60; 
        }

        public static bool IsValidHip(double hip)
        {
            return hip > 0 && hip <= 150; 
        }

        public static bool IsValidWaist(double waist)
        {
            return waist > 0 && waist <= 150; 
        }

        public static bool IsValidArmhole(double armhole)
        {
            return armhole > 0 && armhole <= 60; 
        }

        public static bool IsValidBiceps(double biceps)
        {
            return biceps > 0 && biceps <= 60; 
        }

        public static bool IsValidPantsWaist(double pantsWaist)
        {
            return pantsWaist > 0 && pantsWaist <= 150; 
        }

        public static bool IsValidCrotch(double crotch)
        {
            return crotch > 0 && crotch <= 50; 
        }

        public static bool IsValidThigh(double thigh)
        {
            return thigh > 0 && thigh <= 100; 
        }

        public static bool IsValidPantsLength(double pantsLength)
        {
            return pantsLength > 0 && pantsLength <= 130; 
        }

        public static bool ValidateAllMeasurements(
            double weight, double height, double neck, double hip, double waist, double armhole,
            double biceps, double pantsWaist, double crotch, double thigh, double pantsLength)
        {
            return IsValidWeight(weight) &&
                   IsValidHeight(height) &&
                   IsValidNeck(neck) &&
                   IsValidHip(hip) &&
                   IsValidWaist(waist) &&
                   IsValidArmhole(armhole) &&
                   IsValidBiceps(biceps) &&
                   IsValidPantsWaist(pantsWaist) &&
                   IsValidCrotch(crotch) &&
                   IsValidThigh(thigh) &&
                   IsValidPantsLength(pantsLength);
        }
    }
}
