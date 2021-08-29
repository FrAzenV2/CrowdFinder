using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.LevelControllers.Scripts
{
    public class PoiList : MonoBehaviour
    {
        [SerializeField] private POI _defaultPlayerPOI;
        public List<POI> Pois;
        public POI CurrentPlayerPOI => _currentPOI;
        
        private void OnEnable()
        {
            foreach (var poi in Pois)
            {
                poi.PlayerSteppedIn += ResetAllCameraPriorities;
            }

            _currentPOI = _defaultPlayerPOI;
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