using System.Collections.Generic;
using UnityEngine;

public class PopovEnter : MonoBehaviour
{
    private static Queue<Popov> popovsToDestroy=new();

    [SerializeField]
    private Popov popovWithCucumber;

    [SerializeField]
    private Popov popovWithEgg;

    public static void ClearQueue()
    {
        popovsToDestroy.Clear();
    }

    public void DestroyYourself()
    {
        Destroy(gameObject);
    }

    public void SpawnPopovWithCucumber()
    {
        popovsToDestroy.Enqueue(Instantiate(popovWithCucumber, transform.position, Quaternion.identity));
        Debug.Log("Cucumber");
    }

    public void SpawnPopovWithEgg()
    {
        popovsToDestroy.Enqueue(Instantiate(popovWithEgg, transform.position, Quaternion.identity));
        Debug.Log("Egg");
    }

    public void Out()
    {
        Destroy(popovsToDestroy.Dequeue().gameObject);
    }
}