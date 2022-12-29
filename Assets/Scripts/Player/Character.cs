using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Character : MonoBehaviour  // Character collision-trigger 
{
    const float plMaxHealth = 100;
    float plCurrentHealth;

    [SerializeField] AudioSource FXAudio;
    [SerializeField] AudioSource WalkAudio;
    [SerializeField] AudioClip woodPickSFX;
    [SerializeField] AudioClip[] FXClips;

    [SerializeField] Combat_Bat batScript;

    [SerializeField] GameObject playerHealthBar;
    [SerializeField] Image healthBarFill;

    [SerializeField] Collider batCollider;

    bool inEnemyRange = false;

    int sceneOnDeath = 3; // index of the scene to be loaded on death

    float sceneTransTimer = 5f;
    float healthResetTimer = 8f;

    public static System.Action woodPicked;
    public static System.Action inStokingRange;
    public static System.Action<bool> inStoveRange;
    public static event System.Action<bool> PlayerKilled;
    

    private void Awake()
    {
        FXAudio.clip = FXClips[0];  // audio take damage  
       // audio walk
        plCurrentHealth = plMaxHealth;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(inEnemyRange)
        healthBarFill.fillAmount = plCurrentHealth / plMaxHealth;
    }

    public void GetDamage(float damage)
    {
        FXAudio.PlayOneShot(FXClips[0]); // audio take damage 
      
        plCurrentHealth -= damage;


        if (plCurrentHealth <= 0)
        {
            WalkAudio.clip = FXClips[1]; // audio death
            WalkAudio.Play();

            PlayerKilled?.Invoke(false);
            PlayerDieAttacked();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieRange"))
        {
            inEnemyRange = true;
            playerHealthBar.SetActive(true);

            StopCoroutine("HealthReset");
        }

        if (other.CompareTag("Wood"))
        {
            WoodPickUpAudio();
            
            woodPicked?.Invoke();
          
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Stove"))
        {
            inStokingRange?.Invoke();
        }

        if (other.CompareTag("Shed"))
        {
            inStoveRange?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shed"))
        {
            CancelInvoke("RangeInvoker");
            inStoveRange?.Invoke(false);
        }

        if (other.CompareTag("ZombieRange"))
        {
            inEnemyRange = false;
            DeactivateHealthBar();
        }
    }

    void DeactivateHealthBar()
    {
        playerHealthBar.SetActive(false);

        if (plCurrentHealth != plMaxHealth)
            StartCoroutine("HealthReset");
    }

    #region Initials / Utils
    IEnumerator PlayerDieAttacked()
    {
        yield return new WaitForSecondsRealtime(sceneTransTimer); 
        SceneManager.LoadScene(sceneOnDeath);
    }

    IEnumerator HealthReset()
    {
        yield return new WaitForSecondsRealtime(healthResetTimer);
        plCurrentHealth = plMaxHealth;
    }
    
    void WoodPickUpAudio()
    {
        WalkAudio.PlayOneShot(woodPickSFX);
    }
    void PlayWeaponPickedSound()
    {
        FXAudio.PlayOneShot(FXClips[2]);
    }

    private void OnEnable()
    {
        Zombies.enemyDied += DeactivateHealthBar;
        IInteractable.weaponEquipped += PlayWeaponPickedSound;
        PlayerInventory.itemSwitched += PlayWeaponPickedSound;
        EnemyDamage.playerHit += GetDamage;
    }

    private void OnDisable()
    {
        Zombies.enemyDied -= DeactivateHealthBar;
        IInteractable.weaponEquipped -= PlayWeaponPickedSound;
        PlayerInventory.itemSwitched -= PlayWeaponPickedSound;
        EnemyDamage.playerHit -= GetDamage;
    }

    #endregion

    #region AnimationEvents
  
    void BatCollisionActivate()
    {
        batCollider.enabled = true;
    }

    void BatCollisionDeactivate()
    {
        batCollider.enabled = false;
    }

    void PlayAudioBat()
    {
        batScript.PlayBatSound();
    }

    #endregion
}
