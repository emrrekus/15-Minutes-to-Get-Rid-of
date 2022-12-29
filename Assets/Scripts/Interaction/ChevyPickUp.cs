using UnityEngine;
using TMPro;

public class ChevyPickUp : MonoBehaviour, IInteractable
{
    public byte displayValue => displayVal;
    public byte itemValue => trunk;
    public string InteractionPrompt => prompt;  
    public GameObject itemUIPrompt => null;
    private InteractionPromptUI interactionPromptUI;

    [SerializeField] public const byte baseballandFlareGun = 1; 

    [SerializeField] Animator animator;
    [SerializeField] GameObject trunkObject;

    [SerializeField] TMP_Text promptText;
    private string prompt = "Open trunk.";  

    bool isTrunkOpened = false;

    const byte trunk = 0;
    public const byte displayVal = 0;

    public void Interact(Interactor interactor)
    {

        if (isTrunkOpened)
        {
            this.GetComponent<Renderer>().materials[1].color = new Color(0,0,0,0);

            trunkObject.SetActive(false);         
            IInteractable.weaponPicked?.Invoke(baseballandFlareGun);
            IInteractable.weaponEquipped?.Invoke();
            this.gameObject.GetComponent<Collider>().enabled = false;            
        }
       
        if (!isTrunkOpened)
        {  
            animator.SetBool("isOpened", true);
            isTrunkOpened = true;
            promptText.text = "Pick up bat.";
        }

    }

    public void EndInteraction(Interactor interactor)
    {
        animator.SetBool("isOpened", false);
        isTrunkOpened = false;
    }
}
