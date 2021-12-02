using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (Player))]
public class ThirdPersonUserControl : MonoBehaviour
{
    private Player m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    private Rigidbody rb;
    protected internal GameObject lastPlatformTouched;
    protected internal Vector3 lastObjectPosition;
    protected internal Vector3 lastPlatformPosition;
    protected internal GameObject lastPlatformPrefab;
    [SerializeField] ParticleSystem respawnParticles1;
    [SerializeField] ParticleSystem respawnParticles2;
    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        HandleRespawn();
    }

    void HandleRespawn()
    {
        if (transform.position.y < -5.0f)
        {
            if (lastPlatformTouched != null)
            {
                Vector3 newPos = lastObjectPosition;
                newPos.y = 13.5f;
                transform.position = newPos;
                rb.velocity = 10 * Vector3.down;
            }
            else
            {
                // ATTENZIONE
                Instantiate(lastPlatformPrefab, lastPlatformPosition, Quaternion.identity);
                Vector3 newPos = lastObjectPosition;
                newPos.y = 13.5f;
                transform.position = newPos;
                rb.velocity = 10 * Vector3.down;
            }
            respawnParticles1.Play();
            respawnParticles2.Play();
        }
    }
    
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v*m_CamForward + h*m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v*Vector3.forward + h*Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        m_Character.Move(m_Move, m_Jump);
        m_Jump = false;
    }
}