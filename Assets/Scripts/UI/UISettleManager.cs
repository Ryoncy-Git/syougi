using UnityEngine;
using TMPro;

public class UISettleManager : MonoBehaviour
{
    public TMP_Text text_winner;
    public GameObject UI_Settled;

    public void Show_SettledUI(bool is1PTurn)
    {
        string playerLabel = "2P";
        if (is1PTurn)
            playerLabel = "1P";

        UI_Settled.SetActive(true);
        text_winner.text = $"{playerLabel} win!!";
    }

    public void Hide_SettledUI()
    {
        UI_Settled.SetActive(false);
    }
}
