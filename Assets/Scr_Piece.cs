using UnityEngine;

public class Scr_Piece : MonoBehaviour
{

    //transform.position - (x, y, -1);
    PieceType pieceType;
    public bool is1PPiece = true;
    // bool isSelected = false;
    Scr_GameManager gameManager;
    Scr_PieceMovement pieceMovement;
    SpriteRenderer sr;



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

        // インスペクターから設定でもいいかも
        pieceType = PieceType.None;

        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        if (y < 4)
        {
            is1PPiece = true;
        }
        else
        {
            is1PPiece = false;
        }

        gameManager.Set_GridGameObject(transform.gameObject, x, y);

        if (y == 2 || y == 6)
        {
            pieceType = PieceType.Hu;
        }
        else if (y == 1 || y == 7)
        {
            if (x == y)
            {
                pieceType = PieceType.Kaku;
            }
            else
            {
                pieceType = PieceType.Hisya;
            }
        }
        else if (y == 0 || y == 8)
        {
            switch (x)
            {
                case 0:
                case 8:
                    pieceType = PieceType.Kyosya;
                    break;

                case 1:
                case 7:
                    pieceType = PieceType.Keima;
                    break;

                case 2:
                case 6:
                    pieceType = PieceType.Gin;
                    break;

                case 3:
                case 5:
                    pieceType = PieceType.Kin;
                    break;

                case 4:
                    pieceType = PieceType.Ou;
                    break;

                default:
                    break;
            }
        }
    }
    public void OnMouseDown()
    {
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
        gameManager.Hide_highlightGrid();
    }

    void Show_path()
    {
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);

        // gamemanagerのほうで範囲外のものははじくようにできているから、
        // show_highlightGridでこっちでは盤面内かどうかを判定する必要はない
        // ただし、駒があるかどうかは判定しないためそこは見る必要がある
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
                pieceMovement.Show_path_Kaku(roundX, roundY, is1PPiece);
                break;

            case PieceType.Hisya:
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

        transform.position = new Vector3(x, y, -1);
        GameObject targetObject = gameManager.Get_GridGameObject(x, y);
        if (targetObject != null)//cacth
        {
            // 持ち駒 = gameManager.Get_GridGameObject(x, y);
            gameManager.Capture_piece(targetObject, is1PPiece);
        }

        gameManager.Set_GridGameObject(null, prevX, prevY);
        gameManager.Set_GridGameObject(this.gameObject, x, y);

        gameManager.Hide_highlightGrid();
        gameManager.DeselectPiece();
    }

    public bool Get_is1PPiece()
    {
        return is1PPiece;
    }

    public PieceType Get_PieceType()
    {
        return pieceType;
    }
}
