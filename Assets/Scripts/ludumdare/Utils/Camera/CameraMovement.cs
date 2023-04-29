using UnityEngine;

namespace LudumDare.Utils.Camera
{
    public class CameraMovement: MonoBehaviour
    {

        [SerializeField]
        private CameraPositionLock cameraPositionLock;
        
        private void Update()
        {
            if(!MouseMovement()) KeyboardMovement();
        }


        public void KeyboardMovement()
        {
            
        }

        public bool MouseMovement()
        {
            return false;
        }
    }
}