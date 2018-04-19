using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTest
{
    public static class Util
    {
        private static Random rnd = new Random();

        public static int GetRandom(int low, int high)
        {
            return rnd.Next(low, high);
        }
    }
}