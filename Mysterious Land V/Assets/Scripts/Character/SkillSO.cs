using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillSO : ScriptableObject
{
    [Header("Attributes")]
    public new string name;
    public int id;
    public Sprite image;
    public float timeCoolDown;
}
