using UnityEngine;
using TMPro;
public class Combat_Bat : MonoBehaviour
{
    [SerializeField] AudioSource batAudioSource;
    [SerializeField] AudioClip[] batAudioClips;

    [SerializeField] TMP_Text batLifeText;
 
    float batDamage = 150f; 
    float batLife = 100;
    float batWornPerc = 1f;

    int counter = 0;


    private void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Zombies>().GetDamage(batDamage);

            batLife -= batWornPerc;
            counter++;

            if (counter == 7)
            {
                BatWornOut();
            }
        }
    }

    void BatWornOut()
    {
        counter = 0;
        batLifeText.text = batLife.ToString() + "%";
        batDamage -= 5;
    }


    public void PlayBatSound()
    {
        batAudioSource.PlayOneShot(batAudioClips[0]);
    }
}
