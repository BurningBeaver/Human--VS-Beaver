using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=
            (Vector3)(10 * (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized *
            Time.deltaTime);
    }
}