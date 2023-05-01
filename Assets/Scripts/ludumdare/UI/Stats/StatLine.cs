using UnityEngine;
using UnityEngine.EventSystems;

namespace LudumDare.UI.Stats
{
    
    [ExecuteAlways]
    public class StatLine: UIBehaviour
    {
        [SerializeField]
        private RectTransform from;
        [SerializeField]
        private RectTransform to;

        private RectTransform _self;

        protected override void Awake()
        {
            _self = GetComponent<RectTransform>();
        }


        private void Update()
        {
            var diff = to.anchoredPosition - from.anchoredPosition;
            _self.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, diff.magnitude-4);
            _self.anchoredPosition = from.anchoredPosition + diff / 2f;
            _self.rotation = Quaternion.FromToRotation(Vector3.right, diff);
        }
    }
}