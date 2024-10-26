using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRanger : CharacterBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool isTargetBullet;

    protected override void Update()
    {
        base.Update();
    }

    protected override void SearchCharacter()
    {
        if(pointAttack == null)
        {
            pointAttack = transform;
        }
        RaycastHit2D[] checks = Physics2D.RaycastAll(transform.position, Vector2.right * transform.localScale.x, character.attackRange, enemyLayer);//kiểm tra trong phạm vi những vật có layer là enemyLayer
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
        if (pointAttack == null)
        {
            pointAttack = transform.GetChild(0);
        }
        
        RaycastHit2D[] checks = Physics2D.RaycastAll(pointAttack.position, Vector2.right * transform.localScale.x, character.attackRange, enemyLayer);//kiểm tra trong phạm vi những vật có layer là enemyLayer
        if (checks.Length > 0)//Nếu trong vùng có
        {
            if (isTargetBullet)
            {
                pointAttack = checks[0].transform;
            }

            BulletSpawn();
        }
    }

    private void BulletSpawn()
    {
        GameObject bullet = Instantiate(bulletPrefab, pointAttack.position, Quaternion.identity);
        if (bullet.TryGetComponent<Bullet>(out Bullet bul))
        {
            bul.moveSpeed = bulletSpeed;
            bul.transform.localScale = transform.localScale;
            bul.damage = character.damage;
            bul.enemyLayer = enemyLayer;
        }
        else if(bullet.TryGetComponent<BulletFire>(out BulletFire bulletFire))
        {
            bulletFire.transform.localScale = transform.localScale;
            bulletFire.enemyLayer = enemyLayer;
            bulletFire.damage = character.damage;
        }
        else if(bullet.TryGetComponent<Bomb>(out Bomb bomb))
        {
            bomb.enemyLayer = enemyLayer;
            bomb.damage = character.damage;
            bomb.transform.position = transform.position;
            bomb.enemy = pointAttack;
        }
        else if(bullet.TryGetComponent<Laser>(out Laser laser))
        {
            laser.UpdateLaserRange(character.damage);
        }
        else if(bullet.TryGetComponent<BallSoul>(out BallSoul ballSoul))
        {
            ballSoul.posTarget = pointAttack.position + new Vector3(character.attackRange, 0, 0) * transform.localScale.x;
            ballSoul.damage = character.damage;
            ballSoul.moveSpeed = bulletSpeed;
            ballSoul.enemyLayer = enemyLayer;
        }
        
    }

    private void OnDrawGizmosSelected()//Hàm vẽ
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(character.attackRange,0,0) * transform.localScale.x);
    }
}
