using UnityEngine;
using UnityEngine.InputSystem;

public class Ingredient : MonoBehaviour
{
    public string ingredientName {get; set;}
    public bool beingCarried {get; set;} = false;
    private Camera m_Camera;
    public Slicer slicer;

    void Awake()
    {
        m_Camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    } 
}
