using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    [SerializeField] protected SkillSO skillSO;
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected LayerMask enemyLayer;
    [HideInInspector] public Vector3 posTarget;

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Check()
    {
        Collider2D[] checks = Physics2D.OverlapCircleAll(posTarget, skillSO.explosionRange, enemyLayer);
        if (checks.Length > 0)
        {
            for (int i = 0; i < checks.Length; i++)
            {
                if (i == skillSO.countEnemy)
                    return;
                checks[i].GetComponent<CharacterBase>().TakeDamage(skillSO.damage);
            }
        }
    }

    public SkillSO GetSkillSO()
    {
        return skillSO;
    }

    private void OnDrawGizmosSelected()//Hàm vẽ
    {
        Gizmos.DrawWireSphere(posTarget, skillSO.explosionRange);
    }
}
