using Assets.scripts.service;
using UnityEngine;

public class RotationTracker : MonoBehaviour
{
    private float currentRotation;

    public bool isEnable;
    private float neededRotation;

    [SerializeField]
    private float prefabOffset;

    [SerializeField]
    private float rotationSpeed;

    private Transform target;

    [SerializeField]
    private string targetTag;

    public void Toggle()
    {
        isEnable = !isEnable;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();
    }

    private void Update()
    {
        if (!isEnable) return;
        currentRotation = (currentRotation < 0 ? 360 + currentRotation : currentRotation) % 360;
        neededRotation = VectorWorker.FindRotationByTarget(transform.position, target.position);
        currentRotation =
            VectorWorker.RotateToTargetWithSpeed(currentRotation, neededRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles =
            new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation + prefabOffset);
    }
}