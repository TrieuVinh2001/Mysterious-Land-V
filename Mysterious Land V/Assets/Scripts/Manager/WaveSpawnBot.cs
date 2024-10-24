using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterWaveBot
{
    public float timeToUpLevel;
    public int level;
}

public class WaveSpawnBot : MonoBehaviour
{
    [SerializeField] private CharacterWaveBot[] characterWaveBot;
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform[] pointSpawn;
    [SerializeField] private int coin;
    [SerializeField] private int level;

    private int coinCharacter;
    private GameObject prefabCharacter;
    private bool canSpawn = true;


    private void Start()
    {
        for (int i = 0; i < characterWaveBot.Length; i++)
        {
            StartCoroutine(UpLevel(characterWaveBot[i].timeToUpLevel, characterWaveBot[i].level));
        }

        StartCoroutine(Coin());
        RandomCharacter();
    }

    private void Update()
    {

        if (coinCharacter < coin && canSpawn)
        {
            coin -= coinCharacter;
            Spawn();
            RandomCharacter();
        }
    }

    private void RandomCharacter()
    {
        prefabCharacter = characterPrefabs[Random.Range(0, characterPrefabs.Length)];
        coinCharacter = (int)prefabCharacter.GetComponent<CharacterBase>().GetCharacterSO().coin;
    }

    private void Spawn()
    {
        Instantiate(characterPrefabs[Random.Range(0, characterPrefabs.Length)], pointSpawn[Random.Range(0, pointSpawn.Length)].position, Quaternion.identity);
        GameManager.instance.ChangeCountEnemy(1);
        StartCoroutine(WaitNextSpawn());
    }

    private IEnumerator WaitNextSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        canSpawn = true;
    }

    private IEnumerator Coin()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            coin += level;
        }
    }

    private IEnumerator UpLevel(float timeDelay, int levelUp)
    {
        yield return new WaitForSeconds(timeDelay);
        level = levelUp;
    }
}
