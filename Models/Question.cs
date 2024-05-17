using static Starting_Project.Enums.Enum;

namespace Starting_Project.Models
{
    public class Question
    {
        public QuestionType Type { get; set; }
        public string QuestionText { get; set; }
        public List<DropdownChoice>? DropdownChoices { get; set; }
    }
}
