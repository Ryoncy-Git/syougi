using UnityEngine;

[System.Flags]
public enum ControlSkillType
{
    None = 0,
    ForceMoveRandom = 1 << 0,
    // 対象を強制的にランダムに移動させる
    Deselectable = 1 << 1,
    // 対象を選択不可にする
    Weeken = 1 << 2,
    // 対象が自分を攻撃できなくなる
}
public class ControlSkill : MonoBehaviour
{
    public bool HasSkill(int skillIndex, ControlSkillType skill)
    {
        return ((int)skill & (1 << skillIndex)) != 0;
    }

    public string GetTextOfSkill(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0: return "force";
            case 1: return "deselectable";
            case 2: return "weeklen";
            default: return "unknown_control";
        }
    }
}
