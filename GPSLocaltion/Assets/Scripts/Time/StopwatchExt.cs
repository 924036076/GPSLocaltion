using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ETools.Extension
{
    public static class StopwatchExt
    {
        /// <summary>
        /// yyyy/MM/dd HH:mm:ss 
        /// </summary>
        public static string ToPattern(this Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// yyyy/MM/dd 
        /// </summary>
        public static string ToShortDatePattern(this Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// yyyy年M月d日
        /// </summary>
        public static string ToLongDatePattern(this Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("yyyy年M月d日");
        }

        /// <summary>
        /// yyyy年M月d日 HH:mm:ss 
        /// </summary>
        public static string ToFullStopwatchPattern(this Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("yyyy年M月d日 HH:mm:ss");
        }

        /// <summary>
        /// MM/dd HH:mm 
        /// </summary>
        public static string ToMiddleStopwatchPattern(this Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("MM/dd HH:mm");
        }

        /// <summary>
        /// HH:mm 
        /// </summary>
        public static string ToShortTimePattern(this Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("HH:mm");
        }

        /// <summary>
        /// HH:mm:ss 
        /// </summary>
        public static string ToLongTimePattern(this Stopwatch self)
        {
            return new DateTime(self.Elapsed.Ticks).ToString("HH:mm:ss");
        }
        
        // Example:
        // var sw = new Stopwatch();
        // sw.Start();
        // sw.Stop();
        // Console.WriteLine( sw.ToLongTimePattern() );
    }
}