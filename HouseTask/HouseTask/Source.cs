using System;

namespace HouseTask
{
    public class Source : Stationare, IChangeStateNotifier
    {
        private State _sourceState = new SourceState(SourceStateEnum.Servicable);
        
        public Source(string name, string documentation, SourceType sourceType) : base(name, documentation)
        {
            SourceType = sourceType;
        }

        public State State => _sourceState;

        public void SetSourceState(SourceState sourceState)
        {
            _sourceState = sourceState;
            StateChanged?.Invoke(this, _sourceState, DateTime.Now);
        }

        public SourceType SourceType { get; }
        
        public event Action<IReportable, State, DateTime> StateChanged;
    }
}