using UnityEngine;
public class Rifle : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => prompt;  
    public GameObject itemUIPrompt => uiPanel; 
    public byte displayValue => displayVal;    
    public byte itemValue => rifleVal;

    [SerializeField] GameObject uiPanel;
    
    private string prompt = "Take rifle.";   
    public const byte displayVal = 1;
    public const byte rifleVal = 2;

    public void EndInteraction(Interactor interactor)
    {
        uiPanel.SetActive(false);
    }

    public void Interact(Interactor interactor)
    {
        IInteractable.weaponPicked?.Invoke(rifleVal);
        IInteractable.weaponEquipped?.Invoke();
        this.gameObject.SetActive(false);
    }

}
