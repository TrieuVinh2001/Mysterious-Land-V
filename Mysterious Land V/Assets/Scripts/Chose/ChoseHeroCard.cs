using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseHeroCard : MonoBehaviour
{
    public CharacterSO heroSO;
    public GameObject coolCard;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickAddCard);
    }

    public void ClickAddCard()
    {
        LevelManager.instance.ClickAddHeroCard(heroSO);

        coolCard.SetActive(true);
    }
}
