using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCard : MonoBehaviour
{
    public CharacterSO characterSO;

    public void ClickRemoveCard()//Ấn vào thẻ đã chọn để xóa
    {
        LevelManager.instance.ClickRemoveCharacterCard(characterSO);
    }
}
