using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/Sound Settings")]
    public class SoundSettings : ScriptableObject
    {
        [Header("Sounds")]
        [Range(0, 1)] public float buttonClick;
        [Range(0, 1)] public float clockChange;
        [Range(0, 1)] public float clockEndTime;
    }
}