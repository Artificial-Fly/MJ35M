 using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables:
    private HealthPoints HealthPointsComponent;
    private MagicPoints MagicPointsComponent;

    public ParticleSystem[] AttackVFXList, HealVFXList, CounterVFXList, UltimateVFXList;

    [SerializeField]
    private GameManager.EnemyActionTypes[] EnemyActionsQueue;
    //delegates:

    private void HandleOnHealthPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
    }
    private void HandleOnHealthPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
    }
    private void HandleOnHealthPointsReachedMinimum()
    {
        //
    }

    private void HandleOnMagicPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
    }
    private void HandleOnMagicPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
    }
    private void HandleOnMagicPointsReachedMinimum()
    {
        //
    }
    //methods:
    public GameManager.EnemyActionTypes GetEnemyActionByIndex(int Index)
    {
        return EnemyActionsQueue[Index];
    }
    public bool Attack()
    {
        //
        bool Success = false;
        foreach (ParticleSystem AttackVFX in AttackVFXList)
        {
            AttackVFX.Play();
        }
        return Success;
    }
    public bool Heal()
    {
        //
        bool Success = false;
        foreach (ParticleSystem HealVFX in HealVFXList)
        {
            HealVFX.Play();
        }
        return Success;
    }
    public bool Counter()
    {
        //
        bool Success = false;
        foreach (ParticleSystem CounterVFX in CounterVFXList)
        {
            CounterVFX.Play();
        }
        return Success;
    }
    public bool Ultimate()
    {
        //
        bool Success = false;
        foreach (ParticleSystem UltimateVFX in UltimateVFXList)
        {
            UltimateVFX.Play();
        }
        return Success;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthPointsComponent = gameObject.GetComponent<HealthPoints>();
        if (HealthPointsComponent != null)
        {
            HealthPointsComponent.OnValueIncreased += HandleOnHealthPointsIncreased;
            HealthPointsComponent.OnValueDecreased += HandleOnHealthPointsDecreased;
            HealthPointsComponent.OnValueReachedMinimum += HandleOnHealthPointsReachedMinimum;
        }
        MagicPointsComponent = gameObject.GetComponent<MagicPoints>();
        if (MagicPointsComponent != null)
        {
            MagicPointsComponent.OnValueIncreased += HandleOnMagicPointsIncreased;
            MagicPointsComponent.OnValueDecreased += HandleOnMagicPointsDecreased;
            MagicPointsComponent.OnValueReachedMinimum += HandleOnMagicPointsReachedMinimum;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
