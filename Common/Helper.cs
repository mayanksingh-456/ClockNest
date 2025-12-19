namespace ClockNest.Common
{
    public class Helper
    {
        static public string GetHoursMinsString(int Minutes)
        {
            string result = string.Empty;

            if (Minutes < 0)
                result = "-";

            Minutes = System.Math.Abs(Minutes);

            string formatHoursMins = String.Format("{0:N0}", (Minutes / 60)) + ":" + String.Format("{00:00}", (Minutes % 60));

            return result += formatHoursMins;
        }

        internal static TimeSpan FormatStringToTimeSpan(string value)
        {
            throw new NotImplementedException();
        }

        internal static string FormatTimeSpanToString(TimeSpan oT_Fixed5)
        {
            throw new NotImplementedException();
        }
    }
}
