using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterSO character;
    [SerializeField] protected bool isAttack;
    [SerializeField] protected Transform checkPoint;//Vị trí kiểm tra
    [SerializeField] protected LayerMask enemyLayer;//Layer của đối thủ
    [SerializeField] protected float health;
    protected float nextAttackTime = 1f;//Biến trung gian để so với thời gian dùng trong hồi chiêu
    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Start()
    {
        health = character.hp;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(character.type == CharacterSO.CharacterType.Player)//Kiểm tra loại nhân vật là player hay enemy
        {
            gameObject.layer = 7;//Số thự tự trong Layer, tương ứng là Player
            enemyLayer.value = 64;//Tuy cùng là Layer nhưng số khác nhau, nên 64 ở đây = Enemy(Tùy thuộc vào thứ tự, nên cần Debug.Log(enemyLayer.value) và chọn layer trong editor trước để xác định)
        }
        else if(character.type == CharacterSO.CharacterType.Enemy)
        {
            gameObject.layer = 6;//Số thự tự trong Layer, tương ứng là Enemy
            enemyLayer.value = 128;// 128 = Player
        }
    }

    protected virtual void Update()
    {
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
        if(character.type == CharacterSO.CharacterType.Enemy)//Nếu là Enemy thì di chuyển từ phải sang trái
        {
            transform.Translate(Vector2.left * character.moveSpeed * Time.deltaTime);
        }
        else if(character.type == CharacterSO.CharacterType.Player)//Nếu là Enemy thì di chuyển từ trái sang phải
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
        }
        else
        {
            health = 0;
            Death();
        }
    }

    protected virtual void Death()//Chết
    {
        Destroy(gameObject);//Hủy nhân vật
    }

    

    public int GetId()
    {
        return character.id;
    }

    public CharacterSO GetCharacterSO()
    {
        return character;
    }
}
