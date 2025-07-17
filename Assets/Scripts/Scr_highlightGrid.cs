using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_highlightGrid : MonoBehaviour
{
    public GameObject prefab_highlightGrid;
    public Scr_GameManager gameManager;
    private GameObject[,] highlightGrid = new GameObject[9, 9];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Obj_GameManager").GetComponent<Scr_GameManager>();// インスペクターから直接指定ができないのでこれ
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (gameManager == null)
            return;

        // マウスがUIの上にあるかチェック
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UIの上なのでスキップ");
            return; // UIの上なので処理しない
        }

        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        gameManager.Click_highlightGrid(roundX, roundY);
        // Debug.Log("行先を選択");
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
                            Quaternion.identity, GameObject.Find("Obj_highlightGrid").transform);

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
