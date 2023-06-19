using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionDisplayScript : MonoBehaviour
{
    [SerializeField] TMP_Text questionCounter;
    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text answer1;
    [SerializeField] TMP_Text answer2;
    [SerializeField] TMP_Text answer3;
    [SerializeField] TMP_Text answer4;   
    List<Question> questionList;
    public int questionPointer = 0;
        
    void Start()
    {
        questionList = QuestionControllerScript.questionsList;
        LoadCurrentQuestion();
    }

    void Update()
    {
        if (questionPointer != QuestionControllerScript.CurrentQuestion)
        {
            questionPointer = QuestionControllerScript.CurrentQuestion;
            LoadCurrentQuestion();
        }
    }

    void LoadCurrentQuestion()
    {
        questionCounter.text = string.Format("Вопрос {0}/{1}", questionPointer + 1, questionList.Count);
        var question = questionList[QuestionControllerScript.CurrentQuestion];
        questionText.text = question.Text;
        answer1.text = question.Answers[0];
        answer2.text = question.Answers[1];
        answer3.text = question.Answers[2];
        answer4.text = question.Answers[3];
    }
}