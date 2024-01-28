using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Potion : MonoBehaviour
{
    public float value;
    public ClientRequest potionContents;
    private MouseFollower _mouseFollower;
    public float perfectValue;
    public void SetPotion(Potion p)
    {
        value = p.value;
        potionContents = p.potionContents;
    }
    void Awake()
    {
        potionContents = new ClientRequest();
        value = 0;
        _mouseFollower = FindObjectOfType<MouseFollower>();
    }
    void Update()
    {
        Mouse mouse = Mouse.current;
        
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            var raycastHits = Physics.RaycastAll(ray);
            foreach(var rhit in raycastHits)
            {
                if(rhit.collider.gameObject == gameObject && _mouseFollower.potionBeingCarried == null)
                {
                    gameObject.transform.SetParent(_mouseFollower.transform);
                    _mouseFollower.potionBeingCarried = this;
                    transform.position = _mouseFollower.transform.position;
                }
            }
        }
    }
    public void AddIngredient(Ingredient ingredient)
    {
        Cauldron c = FindObjectOfType<Cauldron>();
        potionContents.Request_required_ingredients.Add((_mouseFollower.ingredientBeingCarried.material.emotion, _mouseFollower.ingredientBeingCarried.material.severity));
        if(_mouseFollower.ingredientBeingCarried.material.severity == Severity.Standard)
        {
            perfectValue += c.perfectWeakPotionValue;
        }
        else
        {
            perfectValue += c.perfectWeakPotionValue * c.strongPotionMultiplier;
        }
    }   
}
