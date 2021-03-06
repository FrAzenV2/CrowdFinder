using UnityEngine;
using Dialogs;
using СlothesConfigs.ScriptableObjectConfig;

namespace Bots_Configs.ScriptableObjectConfig
{
    [CreateAssetMenu(fileName = "New Bot Config", menuName = "Bots/new Bot Config", order = 0)]
    public class BotConfig : ScriptableObject
    {
        [SerializeField] private string _botName;
        [SerializeField] private ClothesConfig[] _clothes;
        [SerializeField] private Sprite _baseSprite;
        [SerializeField] private int _botId;

        public bool IsBotStatic;
        
        public DialogSO TargetFoundDialog;
        public DialogSO FakeTargetDialog;
        public DialogSO CorrectTargetDialog;

        public int BotId
        {
            get => _botId;
            set => _botId = value;
        }

        public string BotName
        {
            get => _botName;
            set => _botName = value;
        }
        public ClothesConfig[] Clothes => _clothes;
        public Sprite BodySprite => _baseSprite;
    
        public void SetName(string botName)
        {
            _botName = botName;
        }

        public void SetBaseSprite(Sprite sprite)
        {
            _baseSprite = sprite;
        }

        public void SetClothes(ClothesConfig[] clothesConfigs)
        {
            _clothes = clothesConfigs;
        }

        public void Initialize(BotConfig config)
        {
            _botName = config.BotName;
            _clothes = config.Clothes;
            _baseSprite = config.BodySprite;
            TargetFoundDialog = config.TargetFoundDialog;
            FakeTargetDialog = config.FakeTargetDialog;
            CorrectTargetDialog = config.CorrectTargetDialog;
        }
    }
}