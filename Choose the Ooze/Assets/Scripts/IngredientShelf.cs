using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientShelf : MonoBehaviour
{
    public GameObject ingredient;
    public IngredientDetails.Material material;
    private MouseFollower _mouseFollower;
    // Start is called before the first frame update
    private Camera m_Camera;
    void Awake()
    {
        _mouseFollower = FindObjectOfType<MouseFollower>();
        m_Camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(MouseHelper.wasClickedThisFrame(gameObject))
        {
            string sound = new string[]{ "creak1", "creak2","creak3","creak4" }[Random.Range(0, 4)];
            AudioManager.instance.Play(sound);
            if(_mouseFollower.ingredientInProcess == false && _mouseFollower.potionBeingCarried == null)
            {
                    GameObject ingredientInstance = Instantiate(ingredient, new Vector3(0, 0, 0), Quaternion.identity, _mouseFollower.transform);
                    ingredientInstance.transform.SetLocalPositionAndRotation(new Vector3(), Quaternion.identity);
                    if(_mouseFollower.ingredientBeingCarried != null)
                    {
                        Destroy(_mouseFollower.ingredientBeingCarried.gameObject);

                    }
                    _mouseFollower.ingredientBeingCarried = ingredientInstance.GetComponent<Ingredient>();
                    _mouseFollower.ingredientBeingCarried.currentLevels = new ProcessingLevels(false);
                    _mouseFollower.ingredientBeingCarried.material = material;
            }
        }
    }
}
