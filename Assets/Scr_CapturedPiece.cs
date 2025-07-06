using UnityEngine;

public class Scr_CapturedPiece : MonoBehaviour
{
    GameObject Koma;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Koma = GameObject.Find("Koma");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn_piece(GameObject piece, int x, int y)
    {
        Instantiate(piece, new Vector3(x, y, -1), Quaternion.identity, Koma.transform);
    }
}
