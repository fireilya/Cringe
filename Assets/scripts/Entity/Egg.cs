using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private Transform morgen;

    [SerializeField]
    private GameObject explode;

    [SerializeField]
    private float moveSpeed=8f;

    private bool isLaunched;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name is "Morgen" or "StaticEnemyShield(Clone)" or "MorgenMouth" && isLaunched)
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        morgen = GameObject.FindWithTag("Morgen").GetComponent<Transform>();
    }

    public void Launch()
    {
        isLaunched=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched)
        {
            transform.position = Vector3.MoveTowards(transform.position, morgen.position, moveSpeed * Time.deltaTime);
        }
    }
}
