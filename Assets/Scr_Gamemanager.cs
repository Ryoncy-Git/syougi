using UnityEngine;

public class Scr_Gamemanager : MonoBehaviour
{
    public static Scr_Gamemanager Instance;

    private Class_Piece selectedPiece;

    void Awake()
    {
        Instance = this;
    }

    public void SelectPiece(Class_Piece piece)
    {
        if (selectedPiece != null)
        {
            selectedPiece.Deselect();
        }

        selectedPiece = piece;
        selectedPiece.Select();
    }
}
