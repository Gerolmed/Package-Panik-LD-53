using LudumDare.Districts;
using UnityEngine;

namespace LudumDare.Utils.Camera
{
    public class CameraDistrictLocker: MonoBehaviour
    {
        [SerializeField]
        private CameraPositionLock cameraPositionLock;

        [SerializeField]
        private DistrictManagerSocket districtManager;

        private void Start()
        {
            var lowestPos = new Vector2();
            var highestPos = new Vector2();
            
            // Find lowest positions
            foreach (var district in districtManager.Instance.GetAll())
            {
                if (district.UpperBound.x > highestPos.x)
                {
                    highestPos.x = district.UpperBound.x;
                }
                if (district.UpperBound.y > highestPos.y)
                {
                    highestPos.y = district.UpperBound.y;
                }
                if (district.LowerBound.x < lowestPos.x)
                {
                    lowestPos.x = district.LowerBound.x;
                }
                if (district.LowerBound.y < lowestPos.y)
                {
                    lowestPos.y = district.LowerBound.y;
                }
            }

            cameraPositionLock.lowerX = lowestPos.x;
            cameraPositionLock.lowerY = lowestPos.y;
            cameraPositionLock.upperX = highestPos.x;
            cameraPositionLock.upperY = highestPos.y;
        }
    }
}