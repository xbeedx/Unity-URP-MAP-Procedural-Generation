/** 
* Fichier :  HeightMapGenerator.cs 
* 
* Auteur :   Boujamaa ATRMOUH
* Date :     Mars 2022
* Groupe :   TP 4C 
* 
* Résumé du fichier : 
* 
*   HeightMapGenerator permet de définir les hauteurs de la carte.
* 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HeightMapGenerator {

	/** 
	* 
	* public static HeightMap GenerateHeightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCentre)
	* 
	* Résumé de la fonction GenerateHeightMap : 
	* 
	*    La fonction est un constructeur qui va créer une structure HeightMap contenant les hauteurs de la cartes.
	* 
	* Paramètres       : entier, entier, parameres, vecteur : longueur, largeur, (forme des courbes, utilisation du fallOff), centre
	* 
	* Valeur de retour : HeightMap : contenant un tableau des valeurs, le point minimum et le point maximum
	* 
	* Description      : 
	* 
	*    Cette fonction va créer une classe d'attribut HeightMap et initialiser ses valeurs.
	*    
	*/


	public static HeightMap GenerateHeightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCentre) {
		float[,] values = Noise.GenerateNoiseMap (width, height, settings.noiseSettings, sampleCentre);
		float[,] falloffMap = null;

		AnimationCurve heightCurve_threadsafe = new AnimationCurve (settings.heightCurve.keys);

		float minValue = float.MaxValue;
		float maxValue = float.MinValue;

		if (settings.useFalloff) {
			if (falloffMap == null)
			{
				falloffMap = FalloffGenerator.GenerateFalloffMap(height+6);
			}
        }

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (settings.useFalloff) {
					values [i, j] = Mathf.Clamp01(values [i,j] - falloffMap[i,j]);
				}
				values [i, j] *= heightCurve_threadsafe.Evaluate (values [i, j]) * settings.heightMultiplier;

				if (values [i, j] > maxValue) {
					maxValue = values [i, j];
				}
				if (values [i, j] < minValue) {
					minValue = values [i, j];
				}
			}
		}

		return new HeightMap (values, minValue, maxValue);
	}
}

public struct HeightMap {
	public readonly float[,] values;
	public readonly float minValue;
	public readonly float maxValue;

	public HeightMap (float[,] values, float minValue, float maxValue)
	{
		this.values = values;
		this.minValue = minValue;
		this.maxValue = maxValue;
	}
}

