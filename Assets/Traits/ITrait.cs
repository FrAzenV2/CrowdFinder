﻿using Objects.Bot.Scripts;

namespace Traits
{
    public interface ITrait
    {
        public Bot Target { get; set; }
        public Bot Sender { get; set; }
        public string GetTraitText();

    }
}