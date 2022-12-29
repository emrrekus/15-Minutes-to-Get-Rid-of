using System.Collections.Generic;
using UnityEngine;
public class Pooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject parent;
        public GameObject spawnObject;
        public int objID;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<int, Queue<GameObject>> poolDictionary;

    Vector3 final;
    Vector3 randomPos;
    List<Vector3> tempRND = new List<Vector3>();

    bool acceptableRange = false; 
    int counter = 0;

    private void Awake()
    {

        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.spawnObject, pool.parent.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.objID, objectPool);
        }

        Vector3 randomPos = randomPosition();
    }


    public GameObject SpawnFromPool(int ID, Vector3 positionToSpawn)
    {
        if (!poolDictionary.ContainsKey(ID))
        {
            Debug.LogWarning($"Pool with tag {ID} doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[ID].Dequeue();
        objectToSpawn.transform.rotation = Quaternion.identity;
        objectToSpawn.transform.position = positionToSpawn;


        Debug.Log(positionToSpawn + " res " + objectToSpawn.transform.position);

        objectToSpawn.SetActive(true);


        poolDictionary[ID].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public Vector3 randomPosition()
    {
        switch (counter)
        {
            case 0:
                randomPos = new Vector3(Random.Range(24, 51), 1, Random.Range(50, 80));
                counter++;
                break;

            case 1:
                randomPos = new Vector3(Random.Range(63, 90), 1, Random.Range(58, 90));
                counter++;
                break;

            case 2:
                randomPos = new Vector3(Random.Range(63, 90), 1, Random.Range(10, 58));
                counter = 0;
                break;
        }


        if (tempRND.Count == 0)
        {
            tempRND.Add(randomPos);
        }
        else if (tempRND.Count >= 15)
        {
            Debug.Log("removed");
            tempRND.RemoveAt(0);
        }
        else if (tempRND.Count > 0 && tempRND.Count <= 15)
        {
            if (!tempRND.Contains(randomPos))
            {
                foreach (var item in tempRND)
                {
                    if (Vector3.Distance(item, randomPos) < 8)
                    {
                        acceptableRange = false;
                        return randomPosition();
                    }

                    else if (Vector3.Distance(item, randomPos) >= 8)
                    {
                        acceptableRange = true;
                    }
                }
            }
            else if (tempRND.Contains(randomPos)) return randomPosition();
        }

        if (acceptableRange == true)
        {
            tempRND.Add(randomPos);
            final = randomPos;
            acceptableRange = false;
        }
        return final;
    }
}

