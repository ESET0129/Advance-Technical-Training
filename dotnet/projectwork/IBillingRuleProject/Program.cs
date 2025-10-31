namespace IBillingRuleProject
{
    //defining interface
    public interface IBillingRule
    {
        double Compute(int units);
    }

    //Domestic
    public class DomesticRule : IBillingRule
    {
        private const double Rate = 6.0;
        private const double FixedCharge = 50.0;

        public double Compute(int units)
        {
            return (units * Rate) + FixedCharge;
        }
    }

    //Commercial
    public class CommercialRule : IBillingRule
    {
        private const double Rate = 8.5;
        private const double FixedCharge = 150.0;

        public double Compute(int units)
        {
            return (units * Rate) + FixedCharge;
        }
    }

    //Agricultural Rule
    public class AgricuktureRule : IBillingRule
    {
        private const double Rate = 3.0;
        private const double FixedCharge = 0.0;

        public double Compute(int units)
        {
            return (units * Rate) + FixedCharge;
        }
    }
    //--------------------------------------------------------------------
    public class BillingEngine
    {
        public IBillingRule Rule { get; set; }
        public BillingEngine(IBillingRule rule)
        {
            this.Rule = rule;
        }
        public double GenerateBill(int units)
        {
            return this.Rule.Compute(units);
        }
    }
    //-------------------------------------------------------------
    public class Program
    {
        static void Main(string[] args)
        {
            const int units = 120;
            Console.WriteLine($"Computing Bills for {units} units\n");
            var domestic = new DomesticRule();
            var commercial = new CommercialRule();
            var agriculture = new AgricuktureRule();

            var engine = new BillingEngine(domestic);

            //domestic
            engine.Rule = domestic;
            double billDomestic = engine.GenerateBill(units);
            Console.WriteLine($"Domestic -> rs{billDomestic}");

            //commercial
            engine.Rule = commercial;
            double billCommercial = engine.GenerateBill(units);
            Console.WriteLine($"Commercial -> rs{billCommercial}");

            //agriculture
            engine.Rule = agriculture;
            double billAgriculture = engine.GenerateBill(units);
            Console.WriteLine($"Agriculture -> rs{billAgriculture}");

            

        }
    }
}


// Polymorphism with Strategy --- IBillingRule
//Goal: Replace switch with polymorphic strategy.

//Problem\ Define:

//#
//public interface IBillingRule { double Compute(int units); }
//class DomesticRule : IBillingRule { /* 6.0/unit + 50 fixed */ }
//class CommercialRule : IBillingRule { /* 8.5/unit + 150 fixed */ }
//class AgricultureRule : IBillingRule { /* 3.0/unit + 0 fixed */ }
//Create BillingEngine with IBillingRule Rule; and double GenerateBill(int units).

//Tasks

//With units=120, compute bills using each rule instance.

//Print category + amount.

//Expected Output

//DOMESTIC -> ₹770.00
//COMMERCIAL -> ₹1170.00
//AGRICULTURE -> ₹360.00
//Stretch

//Add TimeOfDay multiplier (e.g., 1.2× for peak) via optional ctor arg or property and apply in Compute.
//Checks

//No switch in engine; pure interface polymorphism.
 