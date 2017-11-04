using System;

namespace Pingvinen.MasterMindOfDoom
{
    /// <summary>
    /// The "code maker", i.e. the entity responsible
    /// for making up and knowing the code and providing
    /// feedback on guesses made by the code breaker.
    /// </summary>
    public class CodeMaker
    {
        private readonly Randoom rng;

        public CodeMaker(Randoom rng)
        {
            this.rng = rng;
        }

        /// <summary>
        /// The generated code
        /// </summary>
        public virtual Code Code { get; set; }

        /// <summary>
        /// Generate the code
        /// </summary>
        /// <param name="length">How long the code should be</param>
        /// <param name="numberOfOptions">How many possible values each slot can hove</param>
        /// <exception cref="InvalidOperationException">Thrown if the instance already has a code</exception>
        public virtual void GenerateCode(int length, int numberOfOptions)
        {
            if (Code != default(Code))
            {
                throw new InvalidOperationException("I already have a code!");
            }

            Code = new Code();
            Code.Slots.AddRange(rng.GetSequence(length, 0, numberOfOptions));
        }
    }
}