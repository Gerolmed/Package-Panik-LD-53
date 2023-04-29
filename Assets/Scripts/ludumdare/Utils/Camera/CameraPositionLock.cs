using Cinemachine;
using UnityEngine;

namespace LudumDare.Utils.Camera
{
    
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's Y co-ordinate
    /// </summary>
    [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class CameraPositionLock : CinemachineExtension
    {
        [SerializeField]
        public float lowerY = 3.12f;
        [SerializeField]
        public float upperY = 15;
        [SerializeField]
        public float lowerX = -10;
        [SerializeField]
        public float upperX = 10;
 
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.y = Mathf.Max(lowerY, Mathf.Min(pos.y, upperY));
                pos.x = Mathf.Max(lowerX, Mathf.Min(pos.x, upperX));
                state.RawPosition = pos;
            }
        }
        
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var lowerPos = new Vector3(lowerX, lowerY, -1);
            var upperPos = new Vector3(upperX, upperY, 1);
            var diff = upperPos - lowerPos;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(lowerPos + diff * .5f, diff);
        }
#endif
    }
}