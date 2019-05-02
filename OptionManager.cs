using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject music;
    public GameObject mOn;
    public GameObject mOff;
    public GameObject sfx;
    public GameObject sOn;
    public GameObject sOff;

    // Start is called before the first frame update
    public void adjustVolume(string context)
    {
        switch (context)
        {
            case "musicOn":
                music.SetActive(true);
                mOn.SetActive(false);
                mOff.SetActive(true);
                break;
            case "musicOff":
                music.SetActive(false);
                mOff.SetActive(false);
                mOn.SetActive(true);
                break;
            case "sfxOn":
                sfx.SetActive(true);
                sOn.SetActive(false);
                sOff.SetActive(true);
                break;
            case "sfxOff":
                sfx.SetActive(false);
                sOff.SetActive(false);
                sOn.SetActive(true);
                break;
        }
    }
}
