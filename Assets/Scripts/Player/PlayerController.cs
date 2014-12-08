using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;

    Animation anim;
    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()//Called even if script is not enabled, good for references
    {
        anim = GetComponent<Animation>();

        floorMask = LayerMask.GetMask("Ground");
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()//Physics Updates
    {
        float h = Input.GetAxisRaw("Horizontal"); //Raw means no acceleration, 1, 0, -1
        float v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(h) + Mathf.Abs(v) > 0)
            anim.CrossFade("soldierRun");
        else
            anim.CrossFade("soldierIdle", 0.1f);
        Move(h, v);
        Turning();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        //Moving diagonallly gives you a different vector length
        //Normalizing means the magnitude is ALWAYS 1 regardless of direction
        //Time.deltaTime makes this happen per SECOND instead of per fixed update
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {

        float deadzone = 0.25f;

        Vector2 shootDirection = new Vector2(Input.GetAxis("HorizAim"),
                                             Input.GetAxis("VertAim") * -1);

        //only use this if we detect xbox joystick movement outside the deadzone
        if (shootDirection.magnitude < deadzone)
            shootDirection = Vector2.zero;
        else
            transform.rotation = Quaternion.LookRotation(new Vector3(shootDirection.x, 0, shootDirection.y));

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        //The ray (position/direction), get info OUT of this function into floorHit
        //Length, limit hits to the floor layer/mask
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //point that it hit the floor minus position of player
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //Z-axis = forward-axis. This function sets the rotation to our mouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }


}
