using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerColtroller : MonoBehaviour
{

    #region varialbels

    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float sneakForce = 10f;
    [SerializeField] private float raycastDistance = 2f;

    private bool isGrounded = true;
    #endregion

    #region sneakVariables

    private BoxCollider2D collider;
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField] private Sprite defaultSpriteRenderer;
    [SerializeField] private Sprite defaultPlayerSprite;
    [SerializeField] private Sprite sneakPlayerSprite;
    [SerializeField] private float defaultPlayerSize = 2f;
    [SerializeField] private float sneakPlayerSize = 1f;
    #endregion

    #region InputVariables

    private Rigidbody2D rb;
    private InputActions input;
    private InputAction jumpAction;
    private InputAction sneakAction;
    #endregion

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    #region InputActions

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();

        input = new InputActions();

        jumpAction = input.PlayerActions.Jump;
        sneakAction = input.PlayerActions.Sneak;
    }

    private void OnEnable()
    {
        EnableInput();

        jumpAction.performed += Jump;

        sneakAction.performed += Sneak;
        sneakAction.canceled += Sneak;
    }

    private void OnDisable()
    {
        DisableInput();

        jumpAction.performed -= Jump;

        sneakAction.performed -= Sneak;
        sneakAction.canceled -= Sneak;
    }

    public void EnableInput()
    {
        input.Enable();
    }

    public void DisableInput()
    {
        input.Disable();
    }
    #endregion

    #region Jump

    void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    #endregion

    #region Sneak

    void Sneak(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, sneakPlayerSize, gameObject.transform.localScale.z);

            transform.position -= new Vector3(0, defaultPlayerSize - sneakPlayerSize, 0);
            rb.AddForce(new Vector2(0, -sneakForce), ForceMode2D.Impulse);

            if (sneakPlayerSprite != null)
            {
                playerSpriteRenderer.sprite = sneakPlayerSprite;
            }
        }

        

        if (context.canceled)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, defaultPlayerSize, gameObject.transform.localScale.z);

            if (defaultPlayerSprite != null)
            {
                playerSpriteRenderer.sprite = defaultPlayerSprite;
            }
        }

        
    }

    #endregion

    #region GroundCheck

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    #endregion
}
