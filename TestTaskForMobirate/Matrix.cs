using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForMobirate
{
    using Pointer = Tuple<Int16, Int16>;

    class Matrix
    {
        private Int16 M;
        private Int16 N;
        private char[,] values;

        private Pointer stopPointer = new Pointer(-1, -1);

        public Matrix(Int16 m, Int16 n)
        {
            M = m;
            N = n;
            values = new char[m, n];
        }

        public void fill()
        {
            for (Int16 i = 0; i < M; ++i)
            {
                readLine(i);
            }
        }

        public void printSequences()
        {
            var result = new List<Sequence>();
            result.AddRange(findSequences(new Pointer(0, 0), colMove, rowMove, '-'));
            result.AddRange(findSequences(new Pointer(0, 0), rowMove, colMove, '|'));
            result.AddRange(findSequences(new Pointer((Int16)(M - 1), 0), directWalk, diagonalMove, '\\'));
            result.AddRange(findSequences(new Pointer((Int16)(M - 1), (Int16)(N - 1)), inverseWalk, reDiagonalMove, '/'));
            result.ForEach(secqunce => Console.WriteLine(secqunce));
        }

        private void readLine(Int16 row)
        {
            Int16 col = 0;
            var input = Console.ReadLine();
            input.Split().ToList().ForEach(item =>
            {
                if (!item.Length.Equals(1)) throw new InputError("Invalid input format!");
                if (col.Equals(N)) throw new InputError("Invalid row too large!");
                values[row, col] = item[0];
                ++col;
            });
            if (!col.Equals(N)) throw new InputError("Invalid row too short!");
        }

        private IEnumerable<Sequence> findSequences(Pointer head, Func<Pointer, Pointer> moveHead,
                                                    Func<Pointer, Pointer> move, char direction)
        {
            var result = new List<Sequence>();
            while (!head.Equals(stopPointer))
            {
                result.AddRange(findInLine(head, move, direction));
                head = moveHead(head);
            }
            return result;
        }

        private IEnumerable<Sequence> findInLine(Pointer head, Func<Pointer, Pointer> move, char direction)
        {
            Int16 counter = 0;
            var position = head;
            var result = new List<Sequence>();
            while (true)
            {
                var next = move(position);
                if (next.Equals(stopPointer)) break;
                var value = getValue(position);
                var nextValue = getValue(next);
                var isEquals = nextValue.Equals(value);
                if (!isEquals && !counter.Equals(0))
                {
                    result.Add(new Sequence(direction, value,
                                            (Int16)(counter + 1),
                                            head));
                    counter = 0;
                }
                if (isEquals && counter.Equals(0)) head = position;
                if (isEquals) ++counter;
                position = next;
            }
            if (!counter.Equals(0))
                result.Add(new Sequence(direction,
                                        getValue(position),
                                        (Int16)(counter + 1),
                                        head));
            return result;
        }

        private char getValue(Pointer pointer)
        {
            return values[pointer.Item1, pointer.Item2];
        }

        private Pointer rowMove(Pointer current)
        {
            var next = new Pointer(current.Item1, (Int16)(current.Item2 + 1));
            return isPointerValid(next) ? next : stopPointer;
        }

        private Pointer colMove(Pointer current)
        {
            var next = new Pointer((Int16)(current.Item1 + 1), current.Item2);
            return isPointerValid(next) ? next : stopPointer;
        }

        private Pointer diagonalMove(Pointer current)
        {
            var next = new Pointer((Int16)(current.Item1 + 1), (Int16)(current.Item2 + 1));
            return isPointerValid(next) ? next : stopPointer;
        }

        private Pointer reDiagonalMove(Pointer current)
        {
            var next = new Pointer((Int16)(current.Item1 + 1), (Int16)(current.Item2 - 1));
            return isPointerValid(next) ? next : stopPointer;
        }

        private Pointer directWalk(Pointer current)
        {
            var next = current.Item1.Equals(0)
                ? new Pointer(current.Item1, (Int16)(current.Item2 + 1))
                : new Pointer((Int16)(current.Item1 - 1), current.Item2);
            return isPointerValid(next) ? next : stopPointer;
        }

        private Pointer inverseWalk(Pointer current)
        {
            var next = current.Item1.Equals(0)
                ? new Pointer(current.Item1, (Int16)(current.Item2 - 1))
                : new Pointer((Int16)(current.Item1 - 1), current.Item2);
            return isPointerValid(next) ? next : stopPointer;
        }

        private bool isPointerValid(Pointer pointer)
        {
            if ((pointer.Item1 < 0) || (pointer.Item1 >= M)) return false;
            if ((pointer.Item2 < 0) || (pointer.Item2 >= N)) return false;
            return true;
        }
    }
}
