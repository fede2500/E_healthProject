namespace DefaultNamespace
{
    public class Question
    {
        private string question;
        private string[] answers;

        public void setQuestion(string question)
        {
            this.question = question;
        }

        public void setAnswers(string[] answers)
        {
            this.answers = answers;
        }

        public string getQuestion()
        {
            return question;
        }

        public string[] getAnswers()
        {
            return answers;
        }
    }
    
}