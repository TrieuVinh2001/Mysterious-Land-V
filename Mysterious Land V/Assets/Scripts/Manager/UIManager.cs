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
    [SerializeField] private TextMeshProUGUI time;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private Button settingButton;

    private void Start()
    {
        settingButton.onClick.AddListener(SettingButtonClick);//Khi nhấn nút thì thực hiện hàm SettingButtonClick
        UpdateUI();
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

    public void UpdateHealthBuildingCharacter(int health)
    {
        healthMain.text = health +"/"+999;
    }

    public void UpdateHealthBuildingEnemy(int health)
    {
        healthMain.text = health + "/" + 999;
    }

    private void SettingButtonClick()
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelButton()
    {

    }
}
