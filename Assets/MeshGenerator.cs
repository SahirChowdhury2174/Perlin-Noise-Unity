
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Vector2Int size;

    [SerializeField] GameObject prefab;

    public float noiseScale;
    public int width = 256;
    public int length = 256;
    public float scale = 0f;
    public float offSetX = 100f;
    public float offSetY = 100f;
    

    void Start()
    {
        offSetX = Random.Range(0f, 9999f);
        offSetY = Random.Range(0f, 9999f);
        CreatePlane();
        gameObject.AddComponent<MeshCollider>();
        Instantiate(prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

    }

    
    void Update()
    {
        CreatePlane();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyAll();
            createAnother();
            Destroy(GetComponent<MeshCollider>());
            gameObject.AddComponent<MeshCollider>();
            
        
            Destroy(GameObject.FindWithTag("ItemGenerator"));
            Instantiate(prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        
    }


    public void DestroyAll()
    {

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Spawned");

        for(int i = 0; i < cubes.Length; i++)
        {
            Destroy(cubes[i]);
        }
    }


    //Actual Map Generation
    public void CreatePlane()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = CreateVerticies();
        mesh.triangles = CreateTriangles();

        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;
    }

    private Vector3[] CreateVerticies()
    {

        Vector3[] vertices = new Vector3[(size.x + 1) * (size.y + 1)];
        

        for(int z = 0 , i = 0; z <= size.y; z++)
        {
            for(int x = 0; x <= size.x; x++)
            {
                float y = noiseGenerator(x, z) * noiseScale;
                vertices[i] = new Vector3(x,y,z);
                i++;
            }
        }

        return vertices;
    }
    private int[] CreateTriangles()
    {
        int[] triangles = new int[size.x * size.y * 6];

        for(int z = 0, vert = 0, tris = 0; z < size.y; z++)
        {
            for(int x = 0; x < size.x; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size.x + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size.x + 1;
                triangles[tris + 5] = vert + size.x + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        return triangles;
    }

    
    float noiseGenerator(int x, int y)
    {
        float xCoord = (float)x / width * scale + offSetX;
        float yCoord = (float)y / length * scale + offSetY;


        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return sample;
    }

    
    public void createAnother()
    {
        offSetX = Random.Range(0f, 9999f);
        offSetY = Random.Range(0f, 9999f);
        CreatePlane();
    }
    
}
