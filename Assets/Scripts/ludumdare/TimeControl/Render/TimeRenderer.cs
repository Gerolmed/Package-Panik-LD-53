using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LudumDare.TimeControl.Render
{
    public class TimeRenderer: UIBehaviour
    {
        [SerializeField]
        private Image minutesDisplay;
        [SerializeField]
        private Image hoursDisplay;
        [SerializeField]
        private TMP_Text dateText;

        [SerializeField]
        private TimeControlManagerSocket timeSocket;

        [SerializeField]
        private Sprite[] minuteSprites;
        [SerializeField]
        private Sprite[] hourSprites;


        private void Update()
        {
            var manager = timeSocket.Instance;

            var hour = manager.HourOfDay;
            var minute = manager.MinuteOfHour;

            minutesDisplay.sprite = CalcSprite(minute, 60, minuteSprites);
            hoursDisplay.sprite = CalcSprite(hour, 24, hourSprites);

            dateText.text =
                "WEEK " + (manager.Week + 1) + " - " + ConvertDay(manager.DayOfWeek);
        }


        private string ConvertDay(int dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case 0: return "Monday";
                case 1: return "Tuesday";
                case 2: return "Wednesday";
                case 3: return "Thursday";
                case 5: return "Friday";
                case 6: return "Saturday";
                case 7: return "Sunday";
            }

            return dayOfWeek.ToString();
        }


        private static Sprite CalcSprite(int current, int max, Sprite[] sprites)
        {
            var percentage = current / (float) max;

            return sprites[Mathf.FloorToInt(percentage * sprites.Length)];
        }
    }
}