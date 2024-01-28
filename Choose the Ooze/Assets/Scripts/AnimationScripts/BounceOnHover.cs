using UnityEngine;

public class BounceOnHover : MonoBehaviour
{

    public float amplitude = 0.0007f;
    public float speed = 4f;
    private bool wasHovering;
    private Vector3 originalLocation;
    // Start is called before the first frame update
    void Start()
    {
        wasHovering = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (MouseHelper.isBeingHoveredThisFrame(this.gameObject)) {
            bounce();
            wasHovering = true;
        }
        else if (wasHovering) {
            this.transform.position =  originalLocation;
            wasHovering = false;
        }
        else {
            originalLocation = this.transform.position;
        }
    }


    void bounce () {
        Vector3 newPos = this.transform.position;
        newPos.y += amplitude * Mathf.Cos(Time.time * speed);
        this.transform.position = newPos;
    }
}
