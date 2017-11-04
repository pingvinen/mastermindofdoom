using System.Collections.Generic;

namespace Pingvinen.MasterMindOfDoom
{
    public class Code
    {
        public Code()
        {
        }

        public Code(params int[] values)
        {
            Slots.AddRange(values);
        }
        
        /// <summary>
        /// The values making up the code
        /// </summary>
        public virtual List<int> Slots { get; set; } = new List<int>();

        public virtual int Length => Slots.Count;
    }
}