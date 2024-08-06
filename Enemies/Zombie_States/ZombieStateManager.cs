using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateManager : MonoBehaviour
{
    public ZombieBaseState currentState;
    public ZombieSpawnState SpawnState = new ZombieSpawnState();
    public ZombieTargetState TargetState = new ZombieTargetState();
    public ZombieAttackState AttackState = new ZombieAttackState();
    public ZombieDeathState DeathState = new ZombieDeathState();

    public SpawnerData spawner { get; private set; }

    public bool isInZone { get; private set; }

    public float health { get; private set; }

    [field : SerializeField] public GameObject powerUp { get; private set; }

    [field: SerializeField] public EnemyType type { get; private set; }

    public Animator animator { get; private set; }

    public PlayerData data { get; private set; }
    public PlayerData killerData { get; private set; }

    [field : SerializeField]
    public GameObject bloodParticles { get; private set; }

    [field: SerializeField]
    public GameObject FuseParticle { get; private set; }

    public Fuse fuse { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SpawnState = gameObject.AddComponent<ZombieSpawnState>();
        TargetState = gameObject.AddComponent<ZombieTargetState>();
        AttackState = gameObject.AddComponent<ZombieAttackState>();
        DeathState = gameObject.AddComponent<ZombieDeathState>();

        animator = GetComponentInChildren<Animator>();

        health = CalculateHealth();

        currentState = TargetState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ZombieBaseState state)
    {
        if (health > 0)
        {
            currentState = state;
            currentState.EnterState(this);
        }
    }

    public void SetSpawner(SpawnerData _spawner)
    {
        spawner = _spawner;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.parent != null && col.transform.parent.TryGetComponent<Fuse>(out Fuse _fuse))
        {
            fuse = _fuse;
        }
        if (col.CompareTag("Bullet"))
        {
            if (health > 0)
            {
                killerData = col.GetComponent<BulletMovement>().data;
                ChangeHealth(-(col.GetComponent<BulletMovement>().GetDamage() * killerData.damageMod));
                Instantiate(bloodParticles, this.transform.position, this.transform.rotation);
                killerData.ChangePoints(10);
                Destroy(col.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.parent != null && col.transform.parent.TryGetComponent<Fuse>(out Fuse fuse))
        {
            fuse = null;
        }
    }

    public void ChangeHealth(float c)
    {
        health += c;
        if (health <= 0)
        {
            currentState = DeathState;
            currentState.EnterState(this);
        }
    }

    private int CalculateHealth()
    {
        int wave = ZoneControl.Instance.roundCounter;
        int mod;
        if (wave < 5)
        {
            mod = (int)Mathf.Ceil((float)(wave * (4 * (wave / 2))));
        }
        else if (wave < 12)
        {
            mod = (int)Mathf.Ceil((float)(wave * (8 * (wave / 3))));
        }
        else if (wave < 20)
        {
            mod = (int)Mathf.Ceil((float)(wave * (12 * (wave / 4))));
        }
        else
        {
            mod = (int)Mathf.Ceil((float)(wave * (16 * (wave / 5))));
        }
        return type.Health + mod;
    }

    public void SetInZone(bool set)
    {
        isInZone = set;
    }

    public void SetPlayer(PlayerData _data)
    {
        data = _data;
    }

    public void SetKiller(PlayerData _data)
    {
        killerData = _data;
    }
}
