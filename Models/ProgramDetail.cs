using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Starting_Project.Models
{
    public class ProgramDetail
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string? ProgramId { get; set; }
        public string ProgramName { get; set; }
        public List<PersonalDetail> PersonalDetails { get; set; }
        public List<Question>? Questions { get; set; }

        [JsonConstructor]
        public ProgramDetail()
        {
            Id = Guid.NewGuid().ToString(); // Initialize the Id property with a new GUID
        }
    }
}
