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
        TrySelectCapturedPiece(PieceType.Hu);
    }

    public void Click_Kyosya()
    {
        TrySelectCapturedPiece(PieceType.Kyosya);
    }

    public void Click_Keima()
    {
        TrySelectCapturedPiece(PieceType.Keima);
    }

    public void Click_Gin()
    {
        TrySelectCapturedPiece(PieceType.Gin);
    }

    public void Click_Kin()
    {
        TrySelectCapturedPiece(PieceType.Kin);
    }

    public void Click_Kaku()
    {
        TrySelectCapturedPiece(PieceType.Kaku);
    }

    public void Click_Hisya()
    {
        TrySelectCapturedPiece(PieceType.Hisya);
    }
    
    private void TrySelectCapturedPiece(PieceType type)
    {
        Dictionary<PieceType, int> dict = gameManager.Get_is1PTurn()
            ? gameManager.Get_CapturedPiece1P()
            : gameManager.Get_CapturedPiece2P();

        if (!dict.ContainsKey(type) || dict[type] <= 0)
        {
            // 持ち駒がない場合は選択不可
            return;
        }

        gameManager.Set_piece_willPut(type);
        Show_Grid_and_Hide_UI();
    }
}