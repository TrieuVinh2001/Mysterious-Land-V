using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : SkillBase
{
    [SerializeField] private float posXStart;
    [SerializeField] private float posXEnd;
    [SerializeField] private float moveSpeed;

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(posXStart, posTarget.y, 0);
    }

    protected override void Update()
    {
        base.Update();
        
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(posXEnd, posTarget.y, 0), moveSpeed * Time.deltaTime);
        if(transform.position == new Vector3(posXEnd, posTarget.y, 0))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterBase>(out CharacterBase characterBase))
        {
            if (enemyLayer != characterBase.enemyLayer)
            {
                characterBase.TakeDamage(skillSO.damage);
            }

        }
    }
}
