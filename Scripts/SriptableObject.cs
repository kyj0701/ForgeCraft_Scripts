using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Characters/Character")]
public class CharacterSO : ScriptableObject
{
    [Header("# Main Info")]
    public int CharacterRank;
    public Enums.CharacterType CharacterType;
    public int CharacterID;
    public string CharacterName;
    public string CharacterClass;
    [TextArea]
    public string CharacterDesc;
    public Sprite CharacterIcon;
    public Sprite CharacterSprite;
    public Enums.SFX[] CharacterSFX;

    [Header("# Level Data")]
    public float BaseHealth;
    public float BaseAttack;
    public float BaseAttackDelay;
    public float BaseDefence;
    public float BaseAttackRange;
    public float[] BaseStatus;
    public float BaseCriticalProb;
    public float BaseCriticalAttack;
    public float BaseHitProb;
    public float BaseDodgeProb;

    [Header("# Skill ID")]
    public int SkillId;
}