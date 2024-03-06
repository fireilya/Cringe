using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 200f;

    public bool IsEnable;


    // Update is called once per frame
    private void Update()
    {
        if (IsEnable) transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    public void EnableRotation(float rotationSpeed)
    {
        IsEnable = true;
        _rotationSpeed = rotationSpeed;
    }
}