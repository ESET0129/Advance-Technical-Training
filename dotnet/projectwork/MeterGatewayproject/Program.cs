namespace MeterGatewayproject
{
    public class Device//device class,  common properties
    {
        public string Id { get; set; }
        public DateTime InstalledOn { get; set; }

        protected Device(string id, DateTime installedOn)
        {
            this.Id = id;
            this.InstalledOn = installedOn;
        }
        public virtual string Describe()
        {
            return $"Device Id {this.Id} | Installed {this.InstalledOn: yyyy-MM-dd}";
        }
    }
    //derived class for meter
    public class Meter : Device
    {
        public int PhaseCount { get; set;}

        //constructor
        public Meter(string id, DateTime installedOn, int phaseCount) : base (id, installedOn)
        {
            this.PhaseCount = phaseCount; 
        }

        //overriding base class
        public override string Describe()
        {
            return $"{base.Describe().Replace("Device","Meter")} | Phases : {this.PhaseCount}";
        }

    }

    //gateway class
    public class Gateway : Device
    {
        public string IpAddress { get; set;}
        //constructor
        public Gateway(string id, DateTime installedOn,string ipaddress) : base (id, installedOn)
        {
            this.IpAddress = ipaddress;
        }

        public override string Describe() //override method for base class of gateway
        {
            return $"{base.Describe().Replace("Device","Gateway")} | IP  {this.IpAddress}";

        }
    }




    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("polymorphism");
            Device[] devices = new Device[] //array
            {
                new Meter("AP-0001",new DateTime(2024,07,01),3),
                new Gateway("GW-11",new DateTime(2025,01,10),"10.0.5.21")
            };

            Console.WriteLine("device description");
            foreach (var d in devices)
            {
                Console.WriteLine(d.Describe());
            }
        }  
    }
}
/*
 Inheritance Basics --- Device → Meter / Gateway
Goal: Establish base class reuse.

Problem

Base class Device:

Props: Id (string), InstalledOn (DateTime).

Method: Virtual Describe() → "Device Id: ... InstalledOn: ..."

Derived:

Meter adds PhaseCount (int) and overrides Describe() to include it.

Gateway adds IpAddress (string) and overrides Describe().

Tasks

Create Device[] with 1 meter + 1 gateway (polymorphic array).

Loop and Console.WriteLine(d.Describe()).

Expected Output

Meter Id: AP-0001 | Installed: 2024-07-01 | Phases: 3
Gateway Id: GW-11 | Installed: 2025-01-10 | IP: 10.0.5.21
Stretch

Mark Device.Describe() virtual and children override; also add protected ctor in base.
Checks

Correct override dispatch via base reference.
 */
