using System;

namespace HouseTask
{
    public class Device : Stationare, IActionable, IChangeStateNotifier
    {
        private State _deviceState = new DeviceState(DeviceStateEnum.Working);
        
        public Device(string name, string documentation, int consumption, Source source, bool hasSubSource) : base(name,
            documentation)
        {
            Consumption = consumption;
            Source = source;
            SubSource = hasSubSource
                ? new Source(source.Name + " portable", source.Documentation + " (portable)", source.SourceType)
                : null;
        }

        public object MakeAction(params object[] @params)
        {
            // some realization just for example
            foreach (var param in @params)
            {
                Console.WriteLine($"Device was invoked with - {param}");
            }
            
            return new object();
        }

        public Source Source { get; }

        public Source SubSource { get; }

        public int Consumption { get; }

        public State State => _deviceState;

        public void SetDeviceState(DeviceState deviceState)
        {
            _deviceState = deviceState;
            StateChanged?.Invoke(this, _deviceState, DateTime.Now);
        }

        public event Action<IReportable, State, DateTime> StateChanged;
    }
}