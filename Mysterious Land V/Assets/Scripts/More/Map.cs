using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image lockImage;
    private Button choseLevelButton;
    public int map;

    private void Start()
    {
        choseLevelButton = GetComponent<Button>();
        levelText.text = "" + map;
        if (map <= PlayerPrefs.GetInt("Map"))
        {
            lockImage.enabled = false;
            choseLevelButton.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        PlayerPrefs.SetInt("MapCurrent", map);

        SceneManager.LoadScene("Level");
    }
}
