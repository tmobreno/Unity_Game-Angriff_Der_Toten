using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerData data;
    private Rigidbody2D rb;
    private bool inTrigger, canInteract;
    private Interactable trigger;

    public Animator animator;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
        InitializeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIPauseMenu.Instance.IsPaused) { return; }
        if (!data.isDead)
        {
            SetMovementInput();

            if (inTrigger && canInteract && Input.GetKey(KeyCode.F) && trigger.CheckCost())
            {
                StartCoroutine(ResetInteraction());
                trigger.Interact();
            }
        }
        else
        {
            animator.SetBool("Dead", true);
        }
    }

    private void FixedUpdate()
    {
        if (data.isDead) { return; }
        if (UIPauseMenu.Instance.IsPaused) { return; }

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MovePosition(1.2f + data.sprintMod, "Footstep_Sprint");
        }
        else // Walk
        {
            MovePosition(1.2f, "Footstep_Walk");
        }

        // Set Walk Animation State
        if ((movement.x != 0 || movement.y != 0)) { animator.SetBool("Walking", true); }
        else { animator.SetBool("Walking", false); }

        if (data.GetEquippedWeapon().type.Hands == 2) { animator.SetBool("IsTwoHanded", true); }
        else { animator.SetBool("IsTwoHanded", false); }
    }

    private void SetMovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void InitializeComponents()
    {
        animator = GetComponentInChildren<Animator>();
        data = this.GetComponent<PlayerData>();
        rb = this.GetComponent<Rigidbody2D>();
        animator.SetBool("Walking", false);
    }

    private void MovePosition(float speedmod, string soundToPlay)
    {
        if(data.GetEquippedWeapon().type.Hands == 2) { speedmod -= 0.2f; }
        if(movement.normalized.x != 0 || movement.normalized.y != 0) { AudioManager.Instance.Play(soundToPlay); }
        else { AudioManager.Instance.Stop(soundToPlay); }
        rb.MovePosition(rb.position + movement.normalized * Time.fixedDeltaTime * data.baseSpeed * speedmod);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent != null && col.transform.parent.TryGetComponent<Interactable>(out Interactable interact))
        {
            trigger = interact;
            trigger.SetPlayerData(data);
            trigger.SetNotificationText();
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.parent != null && col.transform.parent.GetComponent<Interactable>() != null && trigger != null)
        {
            trigger.ClearNotificationText();
            trigger.SetPlayerData(null);
            trigger = null;
            inTrigger = false;
        }
    }

    private IEnumerator ResetInteraction()
    {
        canInteract = false;
        yield return new WaitForSeconds(2);
        canInteract = true;
    }
}
