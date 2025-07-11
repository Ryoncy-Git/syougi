using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_highlightGrid : MonoBehaviour
{
    public Scr_GameManager gameManager;
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
        Debug.Log("行先を選択");
    }
}
