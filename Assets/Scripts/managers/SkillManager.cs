using UnityEngine;

[System.Flags]
public enum SkillType
{
    None = 0,
    Attack = 1,
    Defend = 2,
    Control = 3,
};


public class SkillManager : MonoBehaviour
{
    public GameManager gameManager;
    public DefendSkill defendSkill;
    public AttackSkill attackSkill;
    public ControllSkill controllSkill;


    public void aaa()
    {

    }
}
