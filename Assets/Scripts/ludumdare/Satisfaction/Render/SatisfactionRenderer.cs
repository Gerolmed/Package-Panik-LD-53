using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LudumDare.Satisfaction.Render
{
    public class SatisfactionRenderer: UIBehaviour
    {

        [SerializeField]
        private SatisfactionManagerSocket satisfactionSocket;
        
        [SerializeField]
        private Animator[] emotes;
        [SerializeField]
        private Sprite[] bars;
        [SerializeField]
        private Image barImage;
        [SerializeField]
        private Slider slider;

        private static readonly int Active = Animator.StringToHash("active");


        private void Update()
        {
            var satisfaction = satisfactionSocket.Instance;
            var percentage = satisfaction.Percentage;

            slider.value = 1-percentage;

            barImage.sprite = bars[Mathf.Min(Mathf.FloorToInt(percentage * bars.Length), bars.Length-1)];

            var emoteIndex = Mathf.Min(Mathf.FloorToInt(percentage * emotes.Length), emotes.Length-1);
            
            for (var i = 0; i < emotes.Length; i++)
            {
                emotes[i].SetBool(Active, i == emoteIndex);
            }
        }
    }
}