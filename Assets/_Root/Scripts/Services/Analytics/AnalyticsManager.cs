using UnityEngine;
using Services.Analytics.UnityAnalytics;

namespace Services.Analytics
{
    internal class AnalyticsManager 
    {
        private IAnalyticsService[] _services;


        public AnalyticsManager() =>
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };


        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void SendStartGame() =>
            SendEvent("StartGame");

        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
    }
}
