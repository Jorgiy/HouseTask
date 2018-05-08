using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseTask
{
    public class ConsumptionReportMaker : IReportMaker
    {
        private readonly Dictionary<IReportable, List<KeyValuePair<DateTime, State>>> _eventStorage;

        public ConsumptionReportMaker(Dictionary<IReportable, List<KeyValuePair<DateTime, State>>> deviceEventStorage)
        {
            _eventStorage = deviceEventStorage;
        }

        public string MakeReport(DateTime dateTime)
        {
            var consumptions = new Dictionary<SourceType, int>();

            foreach (SourceType sourceType in Enum.GetValues(typeof(SourceType)))
            {
                // fill dictinonary with any available source types
                consumptions.Add(sourceType, 0);
            }

            foreach (var stationareEntry in _eventStorage)
            {
                if (stationareEntry.Key is Device device)
                {
                    // retrive needed entry of each device
                    var neededState = stationareEntry.Value.OrderByDescending(x => x.Key)
                        .FirstOrDefault(x => x.Key <= dateTime).Value as DeviceState;

                    if (neededState == null || neededState.Current == DeviceStateEnum.NotWorking) continue;

                    // check it's source condition on those moment
                    var neededSourceState = _eventStorage[device.Source].OrderByDescending(x => x.Key)
                        .FirstOrDefault(x => x.Key <= dateTime).Value as SourceState;
                    
                    if (neededSourceState == null) continue;

                    // check battarey/gasholder etc status if main source was defected on those moment
                    if (neededSourceState.Current == SourceStateEnum.Defective)
                    {
                        if (device.SubSource == null) continue;
                        
                        var neededSubSourceState = _eventStorage[device.SubSource].OrderByDescending(x => x.Key)
                            .FirstOrDefault(x => x.Key <= dateTime).Value as SourceState;

                        if (neededSubSourceState == null || neededSubSourceState.Current == SourceStateEnum.Defective)
                            continue;
                    }
                    
                    consumptions[device.Source.SourceType] += device.Consumption;
                }
            }

            return string.Join(", ", consumptions.Select(x => $"{x.Key.ToString()} - {x.Value.ToString()}"));
        }
    }
}