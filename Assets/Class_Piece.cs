using UnityEngine;

public class Class_Piece : MonoBehaviour
{
    public bool isSelected = false;
    private void OnMouseDown()
    {
        Scr_Gamemanager.Instance.SelectPiece(this);
    }

    public void Select()
    {
        isSelected = true;
        Debug.Log("選択しました");
        // ハイライト表示などがあればここで
    }

    public void Deselect()
    {
        isSelected = false;
        // ハイライト解除など
    }
}
