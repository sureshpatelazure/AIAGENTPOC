using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentPOC.Model
{
    public class AIAgentInput    {
        public string Name { get; set; }
        public string Instructions { get; set; }
        public List<KernelArguments> Arguments {get;set;}

        public Kernel Kernel { get; set; }
    }
}
