using Enumerators;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Interface
{
    public class MatchEndController : MonoBehaviour
    {
        [SerializeField] private Sprite blackPiece;
        [SerializeField] private Sprite whitePiece;
        [SerializeField] private Image winnerImage;
        [SerializeField] private TextMeshProUGUI winnerText;

        private const string WIN_MESSAGE = "has won on time!";

        public MatchEndController()
        {
            ClockEvents.ClockTimeEndedEvent.AddListener(EndMatch);
        }

        private void EndMatch(PlayerPiece pieces)
        {
            switch (pieces)
            {
                case PlayerPiece.White:
                    winnerText.text = $"White {WIN_MESSAGE}";
                    winnerImage.sprite = whitePiece;
                    break;
                case PlayerPiece.Black:
                    winnerText.text = $"Black {WIN_MESSAGE}";
                    winnerImage.sprite = blackPiece;
                    break;
            }
            
            InterfaceEvents.EnablePanelEvent.Invoke("EndMatch");
        }
    }
}