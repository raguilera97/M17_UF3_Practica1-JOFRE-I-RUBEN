using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGameSimulator : MonoBehaviour
{
    public float currentTime = 0f;
    private float startingTime = 60f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
