using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHeroClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SelectedCharacter selectChar;
    private Button button;
    public CharacterSO characterSO;
    [SerializeField] private Image coolDownImage;

    private void Start()
    {
        button = GetComponent<Button>();
        
        GetComponent<Image>().sprite = characterSO.image;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.enabled)//Nếu nút hiện
        {
            selectChar.isHero = true;
            selectChar.isSkill = false;
            selectChar.indexSelected = -1;
            button.enabled = false;
        }
    }
}
