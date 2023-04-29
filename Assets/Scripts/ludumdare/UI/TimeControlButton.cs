using System;
using LudumDare.TimeControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LudumDare.UI
{
    public class TimeControlButton: UIBehaviour
    {
        [SerializeField]
        private TimeControlButtonEvent onClick;

        [SerializeField]
        private TimeMode timeMode;
        
        [SerializeField]
        private Sprite normalState;
        
        [SerializeField]
        private Sprite pressedState;

        public TimeMode TimeMode => timeMode;


        public void DoClick()
        {
            onClick.Invoke(this);
        }


        public void SetActive(bool active)
        {
            GetComponent<Image>().sprite = active ? pressedState : normalState;
        }
    }
    
    [Serializable]
    public class TimeControlButtonEvent: UnityEvent<TimeControlButton> {}
}