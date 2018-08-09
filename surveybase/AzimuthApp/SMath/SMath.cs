using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXY
{
    public static class SMath
    {
        public const double PI = Math.PI;
        public const double TWOPI = 2 * Math.PI;
        public const double TODEG = 180.0 / PI;
        public const double TORAD = PI / 180.0;
        public const double TOSECOND = 180.0 * 3600.0 / PI;

        public static void DMStoDMS(double dmsAngle, out int d, out int m, out double s)
        {
            dmsAngle *= 10000;
            int angle = (int)Math.Round(dmsAngle);
            d = angle / 10000;
            angle -= d * 10000;
            m = angle / 100;
            s = dmsAngle - d * 10000 - m * 100;
        }

        public static double DMStoRAD(double dmsAngle)
        {
            DMStoDMS(dmsAngle, out int d, out int m, out double s);
            return (d + m / 60.0 + s / 3600.0) * TORAD;
        }

        public static string DMStoString(double dmsAngle)
        {
            DMStoDMS(dmsAngle, out int d, out int m, out double s);
            return $"{d}°{m:00}′{s:00.0####}″";
        }

        public static void RADtoDMS(double radAngle, out int d, out int m, out double s)
        {
            radAngle *= TOSECOND;
            int angle = (int)Math.Round(radAngle);
            d = angle / 3600;
            angle -= d * 3600;
            m = angle / 60;
            s = radAngle - d * 3600 - m * 60;
        }

        public static double RADtoDMS(double radAngle)
        {
            RADtoDMS(radAngle, out int d, out int m, out double s);
            return (d + m / 100.0 + s / 10000.0);
        }

        public static string RADtoString(double radAngle)
        {
            RADtoDMS(radAngle, out int d, out int m, out double s);
            return $"{d}°{m:00}′{s:00.0####}″";
        }

        public static double Azimuth(double xA, double yA, double xB, double yB, out double azimuth)
        {
            double dx = xB - xA;
            double dy = yB - yA;
            azimuth = Math.Atan2(dy, dx) + (dy < 0 ? 1 : 0) * TWOPI;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
