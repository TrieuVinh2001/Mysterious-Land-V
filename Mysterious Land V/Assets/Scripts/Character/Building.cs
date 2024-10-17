using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int hp;

    private void Start()
    {
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if (hp - damage > 0)
        {
            UpdateUI();
            hp -= damage;
        }
        else
        {
            hp = 0;
            UpdateUI();
            if (gameObject.layer == 7)//Player
            {
                GameManager.instance.uiManager.GameLose();
            }
            else
            {
                GameManager.instance.uiManager.GameWin();
            }
            DestroyBuilding();
        }
    }

    private void UpdateUI()
    {
        if (gameObject.layer == 7)
        {
            GameManager.instance.UpdateHealthBuildCharacter(hp);
        }
        else
        {
            GameManager.instance.UpdateHealthBuildEnemy(hp);
        }
    }

    private void DestroyBuilding()
    {
        Destroy(gameObject);
    }
}
