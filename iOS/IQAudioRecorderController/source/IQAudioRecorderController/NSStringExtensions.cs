using System;
using Foundation;

namespace IQAudioRecorderController {
    
    /// <summary>
    /// NSString extensions.
    /// </summary>
	internal static class NSStringExtensions {
        
        #region Static Methods

		/// <summary>
		/// Times the string for time interval.
		/// </summary>
		/// <returns>The string for time interval.</returns>
		/// <param name="timeInterval">Time interval.</param>
        public static String TimeStringForTimeInterval(double timeInterval) {
            // 
             var ti = timeInterval;
             var seconds = ti % 60;
			 var minutes = (ti / 60) % 60;
			 var hours = (ti / 3600);
             
             if (hours > 0)
             {
				return String.Format ("{0:0.00}:{1:0.00}:{2:0.00}",hours, minutes, seconds);
             }
             else
             {
				return String.Format ("{0:0.00}:{1:0.00}",hours, minutes);
             }
        }
        #endregion
    }

}
