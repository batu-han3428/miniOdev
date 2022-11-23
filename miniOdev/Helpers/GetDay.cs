namespace miniOdev.Helpers
{
    public static class GetDay
    {
        public static DayOfWeek GetDayOfWeek(int day)
        {
            if (day == (int)DayOfWeek.Sunday)
            {
                return DayOfWeek.Sunday;
            }
            else if (day == (int)DayOfWeek.Monday)
            {
                return DayOfWeek.Monday;
            }
            else if (day == (int)DayOfWeek.Tuesday)
            {
                return DayOfWeek.Tuesday;
            }
            else if (day == (int)DayOfWeek.Wednesday)
            {
                return DayOfWeek.Wednesday;
            }
            else if (day == (int)DayOfWeek.Thursday)
            {
                return DayOfWeek.Thursday;
            }
            else if (day == (int)DayOfWeek.Friday)
            {
                return DayOfWeek.Friday;
            }
            else if (day == (int)DayOfWeek.Saturday)
            {
                return DayOfWeek.Saturday;
            }
            else
            {
                return DayOfWeek.Monday;
            }
        }
    }
}
