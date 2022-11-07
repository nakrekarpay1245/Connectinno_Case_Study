using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSelector : MonoBehaviour
{
    // CONSTANTS
    private const float DOUBLE_CLICK_TIME = 0.2f;

    // PRIVATE PARAMETERS
    private Vector3 raycastPosition;

    private RaycastHit raycastHit;
    private Ray ray;

    private Camera raycastCamera;

    private Ingredient currentIngredient;

    private float lastClickTime;

    private void Awake()
    {
        raycastCamera = Camera.main;
    }

    private void Update()
    {
        ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                float timeSinceLastClick = Time.time - lastClickTime;
                if (raycastHit.collider.TryGetComponent<Ingredient>(out currentIngredient))
                {
                    if (currentIngredient)
                    {
                        if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
                        {
                            currentIngredient.PutToPan();
                            // Debug.Log("Double Tap : " + timeSinceLastClick);
                        }
                        else
                        {
                            currentIngredient.Grab();
                            // Debug.Log("Only Tap : " + timeSinceLastClick);
                        }
                    }
                }
                lastClickTime = Time.time;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (currentIngredient)
                {
                    currentIngredient.Drag(raycastHit.point);
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                // Debug.Log("Mouse UP ");
                if (currentIngredient)
                {
                    currentIngredient.Leave();
                    currentIngredient = null;
                }
            }
        }
    }
}
