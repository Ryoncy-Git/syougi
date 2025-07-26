using UnityEngine;

public class Scr_highlightGridManager : MonoBehaviour
{
    public GameObject prefab_highlightGrid;
    public Scr_GameManager gameManager;
    private GameObject[,] highlightGrid = new GameObject[9, 9];

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
                Instantiate(prefab_highlightGrid, new Vector3(x, y, -2),
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
}
