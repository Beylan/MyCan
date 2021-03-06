using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Keyboard Input")]   
    [SerializeField] KeyCode moveForward;     
    [SerializeField] KeyCode moveBackward;
    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode run;
    [SerializeField] KeyCode jump;
    [SerializeField] KeyCode loot;
    [SerializeField] KeyCode lootExit;

    [Header("Variables")]
    [SerializeField] float currentSpeed; 
    [SerializeField] float walkSpeed;
    [SerializeField] float forwardRunSpeed;
    [SerializeField] float otherRunSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float resetSpeed;
    [SerializeField] float increaseSpeed;
    [SerializeField] float decreaseSpeed;
    [SerializeField] float fillAmount;
    [SerializeField] float unloadAmount;
    [SerializeField] float jumpHeight;
    [SerializeField] float groundDistance;
    [SerializeField] float clampRate;
    [SerializeField] float xMov;
    [SerializeField] float zMov;
    [SerializeField] bool isRun;
    [SerializeField] bool xReleased, zReleased;
    [SerializeField] LayerMask mask;
    
    
    [Header("Required Components")]
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] Text runTextTest;
    [SerializeField] Text speedTextTest;
    [SerializeField] GameObject runEnergyImage;
    [SerializeField] GameObject lootImage;
    [SerializeField] GameObject lootPanel;
    [SerializeField] MouseLook look;

    private void Update()
    {
        #region Key Control
        if (Input.GetKey(moveForward) && zMov <= maxSpeed)
        {
            zReleased = false;
            zMov += increaseSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveBackward) && zMov >= -maxSpeed)
        {
            zReleased = false;
            zMov -= increaseSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveRight) && xMov <= maxSpeed)
        {
            xReleased = false;
            xMov += increaseSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveLeft) && xMov >= -maxSpeed)
        {
            xReleased = false;
            xMov -= increaseSpeed * Time.deltaTime;
        }

        if ((Input.GetKeyUp(moveForward) && zMov >= resetSpeed) || (Input.GetKeyUp(moveBackward) && zMov <= resetSpeed))
            zReleased = true;
        if ((Input.GetKeyUp(moveLeft) && xMov <= resetSpeed) || (Input.GetKeyUp(moveRight) && xMov >= resetSpeed))
            xReleased = true;

		if (zReleased && zMov != 0)
            zMov = Mathf.LerpAngle(zMov, 0, decreaseSpeed * Time.deltaTime);
        if (xReleased && xMov != 0)
            xMov = Mathf.LerpAngle(xMov, 0, decreaseSpeed * Time.deltaTime);
        #endregion

        #region Jump
        if (Input.GetKeyDown(jump) && isGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        #endregion

        #region Run
        if (Input.GetKey(run))
        {
            if (zMov.ToString("0") != "0" || xMov.ToString("0") != "0")
            {
                if (zMov > 0)
                {
                    //currentSpeed = forwardRunSpeed; // y??r??me h??z?? ??ne ko??ma h??z??yla de??i??tirilir.
                    currentSpeed = Mathf.LerpAngle(currentSpeed, forwardRunSpeed, increaseSpeed * Time.deltaTime);
                }
                else
				{
                    //currentSpeed = otherRunSpeed; // y??r??me h??z?? di??er y??nlere ko??ma h??z??yla de??i??tirilir.
                    currentSpeed = Mathf.LerpAngle(currentSpeed, otherRunSpeed, increaseSpeed * Time.deltaTime);
                }
                isRun = true; // ko??uyormu kontrol??.            
                runTextTest.text = "Ko??ma Aktif"; // Test aray??z?? i??in ge??ici bir yaz??.
                runEnergyImage.GetComponent<Image>().fillAmount -= unloadAmount * Time.deltaTime; // ko??uyorsa ko??u enerji bar??ndaki de??eri d??????r??r.
            }
            if (runEnergyImage.GetComponent<Image>().fillAmount == 0 || (zMov.ToString("0") == "0" && xMov.ToString("0") == "0")) // ko??u enerji bar?? bittiyse veya y??r??meyi b??rakt??ysa ko??amaz y??r??r.           
            {
                currentSpeed = walkSpeed;
                isRun = false;
            }
        }
        else if (Input.GetKeyUp(run))
        {
            //currentSpeed = walkSpeed; // ko??ma h??z?? y??r??me h??z??yla de??i??tirilir.                        
            runTextTest.text = "Ko??ma Aktif De??il";// Test aray??z?? i??in ge??ici bir yaz??.
            isRun = false; // ko??uyor mu kontrol??.            
        }

        if (isRun == false) { 
            runEnergyImage.GetComponent<Image>().fillAmount += fillAmount * Time.deltaTime; // ko??muyorsa ko??u enerji bar??ndaki de??eri artt??r??r.    
            currentSpeed = Mathf.LerpAngle(currentSpeed, walkSpeed, increaseSpeed * Time.deltaTime);
        }
        speedTextTest.text = "H??z : " + currentSpeed; // Test aray??z?? i??in ge??ici bir yaz??.
        #endregion

        #region Loot E (Test)
        if (Input.GetKey(loot))
        {
            lootImage.GetComponent<Image>().fillAmount += 0.01f;
            if (lootImage.GetComponent<Image>().fillAmount == 1)
            {
                lootPanel.SetActive(true);
            }
        }
        else if (Input.GetKeyUp(loot))
        {
            lootImage.GetComponent<Image>().fillAmount = 0;
        }
        else if (Input.GetKey(lootExit))
        {
            lootPanel.SetActive(false);
        }
        #endregion

    }

    private void FixedUpdate()
    {
        Vector3 move = transform.right * xMov + transform.forward * zMov;
        move = Vector3.ClampMagnitude(move, clampRate);
        rigidbody.velocity = new Vector3(move.x * currentSpeed, rigidbody.velocity.y, move.z * currentSpeed);
        look.CamMove();
    }

    private bool isGrounded()
    {
        return Physics.CheckCapsule(capsuleCollider.bounds.center, new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.min.y - 0.1f, capsuleCollider.bounds.center.z), groundDistance, mask);
    }
}

