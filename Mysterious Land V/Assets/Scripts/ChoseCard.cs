using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseCard : MonoBehaviour
{

    [SerializeField] private CharacterSO characterSO;

    private int idCard;

    private void Start()
    {
        idCard = characterSO.id;
    }

    public void ClickAddCard()//Nhấn để thêm thẻ vào danh sách các thẻ sẽ dùng trong màn chơi
    {
        LevelManager.instance.idCharacters.Add(idCard);

        LevelManager.instance.ShowCard(characterSO);   
    }

}
