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
    public ControlSkill controlSkill;
    public UIManager uiManager;


    public void ShowSkills(Piece piece)
    {
        if (piece == null) return;

        uiManager.ShowSkills(piece);
    }
}
