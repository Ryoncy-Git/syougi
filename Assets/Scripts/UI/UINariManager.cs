using UnityEngine;

public class UINariManager : MonoBehaviour
{
    // managers
    public Scr_GameManager gameManager;
    public GameObject UI_Nari;
    public void Button_Naru()
    {
        gameManager.Click_Naru();
        Hide_NariUI();
    }

    public void Button_Naranai()
    {
        gameManager.Click_Naranai();
        Hide_NariUI();
    }

    public void Show_NariUI()
    {
        UI_Nari.SetActive(true);
    }

    public void Hide_NariUI()
    {
        UI_Nari.SetActive(false);
    }
}
