using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    private Scr_Piece selectedPiece;
    private Transform[,] grid = new Transform[9, 9];
    bool is1PTurn = true;

    public void SelectPiece(Scr_Piece piece)
    {
        if (is1PTurn)
        {
            if (selectedPiece != null)
            {
                selectedPiece.Deselect();
            }

            selectedPiece = piece;
            selectedPiece.Select();
        }
    }
    public void grid_setTransform(Transform piece, int x, int y)
    {
        grid[x, y] = piece;
    }

    public Transform grid_getTransform(int x, int y)
    {
        return grid[x, y];
    }

    public void change_turn()
    {
        is1PTurn = !is1PTurn;
    }
}
