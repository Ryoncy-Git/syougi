using UnityEngine;
using UnityEngine.EventSystems;

public class Sonohen : MonoBehaviour
{
    public Scr_GameManager gameManager;
    public void OnMouseDown()
    {
        // Debug.Log("clicked sonohen");
        if (gameManager == null)
            return;

        // マウスがUIの上にあるかチェック
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // Debug.Log("UIの上なのでスキップ");
            return; // UIの上なので処理しない
        }

        gameManager.Click_sonohen();
        // Debug.Log("行先を選択");
    }
}

