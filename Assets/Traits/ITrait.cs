using Objects.Bots.Scripts;

namespace Traits
{
    public interface ITrait
    {
        public Bot Target { get; set; }
        public Bot Sender { get; set; }
        public string GetTraitText();

        public bool IsTraitOfMainTarget { get; set; }
    }
}