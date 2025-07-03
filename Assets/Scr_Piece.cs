using UnityEngine;

public class Scr_Piece : MonoBehaviour
{

    //transform.position - (x, y, -1);
    private enum PieceType
    {
        Hu,
        Kyosya,
        Keima,
        Gin,
        Kin,
        Ou,
        Kaku,
        Hisya,
        None
    };
    PieceType pieceType;
    private bool is1PPiece = true;
    // bool isSelected = false;
    Scr_GameManager gameManager;
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

        InitPiece();
    }

    void InitPiece()
    {
        

        pieceType = PieceType.None;

        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

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
        // isSelected = true;
        Debug.Log("選択しました");
        // ハイライト表示などがあればここで
        sr.color = Color.gray;
        Show_path();
    }

    public void Deselect()
    {
        // isSelected = false;
        // ハイライト解除など
        sr.color = Color.white;
        gameManager.Hide_highlightGrid();
    }

    void Show_path()
    {
        //showing path

        // Show_highlightGrid(x, y) は「マスが空 or 敵なら表示」という前提です。
        // → 必要なら Get_GridGameObject(x, y) で駒の存在をチェックして、途中でループ break させる処理も追加できます。

        // すべて 1P側の動きです。2Pの場合は roundY ± n の符号を反転する必要があります。



        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        switch (pieceType)
        {
            case PieceType.Hu:
                gameManager.Show_highlightGrid(roundX, roundY + 1);
                break;

            case PieceType.Kyosya:
                int stopNum = (9 - 1) - roundY;
                for (int i = 1; i + roundY < 9; i++)
                {   
                    GameObject target = gameManager.Get_GridGameObject(roundX, roundY + i);
                    if (target != null)
                    {
                        // Debug.Log("target != null");
                        Scr_Piece targetPiece = target.GetComponent<Scr_Piece>();
                        if (targetPiece != null)
                        {
                            if (targetPiece.Get_is1PPiece())
                            {
                                stopNum = i - 1;
                            }
                            else
                            {
                                stopNum = i;
                            }
                            break;
                        }
                    } 
                }

                for (int i = 1; i <= stopNum; i++)
                {
                    gameManager.Show_highlightGrid(roundX, roundY + i);
                }          
                break;

            case PieceType.Keima:
                if (roundY + 2 < 9)
                {
                    if (roundX - 1 >= 0)
                        gameManager.Show_highlightGrid(roundX - 1, roundY + 2);
                    if (roundX + 1 < 9)
                        gameManager.Show_highlightGrid(roundX + 1, roundY + 2);
                }
                break;

            case PieceType.Gin:
                gameManager.Show_highlightGrid(roundX, roundY + 1);      // 前
                if (roundX > 0)
                {
                    gameManager.Show_highlightGrid(roundX - 1, roundY + 1); // 左前
                    gameManager.Show_highlightGrid(roundX - 1, roundY - 1); // 左後
                }
                if (roundX < 8)
                {
                    gameManager.Show_highlightGrid(roundX + 1, roundY + 1); // 右前
                    gameManager.Show_highlightGrid(roundX + 1, roundY - 1); // 右後
                }
                break;

            case PieceType.Kin:
                gameManager.Show_highlightGrid(roundX, roundY + 1); // 前
                gameManager.Show_highlightGrid(roundX, roundY - 1); // 後
                if (roundX > 0)
                {
                    gameManager.Show_highlightGrid(roundX - 1, roundY);     // 左
                    gameManager.Show_highlightGrid(roundX - 1, roundY + 1); // 左前
                }
                if (roundX < 8)
                {
                    gameManager.Show_highlightGrid(roundX + 1, roundY);     // 右
                    gameManager.Show_highlightGrid(roundX + 1, roundY + 1); // 右前
                }
                break;

            case PieceType.Ou:
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        int nx = roundX + dx;
                        int ny = roundY + dy;

                        if (nx >= 0 && nx < 9 && ny >= 0 && ny < 9)
                        {
                            gameManager.Show_highlightGrid(nx, ny);
                        }
                    }
                }
                break;

            case PieceType.Kaku:
                for (int i = 1; i < 9; i++)
                {
                    // 左上
                    if (roundX - i >= 0 && roundY + i < 9)
                        gameManager.Show_highlightGrid(roundX - i, roundY + i);
                    // 右上
                    if (roundX + i < 9 && roundY + i < 9)
                        gameManager.Show_highlightGrid(roundX + i, roundY + i);
                    // 左下
                    if (roundX - i >= 0 && roundY - i >= 0)
                        gameManager.Show_highlightGrid(roundX - i, roundY - i);
                    // 右下
                    if (roundX + i < 9 && roundY - i >= 0)
                        gameManager.Show_highlightGrid(roundX + i, roundY - i);
                }
                break;


            case PieceType.Hisya:
                for (int i = 1; i < 9; i++)
                {
                    // 上
                    if (roundY + i < 9)
                        gameManager.Show_highlightGrid(roundX, roundY + i);
                    // 下
                    if (roundY - i >= 0)
                        gameManager.Show_highlightGrid(roundX, roundY - i);
                    // 左
                    if (roundX - i >= 0)
                        gameManager.Show_highlightGrid(roundX - i, roundY);
                    // 右
                    if (roundX + i < 9)
                        gameManager.Show_highlightGrid(roundX + i, roundY);
                }
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

        if (gameManager.Get_GridGameObject(x, y) != null)//cacth
        {
            // 持ち駒 = gameManager.Get_GridGameObject(x, y);
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
}
