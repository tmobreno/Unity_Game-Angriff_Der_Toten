using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private float force, damage, offset;
    public PlayerData data { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
        UpdateMousePosition();
        UpdateDirection();
    }

    private void InitializeComponents()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = this.GetComponent<Rigidbody2D>();
        force = 18;
        Destroy(this.gameObject, 3);
    }

    private void UpdateMousePosition()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    private void UpdateDirection()
    {
        Vector3 direction = mousePos - transform.position;

        direction = Quaternion.Euler(0, 0, offset) * direction;

        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    public void SetOffset(float _offset)
    {
        offset = _offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPlayerData(PlayerData _data)
    {
        data = _data;
    }
}
