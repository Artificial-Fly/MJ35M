using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables:
    private HealthPoints HealthPointsComponent;
    private EnergyPoints EnergyPointsComponent;

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

    private void HandleOnEnergyPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
    }
    private void HandleOnEnergyPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
    }
    private void HandleOnEnergyPointsReachedMinimum()
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
        EnergyPointsComponent = gameObject.GetComponent<EnergyPoints>();
        if (EnergyPointsComponent != null)
        {
            EnergyPointsComponent.OnValueIncreased += HandleOnEnergyPointsIncreased;
            EnergyPointsComponent.OnValueDecreased += HandleOnEnergyPointsDecreased;
            EnergyPointsComponent.OnValueReachedMinimum += HandleOnEnergyPointsReachedMinimum;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
