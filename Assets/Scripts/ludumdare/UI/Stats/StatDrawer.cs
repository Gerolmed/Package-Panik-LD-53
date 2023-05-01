using System.Linq;
using LudumDare.Delivery;
using LudumDare.Districts;
using LudumDare.TimeControl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LudumDare.UI.Stats
{
    public class StatDrawer: UIBehaviour
    {
        [SerializeField]
        private RectTransform[] points;
        [SerializeField]
        private TimeControlManagerSocket timeControlManagerSocket;
        [SerializeField]
        private DistrictManagerSocket districtManagerSocket;

        [SerializeField]
        private RectTransform top;
        [SerializeField]
        private TMP_Text deliveryCountText;
        
        [SerializeField]
        private RectTransform bottom;

        private RandomMathFunction _mathFunction = new();

        protected void Update()
        {
            CalculateStats();
        }


        private void CalculateStats()
        {
            var day = GetDayCycle();
            var cyclesPerDay = timeControlManagerSocket.Instance.CyclesPerDay;

            var scores = points.Select((_, i) =>
                GetDayStats(day + i * cyclesPerDay, cyclesPerDay)).ToList();
            
            var highest = scores.Max();
            var lowest = scores.Min();

            var range = highest - lowest;
            range = Mathf.Max(range, 1);

            var heightRange = top.anchoredPosition.y - bottom.anchoredPosition.y;

            for (var i = 0; i < points.Length; i++)
            {
                var rectTransform = points[i];
                var posPerc = (scores[i] - lowest) / (float) range;
                
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x,
                    bottom.anchoredPosition.y +
                    heightRange * posPerc);
            }

            deliveryCountText.text = $"{scores[0]:n0}";
        }


        private int GetDayStats(int day, int cyclesPerDay)
        {
            var total = 0f;


            foreach (var district in districtManagerSocket.Instance.GetUnlocked())
            {
                var timeManager = timeControlManagerSocket.Instance;
                var districtCycleOffset = Mathf.FloorToInt(timeManager.CyclesPerDay *
                    district.UnlockedSince / 60 / 24);
                    
                for (var i = 0; i < cyclesPerDay; i++)
                {
                    total += _mathFunction.GetAvgForDay(day + i - districtCycleOffset);
                }
            }

            return (int) total;
        }


        private int GetDayCycle()
        {
            var time = timeControlManagerSocket.Instance.IngameTime;
            var day = Mathf.FloorToInt(time/ (60 * 24));
            return day * timeControlManagerSocket.Instance.CyclesPerDay;
        }
    }
}