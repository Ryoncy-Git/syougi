using UnityEngine;
using System.Collections.Generic;

public class Scr_CaptureManager : MonoBehaviour
{
    public Scr_PieceFactory pieceFactory;
    public Scr_UI Scr_ui;
    public Scr_GameManager gameManager;
    Dictionary<PieceType, int> capturedPieces_1P = new Dictionary<PieceType, int>();
    Dictionary<PieceType, int> capturedPieces_2P = new Dictionary<PieceType, int>();

    void Start()
    {
        // initialize of capturedPieces     
        foreach (PieceType type in System.Enum.GetValues(typeof(PieceType)))
        {
            capturedPieces_1P[type] = 0;
            capturedPieces_2P[type] = 0;
        }
    }
    public void Capture_piece(GameObject piece, bool is1P) // 1Pが捕まえたのか
    {
        // piece は捕まえられたピース
        // is1Pは捕まえる側がどっちなのかを表す
        Scr_Piece targetPiece = piece.GetComponent<Scr_Piece>();
        if (targetPiece == null) return;

        PieceType targetPieceType = targetPiece.Get_PieceType();

        if (targetPieceType == PieceType.Ou)
            Scr_ui.Settled(gameManager.Get_is1PTurn());

        var dict = is1P ? capturedPieces_1P : capturedPieces_2P;
        if (dict.ContainsKey(targetPieceType))
            dict[targetPieceType]++;
        else
            dict[targetPieceType] = 1;


        if (piece != null)
        {
            Destroy(piece);
        }

        // Scr_ui.Show_capturedPiece(capturedPieces_1P, capturedPieces_2P);
        Scr_ui.Show_Box();

        // Debug.Log($"キャプチャ対象: {targetPieceType}, 自分のターン: {is1P}, 駒の持ち主: {targetPiece.Get_is1PPiece()}");
    }

    public Dictionary<PieceType, int> Get_CapturedPiece1P()
    {
        return capturedPieces_1P;
    }

    public Dictionary<PieceType, int> Get_CapturedPiece2P()
    {
        return capturedPieces_2P;
    }

    public void Add_DictPiece(bool is1P, PieceType pieceType, int value)
    {
        if (is1P)
        {
            capturedPieces_1P[pieceType] += value;
        }
        else
        {
            capturedPieces_2P[pieceType] += value;
        }   
    }
}
