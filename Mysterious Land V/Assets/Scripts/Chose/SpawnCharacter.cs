﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    private SelectedCharacter selectedChar;

    private void Start()
    {
        selectedChar = GetComponent<SelectedCharacter>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.countCharacter < GameManager.instance.maxCountCharacter)//khi nhấn vào khu vực thả quân
        {
            if (selectedChar.indexSelected < 0)
                return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);//xác định điểm nhấn dựa vào raycast

            if (hit.collider != null && hit.collider.TryGetComponent<AreaSpawn>(out AreaSpawn area))
            {
                int coinCharacter = selectedChar.characterPrefabs[selectedChar.indexSelected].GetComponent<CharacterBase>().GetCharacterSO().coin;
                if (coinCharacter > GameManager.instance.coin)
                    return;

                Spawn(selectedChar.indexSelected, area.posSpawn, area.gameObject, coinCharacter);//Tạo quân dựa vào thứ tự trong list, vị trí, gameobject cha
                selectedChar.indexSelected = -1;//Reset lại index để phải chọn lại quân để spawn
            }
        }
    }

    private void Spawn(int spawnIndex, Vector2 posSpawn, GameObject areaParent, int coinCharacter)
    {
        GameObject newChar = Instantiate(selectedChar.characterPrefabs[spawnIndex], posSpawn, Quaternion.identity);
        newChar.transform.parent = areaParent.transform; //đưa gameobject làm con của gameobject areaParent 

        GameManager.instance.ChangeCoin(-coinCharacter);
        GameManager.instance.ChangeCountCharacter(1);
    }
}