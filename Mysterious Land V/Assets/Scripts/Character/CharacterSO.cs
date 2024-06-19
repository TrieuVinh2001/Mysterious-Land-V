using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public enum CharacterType { Player, Enemy}

    [Header("Attributes")]
    public new string name;
    public int id;
    public CharacterType type;
    public Sprite image;
    public int coin;
    public int hp;
    public int damage;
    public float armor;
    public float moveSpeed;
    public float attackSpeed;
    public float attackRange;
    public float critical;//Chí mạng
    public float bloodSucker;//Hút máu
    public float armorPiercing;//Xuyên giáp
    public float healing;//Hồi máu
    public float timeCoolDown;//Thời gian chờ triệu hồi
}
