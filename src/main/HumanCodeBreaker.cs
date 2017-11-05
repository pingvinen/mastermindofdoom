using System;
using System.Linq;

namespace Pingvinen.MasterMindOfDoom
{
    public class HumanCodeBreaker : CodeBreakerBase
    {
        public override Guess GetNextGuess()
        {
            //
            // print history
            //
            if (!Guesses.IsEmpty)
            {
                Console.WriteLine("Your previous guesses");
                Console.WriteLine("---------------------");
                
                for (var i = 0; i < Guesses.Count; i++)
                {
                    PrintGuess(i+1, Guesses[i]);
                }
            }
            
            //
            // pick up new guess
            //
            var g = new Guess();
            
            Console.Write("Enter your next guess: ");
            var input = Console.ReadLine();
            var parts = input.Split(new [] {" "}, StringSplitOptions.RemoveEmptyEntries);
            
            g.Code.Slots.AddRange(parts.ToList().Select(x => Convert.ToInt32(x)));
            
            //
            // "save" the guess
            //
            Guesses.Add(g);

            return g;
        }

        private static void PrintGuess(int guessNumber, Guess g)
        {
            Console.Write($"{guessNumber}: ");
            Console.Write(string.Join(" ", g.Code.Slots));
            Console.Write(" => ");
            
            g.Feedback.Matches.ForEach(m =>
            {
                switch (m)
                {
                    case Match.ValueAndPosition:
                        Console.Write("! ");
                        break;
                    
                    case Match.ValueOnly:
                        Console.Write("- ");
                        break;
                        
                    default:
                        throw new NotSupportedException($"Do not know how to render {m}");    
                }
            });
            
            Console.WriteLine();
        }

        public virtual void RemoveLastGuess()
        {
            Guesses.RemoveAt(Guesses.Count - 1);
        }
    }
}