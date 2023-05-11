using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private float horizontalValue;
    private float verticalValue;
    private void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime;
        verticalValue = Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;
        transform.Translate(horizontalValue, 0, verticalValue);
    }
}
