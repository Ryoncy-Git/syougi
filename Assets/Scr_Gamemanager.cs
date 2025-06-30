using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    private Scr_Class_Piece selectedPiece;
    private Transform[,] grid = new Transform[9, 9];

    public void SelectPiece(Scr_Class_Piece piece)
    {
        if (selectedPiece != null)
        {
            selectedPiece.Deselect();
        }

        selectedPiece = piece;
        selectedPiece.Select();
    }

    public void grid_SetTransform(Transform piece, int x, int y)
    {
        grid[x, y] = piece;
    }

    public Transform grid_GetTransform(int x, int y)
    {
        return grid[x, y];
    }
}
