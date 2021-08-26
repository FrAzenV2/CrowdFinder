using Objects.Bots.Scripts;
using Objects.LevelControllers;
using UnityEngine;

public class POIBotSetter : MonoBehaviour
{
    [SerializeField] private POI _poi;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.TryGetComponent(out Bot bot)) return;

        bot.SetCurrentPoi(_poi);
    }
}
