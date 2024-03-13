/** 
  * Fichier :  JoFalloffGeneratoreur.cs 
  * 
  * Auteur :   Boujamaa ATRMOUH
  * Date :     Mars 2022
  * Groupe :   TP 4C 
  * 
  * Résumé du fichier : 
  * 
  *   FalloffGenerator permet de délimiter la zone de génération du terrain.
  *   Elle sert nottament à définir une île.
  * 
  */


using UnityEngine;
using System.Collections;

public static class FalloffGenerator {

	/** 
	* 
	* public static float[,] GenerateFalloffMap(int size)
	* 
	* Résumé de la fonction GenerateFalloffMap : 
	* 
	*    La fonction va créer un tableau d'entier de la taille de la map, 
	*	 Et determiner une zone noire oû sera generé le terrain.
	* 
	* Paramètres       : entier : taille de la map
	* 
	* Valeur de retour : tableau de réels: une zone noir sur fond blanc, le noir représentant la zone de generation
	* 
	* Description      : 
	* 
	*    Cette fonction va créer une zone et definir la couleur à chaques intersections.
	*    
	*/

	public static float[,] GenerateFalloffMap(int size) {
		float[,] map = new float[size,size];

		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				float x = i / (float)size * 2 - 1;
				float y = j / (float)size * 2 - 1;

				float value = Mathf.Max (Mathf.Abs (x), Mathf.Abs (y));
				map [i, j] = Evaluate(value);
			}
		}

		return map;
	}

	/** 
	* 
	* static float Evaluate(float value)
	* 
	* Résumé de la fonction Evaluate : 
	* 
	*    La fonction retourne le résultat d'une opération passée sur un entier donné.
	* 
	* Paramètres       : Réel : valeur à calculer
	* 
	* Valeur de retour : réel: résultat de l'opération
	* 
	* Description      : 
	* 
	*   fonction f(x) = x^{a} / { {x^{a}+ (b-bx)^{a} }.
	*   En augmentant le b, on augment la taille de la zone et inversement.
	*    
	* 
	*/

	static float Evaluate(float value) {
		float a = 3;
		float b = 5.2f;

		return Mathf.Pow (value, a) / (Mathf.Pow (value, a) + Mathf.Pow (b - b * value, a));
	}
}
