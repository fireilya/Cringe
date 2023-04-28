using System.Linq;
using UnityEngine;

public class PlayerHitFromTriggersController : MonoBehaviour
{
    [SerializeField]
    public HitFromTrigger[] HitFromTriggers;

    [SerializeField]
    public Player triggerSource;

    void Start()
    {
        HitFromTriggers = triggerSource.GetHitFromTriggers();
    }

    public void SetOtherTriggersLockValue(int callingSide, bool lockValue)
    {
        for (int i = 0; i < HitFromTriggers.Length; i++)
        {
            if (i == callingSide && !HitFromTriggers[i].isLocked) continue;
            HitFromTriggers[i].isLocked = lockValue;
        }
    }

    void Update()
    {
        if (!HitFromTriggers.All(x => x.isLocked)) return;
        foreach (var trigger in HitFromTriggers)
        {
            trigger.isLocked=false;
        }
    }
}
