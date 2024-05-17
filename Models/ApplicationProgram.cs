using Newtonsoft.Json;

namespace Starting_Project.Models
{
    public class ApplicationProgram
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public Dictionary<string, string> PersonalDetails { get; set; }
        public Dictionary<string, string> QuestionAnswers { get; set; }

        [JsonConstructor]
        public ApplicationProgram()
        {
            Id = Guid.NewGuid().ToString(); // Initialize the Id property with a new GUID
        }
    }
}
