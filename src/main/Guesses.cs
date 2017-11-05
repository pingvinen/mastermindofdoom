using System.Collections.ObjectModel;

namespace Pingvinen.MasterMindOfDoom
{
    public class Guesses : Collection<Guess>
    {
        public virtual bool IsEmpty => Items.Count == 0;
    }
}