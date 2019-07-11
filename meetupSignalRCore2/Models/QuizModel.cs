using System.Collections.Generic;

namespace meetupSignalRCore2.Models
{
    public class QuizModel
    {
        public string Question { get; set; }
        public List<int> Answers { get; set; }

        public QuizModel()
        {
            Answers = new List<int>();
        }
    }
}
