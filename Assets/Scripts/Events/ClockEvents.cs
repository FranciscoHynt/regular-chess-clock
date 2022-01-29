using Enumerators;
using UnityEngine.Events;

namespace Events
{
    public static class ClockEvents
    {
        public static readonly PauseClockEvent PauseClockEvent = new PauseClockEvent();
        public static readonly ChangePlayerEvent ChangePlayerEvent = new ChangePlayerEvent();
        public static readonly ConfigureClockEvent ConfigureClockEvent = new ConfigureClockEvent();
        public static readonly ClockTimeEndedEvent ClockTimeEndedEvent = new ClockTimeEndedEvent();
        public static readonly ChangeClockStateEvent ChangeClockStateEvent = new ChangeClockStateEvent();
    }

    public class PauseClockEvent : UnityEvent{}
    public class ChangePlayerEvent : UnityEvent<PlayerPiece>{}
    public class ClockTimeEndedEvent : UnityEvent<PlayerPiece>{}
    public class ChangeClockStateEvent : UnityEvent<ClockState>{}
    public class ConfigureClockEvent : UnityEvent<ConfigureClockEventData>{}

    public readonly struct ConfigureClockEventData
    {
        public int ClockTime { get; }
        public int ExtraSeconds { get; }

        public ConfigureClockEventData(int clockTime, int extraSeconds)
        {
            ClockTime = clockTime;
            ExtraSeconds = extraSeconds;
        }
    }
}