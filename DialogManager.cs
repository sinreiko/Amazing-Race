using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public ArrayList speech = new ArrayList();
    public Text textDisplay;//
    public Text displayTapContinue;//
    public string[] engSentences;
    public string[] chiSentences;
    private int index;
    public float typingSpeed;
    public float appearSpeed;

    public bool nextSentence;
    //public Animator textDisplayAnim;
    private AudioSource source;
    public static int touchCount;

    public bool chinese;
    public bool english;

    public GameObject person1Dialog; //
    public GameObject person2Dialog;//
    public GameObject tapContinue1;
    public GameObject tapContinue2;
    public Text speech1;
    public Text speech2;

    void Start()
    {
        //Get audio source
        source = GetComponent<AudioSource>();

        //For type writing first sentence
        StartCoroutine(DisplayLetter());
    }

    void Update()
    {
        if (index < 2)
        {
            person1Dialog.SetActive(true);
            person2Dialog.SetActive(false);
        }

        else if (index > 1)
        {
            person1Dialog.SetActive(false);
            person2Dialog.SetActive(true);
        }


        GameObject t = GameObject.FindGameObjectWithTag("dialog");
        //When user tap && check if the sentence has finish
        if (Input.touchCount > 0 && nextSentence == true) 
        {
            NextSentence();
        }

        //Loop for checking if the animation has finish so they can press next
        StartCoroutine(CheckConditionSentence());

        print(index);

    }

    IEnumerator CheckConditionSentence()
    {
        if (nextSentence == false) //limit the no. of time the bloew function runs 
        {
            Text tapContinue1Txt = tapContinue1.GetComponent<Text>();
            Text tapContinue2Txt = tapContinue2.GetComponent<Text>();

            //if typewrite effect is finished 
            if (speech1.text == engSentences[index] && english == true || speech2.text == engSentences[index] && english) //For english language
                {
                    //Enable player to press next
                    nextSentence = true;

                    //Wait for a few millisecond before tap next appear
                    yield return new WaitForSeconds(appearSpeed);
                    tapContinue1.SetActive(true);
                    tapContinue2.SetActive(true);
                }

                else if (speech1.text == chiSentences[index] && chinese == true || speech2.text == engSentences[index] && english) // For Chinese Language
                {
                    //Enable player to press next
                    nextSentence = true;

                    //Wait for a few millisecond before tap next appear
                    yield return new WaitForSeconds(appearSpeed);
                    tapContinue1.SetActive(true);
                    tapContinue2.SetActive(true);
                }


                else
                {
                    // hide tap next
                    tapContinue1.SetActive(false);
                    tapContinue2.SetActive(false);
                }
        }
    }




    IEnumerator DisplayLetter()
    {
        Text tapContinue1Txt = tapContinue1.GetComponent<Text>();
        Text tapContinue2Txt = tapContinue2.GetComponent<Text>();

        if (english == true)
        {
            tapContinue1Txt.text = "Tap to continue";
            tapContinue2Txt.text = "Tap to continue";

            //Creating type writer effect
            foreach (char letter in engSentences[index].ToCharArray())
            {
                speech1.text += letter;
                speech2.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        else if (chinese == true)
        {
            tapContinue1Txt.text = "点按即可继续";
            tapContinue2Txt.text = "点按即可继续";

            //Creating type writer effec
            foreach (char letter in chiSentences[index].ToCharArray())
            {
                speech1.text += letter;
                speech2.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

    public void NextSentence()
    {
        //textDisplayAnim.SetTrigger("fadeIn");

        //play audio sond and reset sentences to default condition
        source.Play();
        nextSentence = false;

        if (index < engSentences.Length - 1)
        {
            index++;
            speech1.text = "";
            speech2.text = "";
            StartCoroutine(DisplayLetter());
        }
        else
        {
            speech1.text = "";
            speech2.text = "";
            nextSentence = false;
        }
    } 
}
