using System.Data.Common;

namespace MeterProject
{

    public class Meter //Meter class with its field's
    {
        //public string MeterSerial;
        //public string Location;
        //public DateTime InstalledOn;
        //public int LastReadingKwh;


        //strech goal
        public string MeterSerial { get; }
        public string Location { get; set; }
        public DateTime InstalledOn { get; }
        public int LastReadingKwh { get; private set; }//private set the meter reading


        //constructor - strech goal --------------------------------------------
        public Meter(string Serial, string Location, DateTime InstalledOn, int InitialReading)
        {
            this.MeterSerial = Serial;  
            this.Location = Location;
            this.InstalledOn = InstalledOn;
            this.LastReadingKwh = InitialReading;
        }

        //add reading methods : useful for meter reading
        public void AddReading(int deltaKwh)
        {
            if (deltaKwh > 0)
            {
                this.LastReadingKwh += deltaKwh;//update reading
                Console.WriteLine($"reading updated.{deltaKwh} Kwh");
            }
            else
            {
                Console.WriteLine($"reading not updated {deltaKwh} Kwh due to data is not positive");
            }
        }

        //strech goal part 2
        public override string ToString()
        {
            return $"{this.MeterSerial} | Location{this.Location} | Reading{this.LastReadingKwh} ";
        }


        //summarizing data
        //public string Summary()
        //{
        //    return $"{this.MeterSerial} | Location{this.Location} | Reading{this.LastReadingKwh}";

        //}
        public string Summary() => this.ToString() ;
    }
    


    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Meter");
            var MeterA = new Meter("AP 0001", "Feeder 12", new DateTime(2023, 01, 15), 15730);   //initiating meter object--------------------------
            //{
            //    MeterSerial = "AP 0001",
            //    Location = "Feeder 12",
            //    InstalledOn = new DateTime(2023, 01, 15),
            //    LastReadingKwh = 15730
            //};

            var MeterB = new Meter("AP 0002", "OTR-9", new DateTime(2022, 11, 20), 9800);       //initiating meter object------------------------
            //{
            //    MeterSerial = "AP 0002",
            //    Location = "OTR-9",
            //    InstalledOn = new DateTime(2022, 11, 20),
            //    LastReadingKwh = 9800
            //};

            Console.WriteLine("\n Initial Summary");
            Console.WriteLine(MeterA.Summary());
            Console.WriteLine(MeterB.Summary());

            //reading
            Console.WriteLine("AddREADING");
            //METERA----------------------------
            Console.WriteLine("\nMETER AP 0001");
            MeterA.AddReading(100);
            MeterA.AddReading(500);
            MeterA.AddReading(0);//invalid input >0


            //METERB------------------------------------
            Console.WriteLine("\nMETER AP0002");
            MeterB.AddReading(100);
            MeterB.AddReading(500);

            //final output------------------------------
            Console.WriteLine("\n final summary");
            Console.WriteLine(MeterA.Summary());
            Console.WriteLine(MeterB.Summary());
        }
    
    }
}