using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMelee : CharacterBase
{
    protected override void Update()
    {
        base.Update();//Cần dùng để sử dụng những cái đã gọi trong hàm ở lớp cha

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
            checks[0].GetComponent<CharacterBase>().TakeDamage(character.damage);
            if(checks[0].TryGetComponent<Building>(out Building buiding))
            {
                buiding.TakeDamage(character.damage);
            }
        }
    }
}
