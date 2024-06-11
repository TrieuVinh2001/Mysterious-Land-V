using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnRandom : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;//Danh sách các nhân vật

    [SerializeField] private Transform[] pointSpawn;//Vị trí sinh ra nhân vật

    //[SerializeField] private float timeWaitStart;
    [SerializeField] private Vector2 timeSpawn;

    void Start()
    {
        StartCoroutine(SpawnTime());
    }

    public void Spawner()
    {
        Instantiate(characterPrefabs[Random.Range(0, characterPrefabs.Length)], pointSpawn[Random.Range(0, pointSpawn.Length)].position, Quaternion.identity);//Sinh ra quái ngẫu nhiên
    }

    //IEnumerator TimeStart()
    //{
    //    yield return new WaitForSeconds(timeWaitStart);
    //    StartCoroutine(SpawnTime());
    //}

    private IEnumerator SpawnTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeSpawn.x, timeSpawn.y));
            Spawner();
        }
    }
}
