using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour {

    [SerializeField] private GameObject prefab = null;

    MapPreview mappreview;
    MeshFilter mf;
    float[,] map; 
    int numVertsPerLine;
    private int[] subMeshesFaceTotals;
    private int totalSubMeshes;

    public void Awake() 
    {
        mappreview = GameObject.Find("Map").GetComponent<MapPreview>();
        mf =  GameObject.Find("Mesh").GetComponent<MeshFilter>();
    }
 
    public void Start() {

        // Spawn the grass prefab at every determined point
        var centerPos = transform.position;
        map = mappreview.values;
        numVertsPerLine = mappreview.meshSettings.numVertsPerLine;
        int i = numVertsPerLine/2;

        
        for (int x = 2; x < numVertsPerLine-2; x ++) {
			for (int y = 2; y < numVertsPerLine-2; y++) {
                GameObject.Instantiate(prefab,new Vector3(-numVertsPerLine/2+x,map[x,y],numVertsPerLine/2-y), Quaternion.identity);
                i--;
            }
        }
        
        
    }
}
