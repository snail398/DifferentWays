using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;

    public float jumpForce = 100f;
    public float stopSpeed = 2f;
    public float smoothTime = 0.2f;
    public Transform groundCheck;

    private Vector3 velocity = Vector3.zero;
    private bool passAnimate = false;
    public bool grounded = false;
    public bool  enviromented = false;
    private Rigidbody rb;
    private bool canUp = false;
    private bool canDown = false;
    private bool enable = true;
    private Transform pathTrigger;
    private Transform targetForPass;
    private Vector3 startPosition;
    private Vector3 viewDirection;
    private Vector3 targetDirection;
    float camRayLength = 100f;

  

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        enviromented = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Enviroment"));
        if (Input.GetButtonDown("Jump") && (grounded||enviromented))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (enable)
        { 
            // Блок передвижения персонажа
            float h = Input.GetAxis("Horizontal");
        
            transform.position = new Vector3(transform.position.x+h/stopSpeed,transform.position.y,transform.position.z);

            /*
            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();
                */
            if (jump)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }
            // Конец блока передвижения персонажа


            //Блок перехода между платформами
            if (canUp && (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow)))
            {
               
                SetEnable(false); //Отключить управление

                //Запуск анимации перехода
                targetForPass = pathTrigger;
                passAnimate = true;
                
               
            }
            if (canDown && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                SetEnable(false);//Отключить управление

                //Запуск анимации перехода
                targetForPass = pathTrigger;
                passAnimate = true;
            }

            //Конец блока перехода между платформами

            Turning();
            GetTargetDirection();
        }

        //Анимация перехода
        if (passAnimate)
        {
            PassAnimation();
        }

        
      
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength,LayerMask.GetMask("rayScreen")))
        {
            Vector3 playerToMouse = floorHit.point - GameObject.Find("head").transform.position;
            playerToMouse.z = transform.position.z;
            viewDirection = new Vector3(playerToMouse.x,playerToMouse.y,0);

            if (viewDirection.x > 0 && !facingRight)
                Flip();
            else if (viewDirection.x <0 && facingRight)
                Flip();
        }
    }

    void GetTargetDirection()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, LayerMask.GetMask("rayScreen")))
        {
            Vector3 playerToMouse = floorHit.point - transform.Find("hand").transform.position;
            playerToMouse.z = transform.position.z;
            targetDirection = new Vector3(playerToMouse.x, playerToMouse.y, 0);
        }
    }

    public Vector3 ReturnTargetDirection()
    {
        return targetDirection;
    }
    

    void PassAnimation()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetForPass.position.x, targetForPass.position.y, targetForPass.position.z), ref velocity, smoothTime);

        //Проверка окончания анимации перехода
        if (Vector3.Distance(transform.position, targetForPass.position) < 0.1f)
        {
            passAnimate = false;
            if (!enable) SetEnable(true);
        }
    }
    public Vector3 GetStartPosition()
    {
        return startPosition;
    }
    public Vector3 GetViewDirrection()
    {
        return viewDirection;
    }
    //Разворот модельки персонажа
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void SetEnable(bool enbl)
    {
        enable = enbl;
        if (!enbl)
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        else
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag.Equals("passTrigger"))
        {
                pathTrigger = other.gameObject.GetComponent<PassTriggerController>().pairTrigger;
                switch (other.gameObject.GetComponent<PassTriggerController>().passDirection)
                {
                    case "up":
                        canUp = true;
                        break;
                    case "down":
                        canDown = true;
                        break;
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("passTrigger"))
        { 
            canDown= false;
            canUp= false;
        }
    }
}
