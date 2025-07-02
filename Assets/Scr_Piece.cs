using UnityEngine;

public class Scr_Piece : MonoBehaviour
{
    bool isSelected = false;
    Scr_GameManager gameManager;
    SpriteRenderer sr;
    private bool[,] highlightGrid = new bool[9, 9];
    bool is1PPiece = true;

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
        sr = GetComponent<SpriteRenderer>();
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
        sr.color = Color.gray;
        show_path();
        // movement();
    }

    public void Deselect()
    {
        isSelected = false;
        // ハイライト解除など
        sr.color = Color.white;
        gameManager.hide_highlightGrid();
    }

    void show_path()
    {
        //showing path
        // switch(pieceKind)
        // {
        // case hu:
        // ひとつ前をハイライト表示
            
        // hu ----------------------------------------
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        gameManager.show_highlightGrid(roundX, roundY + 1);
            // break;
            // }
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
