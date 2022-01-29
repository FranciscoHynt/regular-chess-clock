using Assets;
using Settings;
using UnityEngine;

namespace Interface
{
    public class SettingsController : MonoBehaviour
    {
        private GameSettings settings;

        private void Awake()
        {
            settings = MainAssets.I.gameSettings;
        }

        public void SoundsSwitch(bool value)
        {
            settings.shouldPlaySounds = value;
        }
    }
}