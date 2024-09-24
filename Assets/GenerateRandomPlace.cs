
using UnityEngine;

public class GenerateRandomPlace : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [SerializeField] int density;

    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 zRange;

    void Start()
    {
        spawnObjects();
    }

    public void spawnObjects()
    {
        RaycastHit hit = new RaycastHit();

        for (int i = 0; i < density; i++)
        {
            float sampleX = Random.Range(xRange.x, xRange.y);
            float sampleY = Random.Range(zRange.x, zRange.y);

            Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);

            if (!Physics.Raycast(rayStart, Vector3.down, out hit, Mathf.Infinity))
                continue;

            if (hit.point.y < minHeight)
                continue;

            Vector3 spawnPosition = hit.point;
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
