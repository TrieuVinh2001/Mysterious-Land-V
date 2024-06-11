using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private UIManager uiManager;

    public int coin;
    public int level;
    [SerializeField] private int coinUp;
    [SerializeField] private int coinToUpLevel;

    public int countCharacter;
    public int maxCountCharacter;
    public int countEnemy;
    public int maxCountEnemy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(CoinGrowth());
    }

    private void Update()
    {
        
    }

    public void UpLevel()
    {
        if (coin > coinToUpLevel)
        {
            coin -= coinToUpLevel;
            coinUp += 1;
            level += 1;
            coinToUpLevel = coinToUpLevel * 2;
            maxCountCharacter += 10;
            uiManager.UpdateUI();
        }
    }

    private IEnumerator CoinGrowth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            coin += coinUp;
            uiManager.UpdateUI();
        }
    }

    public void ChangeCoin(int count)
    {
        coin += count;
        uiManager.UpdateUI();
    }

    public void ChangeCountCharacter(int count)
    {
        countCharacter += count;
        uiManager.UpdateUI();
    }

    public void ChangeCountEnemy(int count)
    {
        countEnemy +=count;
        uiManager.UpdateUI();
    }

    public void UpdateHealthBuildCharacter(int healthBuild)
    {
        uiManager.UpdateHealthBuildingCharacter(healthBuild);
    }

    public void UpdateHealthBuildEnemy(int healthBuild)
    {
        uiManager.UpdateHealthBuildingEnemy(healthBuild);
    }
}
