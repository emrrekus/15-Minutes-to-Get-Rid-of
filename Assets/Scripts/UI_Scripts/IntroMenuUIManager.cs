using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject introUI;

    [SerializeField] GameObject introductionText;
    [SerializeField] GameObject controlsText;

    bool isGamePaused = false;
    [SerializeField] Button bttnNext;
    [SerializeField] Button bttnPrevious;

    public VideoPlayer video;

  

    private void Start()
    {
       CancelInvoke();
       video.loopPointReached += ActivateIntroUI;
    }

    private void ActivateIntroUI(VideoPlayer video)
    {
        introUI.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void Next()
    {
        introductionText.SetActive(false);
        controlsText.SetActive(true);

        bttnNext.interactable = false;
        bttnPrevious.interactable = true;
    }

    public void Previous()
    {
        introductionText.SetActive(true);
        controlsText.SetActive(false);
        bttnNext.interactable = true;
        bttnPrevious.interactable = false;
    }

   
}
