using UnityEngine;
using System.Collections.Generic;

public class Scr_GameManager : MonoBehaviour
{
    private Scr_Piece selectedPiece;
    private GameObject[,] grid = new GameObject[9, 9];
    private GameObject[,] highlightGrid = new GameObject[9, 9];
    bool is1PTurn = true;
    Dictionary<PieceType, int> capturedPieces_1P = new Dictionary<PieceType, int>();
    Dictionary<PieceType, int> capturedPieces_2P = new Dictionary<PieceType, int>();
    int NaruX, NaruY;
    Scr_UI Scr_ui;

    void Start()
    {
        Scr_ui = GameObject.Find("UserInterface").GetComponent<Scr_UI>();
        Init();
    }

    void Init()
    {
        // initialize of highlightGrid
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("Tag_highlightGrid");

        int index = 0;
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (index < foundObjects.Length)
                {
                    highlightGrid[x, y] = foundObjects[index];
                    index++;

                    highlightGrid[x, y].transform.position = new Vector3(x, y, -2);
                    highlightGrid[x, y].transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    highlightGrid[x, y].SetActive(false);
                }
            }
        }

        // initialize of capturedPieces
        foreach (PieceType type in System.Enum.GetValues(typeof(PieceType)))
        {
            capturedPieces_1P[type] = 0;
            capturedPieces_2P[type] = 0;
        }
    }

    public void SelectPiece(Scr_Piece piece)
    {

        if (selectedPiece != null)
        {
            selectedPiece.Deselect();
        }
        selectedPiece = piece;
        selectedPiece.Select();
    }

    public void DeselectPiece()
    {
        selectedPiece.Deselect();
        selectedPiece = null;
    }
    public void Set_GridGameObject(GameObject piece, int x, int y)
    {
        if (x >= 0 && x < 9 && y >= 0 && y < 9)
        {
            grid[x, y] = piece;
        }
    }

    public GameObject Get_GridGameObject(int x, int y)
    {
        if (x >= 0 && x < 9 && y >= 0 && y < 9)
        {
            return grid[x, y];
        }

        return null;
    }

    public void Change_turn()
    {
        is1PTurn = !is1PTurn;
    }

    public void Show_highlightGrid(int roundX, int roundY)
    {
        if (roundX >= 0 && roundX < 9 && roundY >= 0 && roundY < 9 && highlightGrid[roundX, roundY] != null)
        {
            highlightGrid[roundX, roundY].SetActive(true);
        }
    }

    public void Hide_highlightGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                highlightGrid[i, j].SetActive(false);
            }
        }
    }

    public void Click_highlightGrid(int x, int y)
    {
        bool canNari = false;
        int prevY = Mathf.RoundToInt(selectedPiece.transform.position.y);
        canNari = (selectedPiece.Get_is1PPiece() && y >= 6) || (selectedPiece.Get_is1PPiece() && prevY >= 6) ||
                  (!selectedPiece.Get_is1PPiece() && y <= 2) || (!selectedPiece.Get_is1PPiece() && prevY <= 2);

        if (canNari && !selectedPiece.Get_isNari())
        {
            NaruX = x;
            NaruY = y;
            // なる画面のUIを表示
            Scr_ui.Show_NariSelect();
            // 入力をUI以外無効化
            // Scr_ui.mukouka();
        }
        else
        {
            if (selectedPiece != null)
            {
                selectedPiece.Movement(x, y);
            }
        }
    }

    public void Click_Naru()
    {
        selectedPiece.Set_Nari(true);
        if (selectedPiece != null)
        {
            selectedPiece.Movement(NaruX, NaruY);
        }
        Scr_ui.Hide_NariSelect();
    }

    public void Click_Naranai()
    {
        if (selectedPiece != null)
        {
            selectedPiece.Movement(NaruX, NaruY);
        }
        Scr_ui.Hide_NariSelect();
    }

    public void Capture_piece(GameObject piece, bool is1P) // 1Pが捕まえたのか
    {
        // piece は捕まえられたピース
        // is1Pは捕まえる側がどっちなのかを表す
        Scr_Piece targetPiece = piece.GetComponent<Scr_Piece>();
        if (targetPiece == null) return;

        PieceType targetPieceType = targetPiece.Get_PieceType();

        if (is1P)
        {
            if (capturedPieces_1P.ContainsKey(targetPieceType))
                capturedPieces_1P[targetPieceType]++;
            else
                capturedPieces_1P[targetPieceType] = 1;
        }
        else
        {
            if (capturedPieces_2P.ContainsKey(targetPieceType))
                capturedPieces_2P[targetPieceType]++;
            else
                capturedPieces_2P[targetPieceType] = 1;
        }

        if (piece != null)
        {
            Destroy(piece);
        }
    }
}
