using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/SkillData")]
public class SkillData : ScriptableObject
{
    public int damage;
    public float coolDown;
    public string animName;
    public AudioClip sfx;
    public GameObject effectPrefab;
}
