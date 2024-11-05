using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    [SerializeField] Terrain terrain;
    [SerializeField, Range(0,0.04f)] float perlinStep;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        float[,] heightMap = new float[
            terrain.terrainData.heightmapResolution,
            terrain.terrainData.heightmapResolution
            ];

        float xCoord = 0;
        for(int x=0; x<heightMap.GetLength(0); x++)
        {
            float yCoord = 0;
            for (int y = 0; y < heightMap.GetLength(1); y++)
            {
                // bed of nails
                // heightMap[x,y] = Random.value;
                heightMap[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
                yCoord += perlinStep;
            }
            xCoord += perlinStep;
        }

        terrain.terrainData.SetHeights(0, 0, heightMap);
    }
}
