using Bots_Configs.ScriptableObjectConfig;
using UnityEngine;

namespace Objects.Bot.Scripts
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public BotConfig Config => _config;
        
        public void Initialize(BotConfig config)
        {
            _config = config;
            _renderer.sprite = _config.BodySprite;
        }
        
        private BotConfig _config;
    }
}