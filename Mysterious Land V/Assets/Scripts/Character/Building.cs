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
            hp -= damage;
            UpdateUI();
        }
        else
        {
            hp = 0;
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
