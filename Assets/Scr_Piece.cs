using UnityEngine;

public class Scr_Piece : MonoBehaviour
{
    //全駒で共通の特徴を書く
    bool isSelected = false;
    Scr_GameManager gameManager;

    void Start()
    {
        init();
    }

    void Update()
    {
        
    }

    void init()
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
        // movement
    }

    public void Deselect()
    {
        isSelected = false;
        // ハイライト解除など
    }

    void show_path()
    {
        //showing path
    }

    void movement()
    {
        // transform = (行先);
        // if(catch)
        // {
        //     catch(行先の駒)
        // }
    }
}
