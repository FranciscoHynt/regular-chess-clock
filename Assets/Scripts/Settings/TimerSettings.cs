using System;
using System.Collections.Generic;
using System.Linq;
using Enumerators;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "TimerColorSettings", menuName = "Settings/Timer Color Settings", order = 0)]
    public class TimerSettings : ScriptableObject
    {
        public Gradient colors;
        public List<ClockTimerData> timersData;

        public ClockTimerData GetClockTimerData(ClockTimeState clockState)
        {
            return timersData.First(data => data.clockTimeState == clockState);
        }
    }

    [Serializable]
    public struct ClockTimerData
    {
        public int fontSize;
        public string dateFormat;
        public ClockTimeState clockTimeState;
    }
}