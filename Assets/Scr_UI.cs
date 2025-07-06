using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Scr_UI : MonoBehaviour
{
    Scr_GameManager gameManager;
    GameObject UI_Nari;
    public TMP_Text text_capturedPiece1;
    public TMP_Text text_capturedPiece2;
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

    public void Show_capturedPiece(Dictionary<PieceType, int> piece1, Dictionary<PieceType, int> piece2)
    {
        text_capturedPiece1.text = BuildCapturedPieceText(piece1, "1P");
        text_capturedPiece2.text = BuildCapturedPieceText(piece2, "2P");
    }
    private string BuildCapturedPieceText(Dictionary<PieceType, int> pieces, string playerLabel)
    {
        return $"Captured Pieces ({playerLabel})\n" +
               $"Hu     : {GetCount(pieces, PieceType.Hu)}\n" +
               $"Kyosya : {GetCount(pieces, PieceType.Kyosya)}\n" +
               $"Keima  : {GetCount(pieces, PieceType.Keima)}\n" +
               $"Gin    : {GetCount(pieces, PieceType.Gin)}\n" +
               $"Kin    : {GetCount(pieces, PieceType.Kin)}\n" +
               $"Kaku   : {GetCount(pieces, PieceType.Kaku)}\n" +
               $"Hisya  : {GetCount(pieces, PieceType.Hisya)}";
    }
    private int GetCount(Dictionary<PieceType, int> dict, PieceType type)
    {
        return dict.ContainsKey(type) ? dict[type] : 0;
    }
}
