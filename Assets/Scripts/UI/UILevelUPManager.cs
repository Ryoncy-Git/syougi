using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevelUPManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiManager;
    private const int NumberOfSkills = 9; // スキルの数
    public Sprite[] sprites;
    public TMP_Text[] texts;
    private Piece targetPiece;
    public GameObject UI_LevelUPDisplay;

    public void ShowLevelUPDisplay(Piece piece)
    {
        targetPiece = piece;
        UI_LevelUPDisplay.SetActive(true);

        RandomDisplay();
    }

    private void RandomDisplay()
    {
        // 3つのスキルをランダムに選ぶ（重複が無いように選ぶ）
        int[] randomIndex =
        {
            Random.Range(0, NumberOfSkills),
            Random.Range(0, NumberOfSkills),
            Random.Range(0, NumberOfSkills)
        };

        while (randomIndex[0] == randomIndex[1] || randomIndex[0] == randomIndex[2] || randomIndex[1] == randomIndex[2])
        {
            randomIndex[0] = Random.Range(0, NumberOfSkills);
            randomIndex[1] = Random.Range(0, NumberOfSkills);
            randomIndex[2] = Random.Range(0, NumberOfSkills);
        }

        // 選んだスキルのスプライトとテキストを設定
        for (int i = 0; i < 3; i++)
        {
            int index = randomIndex[i];
            if (index < sprites.Length && index < texts.Length)
            {
                // スプライトを設定
                Image image = UI_LevelUPDisplay.transform.GetChild(i).GetComponent<Image>();
                image.sprite = sprites[index];
            }
        }
    }
}
