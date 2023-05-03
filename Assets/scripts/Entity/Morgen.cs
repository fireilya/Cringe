using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Morgen : MonoBehaviour
{
    protected int health;

    [SerializeField, FormerlySerializedAs("text")]
    protected TMP_Text MorgenHealth;
    [SerializeField]
    private GameController gameController;
    void Start()
    {
        MorgenHealth.text = health.ToString();
    }

    public void ResetCurrentState(int state)
    {
        switch (state)
        {
            case 0:
                health = 1000;
                MorgenHealth.text = health.ToString();
                break;
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health<=0)
        {
            gameController.Win();
        }
        MorgenHealth.text = health.ToString();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "rocket":
                Hit(50);
                break;
            case "bullet":
                Hit(1);
                break;
            case "Egg":
                Hit(40);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
