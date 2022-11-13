using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MealManager : MonoBehaviour
{
    [Header("User Interface")]
    [Tooltip("Display list of all ingredients")]
    [SerializeField]
    private List<Image> ingredientIconList;
    [Tooltip("Display list of count of all ingredients")]
    [SerializeField]
    private List<TextMeshProUGUI> ingredientCountTextList;
    [Tooltip("Meal name display")]
    [SerializeField]
    private TextMeshProUGUI mealNameText;

    [Tooltip("Minimum meal count on current level")]
    [Range(2, 5)]
    [SerializeField]
    private int minMealCount;
    [Tooltip("Maximum meal count on current level")]
    [Range(2, 5)]
    [SerializeField]
    private int maxMealCount;
    //Meal count on current level
    private int mealCount;

    [SerializeField]
    private Meal mealPrefab;

    [Tooltip("Locations where the meals will generate")]
    [SerializeField]
    private List<Transform> mealTransformList;

    [Tooltip("List of generated meals")]
    public List<Meal> mealList;

    // PRIVATE PARAMETERS    
    private int mealIndex;
    private Meal currentMeal;

    public static MealManager singleton;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
    }

    /// <summary>
    /// Sets Randomly Meal Count between minimum and maxium meal count
    /// </summary>
    public void SetMealCount()
    {
        mealCount = Random.Range(minMealCount, (maxMealCount + 1));
        Debug.Log("MC : " + mealCount);
    }

    /// <summary>
    /// Produce meal amount mealCount
    /// </summary>
    public void GenerateMeal()
    {
        SetMealCount();
        for (int i = 0; i < mealCount; i++)
        {
            Meal _meal = Instantiate(mealPrefab, mealTransformList[i].position, Quaternion.identity);
            _meal.transform.parent = mealTransformList[i];
            mealList.Add(_meal);
        }
        currentMeal = mealList[0];
        currentMeal.SetMeal();
        currentMeal.StartMeal();
        DisplayMealIngredients(currentMeal.ingredientsInMeal);
        DisplayIngredientCounts(currentMeal.ingredientsCount);
        DisplayMealName(currentMeal.GetMealName());
    }

    /// <summary>
    /// Changes the meal
    /// </summary>
    public void NextMeal()
    {
        mealIndex++;
        if (mealIndex >= mealCount)
        {
            Debug.Log("Level Completed");
            LevelManager.singleton.FinishLevel(true);
            FinishMeals();
        }
        else
        {
            Debug.Log("Next Meal");
            currentMeal = mealList[mealIndex];
            currentMeal.SetMeal();
            currentMeal.StartMeal();
            DisplayMealIngredients(currentMeal.ingredientsInMeal);
            DisplayIngredientCounts(currentMeal.ingredientsCount);
            DisplayMealName(currentMeal.GetMealName());
        }
    }


    /// <summary>
    /// Removes completed meal from the list
    /// </summary>
    public void FinishMeals()
    {
        mealIndex = 0;
        int _count = mealList.Count;

        for (int i = 0; i < _count; i++)
        {
            mealList[mealList.Count - 1].FinishMeal();
            mealList.Remove(mealList[mealList.Count - 1]);
        }
    }

    /// <summary>
    /// Returns the status of the ingredient sent to the
    /// </summary>
    /// <param name="_ingredient"></param>
    /// <returns></returns>
    public bool ControlMeal(Ingredient _ingredient)
    {
        return currentMeal.ControlMeal(_ingredient);
    }

    /// <summary>
    /// Displays the name of meal
    /// </summary>
    public void DisplayMealName(string _mealName)
    {
        mealNameText.text = _mealName;
    }

    /// <summary>
    /// Displays the contents of the meal
    /// </summary>
    /// <param name="_ingredientList"></param>
    public void DisplayMealIngredients(List<Ingredient> _ingredientList)
    {
        for (int i = 0; i < ingredientIconList.Count; i++)
        {
            ingredientIconList[i].sprite = _ingredientList[i].ingredientIcon;
        }
    }

    /// <summary>
    /// Displays the counts of the contents of the meal
    /// </summary>
    /// <param name="_ingredientCountList"></param>
    public void DisplayIngredientCounts(List<int> _ingredientCountList)
    {
        for (int i = 0; i < ingredientIconList.Count; i++)
        {
            ingredientCountTextList[i].text = "x" + _ingredientCountList[i].ToString();
        }
    }
}
