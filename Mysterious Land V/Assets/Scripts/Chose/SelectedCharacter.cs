using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedCharacter : MonoBehaviour
{
    [SerializeField] private bool dataChar;
    [SerializeField] private List<CharacterSO> charSO = new List<CharacterSO>();
    public GameObject prefab;
    public List<GameObject> characterPrefabs = new List<GameObject>();//Danh sách prefab sẽ được chọn trong màn chơi
    public GameObject heroPrefab;
    public GameObject skillPrefab;
    [SerializeField] private GameObject[] allPrefabChar;//Tất cả prefab nhân vật
    [SerializeField] private GameObject[] allPrefabHero;
    [SerializeField] private GameObject[] allPrefabSkill;

    public GameObject[] cards;//Các ô thẻ nhân vật
    [SerializeField] private GameObject cardHero;//Thẻ chọn anh hùng
    [SerializeField] private GameObject cardSkill;//Thẻ chọn kỹ năng

    [SerializeField] private List<CharacterSO> characterSO = new List<CharacterSO>();
    [SerializeField] private int idHero;
    [SerializeField] private int idSkill;

    public bool isSkill;

    private void Awake()
    {
        GetData();//Lấy Id

        if (!dataChar)
        {
            characterSO = charSO;
        }

        GetCharacter();
        GetHero();
        GetSkill();
    }

    private void GetCharacter()
    {
        for (int i = 0; i < characterSO.Count; i++)
        {
            foreach (var prefab in allPrefabChar)//Thêm prefab vào danh sách các prefab sẽ dùng trong màn chơi
            {
                if (prefab.GetComponent<CharacterBase>().GetCharacterSO() == characterSO[i])
                {
                    characterPrefabs.Add(prefab);
                    cards[i].GetComponent<CardClick>().characterSO = characterSO[i];
                    cards[i].GetComponent<CardClick>().prefab = prefab;
                    cards[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + characterSO[i].coin;
                }
            }

            cards[i].transform.parent.gameObject.SetActive(true);//Hiện thẻ
        }
    }

    private void GetHero()
    {
        //cardHero.transform.parent.gameObject.SetActive(true);//Hiện thẻ

        foreach (var prefab in allPrefabHero)//Thêm prefab vào danh sách các prefab sẽ dùng trong màn chơi
        {
            if (prefab.GetComponent<CharacterBase>().GetCharacterSO().id == idHero)
            {
                heroPrefab = prefab;
                cardHero.GetComponent<CardHeroClick>().characterSO = prefab.GetComponent<CharacterBase>().GetCharacterSO();
            }
        }
    }

    private void GetSkill()
    {

        //cardSkill.transform.parent.gameObject.SetActive(true);//Hiện thẻ

        foreach (var prefab in allPrefabSkill)//Thêm prefab vào danh sách các prefab sẽ dùng trong màn chơi
        {
            if (prefab.GetComponent<SkillBase>().GetSkillSO().id == idSkill)
            {
                skillPrefab = prefab;
                cardSkill.GetComponent<CardSkillClick>().skillSO = prefab.GetComponent<SkillBase>().GetSkillSO();
            }
        }
    }

    private void GetData()
    {
        if (PlayerPrefs.HasKey("Data"))//Kiểm tra key
        {
            string jsonData = PlayerPrefs.GetString("Data");//Lấy dữ liệu trong key đã được lưu ở LevelManager
            DataContainer receivedData = JsonUtility.FromJson<DataContainer>(jsonData);//Chuyển dữ liệu từ json sang data cần dùng

            foreach (CharacterSO charSO in receivedData.listSO)//Thêm các id lấy trong dữ liệu vào danh sách id nhân vật
            {
                characterSO.Add(charSO);
            }
        }

        idHero = PlayerPrefs.GetInt("Hero");
        idSkill = PlayerPrefs.GetInt("Skill");
    }
}
