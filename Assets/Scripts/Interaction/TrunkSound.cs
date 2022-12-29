using UnityEngine;

public class TrunkSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    void PlayAudioTrunk()
    {
        audioSource.Play();
    }

}
