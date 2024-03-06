using System.Collections;
using UnityEngine;

public class NiggerDropper : MonoBehaviour
{
    [SerializeField]
    private NiggerOnParachute niggerToDrop;

    private readonly float offset = 1.2566f;

    public IEnumerator DropNiggers()
    {
        for (var j = 0; j < 3; j++)
        {
            for (var i = -offset * 2; i <= offset * 2; i += offset)
                Instantiate(niggerToDrop,
                    new Vector3(transform.localPosition.x + i, transform.localPosition.y, transform.localPosition.z),
                    Quaternion.identity);

            yield return new WaitForSeconds(1.5f);
        }
    }

    public void StartDropping()
    {
        StartCoroutine(DropNiggers());
    }
}