using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public float maxHealth;
    public AttackData[] attacks;
    public string winAnimationName;
}

[System.Serializable]
public class AttackData
{
    public string attackName;
    public float minDamage;
    public float maxDamage;
    public string animationName;
    public float attackDuration;
    public GameObject attackParticles;
    public GameObject attackHitParticles;
}
