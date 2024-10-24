using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private int allLevel;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private bool isMap;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LevelCurrent"))
        {
            PlayerPrefs.DeleteKey("LevelCurrent");
        }
        
        if (PlayerPrefs.HasKey("MapCurrent"))
        {
            PlayerPrefs.DeleteKey("MapCurrent");
        }

        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        if (!PlayerPrefs.HasKey("Map"))
        {
            PlayerPrefs.SetInt("Map", 1);
        }
        
        Create();
    }

    private void Create()
    {
        if (isMap)
        {
            for (int i = 1; i <= allLevel; i++)
            {
                var levelButton = Instantiate(levelPrefab, transform);
                levelButton.GetComponent<Map>().map = i;
            }
        }
        else
        {
            for (int i = 1; i <= allLevel; i++)
            {
                var levelButton = Instantiate(levelPrefab, transform);
                levelButton.GetComponent<Level>().level = i;
            }
        }
        
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Home");
    }

    public void UnlockAllLevel()
    {
        PlayerPrefs.SetInt("Level", allLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LockAllLevel()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void UnlockAllMap()
    {
        PlayerPrefs.SetInt("Map", allLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LockAllMap()
    {
        PlayerPrefs.SetInt("Map", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
