namespace CS._2._002WeeklyConsumptionBasics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Weekly Energy Consumption ");
            int[] daily = { 4, 5, 6, 0, 7, 8, 5 };
            int total = 0;
            int outageCount = 0;
            int maxUsage = -1;
            int maxDayIndex = 0;

            for(int i = 0; i < daily.Length; i++)
            {
                int currentReading = daily[i];
                int dayNumber = i + 1;
                total += currentReading;

                if(currentReading > maxUsage)
                {
                    maxUsage = currentReading;
                    maxDayIndex = dayNumber;
                }

                if (currentReading == 0)
                {
                    outageCount++;
                }
            }
            double average = (double)total / daily.Length;
            string averageFormatted = average.ToString("0.00");

            Console.WriteLine($"Total:{total} kWh | Average: {averageFormatted} kWh | Max: {maxUsage} kWh (Day{maxDayIndex}) | Outage: {outageCount}");

        }
    }
}
