using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedCharacter : MonoBehaviour
{
    public int indexSelected;//Thứ tự của character sẽ được spawn trong list
    public List<GameObject> characterPrefabs = new List<GameObject>();//Danh sách prefab sẽ được chọn trong màn chơi
    [SerializeField] private GameObject[] allPrefabsChar;//Tất cả prefab
    [SerializeField] private GameObject[] cards;//Các ô thẻ cố định trong màn

    [SerializeField] private List<int> idCharacters = new List<int>();//Danh sách id của nhân vật

    private void Start()
    {
        GetIdCharacter();//Lấy Id của nhân vật

        for (int i = 0; i < idCharacters.Count; i++)
        {
            cards[i].GetComponent<CardClick>().index = i;
            
            cards[i].transform.parent.gameObject.SetActive(true);//Hiện thẻ

            foreach (var prefab in allPrefabsChar)//Thêm prefab vào danh sách các prefab sẽ dùng trong màn chơi
            {
                if(prefab.GetComponent<CharacterBase>().GetCharacterSO().id == idCharacters[i])
                {
                    characterPrefabs.Add(prefab);
                    cards[i].GetComponent<CardClick>().characterSO = prefab.GetComponent<CharacterBase>().GetCharacterSO();
                    cards[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + prefab.GetComponent<CharacterBase>().GetCharacterSO().coin;
                }
            }
        }
    }

    private void GetIdCharacter()
    {
        if (PlayerPrefs.HasKey("Data"))//Kiểm tra key
        {
            string jsonData = PlayerPrefs.GetString("Data");//Lấy dữ liệu trong key đã được lưu ở LevelManager
            DataContainer receivedData = JsonUtility.FromJson<DataContainer>(jsonData);//Chuyển dữ liệu từ json sang data cần dùng

            foreach (int id in receivedData.idList)//Thêm các id lấy trong dữ liệu vào danh sách id nhân vật
            {
                idCharacters.Add(id);
            }
        }
    }
}
