using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titor : MonoBehaviour
{
    [SerializeField]
    private Rocket[] rockets;

    [SerializeField]
    private GameObject[] bonuses;

    public void DropBonuses()
    {
        foreach (var bonus in bonuses)
        {
            bonus.transform.parent=null;
            var bonusRB=bonus.GetComponent<Rigidbody2D>();
            bonusRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void LaunchRockets()
    {
        foreach (var rocket in rockets)
        {
            rocket.transform.parent=null;
            rocket.Launch();
        }
    }

    public void DestroyTitor()
    {
        Destroy(gameObject);
    }
}
