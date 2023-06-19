using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionControllerScript : MonoBehaviour
{
    public TextAsset textFile;
    static GameObject GameCanvas;
    static GameObject Button1;
    static GameObject Button2;
    static GameObject Button3;
    static GameObject Button4;
    static GameObject BlurPanel;
    static GameObject UnclickablePanel;
    static GameObject ResultMenu;
    [SerializeField] TMP_Text result;
    Transform[] buttons;
    public static TextAsset text;
    static public List<Question> questionsList;
    public static int CurrentQuestion = 0;
    bool[] buttonsState = new bool[4];
    public int Score = 0;
    public static int TimesClicked = 0;
    public static string theme;
    System.Random rng;

    void Start()
    {
        rng = new System.Random();
        string fileContents = text.text;
        //Debug.Log(fileContents);
        questionsList = new QuestionList(fileContents).list;
        GameCanvas = GameObject.Find("Canvas");
        BlurPanel = GameCanvas.transform.Find("BlurPanel").gameObject;
        UnclickablePanel = GameCanvas.transform.Find("UnclickablePanel").gameObject;
        Button1 = GameObject.Find("Button1");
        Button2 = GameObject.Find("Button2");
        Button3 = GameObject.Find("Button3");
        Button4 = GameObject.Find("Button4");
        buttons = new Transform[4] { Button1.transform, Button2.transform, Button3.transform, Button4.transform };
        ResultMenu = GameCanvas.transform.Find("ResultMenu").gameObject;
        questionsList = Question.GetQuestionsList(fileContents);
        ShuffleQuestions(questionsList, rng);
    }

    void Update()
    {
        
    }
    void ShuffleQuestions(List<Question> questionList, System.Random rng)
    {
        questionList.Shuffle(rng);
        foreach (var question in questionsList)
        {
            question.Answers.Shuffle(rng);
        }
    }
    public void NextButtonAction()
    {
        switch (TimesClicked)
        {
            case 0:
                SubmitAnswer();
                LockAnswerButtons(true);
                break;
            case 1:
                if (CurrentQuestion != questionsList.Count - 1)
                {
                    ResetAnswerButtons(buttons);
                    CurrentQuestion++;
                    TimesClicked--;
                }
                else
                {
                    TimesClicked++;
                    BlurPanel.SetActive(true);
                    ResultMenu.SetActive(true);
                    result.text = string.Format("Ваш результат:\n{0} / {1}", Score, questionsList.Count);
                    float percentile = 100.0f * Score / questionsList.Count;
                    if (percentile > PlayerPrefs.GetFloat(theme))
                        PlayerPrefs.SetFloat(theme, percentile);
                    Debug.Log(percentile);
                }
                LockAnswerButtons(false);
                break;
            case 2:
                TimesClicked = 0;
                CurrentQuestion = 0;
                LoadSceneByName("Menu");
                break;
        }
    }
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ResetGameplay()
    {
        Score = 0;
        CurrentQuestion = 0;
    }
    void LockAnswerButtons(bool state)
    {
        UnclickablePanel.SetActive(state);
    }
    void ResetAnswerButtons(Transform[] buttons)
    {
        for (int i = 0; i < buttonsState.Length; i++)
        {
            buttonsState[i] = false;
        }
        foreach (var button in buttons)
        {
            var selected = button.Find("Selected");
            var correct = button.Find("Correct");
            var incorrect = button.Find("Incorrect");
            selected.gameObject.SetActive(false);
            correct.gameObject.SetActive(false);
            incorrect.gameObject.SetActive(false);
        }
    }
    public void SubmitAnswer()
    {
        if (CheckAnswer())
            Score++;
        var question = questionsList[CurrentQuestion];
        for (int i = 0; i < question.Answers.Count; i++)
        {
            var button = GameObject.Find("Button" + (i+1).ToString());
            if (question.RightAnswers.Contains(question.Answers[i]))
            {
                var correctPanel = button.transform.Find("Correct");
                correctPanel.gameObject.SetActive(true);
            }
            else if (buttonsState[i])
            {
                var incorrectPanel = button.transform.Find("Incorrect");
                incorrectPanel.gameObject.SetActive(true);
            }
        }
        TimesClicked++;
    }
    public bool CheckAnswer()
    {
        var question = questionsList[CurrentQuestion];
        var receivedAnswers = new List<string>();
        for(int i = 0; i < buttonsState.Length; i++)
            if (buttonsState[i])
                receivedAnswers.Add(question.Answers[i]);
        if (receivedAnswers.Count != question.RightAnswers.Count)
            return false;
        for (int i = 0; i < receivedAnswers.Count; i++)
            if (receivedAnswers[i] != question.RightAnswers[i])
                return false;
        return true;
    }
    public void SelectCurrentButton()
    {
        var buttonName = EventSystem.current.currentSelectedGameObject.name;
        SelectButton(GameObject.Find(buttonName).transform);
        var buttonNumber = buttonName[buttonName.Length-1] - '0' - 1;
        if (buttonsState[buttonNumber] == false)
            buttonsState[buttonNumber] = true;
        else
            buttonsState[buttonNumber] = false;
    }
    void SelectButton(Transform button)
    {
        var panel = button.Find("Selected").gameObject;
        bool selected = panel.activeSelf;
        if (!selected)
            panel.SetActive(true);
        else
            panel.SetActive(false);
    }
}