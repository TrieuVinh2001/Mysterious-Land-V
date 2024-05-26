using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private float hp;

    public void TakeDamage(float damage)
    {
        if (hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
            DestroyBuilding();
        }
    }

    private void DestroyBuilding()
    {
        Destroy(gameObject);
    }
}
