using UnityEngine;

public class MainEnemyMouth : Morgen
{
    public void Hit(int damage)
    {
        health -= damage;
        MorgenHealth.text = health.ToString();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "rocket")
        {
            Hit(50);
        }
    }
}
