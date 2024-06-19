using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCharacter : MonoBehaviour
{
    public CharacterSO characterSO;
    [SerializeField] private GameObject buyButton;

    public void BuyButton()
    {
        buyButton.SetActive(false);

        if (PlayerPrefs.HasKey("DataCharacterOwner"))
        {
            //Lấy danh sách từ bộ nhớ
            string getJsonData = PlayerPrefs.GetString("DataCharacterOwner");
            DataCharacterOwner receivedData = JsonUtility.FromJson<DataCharacterOwner>(getJsonData);
            receivedData.idList.Add(characterSO.id);//Thêm id vào danh sách

            //Lưu lại danh sách
            string jsonData = JsonUtility.ToJson(receivedData);
            PlayerPrefs.SetString("DataCharacterOwner", jsonData);
        }
        else
        {
            DataCharacterOwner receivedData = new DataCharacterOwner();
            receivedData.idList.Add(characterSO.id);//Thêm id vào danh sách

            //Lưu lại danh sách
            string jsonData = JsonUtility.ToJson(receivedData);
            PlayerPrefs.SetString("DataCharacterOwner", jsonData);
        }
    }
}
