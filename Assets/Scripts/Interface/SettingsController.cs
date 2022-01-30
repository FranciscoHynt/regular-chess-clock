using Assets;
using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Interface
{
    public class SettingsController : MonoBehaviour
    {
        private Toggle soundToggle;
        private GameSettings settings;

        private void Awake()
        {
            settings = MainAssets.I.gameSettings;
            soundToggle = GetComponentInChildren<Toggle>();

            soundToggle.isOn = settings.shouldPlaySounds;
        }

        public void SoundsSwitch(bool value)
        {
            settings.shouldPlaySounds = value;
        }
    }
}