using UnityEngine.Events;

namespace Events
{
    public static class ClockEvents
    {
        public static readonly StartClockEvent StartClockEvent = new StartClockEvent();
    }

    public class StartClockEvent : UnityEvent<StartClockEventData>{}
    
    public struct StartClockEventData
    {
        public int ClockTime { get; set; }
        public int ExtraTime { get; set; }

        public StartClockEventData(int clockTime, int extraTime)
        {
            this.ClockTime = clockTime;
            this.ExtraTime = extraTime;
        }
    }
}