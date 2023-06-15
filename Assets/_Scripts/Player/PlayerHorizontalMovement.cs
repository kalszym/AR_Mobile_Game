using Photon.Pun;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviourPun
{
    public float SpeedMultiplayer;
    public static int Direction = 1;
    public Camera ArCam;
    private SideDetector _sideDetector;
    private Rigidbody _rb;
    public Animator anim;
    [SerializeField][Range(0, 1)] private float _lerpConstant;


    private void Start()
    {
        InitIfDoesNotExist();
        anim = GetComponentInChildren<Animator>();
    }
    public void Update()
    {
      
    }
    private void InitIfDoesNotExist()
    {
        if (photonView.IsMine && (_sideDetector || _rb == null))
        {
            _sideDetector = FindObjectOfType<SideDetector>();
            _rb = GetComponent<Rigidbody>();
        }
    }

    private void CheckCameraAndPlayerDirection()
    {
        InitIfDoesNotExist();
        if (_sideDetector.CurrentSide == side.back)
            Direction = -1;
        else
            Direction = 1;
    }

    public void Move(float playerSpeed)
    {
        if (base.photonView.IsMine)
        {
            
            CheckCameraAndPlayerDirection();
            transform.position += GetVectorDepthDirection() * SpeedMultiplayer * Direction * playerSpeed * Time.deltaTime;
        }
        if (playerSpeed > 0.0f || playerSpeed<0.0f)
        {
            anim.SetBool("iswalk", true);

        }
        else anim.SetBool("iswalk", false);
        if (playerSpeed>0.0f)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);

        }
        else if (playerSpeed<0.0f)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else { transform.rotation = Quaternion.Euler(0, 180, 0); }

    }

    private Vector3 GetVectorDepthDirection()
    {
        if (_sideDetector.IsZDepthAxis)
            return Vector3.forward;
        else
            return Vector3.right;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0 && base.photonView.IsMine)
        {
            _rb.velocity = Vector3.zero;
        }
    }
   
}
