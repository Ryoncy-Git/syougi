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
public class ControllSkill : MonoBehaviour
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
