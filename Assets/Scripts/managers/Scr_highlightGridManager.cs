using UnityEngine;

public class Scr_highlightGridManager : MonoBehaviour
{
    public GameObject prefab_highlightGrid;
    public Scr_PieceFactory pieceFactory;
    public Scr_GameManager gameManager;
    private GameObject[,] highlightGrid = new GameObject[9, 9];
    private int highlightGridOffSet = -2;

    void Start()
    {
        init_highlightGrid();
    }
    public void init_highlightGrid()
    {
        // initialize of highlightGrid
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                GameObject generated =
                Instantiate(prefab_highlightGrid, new Vector3(x, y, highlightGridOffSet),
                            Quaternion.identity, GameObject.Find("Obj_highlightGridManager").transform);

                highlightGrid[x, y] = generated;
                generated.SetActive(false);
            }
        }
    }

    public void Hide_highlightGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (highlightGrid[i, j] == null)
                    continue;

                highlightGrid[i, j].SetActive(false);
            }
        }
    }

    public void Show_highlightGrid(int roundX, int roundY)
    {
        if (roundX >= 0 && roundX < 9 && roundY >= 0 && roundY < 9 && highlightGrid[roundX, roundY] != null)
        {
            highlightGrid[roundX, roundY].SetActive(true);
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
        if (will.GetComponent<Scr_Piece>() == null)
        {
            Debug.LogError("Selected piece does not have Scr_Piece component.");
            return;
        }

        if (will.GetComponent<Scr_Piece>().Get_PieceType() == PieceType.Hu) // 2歩チェック
        {
            for (int i = 0; i < 9; i++)
            {
                bool isExistHu = false;
                for (int j = 0; j < 9; j++)
                {
                    GameObject Grid = gameManager.Get_GridGameObject(i, j);
                    if (Grid == null)
                        continue;

                    if (gameManager.Get_GridGameObject(i, j).GetComponent<Scr_Piece>().Get_PieceType() == PieceType.Hu)
                    {
                        isExistHu = true;
                        break;
                    }
                }

                if (!isExistHu)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Show_highlightGrid(i, j);
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
                        Show_highlightGrid(i, j);
                }
            }
        }

        gameManager.Set_isSpawnTurn(true);
    }
}
