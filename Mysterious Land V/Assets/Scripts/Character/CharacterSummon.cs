using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSummon : CharacterBase
{
    public float timeSummon;//Thời gian chờ triệu hồi
    public GameObject batPrefab;//Dơi triệu hồi
    public Transform pointSummon;//Điểm triệu hồi
    private float nextSummonTime = 3f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Attack();
    }

    protected override void SearchCharacter()
    {
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
        if (Time.time > nextSummonTime)
        {
            isAttack = true;
            anim.SetTrigger("Attack");
            nextSummonTime = Time.time + timeSummon;//Thời gian chờ triệu hồi
            Instantiate(batPrefab, pointSummon.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.6f, 0.6f), 0), Quaternion.identity);
            Instantiate(batPrefab, pointSummon.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.6f, 0.6f), 0), Quaternion.identity);
            Instantiate(batPrefab, pointSummon.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.6f, 0.6f), 0), Quaternion.identity);
            StartCoroutine(Speed());//Di chuyển tiếp
        }
    }

    IEnumerator Speed()
    {
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
    }

    private void OnDrawGizmosSelected()//Hàm vẽ
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(character.attackRange, 0, 0) * transform.localScale.x);
    }
}
