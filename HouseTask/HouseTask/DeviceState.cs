namespace HouseTask
{
    public class DeviceState : State
    {
        public DeviceState(DeviceStateEnum current)
        {
            Current = current;
        }

        public DeviceStateEnum Current { get; }
    }
}