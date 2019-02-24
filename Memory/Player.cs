using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    [Serializable]
    class Player
    {
        public string Name { get; set; }
        public int ClickCount { get; set; }
        public string TimePassed { get; set; }
    }
}
