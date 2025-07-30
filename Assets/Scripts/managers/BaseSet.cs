using UnityEngine;

public class BaseSet : MonoBehaviour
{
    public GameObject basePrefab;
    public GameObject baseObject;

    void Start()
    {
        SetBase();
        // SetBox();
    }

    private void SetBase()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Instantiate(basePrefab, new Vector3(i, j, 0), Quaternion.identity, baseObject.transform);
            }
        }
    }

    private void SetBox()
    {
        Vector3 offset = new Vector3(-1.3f, 2.5f, 0f);
        GameObject Box1 =
        Instantiate(basePrefab, new Vector3(0f, 4f, 0f) + offset, Quaternion.identity, baseObject.transform);

        Box1.transform.localScale = new Vector3(0.2f, 0.8f, 1.0f);

        GameObject Box2 =
        Instantiate(basePrefab, new Vector3(8f, 4f, 0f) - offset, Quaternion.identity, baseObject.transform);

        Box2.transform.localScale = new Vector3(0.2f, 0.8f, 1.0f);
    }
}
