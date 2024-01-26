using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ingredient : MonoBehaviour
{
    public string ingredinetName {get; set;}
    public bool beingCarried {get; set;} = false;
    private Camera m_Camera;

    public SpriteMask hidingMask;

    void Awake()
    {
        m_Camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        hidingMask = this.GetComponentInChildren<SpriteMask>();
        hidingMask.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.collider.gameObject == this.gameObject)
                {
                    Slice(Camera.main.ScreenToWorldPoint(mousePosition).y);
                }
            }
        }
    }

    public void Slice(float slicePointX) {
        Transform maskTransform = hidingMask.gameObject.transform;
        maskTransform.position = new Vector3(slicePointX, maskTransform.transform.position.y, 0);

        hidingMask.gameObject.SetActive(true); 
   }
}
