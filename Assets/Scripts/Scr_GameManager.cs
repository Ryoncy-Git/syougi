using UnityEngine;
using System.Collections.Generic;

public class Scr_GameManager : MonoBehaviour
{
    // instances
    public GameObject prefab_highlightGrid;
    public Scr_UI Scr_ui;
    public GameObject Koma;

    private Scr_Piece selectedPiece;

    // variants
    private GameObject[,] grid = new GameObject[9, 9];
    private GameObject[,] highlightGrid = new GameObject[9, 9];
    bool is1PTurn = true;
    Dictionary<PieceType, int> capturedPieces_1P = new Dictionary<PieceType, int>();
    Dictionary<PieceType, int> capturedPieces_2P = new Dictionary<PieceType, int>();
    int NaruX, NaruY;
    bool isSpawnTurn = false;
    bool isNariUIActive = false;
    public GameObject[] pieces;
    GameObject piece_willPut;


    void Start()
    {
        Init();

        Scr_ui.Show_capturedPiece(capturedPieces_1P, capturedPieces_2P);

    }

    void Init()
    {
        // initialize of highlightGrid
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                GameObject generated =
                Instantiate(prefab_highlightGrid, new Vector3(x, y, -2),
                            Quaternion.identity, GameObject.Find("Obj_highlightGrid").transform);

                highlightGrid[x, y] = generated;
                generated.SetActive(false);
            }
        }


        // initialize of capturedPieces     
        foreach (PieceType type in System.Enum.GetValues(typeof(PieceType)))
        {
            capturedPieces_1P[type] = 0;
            capturedPieces_2P[type] = 0;
        }

        isSpawnTurn = false;

        Put_initPiece();
    }

    // --------------------------------------------------
    void Put_initPiece()
    {
        // Hu（歩）: 横一列ずつ配置
        GameObject hu = GetPrefabByPieceType(PieceType.Hu);
        for (int i = 0; i < 9; i++)
        {
            PlacePiece(hu, i, 2, true);
            PlacePiece(hu, i, 6, false);
        }

        // Kyosya（香車）
        GameObject kyosya = GetPrefabByPieceType(PieceType.Kyosya);
        PlaceMirrorPair(kyosya, 0, 0);
        PlaceMirrorPair(kyosya, 0, 8, false);

        // Keima（桂馬）
        GameObject keima = GetPrefabByPieceType(PieceType.Keima);
        PlaceMirrorPair(keima, 1, 0);
        PlaceMirrorPair(keima, 1, 8, false);

        // Gin（銀）
        GameObject gin = GetPrefabByPieceType(PieceType.Gin);
        PlaceMirrorPair(gin, 2, 0);
        PlaceMirrorPair(gin, 2, 8, false);

        // Kin（金）
        GameObject kin = GetPrefabByPieceType(PieceType.Kin);
        PlaceMirrorPair(kin, 3, 0);
        PlaceMirrorPair(kin, 3, 8, false);

        // Kaku（角）
        GameObject kaku = GetPrefabByPieceType(PieceType.Kaku);
        PlacePiece(kaku, 1, 1, true);
        PlacePiece(kaku, 7, 7, false);

        // Hisya（飛車）
        GameObject hisya = GetPrefabByPieceType(PieceType.Hisya);
        PlacePiece(hisya, 7, 1, true);
        PlacePiece(hisya, 1, 7, false);

        // Ou（王）
        GameObject ou = GetPrefabByPieceType(PieceType.Ou);
        PlacePiece(ou, 4, 0, true);
        PlacePiece(ou, 4, 8, false);
    }

    // 指定のPieceTypeのPrefabを取得
    GameObject GetPrefabByPieceType(PieceType type)
    {
        foreach (GameObject p in pieces)
        {
            Scr_Piece scrPiece = p.GetComponent<Scr_Piece>();
            if (scrPiece != null && scrPiece.Get_PieceType() == type)
            {
                return p;
            }
        }
        Debug.LogError($"PieceType {type} が pieces に見つかりませんでした！");
        return null;
    }

    // 駒を1つだけ配置
    void PlacePiece(GameObject prefab, int x, int y, bool is1P)
    {
        if (prefab == null) return;
        Instantiate(prefab, new Vector3(x, y, -1), Quaternion.identity, Koma.transform)
            .GetComponent<Scr_Piece>().Set_is1PPiece(is1P);
    }

    // 左右対称に2つ配置（左と右）
    void PlaceMirrorPair(GameObject prefab, int offsetX, int y, bool is1PTurnBottom = true)
    {
        PlacePiece(prefab, offsetX, y, is1PTurnBottom);
        PlacePiece(prefab, 8 - offsetX, y, is1PTurnBottom);
    }

    // --------------------------------------------------------

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

    public void Change_turn()
    {
        is1PTurn = !is1PTurn;
    }

    public void Show_highlightGrid(int roundX, int roundY)
    {
        if (roundX >= 0 && roundX < 9 && roundY >= 0 && roundY < 9 && highlightGrid[roundX, roundY] != null)
        {
            highlightGrid[roundX, roundY].SetActive(true);
        }
    }

    public void Hide_highlightGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                highlightGrid[i, j].SetActive(false);
            }
        }
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
    }

    void HandleSpawnTurn(int x, int y)
    {
        // 駒の生成
        GameObject inst =
        Instantiate(piece_willPut, new Vector3(x, y, -1), Quaternion.identity, Koma.transform);
        Set_GridGameObject(inst, x, y);
        inst.GetComponent<Scr_Piece>().Set_is1PPiece(is1PTurn);

        // 対応する持ち駒を一つ減らす
        PieceType pieceType =
        piece_willPut.GetComponent<Scr_Piece>().Get_PieceType();
        if (is1PTurn)
        {
            capturedPieces_1P[pieceType] -= 1;
        }
        else
        {
            capturedPieces_2P[pieceType] -= 1;
        }

        // ターンの変更、グリッドの非表示など
        isSpawnTurn = false;
        Hide_highlightGrid();
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

    public void Capture_piece(GameObject piece, bool is1P) // 1Pが捕まえたのか
    {
        // piece は捕まえられたピース
        // is1Pは捕まえる側がどっちなのかを表す
        Scr_Piece targetPiece = piece.GetComponent<Scr_Piece>();
        if (targetPiece == null) return;

        PieceType targetPieceType = targetPiece.Get_PieceType();

        var dict = is1P ? capturedPieces_1P : capturedPieces_2P;
        if (dict.ContainsKey(targetPieceType))
            dict[targetPieceType]++;
        else
            dict[targetPieceType] = 1;
        

        if (piece != null)
        {
            Destroy(piece);
        }

        Scr_ui.Show_capturedPiece(capturedPieces_1P, capturedPieces_2P);
    }

    public void Set_isSpawnTurn(bool state)
    {
        isSpawnTurn = state;
    }
    public bool Get_isSpawnTurn()
    {
        return isSpawnTurn;
    }
    public void Set_piece_willPut(PieceType pieceType)
    {
        foreach (GameObject p in pieces)
        {
            Scr_Piece scrPiece = p.GetComponent<Scr_Piece>();
            if (scrPiece != null && scrPiece.Get_PieceType() == pieceType)
            {
                piece_willPut = p;
                break; // 最初に見つかった1つで十分なら break でループを抜ける
            }
        }
    }

    public Dictionary<PieceType, int> Get_CapturedPiece1P()
    {
        return capturedPieces_1P;
    }

    public Dictionary<PieceType, int> Get_CapturedPiece2P()
    {
        return capturedPieces_2P;
    }
}
