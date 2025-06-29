using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    private Scr_Class_Piece selectedPiece;

    public void SelectPiece(Scr_Class_Piece piece)
    {
        if (selectedPiece != null)
        {
            selectedPiece.Deselect();
        }

        selectedPiece = piece;
        selectedPiece.Select();
    }
}
