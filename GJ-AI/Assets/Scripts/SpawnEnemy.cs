using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform pointSpawn;
    public float timeWaitStart;
    public Vector2 timeSpawn;
    public float timeRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawner()
    {
        //yield return new WaitForSeconds(Random.Range(timeSpawn.x,timeSpawn.y));
        //yield return new WaitForSeconds(5f);
        Instantiate(enemies[Random.Range(0, enemies.Length)], pointSpawn.position, Quaternion.identity);//Sinh ra quái ngẫu nhiên
    }

    IEnumerator TimeStart()
    {
        yield return new WaitForSeconds(timeWaitStart);
        InvokeRepeating("Spawner", timeRate, Random.Range(timeSpawn.x, timeSpawn.y));//Gọi Hàm Spawer sau 1 khoảng thời gian
    }
}
