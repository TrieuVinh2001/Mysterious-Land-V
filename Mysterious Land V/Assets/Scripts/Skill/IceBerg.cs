using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBerg : SkillBase
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

    }

    private void AttackEventAnimation()
    {
        Check();
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
