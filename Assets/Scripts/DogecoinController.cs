using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogecoinController : MonoBehaviour
{
    float fallSpeed = -0.08f;
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        float x = ((float) random.NextDouble()*4f) - 2f;
        this.transform.position = new Vector3(x, 5.5f, 1);
    }


    void FixedUpdate()
    {
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + fallSpeed, this.transform.position.z);
    }
}
