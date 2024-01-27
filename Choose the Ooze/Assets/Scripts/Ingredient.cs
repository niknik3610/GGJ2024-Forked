using UnityEngine;
using UnityEngine.InputSystem;



public class Ingredient : MonoBehaviour
{
    public Material material;
    public ProcessingLevels requiredLevels;
    public ProcessingLevels currentLevels;
    public bool beingCarried {get; set;} = false;
    private Camera m_Camera;

    void Awake()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    } 
}
