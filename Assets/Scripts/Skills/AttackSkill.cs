using UnityEngine;

[System.Flags]
public enum AttackSkillType
{
    None = 0,
    Twice = 1 << 0,
    // 二回行動できる
    Scout = 1 << 1,
    // 対象をキャプチャするのではなく、自駒にする
    // = 1 << 2,
}
public class AttackSkill : MonoBehaviour
{
    public bool HasSkill(int skillIndex, AttackSkillType skill)
    {
        return ((int)skill & (1 << skillIndex)) != 0;
    }
    public string GetTextOfSkill(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0: return "Twice";
            case 1: return "Scout";
            default: return "unknown_attack";
        }
    }
}
