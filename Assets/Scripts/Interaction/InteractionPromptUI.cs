using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InteractionPromptUI : MonoBehaviour
{
    
    [SerializeField] GameObject uiPanel;

    public bool isDisplayed = false;
    [SerializeField] private TextMeshProUGUI promptText;

    public static bool canActivate = true;

    private void Awake()
    {

        uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = Camera.main.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up * 3);
    }

    public void DisplayPromptOnItem(GameObject itemUIPanel)
    {
        if(canActivate)
        {
            itemUIPanel.SetActive(true);
        }
        
    }
 

    public void DisplayPrompt(string prompText)
    {   
        promptText.text = prompText;
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void ClosePrompt()
    {
        uiPanel.SetActive(false);
        isDisplayed = false;
    }

}
