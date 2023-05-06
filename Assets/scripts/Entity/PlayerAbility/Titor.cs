using UnityEngine;

public class Titor : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bonuses;

    [SerializeField]
    private Rocket[] rockets;

    public void DropBonuses()
    {
        foreach (var bonus in bonuses)
        {
            bonus.transform.parent = null;
            var bonusRB = bonus.GetComponent<Rigidbody2D>();
            bonusRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void LaunchRockets()
    {
        foreach (var rocket in rockets)
        {
            rocket.transform.parent = null;
            rocket.Launch();
        }
    }

    public void DestroyTitor()
    {
        Destroy(gameObject);
    }
}