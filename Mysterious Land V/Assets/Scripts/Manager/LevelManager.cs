using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

[Serializable]
public class DataContainer
{
    public List<int> idList;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private DataContainer dataContainer;
    public List<int> idCharacters = new List<int>();
    [SerializeField] private GameObject[] cards;//Danh sách các ô thẻ trong phần ShowCard

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

    public void ShowCard(CharacterSO characterSO)//Hiện các thẻ đã chọn
    {
        foreach (var card in cards)
        {
            if(card.activeInHierarchy == false)//Kiểm tra thẻ chưa hiện
            {
                card.GetComponent<Image>().sprite = characterSO.image;
                card.GetComponent<ShowCard>().id = characterSO.id;
                card.SetActive(true);//Hiện thẻ
                break;
            }
        }
    }

    public void ShowAfterRemove(int idCardRemove)//Hiện lại thẻ sau khi xóa 1 thẻ đã chọn
    {
        foreach (var card in cards)
        {
            if(card.GetComponent<ShowCard>().id == idCardRemove)
            {
                card.SetActive(false);//Ẩn thẻ đã xóa
            }
        }

        ShowAllCard();//Hiện tất cả các thẻ sau khi xóa
    }

    private void ShowAllCard()
    {
        for (int i = 0; i < cards.Length - 1; i++)
        {
            if(cards[i+1].activeInHierarchy == true)
            {
                cards[i + 1].SetActive(false);
                cards[i].GetComponent<Image>().sprite = cards[i+1].GetComponent<Image>().sprite;
                cards[i].GetComponent<ShowCard>().id = cards[i+1].GetComponent<ShowCard>().id;
                cards[i].SetActive(true);
            }
        }
    }

    public void LoadLevel()//Chuyển đến scene màn chơi
    {
        dataContainer = new DataContainer();
        dataContainer.idList = idCharacters;

        string jsonData = JsonUtility.ToJson(dataContainer);
        PlayerPrefs.SetString("Data", jsonData);//Xét giá trị cho dữ liệu để dùng khi chuyển sang màn chơi

        SceneManager.LoadScene(0);//Chuyển scene
    }
}
