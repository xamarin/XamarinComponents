using System.Collections.Generic;
using Android.Content;

namespace DeviceYearClass
{
    public static class YearClass
    {
        private const long MB = 1024 * 1024;
        private const int MHZ_IN_KHZ = 1000;

        // Year definitions
        public const int YearUnknown = -1;
        public const int Year2008 = 2008;
        public const int Year2009 = 2009;
        public const int Year2010 = 2010;
        public const int Year2011 = 2011;
        public const int Year2012 = 2012;
        public const int Year2013 = 2013;
        public const int Year2014 = 2014;

        // the result
        private volatile static int yearClass = 0;

        /// <summary>
        /// Analyzes an Android device's specifications and calculates which year the device would 
        /// be considered "high end". 
        /// </summary>
        public static int GetDeviceYearClass(Context c)
        {
            if (yearClass == 0)
            {
                lock (typeof(YearClass))
                {
                    if (yearClass == 0)
                    {
                        yearClass = CategorizeByYear(c);
                    }
                }
            }
            return yearClass;
        }

        /// <summary>
        /// Calculates the "best-in-class year" of the device. This represents the top-end or flagship
        /// devices of that year, not the actual release year of the phone. For example, the Galaxy Duos
        /// S was released in 2012, but its specs are very similar to the Galaxy S that was released in
        /// 2010 as a then top-of-the-line phone, so it is a 2010 device.
        /// </summary>
        /// <returns> The year when this device would have been considered top-of-the-line. </returns>
        private static int CategorizeByYear(Context c)
        {
            // Get the years
            var years = new List<int>();
            AddYear(years, GetNumCoresYear());
            AddYear(years, GetClockSpeedYear());
            AddYear(years, GetRamYear(c));
            years.Sort();

            if ((years.Count & 0x01) == 1)
            {
                // Odd number; pluck the median.
                return years[years.Count / 2];
            }
            else
            {
                // Even number. Average the two "center" values.
                int idx = years.Count / 2 - 1;
                // There's an implicit rounding down in here: 2011.5 becomes 2011.
                return years[idx] + (years[idx + 1] - years[idx]) / 2;
            }
        }

        /// <summary>
        /// Calculates the year class by the number of processor cores the phone has.
        /// Evaluations are based off the table below:
        /// 4 or More ~ 2012,
        /// 2 or 3    ~ 2011,
        /// 1         ~ 2008
        /// </summary>
        /// <returns> the year in which top-of-the-line phones had the same number of processors as this phone. </returns>
        private static int GetNumCoresYear()
        {
            var cores = DeviceInfo.GetNumberOfCPUCores();
            if (cores < 1)
            {
                return YearUnknown;
            }
            if (cores == 1)
            {
                return Year2008;
            }
            if (cores <= 3)
            {
                return Year2011;
            }
            return Year2012;
        }

        /// <summary>
        /// Calculates the year class by the clock speed of the cores in the phone:
        /// greater than   2GHz ~ 2014,
        /// less than      2GHz ~ 2013,
        /// less than    1.5GHz ~ 2012,
        /// less than    1.2GHz ~ 2011,
        /// less than      1GHz ~ 2010,
        /// less than    600MHz ~ 2009,
        /// less than    528MHz ~ 2008
        /// </summary>
        /// <returns>The year in which top-of-the-line phones had the same clock speed.</returns>
        private static int GetClockSpeedYear()
        {
            var clockSpeedKHz = DeviceInfo.GetCPUMaxFreqKHz();
            if (clockSpeedKHz == DeviceInfo.Unknown)
            {
                return YearUnknown;
            }
            // These cut-offs include 20MHz of "slop" because my "1.5GHz" Galaxy S3 reports
            // its clock speed as 1512000. So we add a little slop to keep things nominally correct.
            if (clockSpeedKHz <= 528 * MHZ_IN_KHZ)
            {
                return Year2008;
            }
            if (clockSpeedKHz <= 620 * MHZ_IN_KHZ)
            {
                return Year2009;
            }
            if (clockSpeedKHz <= 1020 * MHZ_IN_KHZ)
            {
                return Year2010;
            }
            if (clockSpeedKHz <= 1220 * MHZ_IN_KHZ)
            {
                return Year2011;
            }
            if (clockSpeedKHz <= 1520 * MHZ_IN_KHZ)
            {
                return Year2012;
            }
            if (clockSpeedKHz <= 2020 * MHZ_IN_KHZ)
            {
                return Year2013;
            }
            return Year2014;
        }

        /// <summary>
        /// Calculates the year class by the amount of RAM the phone has:
        /// greater than   2GB ~ 2014,
        /// less than      2GB ~ 2013,
        /// less than    1.5GB ~ 2012,
        /// less than      1GB ~ 2011,
        /// less than    512MB ~ 2010,
        /// less than    256MB ~ 2009,
        /// less than    128MB ~ 2008
        /// </summary>
        /// <returns>The year in which top-of-the-line phones had the same amount of RAM as this phone.</returns>
        private static int GetRamYear(Context c)
        {
            var totalRam = DeviceInfo.GetTotalMemory(c);
            if (totalRam <= 0)
            {
                return YearUnknown;
            }
            if (totalRam <= 192 * MB)
            {
                return Year2008;
            }
            if (totalRam <= 290 * MB)
            {
                return Year2009;
            }
            if (totalRam <= 512 * MB)
            {
                return Year2010;
            }
            if (totalRam <= 1024 * MB)
            {
                return Year2011;
            }
            if (totalRam <= 1536 * MB)
            {
                return Year2012;
            }
            if (totalRam <= 2048 * MB)
            {
                return Year2013;
            }
            return Year2014;
        }

        private static void AddYear(List<int> years, int yearClass)
        {
            if (yearClass != YearUnknown)
            {
                years.Add(yearClass);
            }
        }
    }
}
