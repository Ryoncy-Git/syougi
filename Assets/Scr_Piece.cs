using UnityEngine;

public class Scr_Piece : MonoBehaviour
{

    //transform.position - (x, y, -1);
    bool isSelected = false;
    Scr_GameManager gameManager;
    SpriteRenderer sr;
    private bool[,] highlightGrid = new bool[9, 9];
    bool is1PPiece = true;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    void Init()
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
        Show_path();
        // movement();
    }

    public void Deselect()
    {
        isSelected = false;
        // ハイライト解除など
        sr.color = Color.white;
        gameManager.Hide_highlightGrid();
    }

    void Show_path()
    {
        //showing path
        // switch(pieceKind)
        // {
        // case hu:
        // ひとつ前をハイライト表示
            
        // hu ----------------------------------------
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        gameManager.Show_highlightGrid(roundX, roundY + 1);
            // break;
            // }
    }

    public void Movement(int x, int y)
    {
        // x, yは行先の座標
        transform.position = new Vector3(x, y, -1);

        if (gameManager.Grid_getGameObject(x, y) != null)//cacth
        {
            // 持ち駒 = gameManager.grid_getGameObject(x, y);
        }
        gameManager.Grid_setGameObject(this.gameObject, x, y);

        gameManager.Hide_highlightGrid();
        gameManager.DeselectPiece();
    }
}
