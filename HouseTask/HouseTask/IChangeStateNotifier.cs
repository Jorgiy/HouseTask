using System;

namespace HouseTask
{
    public interface IChangeStateNotifier : IReportable
    {
        event Action<IReportable, State, DateTime> StateChanged;
    }
}