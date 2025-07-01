using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    private Scr_Piece selectedPiece;
    private GameObject[,] grid = new GameObject[9, 9];
    private GameObject[,] highlightGrid = new GameObject[9, 9];
    bool is1PTurn = true;

    void Start()
    {
        init();
    }

    void init()
    {
        highlightGrid[0, 3] = GameObject.Find("highlightGrid");
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
    public void grid_setGameObject(GameObject piece, int x, int y)
    {
        grid[x, y] = piece;
    }

    public GameObject grid_getGameObject(int x, int y)
    {
        return grid[x, y];
    }

    public void change_turn()
    {
        is1PTurn = !is1PTurn;
    }

    public void show_highlightGrid(int roundX, int roundY)
    {
        if (highlightGrid[roundX, roundY] != null)
        {
            highlightGrid[roundX, roundY].SetActive(true);
        }
    }
}
