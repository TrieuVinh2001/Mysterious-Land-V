using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoul : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    public LayerMask enemyLayer;

    public Vector3 posTarget;

    private Vector3 posStart;
    private bool isDestroy = false;

    private Rigidbody2D rb;
    private float spin = 2000;
    private float angle;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        posStart = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Rotation();
        transform.position = Vector2.MoveTowards(transform.position, posTarget, moveSpeed * Time.deltaTime);
        if(transform.position == posTarget)
        {
            posTarget = posStart;
            isDestroy = true;
        }

        if(transform.position == posStart && isDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void Rotation()
    {
        angle = transform.rotation.eulerAngles.z;
        angle += rotationSpeed * Time.deltaTime;  //Tăng vị trí quay
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Quay tròn
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<CharacterBase>(out CharacterBase characterBase))
        {
            if(enemyLayer != characterBase.enemyLayer)
            {
                characterBase.TakeDamage(damage);
            }
            
        }
        
        if(collision.TryGetComponent<Building>(out Building building))
        {
            building.TakeDamage(damage);
        }
    }
}
