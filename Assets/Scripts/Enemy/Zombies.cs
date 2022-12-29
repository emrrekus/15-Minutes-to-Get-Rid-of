using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Zombies : MonoBehaviour
{
    protected static readonly int isWalking = Animator.StringToHash("isWalking");
    protected static readonly int hasAttacked = Animator.StringToHash("hasAttacked");
    protected static readonly int hasDied = Animator.StringToHash("hasDied");
      
    const float maxHealth = 100f;
    float currentHealth;

    [SerializeField]float speed = 3f;
    [SerializeField]float attackRange = 1.5f;
    [SerializeField]float followRange = 20;
    [SerializeField]float damageCapacity = 5;      

    [SerializeField] GameObject zombieHealthBar;
    [SerializeField] Collider zombieHitPoint;
    [SerializeField] Image healthBarFill;

    float distance;
    float maxDistance = 15;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;

    public static event System.Action enemyDied;
    public static event System.Action<int> enemyDroppedBullet;
    public static event System.Action enemyTookDamage;

    void Awake()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = speed;
    }

    private void Update()
    {
        FollowSetup();
    }
    
   
    public void GetDamage(float damageAmount)
    {
        enemyTookDamage?.Invoke();

        if (distance < maxDistance)
        {
            damageAmount /= 10;
        }
        else
        {
            damageAmount /= 20;
        }
        currentHealth -= damageAmount;
        healthBarFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            enemyDied?.Invoke();

            EnemySpawner.enemyCount--; 
            StartCoroutine(EnemyDeactivator());
        }
    }
    protected virtual void FollowSetup()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (distance <= followRange && distance > attackRange)
        {          
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            zombieHealthBar.SetActive(true);

            animator.SetBool(isWalking, true);
        }

        if (distance < attackRange)
        {
            agent.isStopped = true;

            animator.SetBool(isWalking, false);
            animator.SetTrigger(hasAttacked);
        }

        if (distance > followRange)
        {
            agent.isStopped = true;
            
            zombieHealthBar.SetActive(false);   
            
            animator.SetBool(isWalking, false);
        }
        if (currentHealth <= 0)
        {
            agent.isStopped = true;
            
            zombieHealthBar.SetActive(false);
            
            animator.SetBool(isWalking, false);
            animator.SetTrigger(hasDied);
        }
    }

    IEnumerator EnemyDeactivator()
    {
        yield return new WaitForSeconds(6f);

        int temp = Random.Range(1,100);

        if(temp > 5)
        {
            enemyDroppedBullet?.Invoke(1);
        }
        
        gameObject.SetActive(false);
    }


    void SetHitPointActive()
    {
        zombieHitPoint.enabled = true;
    }

    void SetHitPointDeactive()
    {
        zombieHitPoint.enabled = false;
    }
}
