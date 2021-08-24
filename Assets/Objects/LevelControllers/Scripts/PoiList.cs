using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Objects.LevelControllers.Scripts
{
    public class PoiList : MonoBehaviour
    {

        public IReadOnlyList<POI> Pois => _pois;

        private void Awake()
        {
            _pois = FindObjectsOfType<POI>().ToList();
        }

        private List<POI> _pois;
    }
}