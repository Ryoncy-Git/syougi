using UnityEngine;

[System.Flags]
public enum DefendSkillType
{
    None = 0,
    Dodge = 1 << 0,
    // 攻撃されるときに１ます周りに移動することで回避する
    Swap = 1 << 1,
    // 攻撃されるときに自分の付近の駒と入れ替わる
    invincible = 1 << 2,
    // 攻撃されるときに無敵状態になる
}

public class DefendSkill : MonoBehaviour
{
    public bool HasSkill(int skillIndex, DefendSkillType skill)
    {
        return ((int)skill & (1 << skillIndex)) != 0;
    }
    public string GetTextOfSkill(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0: return "回避";
            case 1: return "入れ替え";
            case 2: return "無敵";
            default: return "不明なスキル";
        }
    }

}
