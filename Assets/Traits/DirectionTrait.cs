using Objects.Bots.Scripts;
using UnityEngine;

namespace Traits
{
    [CreateAssetMenu(fileName = "New Direction Trait", menuName = "Traits/Direction", order = 0)]
    public class DirectionTrait : ScriptableObject, ITrait
    {
        public Vector2 Direction;
        public Bot Target { get; set; }
        public Bot Sender { get; set; }

        public bool IsTraitOfMainTarget { get; set; }
        [SerializeField] private string[] _directionsText = {"Downwards", "Upwards", "Left", "Right"};


        public string GetTraitText()
        {
            string traitText;

            if (Direction.Equals(Vector2.down))
                traitText = _directionsText[0];
            else if (Direction.Equals(Vector2.up))
                traitText = _directionsText[1];
            else if (Direction.Equals(Vector2.left))
                traitText = _directionsText[2];
            else
                traitText = _directionsText[3];

            return traitText;
        }

        public void CalculateDirection(Bot target, Bot sender)
        {
            Target = target;
            Vector2 startPos = sender.transform.position;
            Vector2 targetPos = target.transform.position;
            var direction = targetPos - startPos;
            direction.Normalize();
            var x = direction.x;
            var y = direction.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
                Direction = x > 0 ? Vector2.right : Vector2.left;
            else
                Direction = y > 0 ? Vector2.up : Vector2.down;
        }
    }
}