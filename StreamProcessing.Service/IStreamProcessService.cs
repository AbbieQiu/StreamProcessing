using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamProcessing.Models;

namespace StreamProcessing.Service
{
    public interface IStreamProcessService
    {
        int GetScore();
        void SetInput(string input);
        Group ParseGroup();
    }
}
