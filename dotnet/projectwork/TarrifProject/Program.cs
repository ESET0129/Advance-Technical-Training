namespace TarrifProject
{
    public class Tarrif
    {
        private const double DefaultRate = 6.0;
        private const double DefaultFixed = 50.0;

        public string Name { get; set; }
        public double RateperKwh { get; set; }
        public double FixedCharge { get; set; }

        //constructor----------------------------------------
        public Tarrif(string name, double rate, double fixedCharge)
        {
            // This is the primary constructor where all logic should reside
            this.Name = name;
            this.RateperKwh = rate;
            this.FixedCharge = fixedCharge;

            // Stretch Goal: Validate the values after setting them (see Step 4)
            // this.Validate(); 

            Console.WriteLine($"Initialized Tariff: {this.Name} (Rate: {this.RateperKwh}, Fixed: {this.FixedCharge})");
        }

        //--------------------------------------------------------
        public Tarrif(string name, double rate) : this(name, rate, DefaultFixed) { }
        public Tarrif(string name) : this(name, DefaultRate) { }

        //---------------------------------------------------------------

        public double ComputeBill(int units)
        {
            return (units * this.RateperKwh) + this.FixedCharge;
        }

    }

        public class program
        {
            static void Main(string[] args)
            {
            //Console.WriteLine("Hello, World!");
            const int units = 120;
            Console.WriteLine($"bill for {units} units");
            //------------------------------------------------------------------
            var domestic = new Tarrif("domestic");
            var commercial = new Tarrif("comercial", rate: 9.0);
            var agricultural = new Tarrif("agri", rate: 3.0, fixedCharge: 50.0);

            Console.WriteLine("\n\ncomputed bill\n");
            //-------------------------------------------------------------------
            //calculating bills
            double billdomestic = domestic.ComputeBill(units);
            Console.WriteLine($"{domestic.Name} : rs{billdomestic}");

            double billcommercial = commercial.ComputeBill(units);
            Console.WriteLine($"{commercial.Name} : rs{billcommercial}");

            double billagri = agricultural.ComputeBill(units);
            Console.WriteLine($"{agricultural.Name} : rs{billagri}");

            }
        }
    
}

/*
 *Constructors & Overloads --- Tariff
Goal: Define overloaded constructors + chaining.

Problem\ Create a Tariff class with:

Props: Name (string), RatePerKwh (double), FixedCharge (double).

Ctors:

Tariff(string name) → defaults: rate=6.0, fixed=50.

Tariff(string name, double rate) → defaults fixed=50.

Tariff(string name, double rate, double fixedCharge).

ComputeBill(int units) → units * rate + fixed.

Tasks

Create three tariffs using different constructors.

For units=120, print computed bill for each.

Expected Output (example)

DOMESTIC: ₹770.00
COMMERCIAL: ₹1170.00
AGRI: ₹410.00
Stretch

Add Validate() that throws if rate <= 0 or fixed < 0. Call in constructors.
Checks

Proper ctor chaining, defaults applied correctly
 */