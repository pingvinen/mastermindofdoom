using System;
using System.Linq;

namespace Pingvinen.MasterMindOfDoom
{
    public class Game
    {
        private readonly CodeMaker codeMaker;

        public Game(CodeMaker codeMaker)
        {
            this.codeMaker = codeMaker;
        }

        public virtual void Play()
        {
            Console.WriteLine("The computer generates a code that you have to break.");
            Console.WriteLine("For each guess you will get feedback in the form of");
            Console.WriteLine(" ! meaning that an element of your code is correct");
            Console.WriteLine(" - meaning that an element of your code has the correct value, but incorrect position");
            Console.WriteLine();
            Console.WriteLine("How long do you want the code to be? (default: 4)");
            var codeLength = ReadInt(4);
            
            Console.WriteLine("How many different values can a key contain? (default 6)");
            var numOptions = ReadInt(6);
            
            Console.WriteLine($"Available values in the code: {string.Join(", ", Enumerable.Range(0, numOptions))}");
            
            var human = new HumanCodeBreaker();

            codeMaker.GenerateCode(codeLength, numOptions);
            
            System.IO.File.WriteAllText("/tmp/codemaker_code", string.Join(" ", codeMaker.Code.Slots));

            Guess guess;
            while (true)
            {
                try
                {
                    guess = human.GetNextGuess();
                    guess.Feedback = codeMaker.CheckGuess(guess.Code);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    human.RemoveLastGuess();
                    continue;
                }
                
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (guess != default(Guess) && guess.IsCorrect)
                {
                    Console.WriteLine("Yay!! You guesses the code :)");
                    break;
                }
            }
        }

        private static int ReadInt(int defaultValue)
        {
            var input = Console.ReadLine();

            if (input.Trim().Equals(string.Empty))
            {
                return defaultValue;
            }

            return Convert.ToInt32(input);
        }
    }
}