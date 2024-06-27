using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardSkillClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SelectedCharacter selectedChar;
    private Button button;
    public SkillSO skillSO;
    [SerializeField] private Image coolDownImage;
    private TextMeshProUGUI timeCoolDownText;

    private float timeCoolDown;
    private float timeDown;
    private bool isCoolDown;

    private void Start()
    {
        button = GetComponent<Button>();
        GetComponent<Image>().sprite = skillSO.image;

        timeCoolDownText = coolDownImage.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        timeCoolDown = skillSO.timeCoolDown;

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
        if (button.enabled)
        {
            selectedChar.isSkill = true;
            selectedChar.isHero = false;
            selectedChar.indexSelected = -1;

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
