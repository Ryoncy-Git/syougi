using UnityEngine;

public class Scr_CapturedPiece : MonoBehaviour
{
    public GameObject[] pieces;
    public Scr_GameManager gameManager;
    public Scr_UI Scr_ui;
    GameObject piece_willPut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn_piece(int x, int y)
    {
        Instantiate(piece_willPut, new Vector3(x, y, -1), Quaternion.identity, this.transform);
    }
    void Show_Grid_and_Hide_UI()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (gameManager.Get_GridGameObject(i, j) == null)
                    gameManager.Show_highlightGrid(i, j);
            }
        }

        Scr_ui.Hide_selectPutPiece();
        gameManager.Set_isSpawnTurn(true);
    }

    public void Click_Hu()
    {
        foreach (GameObject p in pieces)
        {
            if (p.name == "Hu")
            {
                piece_willPut = p;
                break;
            }
        }

        Show_Grid_and_Hide_UI();
    }

    public void Click_Kyosya()
    {

    }
}
