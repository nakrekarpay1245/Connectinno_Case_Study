using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Meal : MonoBehaviour
{
    [Header("Ingredient Lists")]
    [Tooltip("Holds the names of meals")]
    [SerializeField]
    private List<string> mealNameList;
    // Name of the meal
    private string mealName;

    [Tooltip("Assign all ingredient prefabs to this list")]
    [SerializeField]
    private List<Ingredient> ingredientPrefabs;

    [Tooltip("Meal contents")]
    public List<Ingredient> ingredientsInMeal;
    [Tooltip("Meal contents count")]
    public List<int> ingredientsCount;
    // Total meal contents count
    public int totalIngredientsCount;

    [Tooltip("Holds the models of meals")]
    [SerializeField]
    private List<GameObject> mealModelList;
    private GameObject mealModel;

    [Tooltip("Meal Steam Particle")]
    [SerializeField]
    private ParticleSystem steamParticle;
    [Tooltip("Meal Done Particle")]
    [SerializeField]
    private ParticleSystem doneParticle;

    private void Awake()
    {
        steamParticle = GetComponentInChildren<ParticleSystem>();
    }

    /// <summary>
    /// Randomly choose three ingredients
    /// </summary>
    public void SetMeal()
    {
        ingredientsInMeal.Clear();
        ingredientsCount.Clear();
        int randomIndex = 0;
        for (int i = 0; i < 3; i++)
        {
            randomIndex = Random.Range(0, ingredientPrefabs.Count);
            if (!ingredientsInMeal.Contains(ingredientPrefabs[randomIndex]))
            {
                ingredientsInMeal.Add(ingredientPrefabs[randomIndex]);
                ingredientsCount.Add(Random.Range(1, 5));
            }
            else
            {
                SetMeal();
                break;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            IngredientManager.singleton.GenerateIngredients(ingredientsInMeal[i], ingredientsCount[i]);
        }
        randomIndex = Random.Range(0, mealNameList.Count);
        mealName = mealNameList[randomIndex];
        mealModel = Instantiate(mealModelList[randomIndex], transform.position, Quaternion.identity);
        mealModel.transform.parent = transform;
        mealModel.transform.DOScale(0, 0);
        IsMealDone();
        // Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName() + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName() + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName() + " Count" + ingredientsCount[2]);
    }

    /// <summary>
    /// Returns the status of the ingredient sent to the
    /// </summary>
    /// <param name="_ingredient"></param>
    /// <returns></returns>
    public bool ControlMeal(Ingredient _ingredient)
    {
        if (ingredientsInMeal[0].GetIngredientName() == _ingredient.GetIngredientName() &&
                ingredientsCount[0] > 0)
        {
            ingredientsCount[0]--;
            //Debug.Log("Sended: " + _ingredient + " ingredientInMeal[0]: " + ingredientsInMeal[0]);
            //Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName()
            //    + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName()
            //    + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName()
            //    + " Count" + ingredientsCount[2]);

            MealManager.singleton.DisplayIngredientCounts(ingredientsCount);
            IsMealDone();

            return true;
        }
        else if (ingredientsInMeal[1].GetIngredientName() == _ingredient.GetIngredientName() &&
                ingredientsCount[1] > 0)
        {
            ingredientsCount[1]--;
            //Debug.Log("Sended: " + _ingredient + " ingredientInMeal[0]: " + ingredientsInMeal[0]);
            //Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName()
            //    + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName()
            //    + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName()
            //    + " Count" + ingredientsCount[2]);

            MealManager.singleton.DisplayIngredientCounts(ingredientsCount);
            IsMealDone();

            return true;
        }
        else if (ingredientsInMeal[2].GetIngredientName() == _ingredient.GetIngredientName() &&
                ingredientsCount[2] > 0)
        {
            ingredientsCount[2]--;
            //Debug.Log("Sended: " + _ingredient + " ingredientInMeal[0]: " + ingredientsInMeal[0]);
            //Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName()
            //    + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName()
            //    + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName()
            //    + " Count" + ingredientsCount[2]);

            MealManager.singleton.DisplayIngredientCounts(ingredientsCount);
            IsMealDone();

            return true;
        }
        else
        {
            //Debug.Log("Ingredient is not in meal: " + _ingredient);
            //Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName()
            //    + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName()
            //    + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName()
            //    + " Count" + ingredientsCount[2]);
            return false;
        }
    }

    /// <summary>
    /// Returns the name of the meal
    /// </summary>
    /// <returns></returns>
    public string GetMealName()
    {
        return mealName;
    }

    /// <summary>
    /// Checks the status of the meal
    /// </summary>
    public void IsMealDone()
    {
        totalIngredientsCount = 0;
        for (int i = 0; i < ingredientsCount.Count; i++)
        {
            totalIngredientsCount += ingredientsCount[i];
        }
        if (totalIngredientsCount <= 0)
        {
            mealModel.transform.DOScale(1, 0.15f);
            MealManager.singleton.NextMeal();
            steamParticle.Play();
            doneParticle.Play();
        }
        // Debug.Log("Total Ingredients In Meal: " + totalIngredientsCount);
    }

    /// <summary>
    /// The size raises from zero to normal
    /// </summary>
    public void StartMeal()
    {
        transform.DOScale(1, 0.15f);
        // Debug.Log(name + "started");
    }

    /// <summary>
    /// The size raises from normal to zero
    /// </summary>
    public void FinishMeal()
    {
        transform.DOScale(0, 0.15f);
        Destroy(gameObject, 1);
        // Debug.Log(name + "finished");
    }
}
