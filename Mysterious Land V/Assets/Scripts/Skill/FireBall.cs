using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SkillBase
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private Vector3 posStart;
    [SerializeField] private Vector3 rotate;
    
    protected override void Start()
    {
        base.Start();
        transform.Rotate(rotate);
        transform.position = new Vector3(posTarget.x, posTarget.y, 0) + posStart;
    }

    protected override void Update()
    {
        base.Update();

        transform.position = Vector2.MoveTowards(transform.position, posTarget, moveSpeed * Time.deltaTime);

        if (transform.position == posTarget)
        {
            Check();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
