using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    [SerializeField] private GameObject explosionPrefab;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.TryGetComponent(out CharacterBase characterBase))
        {
            characterBase.TakeDamage(damage);
            DestroyBullet();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.TryGetComponent(out Building building))
        {
            building.TakeDamage(damage);
            DestroyBullet();
        }
        
    }

    public void DestroyBullet()
    {
        GameObject explosion =  Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject);
    }
}
