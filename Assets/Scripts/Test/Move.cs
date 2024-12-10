using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Speed of the movement

    [SerializeField]
    private bool move = false; // Speed of the movement

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            // Move the object along the x-axis
            transform.position += new Vector3( 0, 0, Time.deltaTime);
        }
    }

    public void updateHit(){
        move = false; 
    }
}
