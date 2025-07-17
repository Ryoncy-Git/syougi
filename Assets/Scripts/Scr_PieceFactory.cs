using UnityEngine;

public class Scr_PieceFactory : MonoBehaviour
{
    public GameObject Obj_Koma;
    public GameObject[] pieces;
    GameObject piece_willPut;
    public Scr_highlightGrid scr_highlightGrid;
    public void Put_initPiece()
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
    public void PlacePiece(GameObject prefab, int x, int y, bool is1P)
    {
        Quaternion rotation = is1P
        ? Quaternion.identity
        : Quaternion.Euler(0, 0, 180);

        if (prefab == null) return;
        Instantiate(prefab, new Vector3(x, y, -1), rotation, Obj_Koma.transform)
            .GetComponent<Scr_Piece>().Set_is1PPiece(is1P);
    }

    // 左右対称に2つ配置（左と右）
    void PlaceMirrorPair(GameObject prefab, int offsetX, int y, bool is1PTurnBottom = true)
    {
        PlacePiece(prefab, offsetX, y, is1PTurnBottom);
        PlacePiece(prefab, 8 - offsetX, y, is1PTurnBottom);
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

    public void Spawn_Piece(int x, int y, bool is1P)
    {
        Quaternion rotation = is1P
        ? Quaternion.identity
        : Quaternion.Euler(0, 0, 180);

        if (piece_willPut == null) return;
        Instantiate(piece_willPut, new Vector3(x, y, -1), rotation, Obj_Koma.transform)
            .GetComponent<Scr_Piece>().Set_is1PPiece(is1P);
    }

    public GameObject Get_piece_willPut()
    {
        return piece_willPut;
    }
}
