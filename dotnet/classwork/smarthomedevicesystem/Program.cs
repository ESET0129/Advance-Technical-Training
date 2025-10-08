namespace smarthomedevicesystem
{
    public interface IDevice
    {
        string DeviceName { get; }
        bool IsOn { get; }
        void turnOn();
        void turnOff();

    }
    public interface ISmartDevice : IDevice
    {
        void ConnectedToWiFi(string networkname);
        void ShowStatus();
    }

    public class Light : ISmartDevice
    {
        public string DeviceName { get; }
        public bool IsOn { get; private set; }
        //add others
    }

    public class Fan : ISmartDevice
    {
        //add detials
    }

    public class  Thermostat : ISmartDevice
    {
     //add detail
    }
    public class Program
    {
        static void Main(string[] args)
        {
            List<ISmartDevice> smartDevices = new List<ISmartDevice>
        {
            new Light("Lamp"),
            new Fan("Ceiling Fan"),
            new Thermostat("Thermostat")
        };
            foreach (var device in smartDevices)
            {
                device.TurnOn(); // Calls Light.TurnOn(), Fan.TurnOn(), or Thermostat.TurnOn()
                device.ConnectToWiFi("MyHomeNet");
            }
        }
    }
    
}

/*
 Task:
 
Create an interface IDevice with properties DeviceName and IsOn, and methods TurnOn() and TurnOff().
 
Create another interface ISmartDevice that inherits IDevice and adds void ConnectToWiFi(string networkName) and void ShowStatus().
 
Implement classes Light, Fan, and Thermostat that implement ISmartDevice with device-specific properties (Brightness, Speed, Temperature).
 
Create a list of ISmartDevice in Main() and perform the following:
 
Turn on all devices
 
Connect all to WiFi
 */
