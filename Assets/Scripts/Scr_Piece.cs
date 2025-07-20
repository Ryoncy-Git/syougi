using UnityEngine;

public class Scr_Piece : MonoBehaviour
{
    //transform.position - (x, y, -1);
    [SerializeField] PieceType pieceType;
    public bool is1PPiece = true;
    public bool isNari = false;
    // bool isSelected = false;
    private Scr_GameManager gameManager;
    private Scr_PieceMovement pieceMovement;
    private Scr_highlightGrid scr_highlightGrid;
    private Scr_CaptureManager captureManager;
    SpriteRenderer sr;

    void Start()
    {
        Init();
    }

    void Init()
    {
        gameManager = GameObject.Find("Obj_GameManager").GetComponent<Scr_GameManager>();
        pieceMovement = GameObject.Find("Obj_PieceMovement").GetComponent<Scr_PieceMovement>();
        scr_highlightGrid = GameObject.Find("Obj_highlightGrid").GetComponent<Scr_highlightGrid>();
        captureManager = GameObject.Find("Obj_CaptureManager").GetComponent<Scr_CaptureManager>();

        if (gameManager == null || pieceMovement == null || scr_highlightGrid == null || captureManager == null)
            Debug.Log("Null!!!!!");

        sr = GetComponent<SpriteRenderer>();

        InitPiece();
    }

    void Update()
    {
        if (gameManager == null || pieceMovement == null || scr_highlightGrid == null || captureManager == null)
            Debug.Log("Null!!!!!");
    }

    void InitPiece()
    {
        
    }
    public void OnMouseDown()
    {
        if (gameManager.Get_is1PTurn() == is1PPiece)
            gameManager.SelectPiece(this);
    }

    public void Select()
    {
        sr.color = Color.gray;
        Show_path();
    }

    public void Deselect()
    {
        sr.color = Color.white;
        scr_highlightGrid.Hide_highlightGrid();
    }

    void Show_path()
    {
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);

        // gamemanagerのほうで範囲外のものははじくようにできているから、
        // show_highlightGrid でこっちでは盤面内かどうかを判定する必要はない
        // ただし、駒があるかどうかは判定しないためそこは見る必要がある
        bool isKinLike = isNari && 
        (pieceType == PieceType.Hu || pieceType == PieceType.Kyosya || 
        pieceType == PieceType.Keima || pieceType == PieceType.Gin);

        if (isKinLike || pieceType == PieceType.Kin)
        {
            pieceMovement.Show_path_Kin(roundX, roundY, is1PPiece);
            return;
        }

        switch (pieceType)
        {
            case PieceType.Hu:
                pieceMovement.Show_path_Hu(roundX, roundY, is1PPiece);
                break;

            case PieceType.Kyosya:
                pieceMovement.Show_path_Kyosya(roundX, roundY, is1PPiece);
                break;

            case PieceType.Keima:
                pieceMovement.Show_path_Keima(roundX, roundY, is1PPiece);
                break;

            case PieceType.Gin:
                pieceMovement.Show_path_Gin(roundX, roundY, is1PPiece);
                break;

            case PieceType.Kin:
                pieceMovement.Show_path_Kin(roundX, roundY, is1PPiece);
                break;

            case PieceType.Ou:
                pieceMovement.Show_path_Ou(roundX, roundY, is1PPiece);
                break;

            case PieceType.Kaku:
                if (isNari)
                    pieceMovement.Show_path_Ou(roundX, roundY, is1PPiece);

                pieceMovement.Show_path_Kaku(roundX, roundY, is1PPiece);
                break;

            case PieceType.Hisya:
                if (isNari)
                    pieceMovement.Show_path_Ou(roundX, roundY, is1PPiece);

                pieceMovement.Show_path_Hisya(roundX, roundY, is1PPiece);
                break;

            default:
                break;
        }
    }

    public void Movement(int x, int y)
    {
        // x, y は行先の座標
        int prevX = Mathf.RoundToInt(transform.position.x);
        int prevY = Mathf.RoundToInt(transform.position.y);

        
        GameObject targetObject = gameManager.Get_GridGameObject(x, y);
        if (targetObject != null)//cacth
        {
            // 持ち駒 = gameManager.Get_GridGameObject(x, y);
            captureManager.Capture_piece(targetObject, is1PPiece);
        }

        transform.position = new Vector3(x, y, -1);
        gameManager.Set_GridGameObject(null, prevX, prevY);
        gameManager.Set_GridGameObject(this.gameObject, x, y);

        scr_highlightGrid.Hide_highlightGrid();
        gameManager.DeselectPiece();

        gameManager.Change_turn();
    }

    public void Set_is1PPiece(bool state)
    {
        is1PPiece = state;
    }

    public bool Get_is1PPiece()
    {
        return is1PPiece;
    }

    public PieceType Get_PieceType()
    {
        return pieceType;
    }

    public void Set_Nari(bool state)
    {
        isNari = state;
    }

    public bool Get_isNari()
    {
        return isNari;
    }
}
