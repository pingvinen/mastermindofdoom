using System.Collections.Generic;

namespace Pingvinen.MasterMindOfDoom
{
    public class Code
    {
        /// <summary>
        /// The values making up the code
        /// </summary>
        public virtual List<int> Slots { get; set; } = new List<int>();
    }
}