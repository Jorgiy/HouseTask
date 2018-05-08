using System;
using System.Collections.Generic;

namespace HouseTask
{
    public class House
    {
        private readonly List<IReportMaker> _reportMakers = new List<IReportMaker>();

        private readonly Dictionary<IReportable, List<KeyValuePair<DateTime, State>>> _eventStorage =
            new Dictionary<IReportable, List<KeyValuePair<DateTime, State>>>();

        public House(List<Room> rooms)
        {
            Rooms = rooms;
            _reportMakers.Add(new ConsumptionReportMaker(_eventStorage));

            foreach (var room in Rooms)
            {
                foreach (var stationare in room.GetRoomObjects())
                {
                    if (stationare is IChangeStateNotifier roomObject)
                    {
                        if (stationare is Device device)
                        {
                            device.StateChanged += AddDeviceChangeEvent;
                            AddDeviceChangeEvent(device, device.State, DateTime.Now);
                            
                            if (device.SubSource != null)
                            {
                                device.SubSource.StateChanged += AddDeviceChangeEvent;
                                AddDeviceChangeEvent(device.SubSource, device.SubSource.State, DateTime.Now);
                            }
                        }
                        else
                        {
                            roomObject.StateChanged += AddDeviceChangeEvent;
                            AddDeviceChangeEvent(roomObject, roomObject.State, DateTime.Now);
                        }
                    }
                }
            }
        }

        public List<Room> Rooms { get; }

        public List<IReportMaker> GetReportMakers()
        {
            return _reportMakers;
        }

        private void AddDeviceChangeEvent(IReportable reportable, State state, DateTime dateTime)
        {
            if (!_eventStorage.ContainsKey(reportable))
            {
                _eventStorage.Add(reportable,
                    new List<KeyValuePair<DateTime, State>> {new KeyValuePair<DateTime, State>(dateTime, state)});
            }
            
            _eventStorage[reportable].Add(new KeyValuePair<DateTime, State>(dateTime, state));
        }
    }
}