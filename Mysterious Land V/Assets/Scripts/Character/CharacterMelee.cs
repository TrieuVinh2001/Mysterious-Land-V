using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMelee : CharacterBase
{
    protected override void Update()
    {
        base.Update();//Cần dùng để sử dụng những cái đã gọi trong hàm ở lớp cha

    }

    protected override void SearchCharacter()
    {
        Collider2D[] checks = Physics2D.OverlapCircleAll(checkPoint.position, character.attackRange, enemyLayer);//kiểm tra trong phạm vi những vật có layer là enemyLayer
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

    public void AttackAnimationEvent()//Dùng trong Animtion Event để gọi hàm gây sát thương
    {
        Collider2D[] checks = Physics2D.OverlapCircleAll(checkPoint.position, character.attackRange, enemyLayer);
        if (checks.Length > 0)
        {
            if (checks[0].TryGetComponent(out Building building))
            {
                building.TakeDamage(character.damage);
                return;
            }

            checks[0].GetComponent<CharacterBase>().TakeDamage(character.damage);
            
        }
    }

    private void OnDrawGizmosSelected()//Hàm vẽ
    {
        if (checkPoint == null)
            return;

        Gizmos.DrawWireSphere(checkPoint.position, character.attackRange);
    }
}
