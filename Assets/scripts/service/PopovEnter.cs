using System.Collections.Generic;
using UnityEngine;

public class PopovEnter : MonoBehaviour
{
    private static readonly Queue<Popov> popovsToDestroy = new();

    [SerializeField]
    private Popov popovWithCucumber;

    [SerializeField]
    private Popov popovWithEgg;


    public void DestroyYourself()
    {
        Destroy(gameObject);
    }

    public void SpawnPopovWithCucumber()
    {
        popovsToDestroy.Enqueue(Instantiate(popovWithCucumber, transform.position, Quaternion.identity));
    }

    public void SpawnPopovWithEgg()
    {
        popovsToDestroy.Enqueue(Instantiate(popovWithEgg, transform.position, Quaternion.identity));
    }

    public void Out()
    {
        Destroy(popovsToDestroy.Dequeue().gameObject);
    }
}