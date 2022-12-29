using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuTransition : MonoBehaviour
{
    void Start()
    {
        SceneManager.UnloadSceneAsync(4);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

   
}
