using UnityEngine;
using UnityEngine.Playables;
using Random = System.Random;

public class AttackController : MonoBehaviour
{
    private int attackWithoutSupply;
    private int SupplyAttackRare = 10;
    private int attackAmount;
    private bool isChooseAllowed = true;
    private Random random = new();
    private int currentAttack;
    private int previousAttack = -1;

    [SerializeField]
    private PlayableDirector[] directors;

    void Start()
    {
        attackAmount = directors.Length;
    }

    public void AllowAttack(bool isStop)
    {
        isChooseAllowed = true;
        if (isStop)
        {
            directors[currentAttack].Stop();
        }
    }

    public void LoseGame()
    {
        isChooseAllowed = false;
        foreach (var director in directors)
        {
            director.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChooseAllowed)
        {
            //var newAttack = -1;
            //do
            //{
            //    newAttack = random.Next(0, int.MaxValue) % attackAmount;

            //} while (newAttack == previousAttack || (newAttack == 3 && attackWithoutSupply < SupplyAttackRare));

            //if (newAttack == 3)
            //{
            //    attackWithoutSupply = 0;
            //}
            //else
            //{
            //    attackWithoutSupply++;
            //}
            //previousAttack = newAttack;
            //currentAttack = newAttack;
            //Debug.Log(currentAttack);
            //directors[currentAttack].Play();

            //currentAttack = 8;
            //directors[8].Play();
            isChooseAllowed = false;
        }
    }
}

