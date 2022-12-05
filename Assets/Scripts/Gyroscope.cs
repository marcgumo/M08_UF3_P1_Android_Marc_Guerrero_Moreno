using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    public Transform rightLimit, leftLimit;
    public float speed;

    void Start()
    {
        
    }
    
    void Update()
    {
        print(Input.acceleration);

        float currentSpeed = speed;

        try
        {
            Transform child0 = transform.GetChild(0);

            if (Input.acceleration.x > 0)
            {
                if (child0.position.x > rightLimit.position.x)
                {
                    currentSpeed = 0;
                }
            }
            else if (Input.acceleration.x < 0)
            {
                if (child0.position.x < leftLimit.position.x)
                {
                    currentSpeed = 0;
                }
            }
        }
        catch
        {

        }

        transform.position += currentSpeed * Input.acceleration.x * Time.deltaTime * Vector3.right;

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit.position.x, rightLimit.position.x),
        //    transform.position.y);
    }
}
