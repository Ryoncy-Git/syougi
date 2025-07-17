using UnityEngine;

public class Scr_Piece : MonoBehaviour
{
    //transform.position - (x, y, -1);
    [SerializeField] PieceType pieceType;
    public bool is1PPiece = true;
    public bool isNari = false;
    // bool isSelected = false;
    Scr_GameManager gameManager;
    Scr_PieceMovement pieceMovement;
    SpriteRenderer sr;
    public Scr_highlightGrid scr_highlightGrid;
    public Scr_CaptureManager captureManager;

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
        pieceMovement = new Scr_PieceMovement(gameManager);

        InitPiece();
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
