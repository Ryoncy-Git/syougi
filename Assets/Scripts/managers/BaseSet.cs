using UnityEngine;

public class BaseSet : MonoBehaviour
{
    public GameObject basePrefab;
    public GameObject baseObject;

    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Instantiate(basePrefab, new Vector3(i, j, 0), Quaternion.identity, baseObject.transform);
            }
        }
    }
}
