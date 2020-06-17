using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCube : MonoBehaviour
{
    public bool Triggered;
    void Start()
    {
        Triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Triggered = true;
    }
}
