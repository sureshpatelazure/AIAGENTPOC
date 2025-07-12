using AIAgentLib.Model;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentLib.SemanticKernalService
{
    internal class HuggingFaceChatCompletionService : IAIServiceConnector
    {
        public Kernel BuildChatCompletion<T>(T connectorConfiguration) where T : AIConnectorServiceConfiguration
        {
            var builder = CreateHuggingFaceKernelBuilder(connectorConfiguration);
            return builder.Build();
        }

        public Kernel BuildChatCompletion<T>(T connectorConfiguration, List<object> Plugins) where T : AIConnectorServiceConfiguration
        {
            var builder = CreateHuggingFaceKernelBuilder(connectorConfiguration);

            foreach (var plugin in Plugins)
            {
                builder.Plugins.AddFromObject(plugin);
            }

            return builder.Build();
        }

        private IKernelBuilder CreateHuggingFaceKernelBuilder<T>(T connectorConfiguration) where T : AIConnectorServiceConfiguration
        {
            if (connectorConfiguration is HuggingFaceConnectorServiceConfiguration huggingFace)
            {
                var builder = Kernel.CreateBuilder();
                builder.AddHuggingFaceChatCompletion(huggingFace.ModelId, new Uri(huggingFace.Uri), huggingFace.ApiKey);
                return builder;
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }
    }
}
