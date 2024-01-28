using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class GrindingMinigame : MonoBehaviour
{
    public GameObject powder;
    public Ingredient raw;
    public List<BoxCollider2D> sensors;
    public SpriteMask mask;
    public Pestle pestle;
    private float startingY;

    const float GRIND_SPEED = 4f;

    BoxCollider2D pestleCollider;
    Collider2D triggeredSensor;
    float grinding_percentage;

    public void EndMinigame()
    {
        pestle.gameObject.SetActive(false);
    }
    public void StartMinigame()
    {
        pestle.gameObject.SetActive(true);
        switch (raw.currentLevels.grindLevel)
        {
            case GrindLevel.None:
                grinding_percentage = 0;
                break;

            case GrindLevel.Low:
                grinding_percentage = 0.3f;
                break;

            case GrindLevel.Medium:
                grinding_percentage = 0.6f;
                break;

            case GrindLevel.High:
                grinding_percentage = 1;
                break;
        }

        UpdatePosition();
    }

    void UpdatePosition()
    {
        // Move the raw and powder ingredients per the grinding percentage
        raw.transform.position = new Vector3(
            raw.transform.position.x,
            startingY
            -(startingY-mask.gameObject.transform.position.y)*grinding_percentage,
            raw.transform.position.z
        );
        powder.transform.position = new Vector3(
            powder.transform.position.x,
            -2.4f + (grinding_percentage * 1.6f),
            powder.transform.position.z
        );
    }

    void ResetMinigame()
    {
        pestleCollider = pestle.GetComponent<BoxCollider2D>();

        switch (raw.currentLevels.grindLevel)
        {
            case GrindLevel.None:
                grinding_percentage = 0;
                break;

            case GrindLevel.Low:
                grinding_percentage = 0.3f;
                break;

            case GrindLevel.Medium:
                grinding_percentage = 0.6f;
                break;

            case GrindLevel.High:
                grinding_percentage = 1;
                break;
        }

        UpdatePosition();
    }

    void UpdatePosition()
    {
        // Move the raw and powder ingredients per the grinding percentage
        raw.transform.position = new Vector3(
            raw.transform.position.x,
            -grinding_percentage,
            raw.transform.position.z
        );
        powder.transform.position = new Vector3(
            powder.transform.position.x,
            grinding_percentage - 1.5f,
            powder.transform.position.z
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        pestleCollider = pestle.GetComponent<BoxCollider2D>();
        startingY = raw.transform.position.y;
        // raw = FindObjectOfType<MouseFollower>().ingredientBeingCarried;
        EndMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (grinding_percentage >= 1) return;
        List<Collider2D> triggeredSensors = new List<Collider2D>();
        int count = pestleCollider.OverlapCollider(
            new ContactFilter2D().NoFilter(),
            triggeredSensors
        );
        if (triggeredSensors.FirstOrDefault() != null && triggeredSensors.FirstOrDefault() != triggeredSensor)
        {
            grinding_percentage += GRIND_SPEED * Time.deltaTime;
            if (grinding_percentage > 1)
            {
                grinding_percentage = 1;
            }
        }
        triggeredSensor = triggeredSensors.FirstOrDefault();

        switch (grinding_percentage)
        {
            case float n when n < 0.3f:
                raw.currentLevels.grindLevel = GrindLevel.None;
                break;

            case float n when n < 0.6f:
                raw.currentLevels.grindLevel = GrindLevel.Low;
                break;

            case float n when n < 1:
                raw.currentLevels.grindLevel = GrindLevel.Medium;
                break;

            case float n when n == 1:
                raw.currentLevels.grindLevel = GrindLevel.High;
                break;
        }

        UpdatePosition();
    }
}
