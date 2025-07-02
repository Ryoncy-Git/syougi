using UnityEngine;

public class Scr_GameManager : MonoBehaviour
{
    private Scr_Piece selectedPiece;
    private GameObject[,] grid = new GameObject[9, 9];
    private GameObject[,] highlightGrid = new GameObject[9, 9];
    bool is1PTurn = true;

    void Start()
    {
        Init();
    }

    void Init()
    {
        // initialize of highlightGrid
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
        if (selectedPiece != null)
        {
            selectedPiece.Movement(x, y);
        }
        else
        {
            Debug.Log("selected Piece == null!");
        }
    }

    bool ValidMovement()
    {
        // もしかしたらいらないかも
        // っていうのもhighlightGridの表示してる範囲しかそもそもクリックできないから、
        //　sshow_highlightGridで光らせるところを制限すればいらないかも


        // if (grid[行先] == myPiece || (0 <= 行先.x < 9 && 0 <= 行先.y < 9))
        // {
        //     return false;
        // }

        // return true;
        return true;
    }
}
