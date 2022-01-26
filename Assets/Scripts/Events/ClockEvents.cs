using Enum;
using UnityEngine.Events;

namespace Events
{
    public static class ClockEvents
    {
        public static readonly ConfigureClockEvent ConfigureClockEvent = new ConfigureClockEvent();
        public static readonly ChangePlayerEvent ChangePlayerEvent = new ChangePlayerEvent();
    }

    public class ConfigureClockEvent : UnityEvent<ConfigureClockEventData>{}
    public class ChangePlayerEvent : UnityEvent<PlayerPieces>{}
    
    public readonly struct ConfigureClockEventData
    {
        public int ClockTime { get; }
        public int ExtraSeconds { get; }

        public ConfigureClockEventData(int clockTime, int extraSeconds)
        {
            this.ClockTime = clockTime;
            this.ExtraSeconds = extraSeconds;
        }
    }
}