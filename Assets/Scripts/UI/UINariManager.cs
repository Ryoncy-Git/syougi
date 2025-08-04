using UnityEngine;
using System.Collections; 

public class UINariManager : MonoBehaviour
{
    // managers
    public GameManager gameManager;
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
        StartCoroutine(ShowNariUICoroutine());
        // UI_Nari.SetActive(true);
    }

    private IEnumerator ShowNariUICoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        UI_Nari.SetActive(true);
    }

    public void Hide_NariUI()
    {
        UI_Nari.SetActive(false);
    }
}
