using UnityEngine;

namespace Objects.Bots.Scripts
{
    public class BotClothes : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hatRenderer;
        [SerializeField] private SpriteRenderer _shirtRenderer;
        [SerializeField] private SpriteRenderer _pantsRenderer;

        public void SetupHat(Sprite hatSprite)
        {
            _hatRenderer.sprite = hatSprite;
        }

        public void SetupShirt(Sprite shirtSprite)
        {
            _shirtRenderer.sprite = shirtSprite;
        }

        public void SetupPants(Sprite pantsSprite)
        {
            _pantsRenderer.sprite = pantsSprite;
        }
    }
}