using UnityEngine;

public class HitFromTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayerHitFromTriggersController controller;
    [SerializeField]
    private int sideCode;


    public bool isLocked;
    private bool isLockOther;

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!isLocked)
        {
            controller.SetOtherTriggersLockValue(sideCode, false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isLocked)
        {
            controller.SetOtherTriggersLockValue(sideCode, true);
        }
    }
}
