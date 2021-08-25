using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Objects.LevelControllers.Scripts
{
    public class PoiList : MonoBehaviour
    {
        public List<POI> Pois;

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

        private void ResetAllCameraPriorities()
        {
            foreach (var poi in Pois)
            {
                poi.ResetCameraPriority();
            }
        }
    }
}