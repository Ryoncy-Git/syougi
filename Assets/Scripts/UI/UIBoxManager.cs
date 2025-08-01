using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UIBoxManager : MonoBehaviour
{
    public GameManager gameManager;
    public PieceFactory pieceFactory;
    public DestGridManager destGridManager;
    public UIManager uiManager;
    public CaptureManager captureManager;

    public GameObject UI_Box;
    public GameObject[] Text_Box;
    public GameObject[] Button;

    public void Show_BoxUI()
    {
        foreach (GameObject box in Text_Box)
        {
            TMP_Text text = box.GetComponent<TMP_Text>();

            if (box.name.Contains("Hu") && box.name.Contains("1"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece1P(), PieceType.Hu).ToString();
            }
            else if (box.name.Contains("Kyosya") && box.name.Contains("1"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece1P(), PieceType.Kyosya).ToString();
            }
            else if (box.name.Contains("Keima") && box.name.Contains("1"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece1P(), PieceType.Keima).ToString();
            }
            else if (box.name.Contains("Gin") && box.name.Contains("1"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece1P(), PieceType.Gin).ToString();
            }
            else if (box.name.Contains("Kin") && box.name.Contains("1"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece1P(), PieceType.Kin).ToString();
            }
            else if (box.name.Contains("Kaku") && box.name.Contains("1"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece1P(), PieceType.Kaku).ToString();
            }
            else if (box.name.Contains("Hisya") && box.name.Contains("1"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece1P(), PieceType.Hisya).ToString();
            }
            // 2P側も同様に
            else if (box.name.Contains("Hu") && box.name.Contains("2"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece2P(), PieceType.Hu).ToString();
            }
            else if (box.name.Contains("Kyosya") && box.name.Contains("2"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece2P(), PieceType.Kyosya).ToString();
            }
            else if (box.name.Contains("Keima") && box.name.Contains("2"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece2P(), PieceType.Keima).ToString();
            }
            else if (box.name.Contains("Gin") && box.name.Contains("2"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece2P(), PieceType.Gin).ToString();
            }
            else if (box.name.Contains("Kin") && box.name.Contains("2"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece2P(), PieceType.Kin).ToString();
            }
            else if (box.name.Contains("Kaku") && box.name.Contains("2"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece2P(), PieceType.Kaku).ToString();
            }
            else if (box.name.Contains("Hisya") && box.name.Contains("2"))
            {
                text.text = GetCount(captureManager.Get_CapturedPiece2P(), PieceType.Hisya).ToString();
            }
        }
    }

    private int GetCount(Dictionary<PieceType, int> dict, PieceType type)
    {
        return dict.ContainsKey(type) ? dict[type] : 0;
    }

    void Show_Button1()
    {
        foreach (GameObject button in Button)
        {
            if (button.name.EndsWith("1"))
            {
                button.SetActive(true);
            }
        }
    }

    void Hide_Button1()
    {
        foreach (GameObject button in Button)
        {
            if (button.name.EndsWith("1"))
            {
                button.SetActive(false);
            }
        }
    }
    void Show_Button2()
    {
        foreach (GameObject button in Button)
        {
            if (button.name.EndsWith("2"))
            {
                button.SetActive(true);
            }
        }
    }
    void Hide_Button2()
    {
        foreach (GameObject button in Button)
        {
            if (button.name.EndsWith("2"))
            {
                button.SetActive(false);
            }
        }
    }

    public void Switching_BoxButton()
    {
        if (gameManager.Get_is1PTurn())
        {
            Hide_Button1();
            Show_Button2();
        }
        else
        {
            Hide_Button2();
            Show_Button1();
        }
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
        gameManager.DeselectPiece();
        Dictionary<PieceType, int> dict = gameManager.Get_is1PTurn()
            ? captureManager.Get_CapturedPiece1P()
            : captureManager.Get_CapturedPiece2P();

        if (!dict.ContainsKey(type) || dict[type] <= 0)
        {
            // 持ち駒がない場合は選択不可
            return;
        }

        pieceFactory.Set_piece_willPut(type);
        Show_Grid_and_Hide_UI();
    }



    void Show_Grid_and_Hide_UI() //手ごまを置くときのやつ
    {
        destGridManager.ShowGridIfNotNihu();
        uiManager.Hide_AllUI();
    }
}
