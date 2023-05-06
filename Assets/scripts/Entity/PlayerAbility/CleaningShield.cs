using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CleaningShield : MonoBehaviour
{
    [SerializeField]
    private float growSpeed;
    [SerializeField]
    private float maxScale;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(
            transform.localScale.x + growSpeed * Time.deltaTime,
            transform.localScale.y + growSpeed * Time.deltaTime,
            transform.localScale.z + growSpeed * Time.deltaTime);
        if (transform.localScale.x > maxScale)
        {
            Destroy(gameObject);
        }
    }
}
