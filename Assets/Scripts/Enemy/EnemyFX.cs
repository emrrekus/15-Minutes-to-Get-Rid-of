using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFX: MonoBehaviour
{
    [SerializeField] AudioSource enemySFX;
    [SerializeField] AudioSource enemySounds;
    public AudioClip[] soundClips;
   

    void PlayDeath()
    {
        StartCoroutine(AudioSwitcher(soundClips.Length-1));
    }

    void PlayRandomHit()
    {
        enemySFX.PlayOneShot(soundClips[Random.Range(0, 2)]);
    }

    IEnumerator AudioSwitcher(int sfx)
    {
        yield return new WaitForSeconds(2f);
        enemySounds.PlayOneShot(soundClips[sfx]);
    }

    private void OnEnable()
    {
        Zombies.enemyDied += PlayDeath;
        Zombies.enemyTookDamage += PlayRandomHit; 
    }
    
    private void OnDisable()
    {
        Zombies.enemyDied -= PlayDeath;
        Zombies.enemyTookDamage -= PlayRandomHit;
    }

}
