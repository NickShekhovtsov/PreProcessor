using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessor
{
    class Construction
    {
        public List<Kernel> kernels { get; set; } = new List<Kernel>();
        public List<double> nodesLoad { get; set; } = new List<double>();
        public int totalWidth { get; set; } = 0;
        public void SaveToJson() { }

    }
}
