using System;
using UnityEngine;
using DG.Tweening;

public class Ingredient : MonoBehaviour
{
    [Header("Type")]
    [Tooltip("The picture of ingredient")]
    public Sprite ingredientIcon;

    [SerializeField]
    [Tooltip("The name of ingredient")]
    private string ingredientName;

    [Header("Components")]
    [SerializeField]
    private Collider collider;

    // PRIVATE PARAMETERS
    // The final position before the start of the action
    private Vector3 lastPosition;
    // Indicates whether into the pand or not
    private bool isInPan;
    // Indicates whether held or not
    private bool isGrab;

    // Reference to pan transform for the knowledge that he enetered or not
    private Transform panTransform;
    private void Awake()
    {
        SetLastPosition(transform.position);
        collider = GetComponent<Collider>();
        panTransform = GameObject.FindWithTag("Pan").transform;
    }

    /// <summary>
    /// Sets the last position
    /// </summary>
    /// <param name="position"></param>
    private void SetLastPosition(Vector3 position)
    {
        lastPosition = position;
    }

    /// <summary>
    /// The size raises from zero to normal
    /// </summary>
    public void StartIngredient()
    {
        transform.DOScale(1, 0.15f);
        // Debug.Log(name + "started");
    }

    /// <summary>
    /// The size raises from normal to zero
    /// </summary>
    public void FinishIngredient()
    {
        transform.DOScale(0, 0.15f).SetEase(Ease.InBack);
        Destroy(gameObject, 1);
        // Debug.Log(name + "finished");
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
        // Debug.Log("Grab");
        isGrab = true;
        IngredientManager.singleton.PlayGrabSound();
    }

    /// <summary>
    /// Drags into the incoming position
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
        // Debug.Log("Leave");
        if (isGrab)
        {
            isGrab = false;
            if (isInPan)
            {
                BackToLastPosition();
            }
        }
    }

    /// <summary>
    /// Will send to the pan
    /// </summary>
    public void PutToPan()
    {
        // Debug.Log("Put To Pan");
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
        // Debug.Log("Back To Last Position");
        transform.DOMove(lastPosition, 0.5f);
    }

    /// <summary>
    /// Check if it is at meal
    /// </summary>
    private void ControlMeal()
    {
        // Debug.Log("Control Meal");
        if (MealManager.singleton.ControlMeal(this))
        {
            // Debug.Log("Nothing! ");
            collider.enabled = false;
            IngredientManager.singleton.PlayFrySound();
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
            // Debug.Log("In Pan");
            isInPan = true;
            ControlMeal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pan"))
        {
            // Debug.Log("Out Pan");
            isInPan = false;
            ControlMeal();
        }
    }
}
