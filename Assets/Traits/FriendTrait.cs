using Objects.Bots.Scripts;
using UnityEngine;
using System;
using СlothesConfigs.ScriptableObjectConfig;

namespace Traits
{
    public class FriendTrait : ScriptableObject, ITrait
    {
        public ITrait trait1;
        public ITrait trait2;

        public Bot Target { get; set; }
        public Bot Sender { get; set; }

        public bool IsTraitOfMainTarget { get; set; }

        public bool Equals(ITrait other)
        {
            if (other.GetType() != GetType())
                return false;
            // TODO: Check for traits in reverse order
            if (other.Target == Target && 
                trait1.Equals(((FriendTrait) other).trait1) && trait2.Equals(((FriendTrait) other).trait2)
                || trait1.Equals(((FriendTrait) other).trait2) && trait2.Equals(((FriendTrait) other).trait1))
                return true;
            return false;
        }

        public string GetTraitText()
        {
            var trait = $"My friend {trait1.GetTraitText()} and {trait2.GetTraitText()}, they might help";
            return trait;
        }
    }
}