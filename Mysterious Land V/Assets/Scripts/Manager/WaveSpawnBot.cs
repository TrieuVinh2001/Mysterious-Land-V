using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnBot : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform[] pointSpawn;
    [SerializeField] private int coin;

    private int coinCharacter;
    private GameObject prefabCharacter;

    private void Start()
    {
        StartCoroutine(Coin());
        RandomCharacter();
    }

    private void Update()
    {
        if (coinCharacter < coin)
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
    }

    private IEnumerator Coin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            coin++;
        }
    }
}
