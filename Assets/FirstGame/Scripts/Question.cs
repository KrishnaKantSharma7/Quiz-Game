using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public Image imageQuestion;
    public Text textQuestion;
    public ToggleGroup toggleGroup;
    public GameObject goNextButton;
    public List<Sprite> spriteList;
    public List<Text> toggleTextList;
    public AudioClip acCorrect;
    public AudioClip acWrong;
    private int currentQuestionIndex = -1;
    private string correctAnswer;
    private List<string> optionAnswer;
    private string textOptionSelected;
    private AudioSource audioSource;
    private int score = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        goNextButton.SetActive(false);
        ShowNextQuestion();
    }
    private void ShowNextQuestion()
    {
        ++currentQuestionIndex;
        if(currentQuestionIndex == spriteList.Count)
        {
            Debug.Log("<color=red> Show Game Over </color>"+score);
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("End");
            return;
        }


        correctAnswer = spriteList[currentQuestionIndex].name;


        imageQuestion.sprite = spriteList[currentQuestionIndex];
        imageQuestion.SetNativeSize();

        optionAnswer = new List<string>();

        optionAnswer.Add(correctAnswer);

       
        optionAnswer.Add(spriteList[GetRandomIndex()].name);

        optionAnswer.Add(spriteList[GetRandomIndex()].name);

        optionAnswer.Add(spriteList[GetRandomIndex()].name);

        ShuffleAnswer();

        for(int i = 0; i < optionAnswer.Count; ++i) 
        {
            toggleTextList[i].text = optionAnswer[i];
        }

    }
    private void ShuffleAnswer()
    {
        string answer = optionAnswer[0];
        int rand = UnityEngine.Random.Range(0, optionAnswer.Count);
        string randstring = optionAnswer[rand];
        optionAnswer[rand] = answer;
        optionAnswer[0] = randstring;
    }
    private int GetRandomIndex()
    {
        int rand = UnityEngine.Random.Range(0, spriteList.Count);

        for (int i = 0; i < optionAnswer.Count; ++i)
        {
            if (optionAnswer[i].Equals(spriteList[rand].name))
                return GetRandomIndex();
        }
        return rand;
    }
    public void OnToggleClick(Text t)
    {
        textOptionSelected = t.text;
        goNextButton.SetActive(true);
       
    }
    public void onNextButtonClick()
    {


        if (checkCorrectAnswer())
        {           
            audioSource.clip = acCorrect;
            ++score;
        }
        else
        {           
            audioSource.clip = acWrong;
        }
        audioSource.Play();
        HideQuestion();
        ShowNextQuestion();
        Invoke("ShowQuestion", 0.5f);
        
    }

    private void ShowQuestion()
    {
        imageQuestion.gameObject.SetActive(true);

        textQuestion.gameObject.SetActive(true);

        toggleGroup.gameObject.SetActive(true);
    }

    private void HideQuestion()
    {
        imageQuestion.gameObject.SetActive(false);

        textQuestion.gameObject.SetActive(false);

        toggleGroup.SetAllTogglesOff(true);

        toggleGroup.gameObject.SetActive(false);

        goNextButton.SetActive(false);
    }

    private bool checkCorrectAnswer()
    {
        if (textOptionSelected.Equals(correctAnswer))
        {
            return true;
        }

        return false;
    }
   
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");

    }
   
}

