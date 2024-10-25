using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardClick : MonoBehaviour, IPointerClickHandler
{
    private Button button;
    public int index;//thứ tự trong list
    public CharacterSO characterSO;
    public GameObject prefab;

    [SerializeField] private Image coolDownImage;
    private TextMeshProUGUI timeCoolDownText;

    private float timeCoolDown;
    private float timeDown;
    private bool isCoolDown;

    private void Start()
    {
        button = GetComponent<Button>();
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
            for (int i = 0; i < SelectedCharacter.instance.cards.Length; i++)
            {
                if(SelectedCharacter.instance.cards[i].GetComponent<CardClick>().index == index)
                {
                    GetComponent<Image>().color = Color.white;
                }
                else
                {
                    SelectedCharacter.instance.cards[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
            }

            SelectedCharacter.instance.prefab = prefab;
            SelectedCharacter.instance.isSkill = false;
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
        GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
}
