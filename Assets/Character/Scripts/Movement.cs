using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Refrences")]
    public Inputs inputsScript;
    public Actions ActionsScript;
    public JumpScript jumpingScript;

    [Header("Variables")]
    public float maxHSpeed;
    public float acceleration;
    public float deceleration;

    private void Awake()
    {
        //get scripts
        inputsScript = GetComponent<Inputs>();
        ActionsScript = GetComponent<Actions>();
        jumpingScript = GetComponent<JumpScript>();

    }

    private void FixedUpdate()
    {
        if (ActionsScript.isDashing || jumpingScript.isWallJumping)
            return;
        Move();

    }

    public void Move()
    {
        //move player
        if (inputsScript.horizontalInput != 0f)
        {
            inputsScript.playerRb.velocity = Vector3.MoveTowards(inputsScript.playerRb.velocity, new Vector3(inputsScript.horizontalInput * maxHSpeed, inputsScript.playerRb.velocity.y, 0f), acceleration);
            //flip character and keep it that way when no inputs        
            Flip();
        }
        else //slow player to stop
            inputsScript.playerRb.velocity = Vector3.MoveTowards(inputsScript.playerRb.velocity, new Vector3(0f, inputsScript.playerRb.velocity.y, 0f), deceleration);
    }

    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = Mathf.Sign(inputsScript.horizontalInput) * Mathf.Abs(transform.localScale.x);
        transform.localScale = currentScale;
    }

}
