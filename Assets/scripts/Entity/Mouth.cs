using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField]
    private Morgen morgen;
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "rocket":
                morgen.Hit(50);
                break;
            case "bullet":
                morgen.Hit(1);
                break;
        }
    }
}
