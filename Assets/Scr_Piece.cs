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
    bool isSelected = false;
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
        isSelected = true;
        Debug.Log("選択しました");
        // ハイライト表示などがあればここで
        sr.color = Color.gray;
        Show_path();
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

            default:
                break;
        }
    }

    public void Movement(int x, int y)
    {
        // x, y は行先の座標
        transform.position = new Vector3(x, y, -1);

        if (gameManager.Get_GridGameObject(x, y) != null)//cacth
        {
            // 持ち駒 = gameManager.Get_GridGameObject(x, y);
        }

        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        gameManager.Set_GridGameObject(null, roundX, roundY);
        gameManager.Set_GridGameObject(this.gameObject, x, y);

        gameManager.Hide_highlightGrid();
        gameManager.DeselectPiece();
    }

    public bool Get_is1PPiece()
    {
        return is1PPiece;
    }
}
