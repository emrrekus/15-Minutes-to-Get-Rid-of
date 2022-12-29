using UnityEngine;

public class Combat_Bullet : MonoBehaviour
{
    float bulletDamage = 350f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Zombies>().GetDamage(bulletDamage);
        }
    }
}
