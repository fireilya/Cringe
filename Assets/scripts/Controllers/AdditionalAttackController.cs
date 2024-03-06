using Assets.scripts;
using UnityEngine;
using UnityEngine.Playables;
using Random = System.Random;

public class AdditionalAttackController : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector[] attacks;

    [SerializeField]
    private Timer attackTimer;

    private PlayableDirector currentAttack;
    private bool isAttackAllowed;
    private readonly Random random = new();

    public void AllowAttack(bool isStop)
    {
        attackTimer.StartTimer(random.Next(5, 15));
        isAttackAllowed = true;
        if (isStop) currentAttack.Stop();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAttackAllowed && attackTimer.IsEnded)
        {
            var newAttack = random.Next() % attacks.Length;
            attacks[newAttack].Play();
            currentAttack = attacks[newAttack];

            //attacks[1].Play();
            //currentAttack = attacks[1];

            isAttackAllowed = false;
        }
    }
}