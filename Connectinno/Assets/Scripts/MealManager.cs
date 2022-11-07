using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealManager : MonoBehaviour
{
    [Range(2, 5)]
    [SerializeField]
    private int mealCount;

    [SerializeField]
    private Meal mealPrefab;
    [SerializeField]
    private List<Transform> mealTransforms;

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
    }

    /// <summary>
    /// Produce meal amount mealCount
    /// </summary>
    public void GenerateMeal()
    {
        for (int i = 0; i < mealCount; i++)
        {
            Meal _meal = Instantiate(mealPrefab, mealTransforms[i].position, Quaternion.identity);
            mealList.Add(_meal);
        }
        currentMeal = mealList[0];
        currentMeal.SetMeal();
    }

    /// <summary>
    /// Changes the meal
    /// </summary>
    public void NextMeal()
    {
        currentMeal = mealList[mealIndex];
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
}
