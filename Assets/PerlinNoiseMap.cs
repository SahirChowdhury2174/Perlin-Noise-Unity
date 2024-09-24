using UnityEngine;

public class PerlinNoiseMap: MonoBehaviour
{

    public int width = 256;    
    public int height = 256;
    public float offSetX = 100f;
    public float offSetY = 100f;

    public float scale = 20f;

    public void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();

    }


    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = calculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color calculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offSetX;
        float yCoord = (float)y / height * scale + offSetY;


        float sample = Mathf.PerlinNoise(x, y);
        return new Color(sample, sample, sample);
    }
}