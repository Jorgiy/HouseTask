namespace HouseTask
{
    public class SourceState : State
    {
        public SourceState(SourceStateEnum current)
        {
            Current = current;
        }

        public SourceStateEnum Current { get; }
    }
}