using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Pooler poolerIns;

    const int maxEnemy = 15;
    public static int enemyCount = 0;
    const int initNum = 0;


    [SerializeField] float spawnTimer = 20f;
    void Start()
    {
        enemyCount = initNum;

        InvokeRepeating("SpawnNew", 1f, spawnTimer);        
    }

    void SpawnNew()
    {
        if (enemyCount < maxEnemy)
        {
            enemyCount++;
            poolerIns.SpawnFromPool(1, poolerIns.randomPosition());

            if (enemyCount == maxEnemy)
            {
                CancelInvoke("SpawnNew");
            }
        }
    }

    private void OnEnable()
    {
        Zombies.enemyDied += SpawnNew;
    }
    private void OnDisable()
    {
        Zombies.enemyDied -= SpawnNew;
    }


}
