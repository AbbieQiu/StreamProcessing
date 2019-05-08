using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessing.Models
{
    public class Garbage : IStream
    {
        private readonly string _garbage;

        public Garbage(string garbage)
        {
            _garbage = garbage;
        }

        public int Score(int outer)
        {
            return 0;
        }
    }
}
