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
        gameManager.show_highlightGrid(roundX, roundY);
        // show_hilightGrid();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (highlightGrid[i, j])
                {
                    Debug.Log(i + ", " + j);
                }
            }
        }
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
