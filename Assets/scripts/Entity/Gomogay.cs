using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;

public class Gomogay : MonoBehaviour
{
    private Rigidbody2D gomoRB;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Transform spawnPoint;
    private Transform playerTransform;
    private Vector2 outDirection;
    [SerializeField]
    private Sprite eye;
    [SerializeField]
    private Sprite gomogay;
    [SerializeField]
    private int attackState;

    [SerializeField]
    private float boostStrength=45;

    [SerializeField]
    private float outBoostStrength = 90;
    [SerializeField]
    private RotationTracker rotationTracker;

    private PlayableDirector firstAttack;
    private PlayableDirector secondAttack;
    private PlayableDirector thirdAttack;

    [SerializeField]
    private ConstantRotation constantRotation;
    


    public void SetNextState()
    {
        attackState++;
        switch (attackState)
        {
            case 1:
                rotationTracker.isEnable=true;
                firstAttack.Play();
                break;
            case 2:
                rotationTracker.isEnable=false;
                gomoRB.velocity=Vector2.zero;
                spriteRenderer.sprite=eye;
                secondAttack.Play();
                break;
            case 3:
                spriteRenderer.sprite = gomogay;
                constantRotation.EnableRotation(3000);
                thirdAttack.Play();
                break;
            case 4:
                var temp = playerTransform.position - transform.position;
                outDirection = new Vector2(temp.x, temp.y).normalized;
                break;
            
        }
    }
    void Start()
    {
        gomoRB=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        firstAttack = GameObject.Find("FirstAttack").GetComponent<PlayableDirector>();
        secondAttack = GameObject.Find("SecondAttack").GetComponent<PlayableDirector>();
        thirdAttack= GameObject.Find("ThirdAttack").GetComponent<PlayableDirector>();
    }

    public void EndGomogayAttack()
    {
        gomoRB.angularVelocity = 0;
        transform.position=spawnPoint.position;
        transform.rotation=Quaternion.identity;
        gomoRB.velocity=Vector2.zero;
        constantRotation.IsEnable = false;
        attackState = 0;
    }

    void FixedUpdate()
    {
        if (attackState == 1)
        {
            gomoRB.AddRelativeForce(Vector2.up*boostStrength);
        }

        if (attackState == 4)
        {
            gomoRB.AddForce(outDirection*outBoostStrength);
        }
    }

    void Update()
    {
        
    }
}
