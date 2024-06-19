using System;

namespace WOPHRMSystem.Helps
{
    public class CommonResources
    {
        public static string System_File_Path;

        public DateTime LocalDatetime()
        {
            DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer 

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time"));
            return localTime;
        }


    }
}
