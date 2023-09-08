namespace MIS.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        // ...
    }
}
