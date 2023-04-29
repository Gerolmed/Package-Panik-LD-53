using LudumDare.TimeControl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LudumDare.UI
{
    public class TimeControls: UIBehaviour
    {
        [SerializeField]
        private TimeControlManagerSocket controlManagerSocket;
        
        public void OnClick(TimeControlButton button)
        {

            controlManagerSocket.Instance.TimeMode = button.TimeMode;
            var buttons = GetComponentsInChildren<TimeControlButton>();
            foreach (var timeControlButton in buttons)
            {
                timeControlButton.SetActive(timeControlButton == button);
            }
        }
    }
}