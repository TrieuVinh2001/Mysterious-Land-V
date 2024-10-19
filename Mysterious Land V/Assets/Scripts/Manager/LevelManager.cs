using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

[Serializable]
public class DataContainer
{
    public List<CharacterSO> listSO;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private DataContainer dataContainer;
    public List<int> idCharacters = new List<int>();
    [SerializeField] private GameObject[] cards;//Danh sách các ô thẻ trong phần ShowCard

    [SerializeField] private GameObject showCards;
    [SerializeField] private GameObject choseCharacter;
    [SerializeField] private GameObject choseHero;
    [SerializeField] private GameObject choseSkill;

    public List<CharacterSO> characterSelected = new List<CharacterSO>();

    [SerializeField] private List<CharacterSO> characterSO =  new List<CharacterSO>();
    [SerializeField] private List<CharacterSO> heroSO =  new List<CharacterSO>();

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
        ShowAllCard();
        //ShowAllHero();
        //ShowAllSkill();
    }

    private void ShowAllCard()
    {
        int i = 0;
        foreach (var charSO in characterSO)
        {
            GameObject card = choseCharacter.transform.GetChild(i).GetChild(0).gameObject;
            i++;
            card.GetComponent<ChoseCard>().characterSO = charSO;
            card.GetComponent<Image>().sprite = charSO.image;
            card.SetActive(true);
        }
    }

    private void ShowAllHero()
    {
        int i = 0;
        foreach (var heroSO in heroSO)
        {
            GameObject card = choseHero.transform.GetChild(i).GetChild(0).gameObject;
            i++;
            //card.GetComponent<ChoseCard>().characterSO = heroSO;
            card.GetComponent<Image>().sprite = heroSO.image;
            card.SetActive(true);
        }
    }

    private void ShowAllSkill()
    {

    }

    public void ClickAddCharacterCard(CharacterSO charSO)
    {
        characterSelected.Add(charSO);
        ShowCard(charSO);
    }

    public void ClickRemoveCharacterCard(CharacterSO charSO)
    {
        characterSelected.Remove(charSO);
        ShowAfterRemove(charSO);
    }

    private void ShowCard(CharacterSO characterSO)//Hiện các thẻ đã chọn
    {
        foreach (var card in cards)
        {
            if (card.activeInHierarchy == false)//Kiểm tra thẻ chưa hiện
            {
                card.GetComponent<Image>().sprite = characterSO.image;
                card.GetComponent<ShowCard>().characterSO = characterSO;
                card.SetActive(true);//Hiện thẻ
                break;
            }
        }
    }

    private void ShowAfterRemove(CharacterSO charSO)//Hiện lại thẻ sau khi xóa 1 thẻ đã chọn
    {
        foreach (var card in cards)
        {
            card.SetActive(false);
        }

        ShowCardSelected();//Hiện tất cả các thẻ sau khi xóa
        HideCoolCard(charSO);
    }

    private void ShowCardSelected()
    {
        int i = 0;
        foreach (var character in characterSelected)
        {
            GameObject card = showCards.transform.GetChild(i).GetChild(0).gameObject;
            i++;
            card.GetComponent<Image>().sprite = character.image;
            card.GetComponent<ShowCard>().characterSO = character;
            card.SetActive(true);//Hiện thẻ
        }
    }

    private void HideCoolCard(CharacterSO charSO)
    {
        for (int i = 0; i < choseCharacter.transform.childCount - 1; i++)
        {
            GameObject card = choseCharacter.transform.GetChild(i).GetChild(0).gameObject;
            if(card.GetComponent<ChoseCard>().characterSO == charSO)
            {
                card.GetComponent<ChoseCard>().coolCard.SetActive(false);
            }
        }
    }

    public void LoadLevel()//Chuyển đến scene màn chơi
    {
        dataContainer = new DataContainer();
        dataContainer.listSO = characterSelected;
        string jsonData = JsonUtility.ToJson(dataContainer);

        PlayerPrefs.SetString("Data", jsonData);//Xét giá trị cho dữ liệu để dùng khi chuyển sang màn chơi
        PlayerPrefs.SetInt("Hero", 1);
        PlayerPrefs.SetInt("Skill", 1);

        SceneManager.LoadScene(0);//Chuyển scene
    }
}
