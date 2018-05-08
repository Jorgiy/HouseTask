using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var gasSource = new Source("Gas", "", SourceType.Gas);
            var electicitySource = new Source("Socket", "", SourceType.Electric);
            var waterSource = new Source("Tap", "", SourceType.Water);
            
            var gasDevice = new Device("Cooker", "", 33, gasSource, false);
            var electricDevice = new Device("Tea pot", "", 10, electicitySource, true);
            var waterDevice = new Device("Faucet", "", 5, waterSource, false);

            var allObjects = new List<Stationare>
            {
                gasSource,
                gasDevice,
                waterDevice,
                waterSource,
                electicitySource,
                electricDevice
            };
            
            var window = new Window("Big window", "");
            
            var room = new Room("Kitchen","", new List<Room>(), new List<Window>() { window });
            
            allObjects.ForEach(x => room.AddObjectToRoom(x));
            
            var house = new House(new List<Room> { room });

            var houseHeports = house.GetReportMakers();

            var consumptionReport = houseHeports.FirstOrDefault(x => x.GetType() == typeof(ConsumptionReportMaker));

            Console.WriteLine(consumptionReport?.MakeReport(DateTime.Now));
            
            electicitySource.SetSourceState(new SourceState(SourceStateEnum.Defective));
            
            waterDevice.SetDeviceState(new DeviceState(DeviceStateEnum.NotWorking));
            
            Console.WriteLine(consumptionReport?.MakeReport(DateTime.Now));

            Console.ReadLine();
        }
    }
}