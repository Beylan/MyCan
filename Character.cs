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
                    //currentSpeed = forwardRunSpeed; // yürüme hızı öne koşma hızıyla değiştirilir.
                    currentSpeed = Mathf.LerpAngle(currentSpeed, forwardRunSpeed, increaseSpeed * Time.deltaTime);
                }
                else
				{
                    //currentSpeed = otherRunSpeed; // yürüme hızı diğer yönlere koşma hızıyla değiştirilir.
                    currentSpeed = Mathf.LerpAngle(currentSpeed, otherRunSpeed, increaseSpeed * Time.deltaTime);
                }
                isRun = true; // koşuyormu kontrolü.            
                runTextTest.text = "Koşma Aktif"; // Test arayüzü için geçici bir yazı.
                runEnergyImage.GetComponent<Image>().fillAmount -= unloadAmount * Time.deltaTime; // koşuyorsa koşu enerji barındaki değeri düşürür.
            }
            if (runEnergyImage.GetComponent<Image>().fillAmount == 0 || (zMov.ToString("0") == "0" && xMov.ToString("0") == "0")) // koşu enerji barı bittiyse veya yürümeyi bıraktıysa koşamaz yürür.           
            {
                currentSpeed = walkSpeed;
                isRun = false;
            }
        }
        else if (Input.GetKeyUp(run))
        {
            //currentSpeed = walkSpeed; // koşma hızı yürüme hızıyla değiştirilir.                        
            runTextTest.text = "Koşma Aktif Değil";// Test arayüzü için geçici bir yazı.
            isRun = false; // koşuyor mu kontrolü.            
        }

        if (isRun == false) { 
            runEnergyImage.GetComponent<Image>().fillAmount += fillAmount * Time.deltaTime; // koşmuyorsa koşu enerji barındaki değeri arttırır.    
            currentSpeed = Mathf.LerpAngle(currentSpeed, walkSpeed, increaseSpeed * Time.deltaTime);
        }
        speedTextTest.text = "Hız : " + currentSpeed; // Test arayüzü için geçici bir yazı.
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
