using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterWave
{
    public GameObject characterPrefab;
    public float timeWaitSpawn;
    public int areaToSpawn;
}

public class WaveSpawn : MonoBehaviour
{
    [SerializeField] private CharacterWave[] characterWaves;
    [SerializeField] private Transform[] pointSpawn;

    void Start()
    {
        for (int i = 0; i < characterWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(characterWaves[i].timeWaitSpawn, characterWaves[i].characterPrefab, pointSpawn[characterWaves[i].areaToSpawn]));//Tạo các wave
        }
    }

    IEnumerator CreateEnemyWave(float delay, GameObject Wave, Transform point)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);//Thời gian chờ tạo wave

        //if (PlayerController.instance != null)
        GameObject enemy = Instantiate(Wave, point.position, Quaternion.identity);//Tạo wave
        enemy.transform.parent = point.transform;
    }
}
