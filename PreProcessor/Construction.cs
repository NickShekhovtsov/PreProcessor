using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace PreProcessor
{
    class Construction
    {
        public List<Kernel> kernels { get; set; } = new List<Kernel>();
        public List<double> nodesLoad { get; set; } = new List<double>();
        public int totalWidth { get; set; } = 0;
        public bool leftSealing { get; set; } = false;
        public bool rightSealing { get; set; } = false;
        public void SaveToJson() {
            string JsonString = JsonSerializer.Serialize(this);
            File.WriteAllText("construction.json", JsonString);
        }

        public void LoadFromJson()
        {
            
        }

    }
}
