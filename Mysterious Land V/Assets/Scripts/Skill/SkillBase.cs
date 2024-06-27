using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    [SerializeField] protected SkillSO skillSO;
    public Vector3 posTarget;

    public SkillSO GetSkillSO()
    {
        return skillSO;
    }
}
