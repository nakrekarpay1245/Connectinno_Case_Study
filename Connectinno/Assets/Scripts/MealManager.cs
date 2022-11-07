using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MealManager : MonoBehaviour
{
    [Header("User Interface")]
    [SerializeField]
    private List<Image> ingredientIconList;
    [SerializeField]
    private List<TextMeshProUGUI> ingredientCountTextList;
    [SerializeField]
    private TextMeshProUGUI mealNameText;

    [Range(2, 5)]
    [SerializeField]
    private int mealCount;

    [SerializeField]
    private Meal mealPrefab;
    [SerializeField]
    private List<Transform> mealTransformList;

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


    //GEÇÝCÝ
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateMeal();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            FoodGenerator.singleton.GenerateObjectRandomPosition(100);
        }
    }

    /// <summary>
    /// Produce meal amount mealCount
    /// </summary>
    public void GenerateMeal()
    {
        for (int i = 0; i < mealCount; i++)
        {
            Meal _meal = Instantiate(mealPrefab, mealTransformList[i].position, Quaternion.identity);
            mealList.Add(_meal);
        }
        currentMeal = mealList[0];
        currentMeal.SetMeal();
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
        }
        else
        {
            Debug.Log("Next Meal");
            currentMeal = mealList[mealIndex];
            currentMeal.SetMeal();
            DisplayMealIngredients(currentMeal.ingredientsInMeal);
            DisplayIngredientCounts(currentMeal.ingredientsCount);
            DisplayMealName(currentMeal.GetMealName());
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
