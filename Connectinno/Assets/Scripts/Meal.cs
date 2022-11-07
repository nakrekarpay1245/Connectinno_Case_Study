using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meal : MonoBehaviour
{
    [Tooltip("Holds the names of meals")]
    [SerializeField]
    private List<string> mealNameList;
    private string mealName;

    [Tooltip("Assign all ingredient prefabs to this list")]
    [SerializeField]
    private List<Ingredient> ingredientPrefabs;

    [Tooltip("Food indicators int the upper menu")]
    public List<Ingredient> ingredientsInMeal;
    public List<int> ingredientsCount;
    public int totalIngredientsCount;

    //[Tooltip("Ingredient indicators int the upper menu")]
    //[SerializeField]
    //private List<FoodDisplay> foodDisplays;

    /// <summary>
    /// Select and shows random 3 through all foods
    /// </summary>
    public void SetMeal()
    {
        ingredientsInMeal.Clear();
        ingredientsCount.Clear();
        // LevelManager.singleton.DisplayMealName(meals[Random.Range(0, meals.Count)]);
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, ingredientPrefabs.Count);
            if (!ingredientsInMeal.Contains(ingredientPrefabs[randomIndex]))
            {
                ingredientsInMeal.Add(ingredientPrefabs[randomIndex]);
                ingredientsCount.Add(Random.Range(1, 5));
            }
            else
            {
                SetMeal();
            }
        }
        mealName = mealNameList[Random.Range(0, mealNameList.Count)];
        IsMealDone();
        // Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName() + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName() + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName() + " Count" + ingredientsCount[2]);
        // StartCoroutine(FoodGenerator.singleton.GenerateFoods(selectedFoods));
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
            MealManager.singleton.NextMeal();
        }
        Debug.Log("Total Ingredients In Meal: " + totalIngredientsCount);
    }
}
