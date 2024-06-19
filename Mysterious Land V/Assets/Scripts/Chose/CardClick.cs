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
            coolDownImage.gameObject.SetActive(true);
            selectChar.indexSelected = index;
            

            timeDown = timeCoolDown;

            isCoolDown = true;
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
}
