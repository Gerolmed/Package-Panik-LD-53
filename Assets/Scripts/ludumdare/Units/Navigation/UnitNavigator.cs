using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare.Units.Navigation
{
    public class UnitNavigator: MonoBehaviour
    {

        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private PathNodeEvent onEnter;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private DeliveryUnit unit;

        public DeliveryUnit Unit
        {
            get => unit;
            set => unit = value;
        }


        public void StartTraversal(IReadOnlyList<PathNode> path)
        {
            StartCoroutine(StartInnerTraversal(path));
        }
        
        private IEnumerator StartInnerTraversal(IReadOnlyList<PathNode> path)
        {
            SetPos(path[0].Pos);
            for (var i = 1; i < path.Count; i++)
            {
                yield return TravelTowards(path[i - 1], path[i]);
                onEnter.Invoke(path[i]);
            }
        }


        private IEnumerator TravelTowards(PathNode from, PathNode to)
        {
            Vector2 fromPos = from.Pos;
            Vector2 toPos = to.Pos;

            UpdateSprite(fromPos, toPos);
            
            var currentPos = fromPos;
            while (currentPos != toPos)
            {
                yield return 0;
                currentPos = Vector2.MoveTowards(currentPos, toPos, Time.deltaTime * speed);
                SetPos(currentPos);
            }
        }


        private void UpdateSprite(Vector2 fromPos, Vector2 toPos)
        {
            var diff = fromPos - toPos;
            var icon =  diff switch
            {
                {y: > 0} => Unit.FacingUp,
                {y: < 0} => Unit.FacingDown,
                {x: < 0} => Unit.FacingLeft,
                _ => Unit.FacingRight
            };

            spriteRenderer.sprite = icon;
        }


        private void SetPos(Vector2 pos)
        {
            transform.position = pos;
        }
    }
    
    [Serializable]
    public class PathNodeEvent: UnityEvent<PathNode> {}

    public struct PathNode
    {
        public Vector2Int Pos { get; set; }
        public PathType Type { get; set; }
    }

    public enum PathType
    {
        Road,
        Target,
        Hub
    }
}