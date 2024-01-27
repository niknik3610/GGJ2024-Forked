using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{ 
    public SpriteMask hiddenIngredientPartOne;
    public SpriteMask hiddenIngredientPartTwo;

    public void Slice(float slicePointX, Ingredient ingredient)
    {
        Transform maskTransform = hiddenIngredientPartOne.gameObject.transform;
        maskTransform.position = new Vector3(slicePointX, maskTransform.transform.position.y, 0);

        hiddenIngredientPartOne.gameObject.SetActive(true); 
   }

   public void HideMasks() {
        hiddenIngredientPartOne.gameObject.SetActive(false);
        hiddenIngredientPartTwo.gameObject.SetActive(false);
   }
}
