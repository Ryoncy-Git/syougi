using UnityEngine;

public class Scr_UI : MonoBehaviour
{
    Scr_GameManager gameManager;
    GameObject UI_Nari;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Obj_GameManager").GetComponent<Scr_GameManager>();
        UI_Nari = GameObject.Find("UI_Nari");
        UI_Nari.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Button_Naru()
    {
        gameManager.Click_Naru();
    }

    public void Button_Naranai()
    {
        gameManager.Click_Naranai();
    }

    public void Show_NariSelect()
    {
        UI_Nari.SetActive(true);
    }

    public void Hide_NariSelect()
    {
        UI_Nari.SetActive(false);
    }
}
