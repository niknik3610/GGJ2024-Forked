using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredinetName {get; set;}
    public bool beingCarried {get; set;} = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            Vector3 mousePos = Input.mousePosition;
            Slice(mousePos.y);
        }
    }

    public void Slice(float slicePointX) {
        SpriteMask mask = this.GetComponentInChildren<SpriteMask>();
        Transform maskTransform = mask.gameObject.transform;
        maskTransform.position = new Vector3(slicePointX, maskTransform.transform.position.y, 0);

        mask.gameObject.SetActive(true); 
   }
}
