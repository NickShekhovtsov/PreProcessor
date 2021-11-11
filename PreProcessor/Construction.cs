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
        public void SaveToJson(string name) {
            string JsonString = JsonSerializer.Serialize(this);
            
            File.WriteAllText(name, JsonString);
        }

        public void LoadFromJson(string filename)
        {
            string fileText = File.ReadAllText(filename);
            Construction cnst = JsonSerializer.Deserialize<Construction>(fileText);
            this.kernels = cnst.kernels;
            this.nodesLoad = cnst.nodesLoad;
            this.totalWidth = cnst.totalWidth;
            this.rightSealing = cnst.rightSealing;
            this.leftSealing = cnst.leftSealing;
        }

    }
}
