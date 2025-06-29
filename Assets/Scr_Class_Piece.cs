using UnityEngine;

public class Scr_Class_Piece : MonoBehaviour
{
    //全駒で共通の特徴を書く
    public bool isSelected = false;
    Scr_GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Obj_GameManager").GetComponent<Scr_GameManager>();
    }
    public void OnMouseDown()
    {
        gameManager.SelectPiece(this);
    }

    public void Select()
    {
        isSelected = true;
        Debug.Log("選択しました");
        // ハイライト表示などがあればここで
        // show_path()
        // if(path != null)
        // {
            // transform.position = (行先)
        // }
    }

    public void Deselect()
    {
        isSelected = false;
        // ハイライト解除など
    }
}
