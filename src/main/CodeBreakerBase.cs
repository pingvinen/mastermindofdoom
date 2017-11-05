using System;

namespace Pingvinen.MasterMindOfDoom
{
    public abstract class CodeBreakerBase
    {
        public virtual string Name { get; set; }
        
        public virtual Guesses Guesses { get; set; } = new Guesses();

        public abstract Guess GetNextGuess();
    }
}