using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRanger : CharacterBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    protected override void Update()
    {
        base.Update();
    }

    protected override void SearchCharacter()
    {
        RaycastHit2D[] checks = Physics2D.RaycastAll(checkPoint.position, Vector2.right, character.attackRange, enemyLayer);//kiểm tra trong phạm vi những vật có layer là enemyLayer
        if (checks.Length > 0)//Nếu trong vùng có
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }

    protected override void Attack()
    {
        base.Attack();

    }

    public void AttackBulletAnimationEvent()//Dùng trong Animtion Event để gọi hàm gây sát thương
    {
        RaycastHit2D[] checks = Physics2D.RaycastAll(checkPoint.position, Vector2.right, character.attackRange, enemyLayer);//kiểm tra trong phạm vi những vật có layer là enemyLayer
        if (checks.Length > 0)//Nếu trong vùng có
        {
            BulletSpawn();
        }
    }

    private void BulletSpawn()
    {
        GameObject bullet = Instantiate(bulletPrefab, checkPoint.position, Quaternion.identity);
        if(bullet.TryGetComponent<Bullet>(out Bullet bul))
        {
            bul.moveSpeed = bulletSpeed;
            bul.damage = character.damage;
        }
        if(bullet.TryGetComponent<BulletFire>(out BulletFire bulletFire))
        {
            bulletFire.enemyLayer = enemyLayer;
            bulletFire.damage = character.damage;
        }
        
    }

    private void OnDrawGizmosSelected()//Hàm vẽ
    {
        if (checkPoint == null)
            return;

        Gizmos.DrawLine(checkPoint.position, checkPoint.position+ new Vector3(character.attackRange,0,0));
    }
}
