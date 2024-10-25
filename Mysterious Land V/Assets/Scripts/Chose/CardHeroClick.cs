using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHeroClick : MonoBehaviour, IPointerClickHandler
{
    private Button button;
    public CharacterSO characterSO;
    public GameObject prefab;
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
            SelectedCharacter.instance.isSkill = false;
            SelectedCharacter.instance.prefab = prefab;
            button.enabled = false;
        }
    }
}
