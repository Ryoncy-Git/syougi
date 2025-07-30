using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Scr_UI : MonoBehaviour
{
    //managers
    public Scr_GameManager gameManager;
    public Scr_PieceFactory pieceFactory;
    public Scr_highlightGridManager scr_highlightGridManager;
    public Scr_CaptureManager captureManager;

    // texts
    public TMP_Text text_winner;

    // objects
    public GameObject UI_Settled;
    public GameObject UI_Nari;
    public GameObject UI_Box;
    public GameObject[] Text_Box;
    public GameObject[] Button;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide_AllUI();

        Show_Box();
        Switching_Box();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Hide_AllUI()
    {
        UI_Nari.SetActive(false);
        UI_Settled.SetActive(false);
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
                    scr_highlightGridManager.Show_highlightGrid(i, j);
            }
        }

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

    public void Settled(bool is1PTurn)
    {
        string playerLabel = "2P";
        if (is1PTurn)
            playerLabel = "1P";

        UI_Settled.SetActive(true);
        text_winner.text = $"{playerLabel} win!!";
    }

    public void Show_Box()
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

    void Show_Button1()
    {
        foreach (GameObject button in Button)
        {
            if (button.name == "Button1")
            {
                button.SetActive(true);
            }
        }
    }

    void Hide_Button1()
    {
        foreach (GameObject button in Button)
        {
            if (button.name == "Button1")
            {
                button.SetActive(false);
            }
        }
    }
    void Show_Button2()
    {
        foreach (GameObject button in Button)
        {
            if (button.name == "Button2")
            {
                button.SetActive(true);
            }
        }
    }
    void Hide_Button2()
    {
        foreach (GameObject button in Button)
        {
            if (button.name == "Button2")
            {
                button.SetActive(false);
            }
        }
    }

    public void Switching_Box()
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
}