using UnityEngine;
using System.Collections;
using System.Linq;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector3 horizontalInput;

    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    public GameObject player;
    public GameObject Sphere;

    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundMask);
        if (isGrounded)
        {
            Debug.Log("Grounded");

        }
        else
        {
            Debug.Log("NOT Grounded");

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 myVector = new Vector3(player.transform.position.x, player.transform.position.y - 5f, player.transform.position.z);

            Instantiate(Sphere, myVector, Quaternion.identity);
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);


            }
            jump = false;
        }
        Debug.Log(isGrounded);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;


    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    public void OnAttackPressed()
    {
        if (CanAttack)
        {
            SwordAttack();
            ResetAttackCooldown();
        }
    }

    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());

    }


    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;
    }
}
