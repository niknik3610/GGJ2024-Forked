using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    public Ingredient ingredientBeingCarried = null;
    public Potion potionBeingCarried = null;
    public bool ingredientInProcess = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        gameObject.transform.position = mousePos;
    }

    public void SetIngredient(Ingredient ing)
    {
        ingredientBeingCarried = ing;
        ing.transform.SetParent(transform);
        ing.transform.SetLocalPositionAndRotation(new Vector3(), Quaternion.identity);
    }
}
