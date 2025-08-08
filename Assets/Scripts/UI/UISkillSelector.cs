using UnityEngine;
using TMPro;

public class UISkillSelector : MonoBehaviour
{
    public GameObject uiSkillSelector;
    public TMP_Text[] texts;
    public Sprite[] sprites;

    public void ShowSkillSelector()
    {
        uiSkillSelector.SetActive(true);
        RandomizeSkill();
    }

    public void HideSkillSelector()
    {
        uiSkillSelector.SetActive(false);
    }

    private void RandomizeSkill()
    {
        
    }
}
