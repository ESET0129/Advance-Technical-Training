namespace QuickBillfromTwoReadings
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Electricity Bill Calculator");
            Console.Write("Enter Meter Serial: ");
            string meterSerial = Console.ReadLine();

            Console.Write("Enter Previous Reading: ");
            if(!int.TryParse(Console.ReadLine(),out int prevReading))
            {
                Console.WriteLine("Enter right Reading");
                return;

            }

            Console.Write("Enter Current Reading: ");
            if (!int.TryParse(Console.ReadLine(), out int currReading) || currReading < prevReading)
            {
                Console.WriteLine("Enter right Reading");
                return;
            }

            int units = currReading - prevReading;
            if(units <= 0)
            {
                Console.WriteLine("Current reading must be greater than or equal to previous reading.");
                return;
            }
            else if(units > 500){
                Console.WriteLine("High Usage Alert");
            }
            else
            {
                const double Rate_Per_Unit = 6.5;
                const double Tax_Rate = 0.05;

                double energyCharge = units * Rate_Per_Unit;
                double tax = energyCharge * Tax_Rate;
                double totalAmount = energyCharge + tax;

                string energyFormatted = energyCharge.ToString("C2");
                string taxFormatted = tax.ToString("C2");
                string totalFormatted = totalAmount.ToString("C2");

                Console.WriteLine($"Meter{meterSerial} | Units:{units} | Energy: {energyFormatted} | Tax: {taxFormatted} | Bill Summary:{totalFormatted}");

            }
        }
    }
}
