using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    public Vector3 dir;
    public LayerMask enemyLayer;
    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
        Destroy(gameObject, 1.5f); 
    }

    private void Update()
    {
        Move();

        Collider2D[] checks = Physics2D.OverlapCircleAll(transform.position, 0.01f, enemyLayer);
        if (checks.Length > 0)
        {
            if (checks[0].TryGetComponent(out Building building))
            {
                building.TakeDamage(damage);
                DestroyBullet();
                return;
            }

            checks[0].GetComponent<CharacterBase>().TakeDamage(damage);
            DestroyBullet();
        }
    }

    private void Move()
    {
        transform.position += Vector3.right * transform.localScale.x * moveSpeed * Time.deltaTime;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == enemyLayer && collision.TryGetComponent(out CharacterBase characterBase))
    //    {
    //        characterBase.TakeDamage(damage);
    //        DestroyBullet();
    //    }
    //    else if (collision.gameObject.layer == enemyLayer && collision.TryGetComponent(out Building building))
    //    {
    //        building.TakeDamage(damage);
    //        DestroyBullet();
    //    }
        
    //}

    public void DestroyBullet()
    {
        GameObject explosion =  Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject);
    }
}
