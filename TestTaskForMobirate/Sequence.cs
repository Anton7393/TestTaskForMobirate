using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForMobirate
{
    using Pointer = Tuple<Int16, Int16>;

    class Sequence
    {
        private char symbol;
        private Int16 count;
        private Pointer head;
        private char direction;

        public Sequence(char direction, char symbol,
            Int16 count, Pointer head)
        {
            this.symbol = symbol;
            this.count = count;
            this.head = head;
            this.direction = direction;
        }

        public override string ToString()
        {
            return direction + String.Format(" [{0} {1}] ", head.Item1 + 1, head.Item2 + 1) + symbol + " " + count.ToString();
        }
    }
}
