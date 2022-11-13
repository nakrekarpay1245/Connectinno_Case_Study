using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    [Header("Ingredient Lists")]
    [Tooltip("All ingredient prefabs")]
    [SerializeField]
    private List<Ingredient> ingredientPrefabList;

    [Tooltip("All generated ingredients")]
    [SerializeField]
    private List<Ingredient> generatedIngredientList;

    [Header("Position Parameters")]
    [Tooltip("Lcoation that the ingredients will be genearte")]
    [SerializeField]
    private Transform generateTransform;

    [Tooltip("The diameter of the circle that generation will be start")]
    [SerializeField]
    private float generateRadius;

    [Tooltip("Minimum x value for the generate ingredients")]
    [SerializeField]
    private float minX;

    [Tooltip("Maximum x value for the generate ingredients")]
    [SerializeField]
    private float maxX;

    [Tooltip("Minimum y value for the generate ingredients")]
    [SerializeField]
    private float minY;

    [Tooltip("Maximum y value for the generate ingredients")]
    [SerializeField]
    private float maxY;

    [Tooltip("The distance between the ingredients to be generated")]
    [SerializeField]
    private float generateCollisionCheckRadius;

    [Header("SFXs")]
    [Tooltip("AudioSource for ingredient SFXs")]
    [SerializeField]
    private AudioSource audioSource;
    [Tooltip("AudioClip for grab ingredient")]
    [SerializeField]
    private AudioClip grabSFX;
    [Tooltip("AudioSource for fry ingredient")]
    [SerializeField]
    private List<AudioClip> sizzleSFXList;

    // Singleton
    public static IngredientManager singleton;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Generates the Ingredients
    /// </summary>
    /// <param name="count"></param>
    public void GenerateIngredients(int _count)
    {
        // Debug.Log("Count: " + count);
        StartCoroutine(GenerateIngredientsRoutine(_count));
    }
    /// <summary>
    /// Generates the only one Ingredient specified amount
    /// </summary>
    /// <param name="count"></param>
    public void GenerateIngredients(Ingredient _ingredient, int _count)
    {
        // Debug.Log("Count: " + count);
        StartCoroutine(GenerateIngredientsRoutine(_ingredient, _count));
    }


    /// <summary>
    /// Generate random ingredients at random position at 0.01 second intervals
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateIngredientsRoutine(int _count)
    {
        FinishGeneratedIngredients();
        generatedIngredientList.Clear();
        for (int i = 0; i < _count; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            randomPosition = new Vector3(Random.Range(minX, maxX),
                                         Random.Range(minY, maxY),
                                         Random.Range(-1, 1));

            randomPosition = new Vector3(randomPosition.x, randomPosition.y, randomPosition.normalized.z);

            if (!Physics.CheckSphere(randomPosition, generateCollisionCheckRadius))
            {
                Ingredient ingredientPrefab =
                    ingredientPrefabList[Random.Range(0, ingredientPrefabList.Count)];

                Ingredient defaultIngredient = Instantiate(ingredientPrefab, randomPosition, Quaternion.identity);
                defaultIngredient.transform.parent = generateTransform;
                generatedIngredientList.Add(defaultIngredient);
                generatedIngredientList[i].StartIngredient();
            }
            else
            {
                i--;
            }
            yield return new WaitForSeconds(0.0001f);
        }
    }

    /// <summary>
    /// Generate specified ingredient at random position at 0.01 second intervals specified amount
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateIngredientsRoutine(Ingredient _ingredient, int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            // Debug.Log("Produced: " + i);
            Vector3 randomPosition = Vector3.zero;
            randomPosition = new Vector3(Random.Range(minX, maxX),
                                         Random.Range(minY, maxY),
                                         Random.Range(-1, 1));

            randomPosition = new Vector3(randomPosition.x, randomPosition.y, randomPosition.normalized.z);

            if (!Physics.CheckSphere(randomPosition, generateCollisionCheckRadius))
            {
                Ingredient defaultIngredient = Instantiate(_ingredient, randomPosition, Quaternion.identity);
                defaultIngredient.transform.parent = generateTransform;
                defaultIngredient.StartIngredient();
                generatedIngredientList.Add(defaultIngredient);
            }
            else
            {
                i--;
            }
            yield return new WaitForSeconds(0.0001f);
        }
    }

    /// <summary>
    /// Finishs all of ingredients on the scene
    /// </summary>
    public void FinishGeneratedIngredients()
    {
        int _count = generatedIngredientList.Count;

        for (int i = 0; i < _count; i++)
        {
            // Debug.Log("FinishGeneratedIngredient: " + i);

            generatedIngredientList[generatedIngredientList.Count - 1].FinishIngredient();
            generatedIngredientList.Remove(generatedIngredientList[generatedIngredientList.Count - 1]);
        }
    }

    /// <summary>
    /// Plays Pop sound
    /// </summary>
    public void PlayGrabSound()
    {
        SetRandomPitch();
        audioSource.PlayOneShot(grabSFX);
    }

    /// <summary>
    /// 
    /// Plays Fry sound
    /// </summary>
    public void PlayFrySound()
    {
        SetRandomPitch();
        AudioClip sizzleSFX = sizzleSFXList[Random.Range(0, sizzleSFXList.Count)];
        audioSource.PlayOneShot(sizzleSFX);
    }

    /// <summary>
    /// Random Pitch for AudioSource
    /// </summary>
    public void SetRandomPitch()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 0.25f);

        Gizmos.DrawWireSphere(generateTransform.position, generateRadius);
    }
}
