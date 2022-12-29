using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get;}
    public byte itemValue { get; } //value to add to inventory 
    public byte displayValue { get; } //1 to show prompt on object, 0 on player

    public GameObject itemUIPrompt { get; }

    public void Interact(Interactor interactor);
    public void EndInteraction(Interactor interactor);

    public static System.Action<byte> weaponPicked;
    public static System.Action weaponEquipped;
}
