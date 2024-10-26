using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    [SerializeField] private Transform pointDealDamage;
    [SerializeField] private float rangeDealDamage;
    public int damage;
    public LayerMask enemyLayer;

    private void AttackEventAnimation()
    {
        DealDamage();
    }

    private void DealDamage()
    {
        Collider2D[] checks = Physics2D.OverlapCircleAll(pointDealDamage.position, rangeDealDamage, enemyLayer);
        if (checks.Length > 0)
        {
            if (checks[0].TryGetComponent(out Building building))
            {
                building.TakeDamage(damage);
                return;
            }

            checks[0].GetComponent<CharacterBase>().TakeDamage(damage);
            //Destroy(gameObject);
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()//Hàm vẽ
    {
        Gizmos.DrawWireSphere(pointDealDamage.position, rangeDealDamage);
    }
}
