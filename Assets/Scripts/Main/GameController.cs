using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private InputField clockTimeInputField;
        [SerializeField] private InputField extraTimeInputField;

        private void Awake()
        {
            clockTimeInputField.characterValidation = InputField.CharacterValidation.Integer;
            extraTimeInputField.characterValidation = InputField.CharacterValidation.Integer;
        }

        public void StartClock()
        {
            int clockTime = int.Parse(clockTimeInputField.text);
            int extraTime = int.Parse(extraTimeInputField.text);

            ClockEvents.StartClockEvent.Invoke(new StartClockEventData(clockTime, extraTime));
        }
    }
}