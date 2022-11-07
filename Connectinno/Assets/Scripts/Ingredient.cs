using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ingredient : MonoBehaviour
{
    [Header("Type")]
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private string ingredientName;

    [Header("Components")]
    [SerializeField]
    private Collider collider;

    // PRIVATE PARAMETERS
    private Vector3 lastPosition;
    private bool isInPan;
    private bool isGrab;

    //GEÇÝCÝ
    private Transform panTransform;
    private void Awake()
    {
        SetLastPosition(transform.position);
        collider = GetComponent<Collider>();
        panTransform = GameObject.FindWithTag("Pan").transform;
    }

    private void Start()
    {
        StartIngredient();
        // LevelManager.singleton.AddFood(this);
    }

    /// <summary>
    /// Sets the lst position
    /// </summary>
    /// <param name="position"></param>
    private void SetLastPosition(Vector3 position)
    {
        lastPosition = position;
    }

    /// <summary>
    /// The size raises from 0 to 0.5
    /// </summary>
    public void StartIngredient()
    {
        transform.DOScale(0.5f, 0.15f);
        Debug.Log(name + "started");
    }

    /// <summary>
    /// The size raises from 0.5 to 0
    /// </summary>
    public void FinishIngredient()
    {
        transform.DOScale(0, 0.15f);
        Debug.Log(name + "finished");
    }

    /// <summary>
    /// Return the ingredientName back
    /// </summary>
    /// <returns></returns>
    public string GetIngredientName()
    {
        return ingredientName;
    }

    /// <summary>
    /// Grabs the Ingredient
    /// </summary>
    public void Grab()
    {
        Debug.Log("Grab");
        // isGrab = true;
    }

    /// <summary>
    /// Deags into the incoming position
    /// </summary>
    /// <param name="mousePosition"></param>
    public void Drag(Vector3 _position)
    {
        Vector3 overrideMousePosition = new Vector3(_position.x, _position.y, transform.position.z);
        transform.position = overrideMousePosition;
    }

    /// <summary>
    /// Leaves the Ingredient
    /// </summary>
    public void Leave()
    {
        Debug.Log("Leave");
        if (isGrab)
        {
            isGrab = false;
            if (isInPan)
            {
                BackToLastPosition();
            }
            else
            {
                Debug.Log("Nothing! ");
            }
        }
    }

    /// <summary>
    /// Will send to the pan
    /// </summary>
    public void PutToPan()
    {
        Debug.Log("Put To Pan");
        if (!isInPan)
        {
            transform.DOMove(panTransform.position, 0.15f);
        }
    }

    /// <summary>
    /// Leads back to the last position
    /// </summary>
    public void BackToLastPosition()
    {
        Debug.Log("Back To Last Position");
        transform.DOMove(lastPosition, 0.5f);
    }

    /// <summary>
    /// Check if it is at meal
    /// </summary>
    private void ControlMeal()
    {
        Debug.Log("Control Meal");
        if (MealManager.singleton.ControlMeal(this))
        {
            // Debug.Log("Nothing! ");
            collider.enabled = false;
        }
        else
        {
            BackToLastPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pan"))
        {
            Debug.Log("In Pan");
            isInPan = true;
            ControlMeal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pan"))
        {
            Debug.Log("Out Pan");
            isInPan = false;
            ControlMeal();
        }
    }
}
