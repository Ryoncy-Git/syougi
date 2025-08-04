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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
