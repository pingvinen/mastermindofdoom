using System;

namespace Pingvinen.MasterMindOfDoom
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MasterMind of Doom");
            
            var rng = new Randoom();
            var codeMaker = new CodeMaker(rng);
            
            var game = new Game(codeMaker);
            
            game.Play();
        }
    }
}
