﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterSO character;
    [SerializeField] protected bool isAttack;
    [SerializeField] protected Transform pointAttack;//Vị trí kiểm tra
    [SerializeField] public LayerMask enemyLayer;//Layer của đối thủ
    [SerializeField] protected float health;
    [SerializeField] protected bool isEnemy;
    [SerializeField] protected bool isSummon;
    [SerializeField] protected GameObject floatingText;
    [SerializeField] protected Image healthImage;
    protected float nextAttackTime = 1f;//Biến trung gian để so với thời gian dùng trong hồi chiêu
    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Start()
    {
        health = character.hp;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //if(character.type == CharacterSO.CharacterType.Player)//Kiểm tra loại nhân vật là player hay enemy
        //{
        //    gameObject.layer = 7;//Số thự tự trong Layer, tương ứng là Player
        //    enemyLayer.value = 64;//Tuy cùng là Layer nhưng số khác nhau, nên 64 ở đây = Enemy(Tùy thuộc vào thứ tự, nên cần Debug.Log(enemyLayer.value) và chọn layer trong editor trước để xác định)
        //}
        //else if(character.type == CharacterSO.CharacterType.Enemy)
        //{
        //    gameObject.layer = 6;//Số thự tự trong Layer, tương ứng là Enemy
        //    enemyLayer.value = 128;// 128 = Player
        //}
    }

    protected virtual void Update()
    {
        healthImage.fillAmount = health / character.hp;

        SearchCharacter();//Tìm kiếm các nhân vật trong phạm vi

        if (!isAttack)
        {
            Move();
        }
        else
        {
            Attack();
        }
    }

    protected virtual void SearchCharacter()
    {
        
    }

    protected virtual void Move()//Di chuyển
    {
        //if(character.type == CharacterSO.CharacterType.Enemy)//Nếu là Enemy thì di chuyển từ phải sang trái
        //{
        //    transform.Translate(Vector2.left * character.moveSpeed * Time.deltaTime);
        //}
        //else if(character.type == CharacterSO.CharacterType.Player)//Nếu là Enemy thì di chuyển từ trái sang phải
        //{
        //    transform.Translate(Vector2.right * character.moveSpeed * Time.deltaTime);
        //}
        if(isEnemy)//Nếu là Enemy thì di chuyển từ phải sang trái
        {
            transform.Translate(Vector2.left * character.moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * character.moveSpeed * Time.deltaTime);
        }
        anim.SetBool("Run", true);
    }

    protected virtual void Attack()//Tấn công
    {
        anim.SetBool("Run", false);

        if (Time.time >= nextAttackTime)
        {
            anim.SetTrigger("Attack");
            nextAttackTime = Time.time + character.attackSpeed;
        }
    }

    public virtual void TakeDamage(float damage)//Nhận sát thương
    {
        if (health - damage > 0)
        {
            health -= damage;

            GameObject prefab = Instantiate(floatingText, healthImage.gameObject.transform.position, Quaternion.identity) as GameObject;//Sinh ra chữ
            prefab.GetComponentInChildren<TextMeshPro>().text = "-" + damage.ToString();//Gán sát thương cho chữ
        }
        else
        {
            health = 0;
            //if (character.type == CharacterSO.CharacterType.Player)//Kiểm tra loại nhân vật là player hay enemy
            //{
            //    GameManager.instance.ChangeCountCharacter(-1);
            //}
            //else if (character.type == CharacterSO.CharacterType.Enemy)
            //{
            //    GameManager.instance.ChangeCountEnemy(-1);
            //}

            if (!isEnemy && !isSummon)//Kiểm tra loại nhân vật là player hay enemy
            {
                GameManager.instance.ChangeCountCharacter(-1);
            }
            else if(isEnemy && !isSummon)
            {
                GameManager.instance.ChangeCountEnemy(-1);
            }
            
            Death();
        }
    }

    protected virtual void Death()//Chết
    {
        Destroy(gameObject);//Hủy nhân vật
    }

    public CharacterSO GetCharacterSO()
    {
        return character;
    }
}
