using UnityEngine;

public class EntityResource : MonoBehaviour
{
    //variables:
    private float CurrentValue = 0.0f;
    public float MaxValue = 10.0f, MinValue = 0.0f;
    public bool SetMaxValueAtStart = true;
    public float InitialValue = 0.0f;

    //delegates:
    public delegate void ValueIncreased(float CurrentValue, float MaxValue, float DeltaKoef);
    public event ValueIncreased OnValueIncreased;
    public delegate void ValueDecreased(float CurrentValue, float MaxValue, float DeltaKoef);
    public event ValueDecreased OnValueDecreased;
    public delegate void ValueReachedMinimum();
    public event ValueReachedMinimum OnValueReachedMinimum;

    //methods:
    public bool IncreaseValue(float Amount)
    {
        bool Success = false;
        //logic goes here
        if (Amount > 0)
        {
            if ((CurrentValue + Amount) > MaxValue)
            {
                CurrentValue = MaxValue;
            }
            else
            {
                CurrentValue = CurrentValue + Amount;
            }
            if (OnValueIncreased != null)
            {
                OnValueIncreased(CurrentValue, MaxValue, CalculateDeltaKoef());
                Success = true;
            }
        }
        //--------------
        return Success;
    }
    public bool DecreaseValue(float Amount)
    {
        bool Success = false;
        //logic goes here
        if (Amount > 0)
        {
            if ((CurrentValue - Amount) < MinValue)
            {
                CurrentValue = MinValue;
            }
            else
            {
                CurrentValue = CurrentValue - Amount;
            }
            if (OnValueDecreased != null)
            {
                OnValueDecreased(CurrentValue, MaxValue, CalculateDeltaKoef());
                Success = true;
                if (CurrentValue == MinValue)
                {
                    if (OnValueReachedMinimum == null)
                    {
                        Success = false;
                    }
                    else
                    {
                        OnValueReachedMinimum();
                    }
                }
            }
        }
        //--------------
        return Success;
    }
    private float CalculateDeltaKoef()
    {
        return CurrentValue / MaxValue;
    }
    public float GetCurrentValue()
    {
        return CurrentValue;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SetMaxValueAtStart)
        {
            IncreaseValue(MaxValue);
        }
        else
        {
            IncreaseValue(InitialValue);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
