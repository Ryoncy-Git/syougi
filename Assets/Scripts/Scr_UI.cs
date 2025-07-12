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
    public GameObject UI_default;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide_AllUI();

        UI_default.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Hide_AllUI()
    {
        UI_Nari.SetActive(false);
        UI_selectPutPiece.SetActive(false);
        UI_default.SetActive(false);
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
        Hide_AllUI();
        UI_Nari.SetActive(true);
    }

    public void Hide_NariSelect()
    {
        UI_Nari.SetActive(false);
        UI_default.SetActive(true);
    }

    public void Show_selectPutPiece()
    {
        Hide_AllUI();
        gameManager.Hide_highlightGrid();
        UI_selectPutPiece.SetActive(true);
    }

    public void Hide_selectPutPiece()
    {
        UI_selectPutPiece.SetActive(false);
        UI_default.SetActive(true);
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



    void Show_Grid_and_Hide_UI() //手ごまを置くときのやつ
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
        gameManager.Set_piece_willPut(PieceType.Kyosya);
        Show_Grid_and_Hide_UI();
    }

    public void Click_Keima()
    {
        gameManager.Set_piece_willPut(PieceType.Keima);
        Show_Grid_and_Hide_UI();
    }

    public void Click_Gin()
    {
        gameManager.Set_piece_willPut(PieceType.Gin);
        Show_Grid_and_Hide_UI();
    }

    public void Click_Kin()
    {
        gameManager.Set_piece_willPut(PieceType.Kin);
        Show_Grid_and_Hide_UI();
    }

    public void Click_Kaku()
    {
        gameManager.Set_piece_willPut(PieceType.Kaku);
        Show_Grid_and_Hide_UI();
    }

    public void Click_Hisya()
    {
        gameManager.Set_piece_willPut(PieceType.Hisya);
        Show_Grid_and_Hide_UI();
    }
}