using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField]
    private GameObject explode;

    private bool isLaunched;
    private Transform morgen;

    [SerializeField]
    private readonly float moveSpeed = 8f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name is "Morgen" or "StaticEnemyShield(Clone)" or "MorgenMouth" && isLaunched)
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        morgen = GameObject.FindWithTag("Morgen").GetComponent<Transform>();
    }

    public void Launch()
    {
        isLaunched = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isLaunched)
            transform.position = Vector3.MoveTowards(transform.position, morgen.position, moveSpeed * Time.deltaTime);
    }
}