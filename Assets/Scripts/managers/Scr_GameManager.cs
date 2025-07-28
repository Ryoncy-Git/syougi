using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    // instances
    
    public Scr_UI Scr_ui;
    public GameObject Koma;
    public Scr_PieceFactory pieceFactory;

    private Scr_Piece selectedPiece;
    public Scr_highlightGridManager scr_highlightGridManager;
    public Scr_CaptureManager captureManager;

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

    public void SelectPiece(Scr_Piece piece)
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
        selectedPiece.Deselect();
        selectedPiece = null;
    }
    public void Set_GridGameObject(GameObject piece, int x, int y)
    {
        if (x >= 0 && x < 9 && y >= 0 && y < 9)
        {
            Debug.Log($"Grid[{x},{y}] = {(grid[x, y] ? grid[x, y].name : "null")}");
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
    }

    public void Click_highlightGrid(int x, int y)
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

        if (canNari && !selectedPiece.Get_isNari())
        {
            NaruX = x;
            NaruY = y;
            // 入力をUI以外無効化
            isNariUIActive = true;
            // なる画面のUIを表示
            Scr_ui.Show_NariSelect();
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
        
        Quaternion rotation = is1PTurn
        ? Quaternion.identity
        : Quaternion.Euler(0, 0, 180);


        // 対応する持ち駒を一つ減らす
        GameObject piece_willPut = pieceFactory.Get_piece_willPut();
        PieceType pieceType =
        piece_willPut.GetComponent<Scr_Piece>().Get_PieceType();


        // 駒の生成
        pieceFactory.Spawn_Piece(x, y, is1PTurn);
        captureManager.Add_DictPiece(is1PTurn, pieceType, -1);

        // ターンの変更、グリッドの非表示など
        isSpawnTurn = false;
        scr_highlightGridManager.Hide_highlightGrid();
        Change_turn();
    }

    public void Click_Naru()
    {
        selectedPiece.Set_Nari(true);
        if (selectedPiece != null)
        {
            // movementをclick highlightGridに描かないのは
            // UIをくりっくしてから移動をしたいから
            selectedPiece.Movement(NaruX, NaruY);
        }
        Scr_ui.Hide_NariSelect();
        isNariUIActive = false;
    }

    public void Click_Naranai()
    {
        if (selectedPiece != null)
        {
            selectedPiece.Movement(NaruX, NaruY);
        }
        Scr_ui.Hide_NariSelect();
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
}
