namespace ClockNest.Common
{
    public class ConvertHoursDecimalToHoursMinutes
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public string HoursMinutes { get; set; }
        public ConvertHoursDecimalToHoursMinutes(decimal decimalValue)
        {
            if (Math.Floor(decimalValue) == decimalValue)
            {
                Hours = Convert.ToInt32(Math.Floor(decimalValue));
                Minutes = 0;
            }
            else
            {
                Hours = Convert.ToInt32((decimalValue < 0m) ? (Math.Floor(decimalValue) + 1m) : Math.Floor(decimalValue));
                Minutes = Convert.ToInt32(60m * (Math.Abs(decimalValue) - (decimal)Math.Abs(Hours)));
            }
            HoursMinutes = Hours + ":" + Minutes.ToString("00");
        }
    }
}
