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
            case 0: return "強制移動";
            case 1: return "選択不可";
            case 2: return "スキル3の説明";
            default: return "不明なスキル";
        }
    }
}
