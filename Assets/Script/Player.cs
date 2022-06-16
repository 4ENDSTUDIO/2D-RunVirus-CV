using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float maxXVelocity = 100;
    public float maxAcceleration = 10;
    public float accelaration = 10;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrouded = false;
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;
    public float jumpGroundThreshold = 1;


    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if(isGrouded || groundDistance <= jumpGroundThreshold)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Jump", true);

                isGrouded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }

        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
            isHoldingJump = false;
        }

        if(isGrouded == true)
        {
            anim.SetBool("Jump", false);
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if(!isGrouded)
        {
            if(isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;
            if(!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            
            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrouded = true;
              
            }
        }
        distance += velocity.x * Time.fixedDeltaTime;

        if(isGrouded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            accelaration = maxAcceleration * (1 - velocityRatio);
            velocity.x += accelaration * Time.fixedDeltaTime;
            
            if(velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }
        }

       


       

        transform.position = pos;
    }
}
