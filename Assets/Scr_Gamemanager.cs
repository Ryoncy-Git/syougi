using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    private Scr_Piece selectedPiece;
    private GameObject[,] grid = new GameObject[9, 9];
    private GameObject[,] highlightGrid = new GameObject[9, 9];
    bool is1PTurn = true;

    void Start()
    {
        init();
    }

    void init()
    {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("Tag_highlightGrid");

        int index = 0;
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (index < foundObjects.Length)
                {
                    highlightGrid[x, y] = foundObjects[index];
                    index++;

                    highlightGrid[x, y].transform.position = new Vector3(x, y, -2);
                    highlightGrid[x, y].transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    highlightGrid[x, y].SetActive(false);
                }
            }
        }
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
    public void grid_setGameObject(GameObject piece, int x, int y)
    {
        grid[x, y] = piece;
    }

    public GameObject grid_getGameObject(int x, int y)
    {
        return grid[x, y];
    }

    public void change_turn()
    {
        is1PTurn = !is1PTurn;
    }

    public void show_highlightGrid(int roundX, int roundY)
    {
        if (highlightGrid[roundX, roundY] != null)
        {
            highlightGrid[roundX, roundY].SetActive(true);
        }
    }

    public void hide_highlightGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                highlightGrid[i, j].SetActive(false);
            }
        }
    }
}
