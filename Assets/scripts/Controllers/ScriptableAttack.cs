using UnityEngine;

public class ScriptableAttack : MonoBehaviour
{
    [SerializeField]
    private TripleLaserAttack tripleLaserAttack;
    [SerializeField]
    private NegrGunAttack negrGunAttack;

    void Start()
    {
        
    }

    void Update()
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
