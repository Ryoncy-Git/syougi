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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
