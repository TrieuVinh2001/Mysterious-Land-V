using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseSkillCard : MonoBehaviour
{
    public SkillSO skillSO;
    public GameObject coolCard;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickAddCard);
    }

    public void ClickAddCard()
    {
        LevelManager.instance.ClickAddSkillCard(skillSO);

        coolCard.SetActive(true);
    }
}
