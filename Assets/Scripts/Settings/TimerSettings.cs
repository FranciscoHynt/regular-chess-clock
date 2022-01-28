using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "TimerColorSettings", menuName = "Settings/Timer Color Settings", order = 0)]
    public class TimerSettings : ScriptableObject
    {
        public Gradient colors;
    }
}