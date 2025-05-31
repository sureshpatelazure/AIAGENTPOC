namespace AIAgentPOC.AgentOrchestration.Model
{
    public class AgentOrchestrationConfig
    {
        public string? AIConnectorName { get; set; }
        public string? IsPluginPresent { get; set; }
        public List<AgentModel>? Agent { get; set; }

    }
    public class AgentModel
    {
        public string? Name { get; set; }
        public string? YamlPromptFilePath { get; set; }
    }

}