using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessing.Models
{
    // interface for group and score
    public interface IStream
    {
        int Score(int outer);
    }
}
