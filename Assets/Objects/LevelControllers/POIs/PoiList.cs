using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.LevelControllers.Scripts
{
    public class PoiList : MonoBehaviour
    {
        public List<POI> Pois;

        public POI CurrentPlayerPOI => _currentPOI;
        
        private void OnEnable()
        {
            foreach (var poi in Pois)
            {
                poi.PlayerSteppedIn += ResetAllCameraPriorities;
            }
        }

        private void OnDisable()
        {
            foreach (var poi in Pois)
            {
                poi.PlayerSteppedIn -= ResetAllCameraPriorities;
            }
        }

        private void ResetAllCameraPriorities(object sender, EventArgs eventArgs)
        {
            foreach (var poi in Pois)
            {
                poi.ResetCameraPriority();
            }

            _currentPOI = (POI) sender;
        }

        private POI _currentPOI;
    }
}