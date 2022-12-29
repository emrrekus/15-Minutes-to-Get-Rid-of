using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    public static IInteractable interactable;

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.isDisplayed)
                {
                    if (interactable.displayValue == 1)
                    {
                        interactionPromptUI.DisplayPromptOnItem(interactable.itemUIPrompt);                       
                    }

                    if (interactable.displayValue == 0)
                    {
                        interactionPromptUI.DisplayPrompt(interactable.InteractionPrompt);
                    }
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(this);
                }
            }
        }

        else
        {
            if (interactionPromptUI.isDisplayed)
            {
               interactionPromptUI.ClosePrompt();

            } 
            
            if (interactable != null)
            {
                interactable.EndInteraction(this);
                interactable = null;
            }

           
        }
    }


}
