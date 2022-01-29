using System;
using System.Collections.Generic;
using System.Linq;
using Enumerators;
using Events;
using UnityEngine;

namespace Interface
{
    public class InterfaceController : MonoBehaviour
    {
        [SerializeField] private List<PanelData> panelDatas;

        private InterfacePanel currentPanel;
        
        private const string WINDOW_IN = "WindowIn";
        private const string WINDOW_OUT = "WindowOut";

        public InterfaceController()
        {
            InterfaceEvents.EnablePanelEvent.AddListener(EnablePanel);
        }

        public void EnablePanel(String panelName)
        {
            Enum.TryParse(panelName, out InterfacePanel menuPlace);
            GetPanelAnimators(menuPlace).Play(WINDOW_IN);
            GetPanelAnimators(currentPanel).Play(WINDOW_OUT);

            currentPanel = menuPlace;
        }

        public void DisablePanel(String panelName)
        {
            Enum.TryParse(panelName, out InterfacePanel menuPlace);
            GetPanelAnimators(menuPlace).Play(WINDOW_OUT);
            GetPanelAnimators(InterfacePanel.Home).Play(WINDOW_IN);
        }

        private Animator GetPanelAnimators(InterfacePanel panel)
        {
            return panelDatas.First(data => data.id == panel).animator;
        }
    }

    [Serializable]
    public class PanelData
    {
        public InterfacePanel id;
        public Animator animator;
    }
}