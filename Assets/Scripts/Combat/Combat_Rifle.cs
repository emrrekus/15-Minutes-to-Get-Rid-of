using System.Collections;
using UnityEngine;
using TMPro;
public class Combat_Rifle : MonoBehaviour
{
    Camera Camera;
    [SerializeField] LayerMask aimMask;
    [SerializeField] AudioSource rifleAudio;
    [SerializeField] AudioClip[] WeaponSfx;

    [SerializeField] ParticleSystem bulletFX;

    [SerializeField] TMP_Text bulletCountText;
    [SerializeField] Pooler poolerInstance;

    private float shootingRange = 250;

    public static int bulletCount;
    const int initBulletCount = 20;
    
    float bulletDeactivation = 1.2f;

    void UpdateBulletCountInventory(int val)
    {
        bulletCount += val;
        bulletCountText.text = "x" + bulletCount.ToString();
    }

    public GameObject rifleTip;
    void Hit()
    {
        if (Input.GetMouseButtonDown(0))
        {   
           
            rifleAudio.Play();
            
            if (bulletCount > 0)
            {             
                bulletFX.Play();

                 Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
                  Ray ray = Camera.main.ScreenPointToRay(screenCenter);
                if (Physics.Raycast(ray, out RaycastHit hit, shootingRange, aimMask))
                {
                    GameObject temp = poolerInstance.SpawnFromPool(0, rifleTip.transform.position); //spawns bullet from pool
                    temp.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * 20f, ForceMode.Impulse);
                    
                    UpdateBulletCountInventory(-1);

                    if (hit.collider.CompareTag("Zombie"))
                    {
                        Debug.Log("zombie is hit.");
                        poolerInstance.SpawnFromPool(2, hit.point); // spawns blood vfx from pool
                    }
                    StartCoroutine(BulletDeactivator(temp));
                }

                #region alternative raycast
                /*    RaycastHit hit;
                    if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, shootingRange))
                    {
                        GameObject temp = poolerInstance.SpawnFromPool(0, rifleTip.transform.position); //spawns bullet from pool
                        temp.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * 20f, ForceMode.Impulse);
                        UpdateBulletCountInventory(-1);

                        if (hit.collider.CompareTag("Zombie"))
                        {
                            Debug.Log("zombie is hit.");
                            poolerInstance.SpawnFromPool(2, hit.point); // spawns blood vfx from pool
                        }

                        StartCoroutine(BulletDeactivator(temp));
                    }*/
                #endregion
            }

            if (bulletCount <= 0)
            {
                rifleAudio.clip = WeaponSfx[1];                
            }
        }
    }

    #region Initials / Utils
    private void Awake()
    {       
        Camera = Camera.main;

        rifleAudio.clip = WeaponSfx[0];

        bulletCount = initBulletCount;
        bulletCountText.text = "x" + initBulletCount;
    }
    private void OnEnable()
    {
        GunSwitchState.GunFired += Hit;
        Zombies.enemyDroppedBullet += UpdateBulletCountInventory;
    }

    private void OnDisable()
    {
        GunSwitchState.GunFired -= Hit;
        Zombies.enemyDroppedBullet -= UpdateBulletCountInventory;
    }

    IEnumerator BulletDeactivator(GameObject obj)
    {
        yield return new WaitForSeconds(bulletDeactivation);
        obj.SetActive(false);
    }

    #endregion
}
