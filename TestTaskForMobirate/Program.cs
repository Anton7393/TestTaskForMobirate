using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForMobirate
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("M=");
                var m = Int16.Parse(Console.ReadLine());
                Console.Write("N=");
                var n = Int16.Parse(Console.ReadLine());
                if ((m < 0) || (n < 0)) throw new InputError("Invalid matrix size!");

                Matrix matrix = new Matrix(m, n);
                Console.WriteLine("Input matrix:");
                matrix.fill();
                Console.WriteLine("Result:");
                matrix.printSequences();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
