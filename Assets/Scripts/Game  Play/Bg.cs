using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg : MonoBehaviour
{
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    [SerializeField] private float movementSpeed = 1f;

    private bool isMovingUp;
    private Vector3 startingPosytion;

    private void Start()
    {
        startingPosytion = transform.position;
    }

    private void Update()
    {
        float moveValue = movementSpeed * Time.deltaTime;
        if (isMovingUp)
        {
            Vector3 targrtPosition = startingPosytion + new Vector3(0, maxY);
            transform.position = Vector3.MoveTowards(transform.position, targrtPosition, moveValue);
            if(transform.position == targrtPosition)
            {
                isMovingUp = false;
            }
        } else {
            Vector3 targrtPosition = startingPosytion + new Vector3(0, minY);
            transform.position = Vector3.MoveTowards(transform.position, targrtPosition, moveValue);
            if (transform.position == targrtPosition)
            {
                isMovingUp = true;
            }
        }
    }
}
