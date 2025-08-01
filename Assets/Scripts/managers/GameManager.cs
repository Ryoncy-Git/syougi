using UnityEngine;

public class GameManager : MonoBehaviour
{
    // instances

    public UIManager uiManager;
    public GameObject Koma;
    public PieceFactory pieceFactory;

    private Piece selectedPiece;
    public DestGridManager destGridManager;
    public CaptureManager captureManager;

    // variants
    private GameObject[,] grid = new GameObject[9, 9];

    bool is1PTurn = true;
    int NaruX, NaruY;
    bool isSpawnTurn = false;
    bool isNariUIActive = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        isSpawnTurn = false;

        pieceFactory.Put_initPiece();
    }

    public void SelectPiece(Piece piece)
    {
        if (selectedPiece != null)
        {
            selectedPiece.Deselect();
        }
        selectedPiece = piece;
        selectedPiece.Select();
    }

    public void DeselectPiece()
    {
        if( selectedPiece == null)
            return;
            
        selectedPiece.Deselect();
        selectedPiece = null;
    }
    public void Set_GridGameObject(GameObject piece, int x, int y)
    {
        if (x >= 0 && x < 9 && y >= 0 && y < 9)
        {
            // Debug.Log($"Grid[{x},{y}] = {(grid[x, y] ? grid[x, y].name : "null")}");
            grid[x, y] = piece;
        }
    }

    public GameObject Get_GridGameObject(int x, int y)
    {
        if (x >= 0 && x < 9 && y >= 0 && y < 9)
        {
            return grid[x, y];
        }

        return null;
    }

    public bool Get_is1PTurn()
    {
        return is1PTurn;
    }

    void Change_turn()
    {
        is1PTurn = !is1PTurn;
        uiManager.Switching_Box();
    }

    public void Click_destGrid(int x, int y)
    {
        if (isNariUIActive)
            return;

        if (isSpawnTurn)
            HandleSpawnTurn(x, y);
        else
            HandleMoveTurn(x, y);

    }

    void HandleMoveTurn(int x, int y)
    {
        bool canNari = false;
        int prevY = Mathf.RoundToInt(selectedPiece.transform.position.y);
        canNari = (selectedPiece.Get_is1PPiece() && y >= 6) || (selectedPiece.Get_is1PPiece() && prevY >= 6) ||
                (!selectedPiece.Get_is1PPiece() && y <= 2) || (!selectedPiece.Get_is1PPiece() && prevY <= 2);

        if(selectedPiece.Get_PieceType() == PieceType.Ou || selectedPiece.Get_PieceType() == PieceType.Kin) 
        {
            canNari = false; // 王と金は成れない
        }

        if (canNari && !selectedPiece.Get_isNari())
        {
            NaruX = x;
            NaruY = y;
            // 入力をUI以外無効化
            isNariUIActive = true;
            // なる画面のUIを表示
            uiManager.Show_NariSelect();
        }
        else
        {
            if (selectedPiece != null)
            {
                selectedPiece.Movement(x, y);
            }
        }

        Change_turn();
    }

    void HandleSpawnTurn(int x, int y)
    {
        // 対応する持ち駒を一つ減らす
        GameObject piece_willPut = pieceFactory.Get_piece_willPut();
        PieceType pieceType =
        piece_willPut.GetComponent<Piece>().Get_PieceType();


        // 駒の生成
        pieceFactory.Spawn_Piece(x, y, is1PTurn);
        captureManager.Add_DictPiece(is1PTurn, pieceType, -1);

        // ターンの変更、グリッドの非表示など
        isSpawnTurn = false;
        destGridManager.Hide_destGrid();
        Change_turn();
    }

    public void Click_Naru()
    {
        selectedPiece.Set_Nari(true);
        if (selectedPiece != null)
        {
            // movementをclick destGridに描かないのは
            // UIをくりっくしてから移動をしたいから
            selectedPiece.Movement(NaruX, NaruY);
        }
        isNariUIActive = false;
    }

    public void Click_Naranai()
    {
        if (selectedPiece != null)
        {
            selectedPiece.Movement(NaruX, NaruY);
        }
        isNariUIActive = false;
    }

    public void Set_isSpawnTurn(bool state)
    {
        isSpawnTurn = state;
    }
    public bool Get_isSpawnTurn()
    {
        return isSpawnTurn;
    }

    public void Click_sonohen()
    {
        if(selectedPiece != null)
        {
            selectedPiece.Deselect();
        }
    }
}
