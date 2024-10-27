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
    [SerializeField] private GameObject[] cards;//Danh sách các ô thẻ trong phần ShowCard

    [SerializeField] private GameObject showCards;
    [SerializeField] private GameObject choseCharacter;
    [SerializeField] private GameObject choseHero;
    [SerializeField] private GameObject choseSkill;

    public List<CharacterSO> characterSelected = new List<CharacterSO>();
    public int idHero;
    public int idSkill;

    [SerializeField] private List<CharacterSO> characterSO =  new List<CharacterSO>();
    [SerializeField] private List<CharacterSO> heroSO =  new List<CharacterSO>();
    [SerializeField] private List<SkillSO> skillSO =  new List<SkillSO>();

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
        ShowAllHero();
        ShowAllSkill();
    }

    private void ShowAllCard()
    {
        for (int i = 0; i < characterSO.Count; i++)
        {
            GameObject card = choseCharacter.transform.GetChild(i).GetChild(0).gameObject;
            card.GetComponent<ChoseCard>().characterSO = characterSO[i];
            card.GetComponent<Image>().sprite = characterSO[i].image;
            card.SetActive(true);
        }
    }

    private void ShowAllHero()
    {
        for (int i = 0; i < heroSO.Count; i++)
        {
            GameObject card = choseHero.transform.GetChild(i).GetChild(0).gameObject;
            card.GetComponent<ChoseHeroCard>().heroSO = heroSO[i];
            card.GetComponent<Image>().sprite = heroSO[i].image;
            card.SetActive(true);
        }
    }

    private void ShowAllSkill()
    {
        for (int i = 0; i < skillSO.Count; i++)
        {
            GameObject card = choseSkill.transform.GetChild(i).GetChild(0).gameObject;
            card.GetComponent<ChoseSkillCard>().skillSO = skillSO[i];
            card.GetComponent<Image>().sprite = skillSO[i].image;
            card.SetActive(true);
        }
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

    public void ClickAddHeroCard(CharacterSO hero)
    {
        idHero = hero.id;
        Debug.Log(idHero);
        for (int i = 0; i < heroSO.Count; i++)//Tắt BGCard của các thẻ k chọn 
        {
            if (heroSO[i] != hero)
            {
                choseHero.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }
    }
    
    public void ClickAddSkillCard(SkillSO skill)
    {
        idSkill = skill.id;
        for (int i = 0; i < skillSO.Count; i++)//Tắt BGCard của các thẻ k chọn 
        {
            if (skillSO[i] != skill)
            {
                choseSkill.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }
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
        PlayerPrefs.SetInt("Hero", idHero);
        PlayerPrefs.SetInt("Skill", idSkill);

        SceneManager.LoadScene("Map_" + PlayerPrefs.GetInt("MapCurrent"));//Chuyển scene
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene("Home");
    }
}
