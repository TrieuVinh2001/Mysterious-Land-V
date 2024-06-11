using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardClick : MonoBehaviour, IPointerClickHandler
{
    private SelectedCharacter selectChar;
    private Button button;
    public int index;//thứ tự trong list
    public CharacterSO characterSO; 

    private void Start()
    {
        
        button = GetComponent<Button>();
        selectChar = GetComponentInParent<SelectedCharacter>();
        GetComponent<Image>().sprite = characterSO.image;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.enabled)//Nếu nút hiện
        {
            selectChar.indexSelected = index;

        }
    }
}
