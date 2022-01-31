using UnityEngine;

namespace Interface
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject exitButton;

        private void Start()
        {
            if (Application.platform != RuntimePlatform.WindowsPlayer)
                exitButton.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}