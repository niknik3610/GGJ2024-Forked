using System;
using UnityEngine;

public class Slicer : MonoBehaviour
{ 
    const int PART_OFF_SET = 10;

    //returns the percentage of the left piece of the ingredient
    public float Slice(float slicePointX, Ingredient ingredient, SpriteMask ingredientOneMask)
    {
        Vector2 aspectRatio = new Vector2(2, 2);

        SpriteRenderer ingrRenderer = ingredient.GetComponent<SpriteRenderer>();
        float ingrWidth = ingrRenderer.bounds.size.x;
        float ingrHeight = ingrRenderer.bounds.size.y;

        Transform maskTransform = ingredientOneMask.gameObject.transform;

        maskTransform.position = new Vector3(
            ingredient.transform.position.x,
            ingredient.transform.position.y
        );

        maskTransform.localScale = aspectRatio;

        float leftBoundIngr = Math.Abs(ingredient.transform.position.x) - (ingrWidth / 2); //left most point
        float localLocationSlicePointX = Math.Abs(slicePointX) - leftBoundIngr;

        float ingrLeftPercent = localLocationSlicePointX / ingrWidth;
        float scaleForSecondPartWidth = 1 - ingrLeftPercent;

        maskTransform.localScale = new Vector3(
            aspectRatio.y,
            -1 * aspectRatio.x * scaleForSecondPartWidth
        );

        Vector3 cutPos = new Vector3(slicePointX - (scaleForSecondPartWidth * ingrWidth / 2), maskTransform.transform.position.y, 0);
        maskTransform.position = cutPos;

        ingredientOneMask.gameObject.SetActive(true);

        Ingredient cutPiece = UnityEngine.Object.Instantiate(
            ingredient,
            new Vector3(
                ingredient.transform.position.x + PART_OFF_SET,
                Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y + ingredientOneMask.bounds.size.y / 2
            ),
            Quaternion.identity
        );

        cutPiece.transform.rotation *= Quaternion.Euler(0, 0, -90);

        SpriteMask partMask = UnityEngine.Object.Instantiate(ingredientOneMask, new Vector3(), Quaternion.identity);
        partMask.transform.rotation *= Quaternion.Euler(0, 0, -90);

        partMask.gameObject.transform.localScale = aspectRatio;
        partMask.gameObject.transform.localScale = new Vector3(
            aspectRatio.y,
            -1 * aspectRatio.x * ingrLeftPercent
        );

        partMask.gameObject.transform.position = new Vector3(
            slicePointX + PART_OFF_SET + (ingrLeftPercent * ingrWidth / 2),
            Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y + ingredientOneMask.bounds.size.y / 2
        );

        return ingrLeftPercent;
    }

   public void HideMasks() {
   }
}
