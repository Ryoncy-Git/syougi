using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Scr_UI : MonoBehaviour
{
    public Scr_GameManager gameManager;
    public GameObject UI_Nari;
    public TMP_Text text_capturedPiece1;
    public TMP_Text text_capturedPiece2;
    public GameObject UI_selectPutPiece;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // gameManager = GameObject.Find("Obj_GameManager").GetComponent<Scr_GameManager>();
        // UI_Nari = GameObject.Find("UI_Nari");
        UI_Nari.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // UI Nari

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

    // show capturedPiece

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

    // select put piece

    public void Show_selectPutPiece()
    {
        UI_selectPutPiece.SetActive(true);
    }

    public void Hide_selectPutPiece()
    {
        UI_selectPutPiece.SetActive(false);
    }
    void Show_Grid_and_Hide_UI()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (gameManager.Get_GridGameObject(i, j) == null)
                    gameManager.Show_highlightGrid(i, j);
            }
        }

        Hide_selectPutPiece();

        gameManager.Set_isSpawnTurn(true);
    }

    public void Click_Hu()
    {
        gameManager.Set_piece_willPut(PieceType.Hu);

        Show_Grid_and_Hide_UI();
    }

    public void Click_Kyosya()
    {

    }
}
