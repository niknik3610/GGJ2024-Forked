using UnityEngine;
using UnityEngine.InputSystem;

public class CuttingBoardMinigame : MonoBehaviour
{
    Camera mainCamera; 
    public Ingredient ingredient;
    public GameObject splitIngredient;
    public Slicer slicer;

    void Awake() {
        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        slicer.HideMasks();
        splitIngredient.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame) {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.collider.gameObject == ingredient.gameObject)
                {
                    float xSlicePos = mainCamera.ScreenToWorldPoint(mousePosition).x;
                    slicer.Slice(xSlicePos, ingredient);
                }
            }
        }
    }
}
