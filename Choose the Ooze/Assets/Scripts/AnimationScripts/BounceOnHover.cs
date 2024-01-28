using UnityEngine;

public class BounceOnHover : MonoBehaviour
{

    public float amplitude = 0.007f;
    public float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver() {
        Vector3 newPos = this.transform.position;
        newPos.y += amplitude * Mathf.Cos(Time.time * speed);
        this.transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
