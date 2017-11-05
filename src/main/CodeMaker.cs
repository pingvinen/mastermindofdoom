using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Check a guess against the actual code and provide feedback
        /// </summary>
        /// <param name="guess">The code to check</param>
        /// <returns>Feedback on the guess</returns>
        /// <exception cref="ArgumentException">Thrown if the guess has a different length than the code</exception>
        public virtual Feedback CheckGuess(Code guess)
        {
            if (guess.Length != Code.Length)
            {
                throw new ArgumentException($"Actual code has {Code.Length} values but guess has {guess.Length}");
            }
            
            var feedback = new Feedback();

            var counts = GetCounts(Code);

            
            //
            // first count exact matches
            //
            for (var i = 0; i < guess.Length; i++)
            {
                var value = guess.Slots[i];
                
                if (value == Code.Slots[i])
                {
                    feedback.Add(Match.ValueAndPosition);
                    counts[value] -= 1;
                }
            }
            
            //
            // count "value" matches
            //
            for (var i = 0; i < guess.Length; i++)
            {
                var value = guess.Slots[i];
                
                if (value != Code.Slots[i] && counts.ContainsKey(value) && counts[value] > 0)
                {
                    feedback.Add(Match.ValueOnly);
                    counts[value] -= 1;
                }
            }
            
            return feedback;
        }

        /// <summary>
        /// Get a dictionary with the count of each distinct
        /// value in the given code.
        /// 
        /// E.g.
        /// The code 1,1,2,2,1
        /// results in the dictionary
        /// 1 = 3
        /// 2 = 2
        /// </summary>
        /// <param name="c">The code to count</param>
        private static Dictionary<int, int> GetCounts(Code c)
        {
            return c.Slots
                .Distinct()
                .ToDictionary(
                    num => num,
                    num => c.Slots.FindAll(x => x == num).Count
                );
        }
    }
}