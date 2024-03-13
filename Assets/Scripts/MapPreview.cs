/** 
* Fichier :  MapPreview.cs 
* 
* Auteur :   Boujamaa ATRMOUH
* Date :     Mars 2022
* Groupe :   TP 4C 
* 
* Résumé du fichier : 
* 
*   MapPreview est le fichier principal. Il définit les différent objets du projet et lance la generation.
* 
*/

using UnityEngine;
using System.Collections;

public class MapPreview : MonoBehaviour {

	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	public MeshCollider meshCollider;

	public MeshSettings meshSettings;
	public HeightMapSettings heightMapSettings;
	

	public Material terrainMaterial;

	[Range(0,4)]
	public int editorPreviewLOD;
	public bool autoUpdate;

	public MeshData mesh;

	public float[,] values;

	public void Awake()
	{
		HeightMap heightMap = HeightMapGenerator.GenerateHeightMap (meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, Vector2.zero);
		mesh = MeshGenerator.GenerateTerrainMesh (heightMap.values,meshSettings, editorPreviewLOD);
		DrawMesh (mesh);
		values = heightMap.values;
	}

	/** 
	* Résumé de la fonction DrawMapInEditor : 
	* La fonction crée un objet HeightMap contenant les hauteurs,
	* Et lance la generation du terrain
	*/


	public void DrawMapInEditor() {
		HeightMap heightMap = HeightMapGenerator.GenerateHeightMap (meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, Vector2.zero);
		mesh = MeshGenerator.GenerateTerrainMesh (heightMap.values,meshSettings, editorPreviewLOD);
		DrawMesh (mesh);
		values = heightMap.values;
	} 

	/** 
	* Résumé de la fonction DrawMesh : 
	* Paramètres: MeshData
	* La fonction crée un mesh,
	* et lui applique un collider et un filtre
	*/

	public void DrawMesh(MeshData meshData) {
		meshFilter.sharedMesh = meshData.CreateMesh();
		meshCollider.sharedMesh = meshData.CreateMesh();

		textureRender.gameObject.SetActive (false);
		meshFilter.gameObject.SetActive (true);
		
	}

	/** 
	* Résumé de la fonction OnValuesUpdated : 
	* La fonction detecte si les paramétres d'entrée ont été modifiés.
	*/

	void OnValuesUpdated() {
		if (!Application.isPlaying) {
			DrawMapInEditor ();
		}
	}

	/** 
	* Résumé de la fonction OnValidate : 
	* La fonction génére un terrain lorsque le bouton est appuié.
	*/

	void OnValidate() {

		if (meshSettings != null) {
			meshSettings.OnValuesUpdated -= OnValuesUpdated;
			meshSettings.OnValuesUpdated += OnValuesUpdated;
		}
		if (heightMapSettings != null) {
			heightMapSettings.OnValuesUpdated -= OnValuesUpdated;
			heightMapSettings.OnValuesUpdated += OnValuesUpdated;
		}
		

	}

}
