using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{ 
    public void Slice(float slicePointX)
    {
        SpriteMask hidingMask = this.GetComponentInChildren<SpriteMask>();
        hidingMask.gameObject.SetActive(false);

        Transform maskTransform = hidingMask.gameObject.transform;
        maskTransform.position = new Vector3(slicePointX, maskTransform.transform.position.y, 0);

        hidingMask.gameObject.SetActive(true); 
   }
}
