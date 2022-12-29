using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    float _timeRemaining = 900;
    public bool _timerIsRunning = false;
    public TMP_Text _timeText;
    [SerializeField] GameObject gameOver;
    private void Start()
    {
        // Starts the timer automatically
        _timerIsRunning = true;
    }
    void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;

                if(_timeRemaining <= 0)
                {
                    gameOver.SetActive(true);
                   
                    StartCoroutine(Waitor());
                }

                DisplayTime(_timeRemaining);
            }
            else
            {
                _timeRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    IEnumerator Waitor()
    {
        yield return new WaitForSeconds(1.5f); 
        Time.timeScale = 0;  Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }
}
