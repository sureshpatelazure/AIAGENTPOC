using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentLib.AIService
{
    public interface IChatCompletionService
    {
        public Task<string?> ChatWithAIAgent(string userInput);


    }
}
