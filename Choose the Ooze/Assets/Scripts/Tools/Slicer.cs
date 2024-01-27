using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Slicer : MonoBehaviour
{ 
    public SpriteMask ingredientOneMask;

    public void Slice(float slicePointX, Ingredient ingredient)
    {
        SpriteRenderer ingrRenderer = ingredient.GetComponent<SpriteRenderer>();
        float ingrWidth = ingrRenderer.bounds.size.x;
        float ingrHeight = ingrRenderer.bounds.size.y;

        Transform maskTransform = ingredientOneMask.gameObject.transform;

        maskTransform.position = new Vector3(
            ingredient.transform.position.x,
            ingredient.transform.position.y
        );

        maskTransform.localScale = new Vector3(1, 2);

        float ingrLeftPercent = (slicePointX + (ingrWidth / 2)) / ingrWidth;
        print(ingrLeftPercent);

        float scaleForSecondPartWidth = 1 - ingrLeftPercent;

        maskTransform.localScale = new Vector3(
            ingrHeight,
            maskTransform.localScale.x * scaleForSecondPartWidth * 2
        );

        Vector3 cutPos = new Vector3(slicePointX + (ingredientOneMask.bounds.size.x / 2), maskTransform.transform.position.y, 0);
        maskTransform.position = cutPos;

        ingredientOneMask.gameObject.SetActive(true); 

        Ingredient cutPiece = Object.Instantiate(
            ingredient,
            new Vector3(
                ingredient.transform.position.x + 5,
                Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y + ingredientOneMask.bounds.size.y / 2
            ),
            Quaternion.identity
        );
        cutPiece.transform.rotation *= Quaternion.Euler(0, 0, -90);
        SpriteMask partMask = cutPiece.GetComponentInChildren<SpriteMask>();

        partMask.transform.localScale = new Vector3(ingrHeight, ingrLeftPercent);
        partMask.transform.rotation *= Quaternion.Euler(0, 0, -180);


        // SpriteMask cutPieceMask = Object.Instantiate(ingredientOneMask, cutPos, Quaternion.identity);


        // cutPieceMask.transform.rotation *= Quaternion.Euler(0, -90, 0);

        // cutPieceMask.gameObject.SetActive(false);
    }

   public void HideMasks() {
        ingredientOneMask.gameObject.SetActive(false);
   }
}
