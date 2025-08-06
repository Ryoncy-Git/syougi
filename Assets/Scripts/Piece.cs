using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
    //transform.position - (x, y, -1);
    // managers
    private GameManager gameManager;
    private PieceMovement pieceMovement;
    private DestGridManager destGridManager;
    private CaptureManager captureManager;
    private UIManager uiManager;
    private SkillManager skillManager;

    // variants
    [SerializeField] PieceType pieceType;
    public bool is1PPiece = true;
    public bool isNari = false;
    // bool isSelected = false;
    SpriteRenderer sr;

    // sprites
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite nariSprite;

    // 自分のゲームとして新しく追加予定のもの
    private int exp = 0;
    private int pieceLevel = 0;
    private AttackSkillType attackSkills = AttackSkillType.None;
    private DefendSkillType defendSkills = DefendSkillType.None;
    private ControlSkillType controlSkills = ControlSkillType.None;

    void Start()
    {
        Init();
    }

    void Init()
    {
        gameManager = GameObject.Find("Obj_GameManager").GetComponent<GameManager>();
        pieceMovement = GameObject.Find("Obj_PieceMovement").GetComponent<PieceMovement>();
        destGridManager = GameObject.Find("Obj_destGridManager").GetComponent<DestGridManager>();
        captureManager = GameObject.Find("Obj_CaptureManager").GetComponent<CaptureManager>();
        uiManager = GameObject.Find("UserInterface").GetComponent<UIManager>();
        skillManager = GameObject.Find("Skills").GetComponent<SkillManager>();

        sr = GetComponent<SpriteRenderer>();

        // variants
        isNari = false;
        UpdateSprite();
    }
    public void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UIの上なのでスキップ");
            return; // UIの上なので処理しない
        }

        if (gameManager.Get_is1PTurn() == is1PPiece)
            gameManager.SelectPiece(this);
    }

    public void Select()
    {
        sr.color = Color.gray;
        destGridManager.Hide_destGrid();
        Show_path();
        skillManager.ShowSkills(this);
        gameManager.Set_isSpawnTurn(false);
    }

    public void Deselect()
    {
        sr.color = Color.white;
        destGridManager.Hide_destGrid();
    }

    void Show_path()
    {
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);

        // gamemanagerのほうで範囲外のものははじくようにできているから、
        // show_destGrid でこっちでは盤面内かどうかを判定する必要はない
        // ただし、駒があるかどうかは判定しないためそこは見る必要がある
        bool isKinLike = isNari &&
        (pieceType == PieceType.Hu || pieceType == PieceType.Kyosya ||
        pieceType == PieceType.Keima || pieceType == PieceType.Gin);

        if (isKinLike || pieceType == PieceType.Kin)
        {
            pieceMovement.Show_path_Kin(roundX, roundY, is1PPiece);
            return;
        }

        switch (pieceType)
        {
            case PieceType.Hu:
                pieceMovement.Show_path_Hu(roundX, roundY, is1PPiece);
                break;

            case PieceType.Kyosya:
                pieceMovement.Show_path_Kyosya(roundX, roundY, is1PPiece);
                break;

            case PieceType.Keima:
                pieceMovement.Show_path_Keima(roundX, roundY, is1PPiece);
                break;

            case PieceType.Gin:
                pieceMovement.Show_path_Gin(roundX, roundY, is1PPiece);
                break;

            case PieceType.Kin:
                pieceMovement.Show_path_Kin(roundX, roundY, is1PPiece);
                break;

            case PieceType.Ou:
                pieceMovement.Show_path_Ou(roundX, roundY, is1PPiece);
                break;

            case PieceType.Kaku:
                if (isNari)
                    pieceMovement.Show_path_Ou(roundX, roundY, is1PPiece);

                pieceMovement.Show_path_Kaku(roundX, roundY, is1PPiece);
                break;

            case PieceType.Hisya:
                if (isNari)
                    pieceMovement.Show_path_Ou(roundX, roundY, is1PPiece);

                pieceMovement.Show_path_Hisya(roundX, roundY, is1PPiece);
                break;

            default:
                break;
        }
    }

    public void Movement(int x, int y)
    {
        // x, y は行先の座標
        int prevX = Mathf.RoundToInt(transform.position.x);
        int prevY = Mathf.RoundToInt(transform.position.y);


        GameObject targetObject = gameManager.Get_GridGameObject(x, y);

        // スキルの処理をここに追加
        // もし移動先の駒がディフェンススキルを持ってるなら
        if (targetObject != null && targetObject.GetComponent<Piece>().GetDefendSkill() != DefendSkillType.None)
        {
            // ディフェンススキルの処理を行う

        }
        targetObject = gameManager.Get_GridGameObject(x, y); // ディフェンススキルで移動するかもしれないので一応更新

        if (targetObject != null && this.GetAttackSkill() != AttackSkillType.None && targetObject != null)
        {
            // 攻撃スキルの処理を行う

        }


        if (targetObject != null)//cacth
        {
            // 持ち駒 = gameManager.Get_GridGameObject(x, y);
            captureManager.Capture_piece(targetObject, is1PPiece);
        }



        // 移動
        transform.position = new Vector3(x, y, -1);
        gameManager.Set_GridGameObject(null, prevX, prevY);
        gameManager.Set_GridGameObject(this.gameObject, x, y);

        // その他の処理
        destGridManager.Hide_destGrid();
        gameManager.DeselectPiece();
    }

    private void UpdateSprite()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        sr.sprite = isNari ? nariSprite : normalSprite;
    }

    public void Set_is1PPiece(bool state)
    {
        is1PPiece = state;
    }

    public bool Get_is1PPiece()
    {
        return is1PPiece;
    }

    public PieceType Get_PieceType()
    {
        return pieceType;
    }

    public void Set_Nari(bool state)
    {
        isNari = state;
        UpdateSprite(); // 追加
    }

    public bool Get_isNari()
    {
        return isNari;
    }

    public AttackSkillType GetAttackSkill()
    {
        return attackSkills;
    }
    public DefendSkillType GetDefendSkill()
    {
        return defendSkills;
    }
    public ControlSkillType GetControlSkill()
    {
        return controlSkills;
    }
    public void AddAttackSkill(AttackSkillType skill)
    {
        attackSkills |= skill;
    }
    public void AddDefendSkill(DefendSkillType skill)
    {
        defendSkills |= skill;
    }
    public void AddControlSkill(ControlSkillType skill)
    {
        controlSkills |= skill;
    }

    public void RemoveAllSkills()
    {
        attackSkills = AttackSkillType.None;
        defendSkills = DefendSkillType.None;
        controlSkills = ControlSkillType.None;
    }


    public void AddExp(int value)
    {
        exp += value;

        bool flag = true;
        while (flag)
        {
            if (pieceLevel <= 0 && exp >= 2)
            {
                LevelUp();
            }
            else if (pieceLevel <= 1 && exp >= 5)
            {
                LevelUp();
            }
            else if (pieceLevel <= 2 && exp >= 10)
            {
                LevelUp();
            }

            if (pieceLevel == 0 && exp < 2 && exp >= 0 ||
               pieceLevel == 1 && exp < 5 && exp >= 2 ||
               pieceLevel == 2 && exp < 10 && exp >= 5)
            {
                flag = false;
            }
        }
    }

    private void LevelUp()
    {
        if (pieceLevel >= 3) return; // 最大レベルは3と仮定

        pieceLevel++;
        // レベルアップ時の処理をここに追加
        // 例えば、スキルの追加や強化など
        uiManager.ShowLevelUPDisplay(this);
        // Debug.Log($"Piece leveled up to level {pieceLevel} with {exp} experience points.");
    }

    public int GetExp()
    {
        return exp;
    }

    public int GetLevel()
    {
        return pieceLevel;
    }
}
