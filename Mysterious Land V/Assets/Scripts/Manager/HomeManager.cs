using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class DataCharacterOwner
{
    public List<int> idList;
}

public class HomeManager : MonoBehaviour
{
    private DataCharacterOwner dataCharacterOwner;
    public List<int> idCharactersOwner = new List<int>();

    [SerializeField] private GameObject settingPanel;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetListCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            LoadList();
        }
    }

    private void LoadList()
    {
        if (PlayerPrefs.HasKey("DataCharacterOwner"))//Kiểm tra key
        {
            string jsonData = PlayerPrefs.GetString("DataCharacterOwner");
            DataCharacterOwner receivedData = JsonUtility.FromJson<DataCharacterOwner>(jsonData);

            foreach (int id in receivedData.idList)
            {
                //idCharactersOwner.Add(id);
                Debug.Log(id);
            }
        }
    }

    public void SetListCharacter()
    {
        dataCharacterOwner = new DataCharacterOwner();
        dataCharacterOwner.idList = idCharactersOwner;

        string jsonData = JsonUtility.ToJson(dataCharacterOwner);
        PlayerPrefs.SetString("DataCharacterOwner", jsonData);//Xét giá trị cho dữ liệu để dùng khi chuyển sang màn chơi

    }

    public void Setting()
    {
        settingPanel.SetActive(true);
    }

    public void Mission()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void Level()
    {
        SceneManager.LoadScene("Level");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
