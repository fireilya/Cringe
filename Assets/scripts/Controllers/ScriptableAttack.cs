using UnityEngine;

public class ScriptableAttack : MonoBehaviour
{
    [SerializeField]
    private NegrGunAttack negrGunAttack;

    [SerializeField]
    private TripleLaserAttack tripleLaserAttack;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void StartNegrGunAttack()
    {
        negrGunAttack.StartAttack();
    }

    public void StartTripleLaserAttack()
    {
        tripleLaserAttack.StartAttack();
    }
}