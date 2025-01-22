// Models/QuestionModel.cs
using System.Collections.Generic;

namespace TeamWorkSpaces.LeisureModule
{
    [System.Serializable]
    public class QuestionData
    {
        public List<Question> questions;
    }

    [System.Serializable]
    public class DictionaryData
    {
        public List<string> cybersecurity_words;
    }

    [System.Serializable]
    public class Question
    {
        public int id;
        public string text;
        public List<CorrectAnswer> correct_answers;
    }

    [System.Serializable]
    public class CorrectAnswer
    {
        public string answer;
        public List<string> words;
    }
}
