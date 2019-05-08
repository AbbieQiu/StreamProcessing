using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessing.Models
{
    public class Group : IStream
    {
        private readonly IStream[] _content;

        public Group(IStream[] content)
        {
            _content = content;
        }

        // get score for each layer
        public int Score(int outer)
        {
            var thisLayer = outer + 1;
            var innerSum = _content.Sum(c => c.Score(thisLayer));
            return thisLayer + innerSum;
        }
    }
}
