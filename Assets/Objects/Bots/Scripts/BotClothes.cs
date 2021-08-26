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
            if(hatSprite==null) _hatRenderer.color = Color.clear;
            else
            {
                _hatRenderer.sprite = hatSprite;
                _hatRenderer.color = Color.white;
            }
            
        }

        public void SetupShirt(Sprite shirtSprite)
        {
            if(shirtSprite==null) _shirtRenderer.color = Color.clear;
            else
            {
                _shirtRenderer.sprite = shirtSprite;
                _shirtRenderer.color = Color.white;
            }
        }

        public void SetupPants(Sprite pantsSprite)
        {
            if(pantsSprite==null) _pantsRenderer.color = Color.clear;
            else
            {
                _pantsRenderer.sprite = pantsSprite;
                _pantsRenderer.color = Color.white;
            }
        }

        public void UpdateSortingOrder(int sortingOrder)
        {
            _hatRenderer.sortingOrder = sortingOrder;
            _pantsRenderer.sortingOrder = sortingOrder;
            _shirtRenderer.sortingOrder = sortingOrder;
        }
    }
}