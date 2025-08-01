using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    public  GameManager gameManager;
    public DestGridManager destGridManager;

    public void Show_path_Hu(int x, int y, bool is1P)
    {
        int destX = x;
        int destY = is1P ? y + 1 : y - 1;
        ShowHighlightIfValid(destX, destY, is1P);
    }

    public void Show_path_Kyosya(int x, int y, bool is1P)
    {
        int dir = is1P ? 1 : -1;

        for (int i = 1; i < 9; i++)
        {
            int destY = y + dir * i;
            if (destY < 0 || destY >= 9) break;

            GameObject target = gameManager.Get_GridGameObject(x, destY);
            if (target == null)
            {
                destGridManager.Show_destGrid(x, destY);
            }
            else
            {
                Piece piece = target.GetComponent<Piece>();
                if (piece != null && piece.Get_is1PPiece() != is1P)
                {
                    destGridManager.Show_destGrid(x, destY);
                }
                break; // 味方 or 敵 どちらでもここで止まる
            }
        }
    }

    public void Show_path_Keima(int x, int y, bool is1P)
    {
        int dy = is1P ? 2 : -2;
        ShowHighlightIfValid(x - 1, y + dy, is1P);
        ShowHighlightIfValid(x + 1, y + dy, is1P);
    }

    public void Show_path_Gin(int x, int y, bool is1P)
    {
        int forward = is1P ? 1 : -1;
        ShowHighlightIfValid(x, y + forward, is1P);
        ShowHighlightIfValid(x - 1, y + forward, is1P);
        ShowHighlightIfValid(x + 1, y + forward, is1P);
        ShowHighlightIfValid(x - 1, y - forward, is1P);
        ShowHighlightIfValid(x + 1, y - forward, is1P);
    }

    public void Show_path_Kin(int x, int y, bool is1P)
    {
        int forward = is1P ? 1 : -1;
        ShowHighlightIfValid(x, y + forward, is1P);
        ShowHighlightIfValid(x - 1, y, is1P);
        ShowHighlightIfValid(x + 1, y, is1P);
        ShowHighlightIfValid(x, y - forward, is1P);
        ShowHighlightIfValid(x - 1, y + forward, is1P);
        ShowHighlightIfValid(x + 1, y + forward, is1P);
    }
    public void Show_path_Ou(int x, int y, bool is1P)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                ShowHighlightIfValid(x + dx, y + dy, is1P);
            }
        }
    }

    public void Show_path_Kaku(int x, int y, bool is1P)
    {
        for (int i = 1; i < 9; i++)
        {
            int dx = i, dy = i;
            if (!ShowAndBreakIfBlocked(x - dx, y + dy, is1P)) break;
        }
        for (int i = 1; i < 9; i++)
        {
            if (!ShowAndBreakIfBlocked(x + i, y + i, is1P)) break;
        }
        for (int i = 1; i < 9; i++)
        {
            if (!ShowAndBreakIfBlocked(x - i, y - i, is1P)) break;
        }
        for (int i = 1; i < 9; i++)
        {
            if (!ShowAndBreakIfBlocked(x + i, y - i, is1P)) break;
        }
    }

    public void Show_path_Hisya(int x, int y, bool is1P)
    {
        for (int i = 1; i < 9; i++)
        {
            if (!ShowAndBreakIfBlocked(x, y + i, is1P)) break;
        }
        for (int i = 1; i < 9; i++)
        {
            if (!ShowAndBreakIfBlocked(x, y - i, is1P)) break;
        }
        for (int i = 1; i < 9; i++)
        {
            if (!ShowAndBreakIfBlocked(x - i, y, is1P)) break;
        }
        for (int i = 1; i < 9; i++)
        {
            if (!ShowAndBreakIfBlocked(x + i, y, is1P)) break;
        }
    }

    // 直線・斜めに進む駒用（true: 続行, false: そこで終了）
    private bool ShowAndBreakIfBlocked(int x, int y, bool is1P)
    {
        if (x < 0 || x >= 9 || y < 0 || y >= 9) return false;

        GameObject target = gameManager.Get_GridGameObject(x, y);
        if (target == null)
        {
            destGridManager.Show_destGrid(x, y);
            return true; // 続ける
        }

        Piece piece = target.GetComponent<Piece>();
        if (piece != null && piece.Get_is1PPiece() != is1P)
        {
            destGridManager.Show_destGrid(x, y);
        }

        return false; // どちらにせよここで止まる
    }
    private void ShowHighlightIfValid(int x, int y, bool is1P)
    {
        if (x < 0 || x >= 9 || y < 0 || y >= 9) return;

        GameObject target = gameManager.Get_GridGameObject(x, y);
        if (target == null)
        {
            destGridManager.Show_destGrid(x, y);
        }
        else
        {
            Piece piece = target.GetComponent<Piece>();
            if (piece != null && piece.Get_is1PPiece() != is1P)
            {
                destGridManager.Show_destGrid(x, y);
            }
        }
    }
}

