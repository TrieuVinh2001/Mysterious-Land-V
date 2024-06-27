using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardClick : MonoBehaviour, IPointerClickHandler
{
    private SelectedCharacter selectChar;
    private Button button;
    public int index;//thứ tự trong list
    public CharacterSO characterSO;

    [SerializeField] private Image coolDownImage;
    private TextMeshProUGUI timeCoolDownText;

    private float timeCoolDown;
    private float timeDown;
    private bool isCoolDown;

    private void Start()
    {
        button = GetComponent<Button>();
        selectChar = GetComponentInParent<SelectedCharacter>();
        GetComponent<Image>().sprite = characterSO.image;
        GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        timeCoolDownText = coolDownImage.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        timeCoolDown = characterSO.timeCoolDown;

    }

    private void Update()
    {
        if (isCoolDown)
        {
            TimeCoolDown();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.enabled)//Nếu nút hiện
        {
            for (int i = 0; i < selectChar.cards.Length; i++)
            {
                if(selectChar.cards[i].GetComponent<CardClick>().index == index)
                {
                    GetComponent<Image>().color = Color.white;
                }
                else
                {
                    selectChar.cards[i].GetComponent<CardClick>().gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
            }
            selectChar.indexSelected = index;
            selectChar.isSkill = false;
            selectChar.isHero = false;
        }
    }

    private void TimeCoolDown()
    {
        timeDown -= Time.deltaTime;
        timeCoolDownText.text = Mathf.RoundToInt(timeDown).ToString();//ép kiểu số nguyên
        coolDownImage.fillAmount = timeDown / timeCoolDown;
        if (timeDown <= 0)
        {
            isCoolDown = false;
            coolDownImage.gameObject.SetActive(false);
        }
    }

    public void CoolDown()
    {
        coolDownImage.gameObject.SetActive(true);
        isCoolDown = true;
        timeDown = timeCoolDown;
    }
}
