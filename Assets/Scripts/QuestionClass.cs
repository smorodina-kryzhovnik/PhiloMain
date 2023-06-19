using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;

public class Question
{
    public string Text;
    public List<string> Answers = new List<string>();
    public List<string> RightAnswers = new List<string>();

    public Question(string input)
    {
        var lines = input.Split(';');
        //foreach (var line in lines)
            //Debug.Log(line);
        this.Text = lines[0];
        for (var answerCounter = 1; answerCounter < lines.Length; answerCounter++)
        {
            var answer = lines[answerCounter];
            if (answer[answer.Length - 1] == '*')
            {
                var trimmedAnswer = answer.TrimEnd('*');
                this.RightAnswers.Add(trimmedAnswer);
                this.Answers.Add(trimmedAnswer);
            }
            else
                this.Answers.Add(answer);
        }
    }
    
    public static List<Question> GetQuestionsList(string input)
    {
        List<Question> result = new List<Question>();
        var questions = input.Split('=').ToList();
        foreach(var question in questions)
        {
            result.Add(new Question(question));
        }
        return result;
    }
}
public class QuestionList
{
    public List<Question> list = new List<Question>();
    public QuestionList(string input)
    {
        var list = input.Split('=');
        foreach(var e in list)
        {
            this.list.Add(new Question(e));
            //Debug.Log("dobavlen vopros");
        }
    }
}
static class Extensions
{
    public static void Shuffle<T>(this IList<T> list, System.Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}