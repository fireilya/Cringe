using Assets.scripts.service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotationTracker : MonoBehaviour
{
    private Transform target;

    private float currentRotation;
    private float neededRotation;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float prefabOffset;

    [SerializeField]
    private string targetTag;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = (currentRotation < 0 ? 360 + currentRotation : currentRotation) % 360;
        neededRotation = VectorWorker.FindRotationByTarget(transform.position, target.position);
        currentRotation =
            VectorWorker.RotateToTargetWithSpeed(currentRotation, neededRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation + prefabOffset);
    }
}
