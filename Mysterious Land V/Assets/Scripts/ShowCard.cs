using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCard : MonoBehaviour
{
    public int id;

    public void ClickRemoveCard()//Ấn vào thẻ đã chọn để xóa
    {
        LevelManager.instance.idCharacters.Remove(id);
        LevelManager.instance.ShowAfterRemove(id);
    }
}
