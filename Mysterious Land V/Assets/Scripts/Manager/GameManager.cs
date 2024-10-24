using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public UIManager uiManager;

    public int coin;
    public int level;
    [SerializeField] private int coinUp;
    public int coinToUpLevel;

    public int countCharacter;
    public int maxCountCharacter;
    public int countEnemy;
    public int maxCountEnemy;

    private float timeCountDownAddCoin = 3;
    public int maxLevel = 5;
    public float time;

    public int diamond;

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

        //StartCoroutine(DisplayBannerWithDelay());
    }

    private IEnumerator DisplayBannerWithDelay()
    {
        yield return new WaitForSeconds(1f);
        AdsManager.Instance.bannerAds.ShowBannerAd();
    }

    private void Start()
    {
        StartCoroutine(CoinGrowth());
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
            uiManager.GameLose();
        }

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        uiManager.time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpLevel()
    {
        if (coin >= coinToUpLevel && level < maxLevel)
        {
            coin -= coinToUpLevel;
            coinUp += 1;
            level += 1;
            if(level != maxLevel)
            {
                coinToUpLevel = coinToUpLevel + 10;
            }
            uiManager.UpdateUI();
            uiManager.UpdateCoinToUpLevelText();
        }
    }

    private IEnumerator CoinGrowth()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeCountDownAddCoin);
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
        countEnemy += count;
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


    public void AddDiamondToData()
    {

    }

    public void AddDiamondReward(int reward)
    {
        diamond += 100 * reward;
    }
}
