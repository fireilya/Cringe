using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopovEnter : MonoBehaviour
{
    [SerializeField]
    private GameObject popovWithCucumber;
    [SerializeField]
    private GameObject popovWithEgg;

    public void SpawnPopovWithCucumber()
    {
        Instantiate(popovWithCucumber);
    }

    public void SpawnPopovWithEgg()
    {
        Instantiate(popovWithEgg);
    }
}
