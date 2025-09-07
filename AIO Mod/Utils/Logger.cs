using System;
using System.Reflection;
using System.Threading;

namespace AIO_Mod.Utils
{
    public class Logger
    {
        public static void l(string message, string assemblyOverride = "")
        {
            Logger.debuglog(message, assemblyOverride);
        }

        public static void error(string message, string assemblyOverride = "")
        {
            Logger.dlogerror(message, assemblyOverride);
        }

        public static void dlogwarn(string message, string assemblyOverride = "")
        {
            if (assemblyOverride == "")
                assemblyOverride = Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"{Logger.TimeStamp()} [WARNING] [{assemblyOverride}]: {message}", (object)assemblyOverride);
        }

        public static void dlogerror(string message, string assemblyOverride = "")
        {
            if (assemblyOverride == "")
                assemblyOverride = Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"{Logger.TimeStamp()} [ERROR] [{assemblyOverride}]: {message}", (object)assemblyOverride);
        }
        public static string TimeStamp()
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, timeZone);
            return $"[{localTime:HH:mm:ss.fff}] [{Thread.CurrentThread.ManagedThreadId}]";
        }

        //public static string TimeStamp()
        //{
        //    return $"{"[" + TimeZoneInfo.ConvertTimeToUtc(System.DateTime.Now).ToString("HH:mm:ss.fff")}] [{(object)Thread.CurrentThread.ManagedThreadId}]";
        //}

        public static void debuglog(string message, string assemblyOverride = "")
        {
            if (assemblyOverride == "")
                assemblyOverride = Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"{Logger.TimeStamp()} [INFO] [{assemblyOverride}]: {message}");
        }
    }
}
