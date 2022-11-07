using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Ingredient> ingredientPrefabList;

    [SerializeField]
    private Transform generateTransform;

    [SerializeField]
    private float generateRadius;

    [SerializeField]
    private float generateCollisionCheckRadius;

    public static FoodGenerator singleton;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    public void GenerateObjectRandomPosition(int count)
    {
        // Debug.Log("Count: " + count);
        StartCoroutine(GenerateFoods(count));
    }


    /// <summary>
    /// Generate random foods at random position at 0.01 second intervals
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateFoods(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Debug.Log("Produced: " + i);
            Vector3 randomPosition =
                generateTransform.position + UnityEngine.Random.insideUnitSphere * generateRadius;

            randomPosition = new Vector3(randomPosition.x, randomPosition.y, 0);

            if (!Physics.CheckSphere(randomPosition, generateCollisionCheckRadius))
            {
                Ingredient ingredientPrefab =
                    ingredientPrefabList[UnityEngine.Random.Range(0, ingredientPrefabList.Count)];

                Ingredient defaultIngredient = Instantiate(ingredientPrefab, randomPosition, Quaternion.identity);
                defaultIngredient.transform.parent = generateTransform;
            }
            else
            {
                i--;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 0.25f);

        Gizmos.DrawWireSphere(generateTransform.position, generateRadius);
    }
}
