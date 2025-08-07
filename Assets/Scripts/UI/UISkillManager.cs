using UnityEngine;
using TMPro;

public class UISkillManager : MonoBehaviour
{
    public TMP_Text[] texts;
    public TMP_Text textExp;
    public TMP_Text textLevel;
    public AttackSkill attackSkill;
    public DefendSkill defendSkill;
    public ControlSkill controlSkill;


    public void ShowSkills(Piece piece)
    {
        AttackSkillType targetSkillAttack = piece.GetAttackSkill();
        DefendSkillType targetSkillDefend = piece.GetDefendSkill();
        ControlSkillType targetSkillControl = piece.GetControlSkill();

        int counter = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Debug.Log(counter + "= counter");
                switch (i)
                {
                    case 0:
                        if (attackSkill.HasSkill(j, targetSkillAttack))
                        {
                            texts[counter].text = attackSkill.GetTextOfSkill(j);
                            counter++;
                        }
                        break;
                    case 1:
                        if (defendSkill.HasSkill(j, targetSkillDefend))
                        {
                            texts[counter].text = defendSkill.GetTextOfSkill(j);
                            counter++;
                        }
                        break;
                    case 2:
                        if (controlSkill.HasSkill(j, targetSkillControl))
                        {
                            texts[counter].text = controlSkill.GetTextOfSkill(j);
                            counter++;
                        }
                        break;
                    default:
                        break;
                }

                if (counter >= 3)
                    break;
            }

            if (counter >= 3)
                break;
        }
        counter = 0;


        ShowEXPandLevel(piece);
    }

    private void ShowEXPandLevel(Piece piece)
    {
        int exp = piece.GetExp();
        int level = piece.GetLevel();

        textExp.text = "EXP: " + exp.ToString();
        textLevel.text = "Level: " + level.ToString();
    }

}


