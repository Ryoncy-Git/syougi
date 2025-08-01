using UnityEngine;

public class DestGridManager : MonoBehaviour
{
    public GameObject prefab_destGrid;
    public PieceFactory pieceFactory;
    public GameManager gameManager;
    private GameObject[,] destGrid = new GameObject[9, 9];
    private int destGridOffSet = -3;

    void Start()
    {
        init_destGrid();
    }
    public void init_destGrid()
    {
        // initialize of destGrid
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                GameObject generated =
                Instantiate(prefab_destGrid, new Vector3(x, y, destGridOffSet),
                            Quaternion.identity, GameObject.Find("Obj_destGridManager").transform);

                destGrid[x, y] = generated;
                generated.SetActive(false);
            }
        }
    }

    public void Hide_destGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (destGrid[i, j] == null)
                    continue;

                destGrid[i, j].SetActive(false);
            }
        }
    }

    public void Show_destGrid(int roundX, int roundY)
    {
        if (roundX >= 0 && roundX < 9 && roundY >= 0 && roundY < 9 && destGrid[roundX, roundY] != null)
        {
            destGrid[roundX, roundY].SetActive(true);
        }
    }

    public void ShowGridIfNotNihu()
    {
        GameObject will = pieceFactory.Get_piece_willPut();
        if (will == null)
        {
            Debug.LogError("No piece selected to put.");
            return;
        }
        if (will.GetComponent<Piece>() == null)
        {
            Debug.LogError("Selected piece does not have Piece component.");
            return;
        }

        if (will.GetComponent<Piece>().Get_PieceType() == PieceType.Hu) // 2歩チェック
        {
            for (int i = 0; i < 9; i++)
            {
                bool isExistHu = false;
                for (int j = 0; j < 9; j++)
                {
                    GameObject Grid = gameManager.Get_GridGameObject(i, j);
                    if (Grid == null)
                        continue;

                    Piece targetPiece = gameManager.Get_GridGameObject(i, j).GetComponent<Piece>();
                    if (targetPiece.Get_PieceType() == PieceType.Hu && (gameManager.Get_is1PTurn() == targetPiece.Get_is1PPiece()) && !targetPiece.Get_isNari())
                    {
                        isExistHu = true;
                        break;
                    }
                }

                if (!isExistHu)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if(gameManager.Get_GridGameObject(i, j) == null)
                            Show_destGrid(i, j);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (gameManager.Get_GridGameObject(i, j) == null)
                        Show_destGrid(i, j);
                }
            }
        }

        gameManager.Set_isSpawnTurn(true);
    }
}
