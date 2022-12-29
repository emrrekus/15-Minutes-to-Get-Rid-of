using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyDamage : MonoBehaviour
{
    float enemyDamage = 10;

    public static Action<float> playerHit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHit?.Invoke(enemyDamage);
           ///other.GetComponent<Character>().GetDamage(enemyDamage);
        }
    }

}
