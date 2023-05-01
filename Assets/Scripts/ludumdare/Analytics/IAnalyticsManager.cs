using LudumDare.Hub.Buildings;

namespace LudumDare.Analytics
{
    public interface IAnalyticsManager
    {
        public void UnlockAnalyticsByUpgrade((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade);
    }
}