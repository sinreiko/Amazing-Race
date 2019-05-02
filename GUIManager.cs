using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum UIs
{
    RIDDLE,
    AR,
    SCOREBOARD,
    MCQ
}

public class GUIManager : MonoBehaviour
{
    
    MainMenuController m_MainMenu;
    StateManager m_stateManager;
    [Header("Mechanics")]
    public double time;
    public double currentTime;
    [Header("UI")]
    public GameObject Riddle;
    public GameObject AR;
    public GameObject Question;
    public GameObject Scoreboard;
    public GameObject modePage;
    public GameObject gamePage;
    public GameObject video;
    public GameObject dialog;
    //--------------------------------------------- Math game ----------------------------
    //ref to the score
    public Text inGameScoreText;

    //ref to score text in game over panel
    public Text scoreOverText;
    //ref to hiscore text in game over panel
    public Text hiScoreOverText;

    //ref to game over panel
    public GameObject gameOverMenu;
    //ref to game over panel animator
    public Animator gameOverAnim;

    //ref to background music
    private AudioSource bgMusic;
    //------------------------------------------------------------------------------------
    [Header("ARCamera")]
    public GameObject ARCamera;
    [Header("Scenes")]
    Scene currentScene;
    public string sceneName;
    public string[] temp;
    // Start is called before the first frame update
    void Start()
    {
        m_stateManager = FindObjectOfType<StateManager>();
        time = video.gameObject.GetComponent<VideoPlayer>().clip.length;
        m_MainMenu = FindObjectOfType<MainMenuController>();
        temp = m_MainMenu.randScenes;
        ActivateUI(UIs.RIDDLE);

        if (m_stateManager != null && m_stateManager.resumeGame)
        {
            modePage.SetActive(false);
            gamePage.SetActive(true);
            m_stateManager.resumeGame = false;
        }
        //check for the music statis and depending on that we make AudioListener vol 1 or 0
        if (GameManager.singleton.isMusicOn == true)
        {
            AudioListener.volume = 1;
        }
        else if (GameManager.singleton.isMusicOn == false)
        {
            AudioListener.volume = 0;
        }

        //we get the audioSource attached to this object
        bgMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (video != null) { 
            currentTime = Mathf.RoundToInt((float)video.gameObject.GetComponent<VideoPlayer>().time);
            //print(currentTime + " : " + time);
        
            if (currentTime >= time)
            {
                video.SetActive(false);
                dialog.SetActive(false);
            }
        }

        //---------------------------------- Math game --------------------------------------------------
        //we check for the game manager
        if (sceneName == "Math") { 
            if (GameManager.singleton != null)
            {
                //and keep updating score value
                inGameScoreText.text = GameManager.singleton.currentScore.ToString();
            }

            //we check if the game is over 
            if (GameManager.singleton.isGameOver == true)
            {
                //if yes the we stop the music and diplay game over panel
                bgMusic.Stop();

                //we update the score text and hi score text
                scoreOverText.text = "" + GameManager.singleton.currentScore;

                hiScoreOverText.text = "" + GameManager.singleton.hiScore;

                //we play the slideIn animation
                gameOverAnim.Play("SlideIn");
            }
        }
        //-------------------------------------------------------------------------------------------------------
    }
    #region App Navigation
    public void GameNav(string context)
    {
        switch (context)
        {
            case "openEasyIndoor":
                modePage.SetActive(true);
                gamePage.SetActive(false);
                break;
            case "closeEasyIndoor":
                modePage.SetActive(false);
                gamePage.SetActive(true);
                if (m_stateManager.playBefore == false)
                {
                    m_stateManager.lastPlayedMode = "Indoor - Easy";
                    m_stateManager.playBefore = true;
                }
                break;
            case "openEasyOutdoor":
                modePage.SetActive(true);
                gamePage.SetActive(false);
                break;
            case "closeEasyOutdoor":
                modePage.SetActive(false);
                gamePage.SetActive(true);
                if (m_stateManager.playBefore == false)
                {
                    m_stateManager.lastPlayedMode = "Outdoor - Easy";
                    m_stateManager.playBefore = true;
                }
                break;
        }
    }
    #endregion

    #region Activate AR|Riddle|Scoreboard|Mcq
    public void ActivateUI(UIs type)
    {
        if (type.Equals(UIs.RIDDLE))
        {
            StartCoroutine("ActivateRiddle");
        }
        else if (type.Equals(UIs.AR))
        {
            StartCoroutine("ActivateAR");
        }
        else if (type.Equals(UIs.SCOREBOARD))
        {
            StartCoroutine("ActivateSB");
        }
        else if (type.Equals(UIs.MCQ))
        {
            StartCoroutine("ActivateMCQ");
        }
    }
    IEnumerator ActivateAR()
    {
        Riddle.SetActive(false);
        Scoreboard.SetActive(false);
        Question.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        AR.SetActive(true);
        ARCamera.SetActive(true);
    }

    IEnumerator ActivateRiddle()
    {
        AR.SetActive(false);
        ARCamera.SetActive(false);
        Scoreboard.SetActive(false);
        Question.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Riddle.SetActive(true);
    }

    IEnumerator ActivateSB()
    {
        AR.SetActive(false);
        ARCamera.SetActive(false);
        Riddle.SetActive(false);
        Question.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Scoreboard.SetActive(true);
    }

    IEnumerator ActivateMCQ()
    {
        AR.SetActive(false);
        ARCamera.SetActive(false);
        Riddle.SetActive(false);
        Scoreboard.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Question.SetActive(true);
    }

    #endregion

    #region NextScene Artifact|Riddle
    public void PlayNextScene()
    {
        List<string> list = new List<string>(temp);
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        int loadedScene;
        int loadScene;

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("<color=purple>RandList: </color>" + list[i]);

            if (list[i] == sceneName && i < list.Count)
            {
                loadedScene = i;
                loadScene = loadedScene + 1;
                SceneManager.LoadScene(list[loadScene]);
                Debug.Log("loadedScene: " + loadedScene + "loadScene: " + list[loadScene]);
                break;
            }
            else
            {
                //do nothing
            }
        }

    }

    #endregion

    #region Page Navigation 
    public void GoToMCQ()
    {
        ActivateUI(UIs.MCQ);
    }

    //----------------------------------------------- Math Game --------------------------------------------------------------
    //ref method to retry button
    public void RetryButton()
    {
        //Application.LoadLevel("GamePlay"); // if you are using unity below 5.3 version
        //when player press retry button the game play scene is loaded and the game over bool is made false
        SceneManager.LoadScene("MathGamePlay");//use this for 5.3 version
        //we make it false because  we need to play the game again
        GameManager.singleton.isGameOver = false;
    }

    //this method is used for taking screen shot and sharing it on any social app , mail , etc
    public void ShareButton()
    {
        ShareScreenScript.instance.ButtonShare(); //this is for android change it for iOS
    }

    //ref method for home button
    public void HomeButton()
    {
        //Application.LoadLevel("MainMenu"); // if you are using unity below 5.3 version
        SceneManager.LoadScene("MathMainMenu");
        GameManager.singleton.isGameOver = false;
    }

    //ref method for back button
    public void BackButton()
    {
        //Application.LoadLevel("ModeSelector"); // if you are using unity below 5.3 version
        SceneManager.LoadScene("ModeSelector");
    }
    //------------------------------------------------------------------------------------------------------------
    #endregion
}


