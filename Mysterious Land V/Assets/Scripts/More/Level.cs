using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image lockImage;
    private Button choseLevelButton;
    public int level;

    private void Start()
    {
        choseLevelButton = GetComponent<Button>();
        levelText.text = "" + level;
        if (level <= PlayerPrefs.GetInt("Level"))
        {
            lockImage.enabled = false;
            choseLevelButton.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        if (!PlayerPrefs.HasKey("LevelCurrent"))
        {
            PlayerPrefs.SetInt("LevelCurrent", level);
        }
        else
        {
            PlayerPrefs.SetInt("LevelCurrent", level);
        }
        SceneManager.LoadScene("Level_" + level);
    }
}
