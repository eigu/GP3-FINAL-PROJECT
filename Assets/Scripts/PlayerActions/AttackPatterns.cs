using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class AttackPatterns : MonoBehaviour
{
    [SerializeField] Animator m_anim;
    [SerializeField] Transform player;
    [SerializeField] private Rigidbody2D m_rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;

  
    [SerializeField] GameObject frontSlash;
    [SerializeField] GameObject backSlash;
    [SerializeField] GameObject leftSlash;
    [SerializeField] GameObject rightSlash;

    public float moveSpeed = 5f;   
    public float maxSize = 5f;     
    public float growthRate = 0.5f; 
    public float destroyDelay = 3f;
 

    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] float dashSpeed = 3f;
    [SerializeField] float backDuration = 0.5f;
    [SerializeField] float backSpeed = 3f;
    [SerializeField] LayerMask wallLayerMask;
    private bool isAttacking;
    public bool isAttackingOut;
    private Vector2 m_lastMovementDirection = Vector2.zero;
    private Vector2 m_movementInput;
    
    private Color lightColor = new Color(0.8f, 0.8f, 0.8f, 1f);

    public bool immune;
    //private KeyCode keyToHold = KeyCode.K;
    private bool canPass = true;

    void Awake()
    {
        frontSlash.SetActive(false);
        backSlash.SetActive(false);
        leftSlash.SetActive(false);
        rightSlash.SetActive(false);
        immune = false;
        isAttacking = false;
    }
    void FixedUpdate()
    {
       
        bool jKeyPressed = Input.GetKey(KeyCode.J);
        bool shiftKeyPressed = Input.GetKey(KeyCode.LeftShift);
        bool spacePressed = Input.GetKey(KeyCode.Space);
        bool kKeyPressed = Input.GetKey(KeyCode.K);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        m_movementInput = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        m_anim.SetFloat("Horizontal", m_movementInput.x);
        m_anim.SetFloat("Vertical", m_movementInput.y);
        m_anim.SetFloat("Speed", m_movementInput.sqrMagnitude);

     //  bool charge = ChargeAttackCheck();


        if (kKeyPressed)
        {
            Debug.Log("yipee");
            CheckSlash(GetDirectionString());
        }


        if (jKeyPressed)
        {
            if (shiftKeyPressed) {
                StartCoroutine(DashAttack());
            }
             
            if(!shiftKeyPressed)
            {
                BaseAttack();
            }

        }

        if (!jKeyPressed)
        {
            NotAttacking();
        }


        if (spacePressed)
        {
            StartCoroutine(BackDash());
        }


        if (m_movementInput.magnitude > 0.1f)
        {
            m_lastMovementDirection = m_movementInput.normalized;
        }
        else
        {
            m_anim.SetFloat("Horizontal", m_lastMovementDirection.x);
            m_anim.SetFloat("Vertical", m_lastMovementDirection.y);
        }

       
    }

    private void OnCollisionEnter(Collision collision) //is trigger allows player to pass thru walls, this is for that
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

            StopPlayer();
        }
    }

    private void StopPlayer()
    {
        canPass = false;

       
    }

    //private bool ChargeAttackCheck()
    //{
    //    float requiredHoldDuration = 2f;
    //    bool isKeyHeldDown = false;
    //    float holdStartTime = 0f;

    //    if (Input.GetKeyDown(keyToHold))
    //    {
    //        isKeyHeldDown = true;
    //        holdStartTime = Time.time;
    //    }

    //    if (Input.GetKeyUp(keyToHold))
    //    {
    //        isKeyHeldDown = false;
    //    }

    //    if (isKeyHeldDown)
    //    {

    //        float holdDuration = Time.time - holdStartTime;

    //        if (holdDuration >= requiredHoldDuration)
    //        {
    //            Debug.Log("yay");
    //            return true;
    //        }

    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    return false;
    //}

    private void BaseAttack()
    {
        isAttacking = true;
        isAttackingOut = true;
        m_anim.SetBool("IsAttacking", isAttacking);

    }

    IEnumerator DashAttack()
    {
       
        isAttacking = true;
        isAttackingOut = true;
        m_anim.SetBool("IsAttacking", true);
        Vector3 startPosition = transform.position;
        Vector3 dashDirection = GetDirection(); 
        float startTime = Time.time;



        while (Time.time - startTime < dashDuration)
        {
            immune = true;
            ImmuneSprite();
            float t = (Time.time - startTime) / dashDuration;
            float dashDistance = t * dashSpeed;
            transform.position = startPosition + dashDirection * dashDistance;
            RaycastHit2D hit = Physics2D.Raycast(startPosition, dashDirection, dashDistance, wallLayerMask);

            if (hit.collider != null)
            {
                dashDistance = hit.distance;
            }

            transform.position = startPosition + dashDirection * dashDistance;

            yield return null;
        }

        m_anim.SetBool("IsAttacking", false);
        isAttacking = false;
        isAttackingOut = false;
        immune = false;
        ResetSpriteColor();
    }


    Vector3 GetDirection()
    {
        Vector3 moveDirection = Vector3.zero;

        if (m_movementInput.magnitude > 0.1f)
        {
            moveDirection = m_movementInput;
        }
        else
        {
            moveDirection = m_lastMovementDirection;
        }

        return moveDirection;
    }

    private string GetDirectionString()
    {
        Vector3 direction = GetDirection();

      
         if (direction.x > 0)
        {
            return "right";
        }
        else if (direction.x < 0)
        {
            return "left";
        }
        else if (direction.y > 0)
        {
            return "back";
        }
        else if (direction.y < 0)
        {
            return "front";
        }
        else
        {
            return "front";
        }
    }


    IEnumerator BackDash() // spacebar
    {
       // m_anim.SetBool("IsStill", true);
        Vector3 startPosition = transform.position;
        Vector3 backDirection = GetDirection();
        float startTime = Time.time;

        while (Time.time - startTime < dashDuration)
        {
            immune = true;
            ImmuneSprite();
            float t = (Time.time - startTime) / backDuration;
            float backDistance = t * backSpeed;
            transform.position = startPosition + (-backDirection * backDistance);


            yield return null;
        }
        immune = false;
        ResetSpriteColor();
       // m_anim.SetBool("IsStill", false);
    }

    private void NotAttacking()
    {
        isAttacking = false;
        m_anim.SetBool("IsAttacking", isAttacking);
        StartCoroutine(NotAttackingDelay());
    }

  

    IEnumerator NotAttackingDelay()
    {

        yield return new WaitForSeconds(1f);
        isAttackingOut = false;

    }

    private void ImmuneSprite() 
    {
        spriteRenderer.color = lightColor;
    }

    private void ResetSpriteColor() // after immunity
    {
        spriteRenderer.color = Color.white;
    }

    private void CheckSlash(string dir)
    {
        if (dir == "front")
        {
            Debug.Log("front");
            StartCoroutine(ChargedAttackFront());
        }

        else if (dir == "back")
        {
            Debug.Log("back");
            StartCoroutine(ChargedAttackBack());
        }


        else if (dir == "left")
        {
            Debug.Log("left");
            StartCoroutine(ChargedAttackLeft());
        }

        else if (dir == "right")
        {
            Debug.Log("right");
            StartCoroutine(ChargedAttackRight());
        }

        else
        {
            Debug.Log("boo");
        }

    }

    IEnumerator ChargedAttackFront()
    {


        GameObject imageInstance = Instantiate(frontSlash, transform.position, Quaternion.identity);
        imageInstance.SetActive(true);

        Vector3 forwardDirection = transform.forward != Vector3.zero ? transform.forward : Vector3.forward;
        Quaternion rotation = Quaternion.LookRotation(forwardDirection, transform.up);
        imageInstance.transform.rotation = rotation;

        imageInstance.transform.localScale = Vector3.zero;
        while (imageInstance.transform.localScale.x < maxSize)
        {
            imageInstance.transform.Translate(forwardDirection * moveSpeed * Time.deltaTime);
            float currentSize = imageInstance.transform.localScale.x;
            float newSize = Mathf.Min(currentSize + growthRate * Time.deltaTime, maxSize);
            imageInstance.transform.localScale = new Vector3(newSize, newSize, newSize);

            yield return null;
        }

        Destroy(imageInstance);
    }

    IEnumerator ChargedAttackLeft()
    {

        GameObject imageInstance = Instantiate(leftSlash, transform.position, Quaternion.identity);
        imageInstance.SetActive(true);

    
        //Vector3 leftDirection = -transform.right; 
        //Quaternion rotation = Quaternion.LookRotation(leftDirection, transform.up);
        //imageInstance.transform.rotation = rotation;

        imageInstance.transform.localScale = Vector3.zero;

        while (imageInstance.transform.localScale.x < maxSize)
        {

            imageInstance.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            float currentSize = imageInstance.transform.localScale.x;
            float newSize = Mathf.Min(currentSize + growthRate * Time.deltaTime, maxSize);
            imageInstance.transform.localScale = new Vector3(newSize, newSize, newSize);

            yield return null;
        }

      
        Destroy(imageInstance);
    }

    IEnumerator ChargedAttackRight()
    {

        GameObject imageInstance = Instantiate(rightSlash, transform.position, Quaternion.identity);
        imageInstance.SetActive(true);


        //Vector3 rightDirection = transform.right;
        //Quaternion rotation = Quaternion.LookRotation(rightDirection, transform.up);
        //imageInstance.transform.rotation = rotation;

        imageInstance.transform.localScale = Vector3.zero;

        while (imageInstance.transform.localScale.x < maxSize)
        {

            imageInstance.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            float currentSize = imageInstance.transform.localScale.x;
            float newSize = Mathf.Min(currentSize + growthRate * Time.deltaTime, maxSize);
            imageInstance.transform.localScale = new Vector3(newSize, newSize, newSize);

            yield return null;
        }


        Destroy(imageInstance);
    }

    IEnumerator ChargedAttackBack()
    {
       Vector3 offset = new Vector3(0f, 1f, 0f);
        GameObject imageInstance = Instantiate(backSlash, transform.position + offset, Quaternion.identity);
        imageInstance.SetActive(true);

  
        //Vector3 backDirection = transform.forward; 
        //Quaternion rotation = Quaternion.LookRotation(backDirection, transform.up);
        //imageInstance.transform.rotation = rotation;

      
        imageInstance.transform.localScale = Vector3.zero;

        while (imageInstance.transform.localScale.x < maxSize)
        {
   
            imageInstance.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);


            float currentSize = imageInstance.transform.localScale.x;
            float newSize = Mathf.Min(currentSize + growthRate * Time.deltaTime, maxSize);
            imageInstance.transform.localScale = new Vector3(newSize, newSize, newSize);

            yield return null;
        }

        Destroy(imageInstance);
    }
}
