using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject contentPrefab;
    [SerializeField] private GameObject content;

    [SerializeField] private CharacterSO[] characterSO;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("DataCharacterOwner"))//Kiểm tra key
        {
            return;
        }
        else
        {
            string jsonData = PlayerPrefs.GetString("DataCharacterOwner");
            DataCharacterOwner receivedData = JsonUtility.FromJson<DataCharacterOwner>(jsonData);

            for (int i = 0; i < characterSO.Length; i++)
            {
                for(int j = 0; j < receivedData.idList.Count; j++)
                {
                    if(characterSO[i].id == receivedData.idList[j])
                    {
                        break;
                    }
                    else if(j == receivedData.idList.Count - 1)
                    {
                        var characterShow = Instantiate(contentPrefab, content.transform);
                        characterShow.transform.GetChild(0).GetComponent<Image>().sprite = characterSO[i].image;
                        characterShow.GetComponent<BuyCharacter>().characterSO = characterSO[i]; 
                    }
                }
                
            }
        }

        
    }

    private void Update()
    {
        
    }

}
