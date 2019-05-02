using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public GameObject quizComp;
    public GameObject correct;
    public GameObject[] wrong;
    public int noOfWrong;
    public int score; //temporarily
    public bool isCorrect;
    // Start is called before the first frame update
    void Start()
    {
        isCorrect = false;
        correct.SetActive(false);
        quizComp.SetActive(false);
        noOfWrong = 0;
        for (int i = 0; i < wrong.Length; i++)
        {
            wrong[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckAnswer(GameObject image)
    {
        if(image.transform.tag == "Correct")
        {
            image.SetActive(true);
            quizComp.SetActive(true);
            isCorrect = true;
            CheckAttempt(isCorrect);
        } else if (image.transform.tag == "Wrong" && !isCorrect)
        {
            image.SetActive(true);
            noOfWrong += 1;
            isCorrect = false;
            CheckAttempt(isCorrect);
        }
    }

    void CheckAttempt(bool answer)
    {
        switch (noOfWrong)
        {
            case 0:
                if (answer == true)
                {
                    score += 100;
                }
                break;
            case 1:
                if (answer == true)
                {
                    Debug.Log("correct after 2nd attempt");
                    score += 50;
                }
                break;
            default:
                score += 0;
                ShowAnswer();
                break;

        }
    }
    void ShowAnswer()
    {
        correct.SetActive(true);
        quizComp.SetActive(true);
    }
}
