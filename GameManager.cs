using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public bool playBefore;
    public bool resumeGame;
    public string lastPlayedMode;
    public bool readInstruc;
    public bool firstTimeIndoor;
    public bool firstTimeOutdoor;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resume()
    {
        if (lastPlayedMode == "Indoor - Easy")
        {
            resumeGame = true;
            SceneManager.LoadScene("Indoor");
        }
        else if (lastPlayedMode == "Outdoor - Easy")
        {
                resumeGame = true;
                SceneManager.LoadScene("Outdoor");
        }
    }
}
