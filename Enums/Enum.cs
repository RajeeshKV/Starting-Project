using Microsoft.AspNetCore.Components;
using System;

namespace Starting_Project.Enums
{
    public class Enum
    {
        public enum Nationality
        {
            American,
            British,
            Canadian,
            French,
            German,
            Indian
        }
        public enum Gender
        {
            Male,
            Female
        }
        public enum QuestionType
        {
            Paragraph,
            YesNo,
            Dropdown,
            Date,
            Number
        }
    }
}
