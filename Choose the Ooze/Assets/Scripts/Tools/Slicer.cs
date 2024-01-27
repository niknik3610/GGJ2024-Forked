using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{ 
    public SpriteMask hiddenIngredientPartOne;

    public void Slice(float slicePointX, Ingredient ingredient)
    {
        Transform maskTransform = hiddenIngredientPartOne.gameObject.transform;
        Vector3 cutPos = new Vector3(slicePointX + (hiddenIngredientPartOne.bounds.size.x / 2), maskTransform.transform.position.y, 0);
        maskTransform.position = cutPos;

        hiddenIngredientPartOne.gameObject.SetActive(true); 

        cutPos.x += 1;
        Object.Instantiate(ingredient, cutPos, Quaternion.identity);

   }

   public void HideMasks() {
        hiddenIngredientPartOne.gameObject.SetActive(false);
   }
}
