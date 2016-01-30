using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public Vector3 rotation;
    public float speedMultiplier = 1;

    public AnimationCurve yMovement;
    public float yMovementAmplitude = 1;
    public float yMovementSpeed = 1f;
    private float initialYPos;
    float timePassed = 0;

    void Start()
    {
        timePassed = 0;
        initialYPos = transform.position.y;
    }

    void Update()
    {
        timePassed += (Time.deltaTime * yMovementSpeed);
        if (timePassed > 1)
            timePassed = 0;

        transform.Rotate(rotation * speedMultiplier * Time.deltaTime);
        transform.position = new Vector3 (transform.position.x, initialYPos + yMovement.Evaluate(timePassed) * yMovementAmplitude, transform.position.z);
    }
}
