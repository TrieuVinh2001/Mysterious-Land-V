using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SkillBase
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float explosionRange;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] protected LayerMask enemyLayer;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(new Vector3(0, 0, -120));
        transform.position = new Vector3(posTarget.x + 5, posTarget.y + 5, 0);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, posTarget, moveSpeed * Time.deltaTime);
        
        if(transform.position == posTarget)
        {
            Check();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Check()
    {
        Collider2D[] checks = Physics2D.OverlapCircleAll(posTarget, explosionRange, enemyLayer);
        if (checks.Length > 0)
        {
            checks[0].GetComponent<CharacterBase>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()//Hàm vẽ
    {
        Gizmos.DrawWireSphere(posTarget, explosionRange);
    }
}
