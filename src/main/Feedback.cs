using System;
using System.Collections.Generic;

namespace Pingvinen.MasterMindOfDoom
{
    public class Feedback
    {
        public virtual List<Match> Matches { get; set; } = new List<Match>();

        public virtual void Add(Match m)
        {
            Matches.Add(m);
        }

        public virtual int ValueAndPositionMatches => Matches.FindAll(x => x == Match.ValueAndPosition).Count;
        
        public virtual int ValueOnlyMatches => Matches.FindAll(x => x == Match.ValueOnly).Count;
    }
}