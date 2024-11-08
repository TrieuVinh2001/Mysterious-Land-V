﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseCard : MonoBehaviour
{
    public CharacterSO characterSO;
    public GameObject coolCard;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickAddCard);
    }

    public void ClickAddCard()//Nhấn để thêm thẻ vào danh sách các thẻ sẽ dùng trong màn chơi
    {
        if (LevelManager.instance.characterSelected.Count >= 6)
            return;

        LevelManager.instance.ClickAddCharacterCard(characterSO);

        coolCard.SetActive(true);
    }

}
