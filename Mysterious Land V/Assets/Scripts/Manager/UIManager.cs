using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI healthMain;
    [SerializeField] private TextMeshProUGUI healthEnemy;
    [SerializeField] private TextMeshProUGUI countCharater;
    [SerializeField] private TextMeshProUGUI countEnemy;
    [SerializeField] private TextMeshProUGUI level;
    public TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI coinToUpLevel;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject noteCoinText;

    //[SerializeField] private Button settingButton;


    private void Start()
    {
        GameManager.instance.uiManager = GetComponent<UIManager>();
        //settingButton.onClick.AddListener(SettingButtonClick);//Khi nhấn nút thì thực hiện hàm SettingButtonClick
        UpdateUI();
        UpdateCoinToUpLevelText();
    }

    private void Update()
    {
        
    }

    public void UpdateUI()
    {
        coin.text = "" + GameManager.instance.coin;
        countCharater.text = GameManager.instance.countCharacter + "/" + GameManager.instance.maxCountCharacter;
        countEnemy.text = "" + GameManager.instance.countEnemy +"/"+ GameManager.instance.maxCountEnemy;
        level.text = "Level: " + GameManager.instance.level;
        
    }

    public void UpdateCoinToUpLevelText()
    {
        coinToUpLevel.text = "" + GameManager.instance.coinToUpLevel;
        if (GameManager.instance.level == GameManager.instance.maxLevel)
        {
            coinToUpLevel.text = "Max";
        }
    }

    public void UpdateHealthBuildingCharacter(int health)
    {
        healthMain.text = health +"/"+999;
        healthMain.gameObject.transform.parent.GetComponent<Image>().fillAmount = (float)health / 999;
    }

    public void UpdateHealthBuildingEnemy(int health)
    {
        healthEnemy.text = health + "/" + 999;
        healthEnemy.gameObject.transform.parent.GetComponent<Image>().fillAmount = (float)health / 999;
    }

    public void SettingButtonClick()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueButtonClick()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void MenuButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }

    public void RetryButtonClick()
    {
        Time.timeScale = 1f;
        //AdsManager.Instance.interstitialAds.ShowInterstitialAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RewardBonusButtonClick()
    {
        //AdsManager.Instance.rewardedAds.ShowRewardedAd();
    }

    public void NextLevelButton(string nameLevel)
    {
        SceneManager.LoadScene(nameLevel);
    }

    public void GameWin()
    {
        winPanel.SetActive(true);

        if (PlayerPrefs.HasKey("LevelCurrent"))
        {
            if (PlayerPrefs.GetInt("LevelCurrent") == PlayerPrefs.GetInt("Level"))
            {
                int level = PlayerPrefs.GetInt("Level") + 1;
                PlayerPrefs.SetInt("Level", level);
            }
        }

        if (PlayerPrefs.HasKey("MapCurrent"))
        {
            if (PlayerPrefs.GetInt("MapCurrent") == PlayerPrefs.GetInt("Map"))
            {
                int map = PlayerPrefs.GetInt("Map") + 1;
                PlayerPrefs.SetInt("Map", map);
            }
        }

        Time.timeScale = 0f;
    }

    public void GameLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NoteCoin()
    {
        noteCoinText.SetActive(true);
        StartCoroutine(HideText());
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(2f);
        noteCoinText.SetActive(false);
    }
}
