using System;
using System.IO;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;

namespace DeviceYearClass
{
    /// <summary>
    /// Helper class for accessing hardware specifications, including the number of CPU cores, CPU clock speed
    /// and total available RAM.
    /// </summary>
    public class DeviceInfo
    {
        private static readonly FileFilter filter = new FileFilter();

        /// <summary>
        /// The default return value of any method in this class when an
        /// error occurs or when processing fails (Currently set to -1). Use this to check if
        /// the information about the device in question was successfully obtained.
        /// </summary>
        public const int Unknown = -1;

        /// <summary>
        /// Reads the number of CPU cores from "/sys/devices/system/cpu/".
        /// </summary>
        /// <returns> Number of CPU cores in the phone, or Unknown = -1 in the event of an error. </returns>
        public static int GetNumberOfCPUCores()
        {
            if (Build.VERSION.SdkInt <= BuildVersionCodes.GingerbreadMr1)
            {
                // Gingerbread doesn't support giving a single application access to both cores, but a
                // handful of devices (Atrix 4G and Droid X2 for example) were released with a dual-core
                // chipset and Gingerbread; that can let an app in the background run without impacting
                // the foreground application. But for our purposes, it makes them single core.
                return 1;
            }

            int cores;
            try
            {
                cores = new Java.IO.File("/sys/devices/system/cpu/").ListFiles(filter).Length;
            }
            catch (Java.Lang.SecurityException)
            {
                cores = Unknown;
            }
            catch (System.Security.SecurityException)
            {
                cores = Unknown;
            }
            catch (NullReferenceException)
            {
                cores = Unknown;
            }
            return cores;
        }

        /// <summary>
        /// Method for reading the clock speed of a CPU core on the device. Will read from either
        /// {@code /sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_max_freq} or {@code /proc/cpuinfo}.
        /// </summary>
        /// <returns> Clock speed of a core on the device, or -1 in the event of an error. </returns>
        public static int GetCPUMaxFreqKHz()
        {
            int maxFreq = Unknown;
            try
            {
                var cores = GetNumberOfCPUCores();
                for (int i = 0; i < cores; i++)
                {
                    var filename = "/sys/devices/system/cpu/cpu" + i + "/cpufreq/cpuinfo_max_freq";
                    if (File.Exists(filename))
                    {
                        var buffer = new byte[128];
                        using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                        {
                            stream.Read(buffer, 0, buffer.Length);
                            var endIndex = 0;
                            // Trim the first number out of the byte buffer.
                            while (char.IsDigit((char)buffer[endIndex]) && endIndex < buffer.Length)
                            {
                                endIndex++;
                            }
                            var str = Encoding.UTF8.GetString(buffer, 0, endIndex);
                            int freqBound;
                            if (int.TryParse(str, out freqBound) && freqBound > maxFreq)
                            {
                                maxFreq = freqBound;
                            }
                        }
                    }
                }
                if (maxFreq == Unknown)
                {
                    using (var stream = new FileStream("/proc/cpuinfo", FileMode.Open, FileAccess.Read))
                    {
                        int freqBound = ParseFileForValue("cpu MHz", stream);
                        freqBound *= 1000; // MHz -> kHz
                        if (freqBound > maxFreq)
                        {
                            maxFreq = freqBound;
                        }
                    }
                }
            }
            catch (IOException)
            {
                maxFreq = Unknown; // Fall through and return unknown.
            }
            return maxFreq;
        }

        /// <summary>
        /// Calculates the total RAM of the device through Android API or /proc/meminfo.
        /// </summary>
        /// <param name="c"> - Context object for current running activity. </param>
        /// <returns> Total RAM that the device has, or DEVICEINFO_UNKNOWN = -1 in the event of an error. </returns>
        public static long GetTotalMemory(Context c)
        {
            // memInfo.totalMem not supported in pre-Jelly Bean APIs.
            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
            {
                var memInfo = new ActivityManager.MemoryInfo();
                var am = ActivityManager.FromContext(c);
                am.GetMemoryInfo(memInfo);
                if (memInfo != null)
                {
                    return memInfo.TotalMem;
                }
                else
                {
                    return Unknown;
                }
            }
            else
            {
                long totalMem = Unknown;
                try
                {
                    using (var stream = new FileStream("/proc/meminfo", FileMode.Open, FileAccess.Read))
                    {
                        totalMem = ParseFileForValue("MemTotal", stream);
                        totalMem *= 1024;
                    }
                }
                catch (IOException)
                {
                }
                return totalMem;
            }
        }

        /// <summary>
        /// Helper method for reading values from system files, using a minimized buffer.
        /// </summary>
        /// <param name="textToMatch">Text in the system files to read for.</param>
        /// <param name="stream">FileInputStream of the system file being read from.</param>
        /// <returns>
        /// A numerical value following textToMatch in specified the system file.
        /// -1 in the event of a failure.
        /// </returns>
        private static int ParseFileForValue(string textToMatch, FileStream stream)
        {
            var buffer = new byte[1024];
            try
            {
                int length = stream.Read(buffer, 0, buffer.Length);
                for (int i = 0; i < length; i++)
                {
                    if (buffer[i] == '\n' || i == 0)
                    {
                        if (buffer[i] == '\n')
                        {
                            i++;
                        }
                        for (int j = i; j < length; j++)
                        {
                            int textIndex = j - i;
                            // Text doesn't match query at some point.
                            if (buffer[j] != textToMatch[textIndex])
                            {
                                break;
                            }
                            // Text matches query here.
                            if (textIndex == textToMatch.Length - 1)
                            {
                                return ExtractValue(buffer, j);
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                // Ignore any exceptions and fall through to return unknown value.
            }
            return Unknown;
        }

        /// <summary>
        /// Parses the next available number after the match in the file being read 
        /// and returns it as an integer.
        /// </summary>
        /// <param name="buffer">The buffer array to looking through.</param>
        /// <param name="index">The index in the buffer array to begin looking.</param>
        /// <returns>
        /// The next number on that line in the buffer, returned as an int. Returns
        /// Unknown = -1 in the event that no more numbers exist on the same line. 
        /// </returns>
        private static int ExtractValue(byte[] buffer, int index)
        {
            while (index < buffer.Length && buffer[index] != '\n')
            {
                if (char.IsDigit((char)buffer[index]))
                {
                    int start = index;
                    index++;
                    while (index < buffer.Length && char.IsDigit((char)buffer[index]))
                    {
                        index++;
                    }
                    string str = Encoding.UTF8.GetString(buffer, start, index - start);
                    int result;
                    if (int.TryParse(str, out result))
                    {
                        return result;
                    }
                    return Unknown;
                }
                index++;
            }
            return Unknown;
        }

        private class FileFilter : Java.Lang.Object, Java.IO.IFileFilter
        {
            public bool Accept(Java.IO.File file)
            {
                var path = file.Name;

                // regex is slow, so checking char by char.
                if (path.StartsWith("cpu", StringComparison.Ordinal))
                {
                    for (int i = 3; i < path.Length; i++)
                    {
                        if (!char.IsDigit(path[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }
    }
}
