using System;


namespace ETools.Extension
{
    public static class DataTimeExt
    {
        /// <summary>
        /// 月曜日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsMonday(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Monday;
        }

        /// <summary>
        /// 火曜日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsTuesday(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Tuesday;
        }

        /// <summary>
        /// 水曜日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsWednesday(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Wednesday;
        }

        /// <summary>
        /// 木曜日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsThursday(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Thursday;
        }

        /// <summary>
        /// 金曜日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsFriday(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Friday;
        }

        /// <summary>
        /// 土曜日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsSaturday(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Saturday;
        }

        /// <summary>
        /// 日曜日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsSunday(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 翌日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime Tomorrow(this DateTime self)
        {
            return self.AddDays(1);
        }

        /// <summary>
        /// 昨日
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime Yesterday(this DateTime self)
        {
            return self.AddDays(-1);
        }

        /// <summary>
        /// 中间日
        /// </summary>
        /// <param name="self"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool IsBetween(this DateTime self, DateTime from, DateTime to)
        {
            return from <= self && to >= self;
        }
    }
}