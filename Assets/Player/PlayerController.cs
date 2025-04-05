using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables:
    private HealthPoints HealthPointsComponent;
    private MagicPoints MagicPointsComponent;

    public ParticleSystem[] AttackVFXList, HealVFXList, CounterVFXList, UltimateVFXList;

    //delegates:

    private void HandleOnHealthPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef) { 
        //
    }
    private void HandleOnHealthPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef) {
        //
    }
    private void HandleOnHealthPointsReachedMinimum() {
        //
    }

    private void HandleOnMagicPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef) {
        //
    }
    private void HandleOnMagicPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef) {
        //
    }
    private void HandleOnMagicPointsReachedMinimum() {
        //
    }
    //methods:
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
    private void HP_InitHUD()
    {
        HealthPointsComponent.IncreaseValue(0.1f);
        //MagicPointsComponent.IncreaseValue(0.0f);
    }
    private void MP_InitHUD()
    {
        //HealthPointsComponent.IncreaseValue(0.0f);
        MagicPointsComponent.IncreaseValue(0.1f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthPointsComponent = gameObject.GetComponent<HealthPoints>();
        if (HealthPointsComponent!=null)
        {
            HealthPointsComponent.OnValueIncreased += HandleOnHealthPointsIncreased;
            HealthPointsComponent.OnValueDecreased += HandleOnHealthPointsDecreased;
            HealthPointsComponent.OnValueReachedMinimum += HandleOnHealthPointsReachedMinimum;
            InvokeRepeating("HP_InitHUD", 0.3f, 0);
        }
        MagicPointsComponent = gameObject.GetComponent<MagicPoints>();
        if (MagicPointsComponent != null)
        {
            MagicPointsComponent.OnValueIncreased += HandleOnMagicPointsIncreased;
            MagicPointsComponent.OnValueDecreased += HandleOnMagicPointsDecreased;
            MagicPointsComponent.OnValueReachedMinimum += HandleOnMagicPointsReachedMinimum;
            InvokeRepeating("MP_InitHUD", 0.3f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
