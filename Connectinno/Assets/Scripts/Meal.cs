using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meal : MonoBehaviour
{
    [Tooltip("Assign all ingredient prefabs to this list")]
    [SerializeField]
    private List<Ingredient> ingredientPrefabs;

    [Tooltip("Food indicators int the upper menu")]
    [SerializeField]
    private List<Ingredient> ingredientsInMeal;
    [SerializeField]
    private List<int> ingredientsCount;

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

        Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName() + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName() + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName() + " Count" + ingredientsCount[2]);
        // StartCoroutine(FoodGenerator.singleton.GenerateFoods(selectedFoods));
    }

    /// <summary>
    /// Returns the status of the ingredient sent to the
    /// </summary>
    /// <param name="_ingredient"></param>
    /// <returns></returns>
    public bool ControlMeal(Ingredient _ingredient)
    {
        if (ingredientsInMeal[0].GetIngredientName() == _ingredient.GetIngredientName())
        {
            ingredientsCount[0]--;
            Debug.Log("Sended: " + _ingredient + " ingredientInMeal[0]: " + ingredientsInMeal[0]);
            Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName() + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName() + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName() + " Count" + ingredientsCount[2]);
            return true;
        }
        else if (ingredientsInMeal[1].GetIngredientName() == _ingredient.GetIngredientName())
        {
            ingredientsCount[1]--;
            Debug.Log("Sended: " + _ingredient + " ingredientInMeal[0]: " + ingredientsInMeal[0]);
            Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName() + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName() + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName() + " Count" + ingredientsCount[2]);
            return true;
        }
        else if (ingredientsInMeal[2].GetIngredientName() == _ingredient.GetIngredientName())
        {
            ingredientsCount[2]--;
            Debug.Log("Sended: " + _ingredient + " ingredientInMeal[0]: " + ingredientsInMeal[0]);
            Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName() + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName() + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName() + " Count" + ingredientsCount[2]);
            return true;
        }
        else
        {
            Debug.Log("Ingredient is not in meal: " + _ingredient);
            Debug.Log("Meal = In 1: " + ingredientsInMeal[0].GetIngredientName() + " Count" + ingredientsCount[0] + " In 2: " + ingredientsInMeal[1].GetIngredientName() + " Count" + ingredientsCount[1] + " In 3: " + ingredientsInMeal[2].GetIngredientName() + " Count" + ingredientsCount[2]);
            return false;
        }
    }
}
