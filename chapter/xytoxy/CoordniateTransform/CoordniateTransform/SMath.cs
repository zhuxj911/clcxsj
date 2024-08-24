using System;

namespace ZXY
{
    public static class SMath
    {
        /// <summary>
        /// 将60进制的度分秒角度转换为弧度
        /// </summary>
        /// <param name="dmsAngle">形如ddd.mmss的60进制的角度</param>
        /// <returns>弧度值</returns>
        public static double DMS2RAD(double dmsAngle)
        {
            dmsAngle *= 1e4;
            int dms = (int)Math.Round(dmsAngle);
            int d = dms / 10000;
            int m = (dms - d * 10000) / 100;
            double s = dmsAngle - d * 1e4 - m * 1e2;
            return  (d + m / 60.0 + s / 3600.0)/180.0*Math.PI;
        }

        ///// <summary>
        ///// 将弧度转换为60进制的度分秒角度
        ///// </summary>
        ///// <param name="radAngle">单位为弧度的角度</param>
        ///// <returns>60进制的角度</returns>
        //public static double RAD2DMS(double radAngle)
        //{
        //    radAngle = radAngle / Math.PI * 180 * 3600;
        //    int sAngle = (int)Math.Round(radAngle);

        //    int d = sAngle / 3600;
        //    int m = (sAngle - d * 3600) / 60;
        //    double s = radAngle - d * 3600 - m * 60.0;

        //    return d + m / 100.0 + s / 10000.0;
        //}

        public static void RAD2DMS(double radAngle,
            out int d, out int m, out double s)
        {
            radAngle = radAngle / Math.PI * 180 * 3600;
            int sAngle = (int)Math.Round(radAngle);

            d = sAngle / 3600;
            m = (sAngle - d * 3600) / 60;
            s = radAngle - d * 3600 - m * 60.0;
        }

        /// <summary>
        /// 将弧度转换为60进制的度分秒角度
        /// </summary>
        /// <param name="radAngle">单位为弧度的角度</param>
        /// <returns>60进制的角度</returns>
        public static double RAD2DMS(double radAngle)
        {
            double s;
            int d, m;

            RAD2DMS(radAngle, out d, out m, out s);
            return d + m / 100.0 + s / 10000.0;
        }

        public static string RAD2DMSString(double radAngle)
        {
            double s;
            int d, m;

            RAD2DMS(radAngle, out d, out m, out s);
            return string.Format("{0}°{1}′{2}″", d, m, s); ;
        }
    }
}
