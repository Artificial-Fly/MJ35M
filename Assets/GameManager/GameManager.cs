using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables:
    private int RoundNumber = 0;
    public GameObject PlayerCharacter, EnemyCharacter;
    public enum PlayerActionTypes
    {
        Attack,
        Heal,
        Counter,
        Ultimate,
        Skip
    }
    public enum EnemyActionTypes
    {
        Attack,
        Heal,
        Counter,
        Ultimate,
        Skip
    }
    private PlayerActionTypes NextPlayerAction;
    private EnemyActionTypes NextEnemyAction;
    private EnemyController EnemyControllerComponent;
    private PlayerController PlayerControllerComponent;
    private HealthPoints PlayerHP, EnemyHP;
    private MagicPoints PlayerMP, EnemyMP;
    public float LowHPCostValue, MidHPCostValue, HighHPCostValue, LowMPCostValue, MidMPCostValue, HighMPCostValue;
    //delegates: 

    //methods: 
    private bool CalculateCurrentRound()
    {
        bool success = false;
        if ((NextPlayerAction != null && NextEnemyAction != null) && (PlayerHP != null && PlayerMP != null) && (EnemyHP!=null && EnemyMP!=null))
        {
            if(NextPlayerAction == PlayerActionTypes.Attack)
            {
                if(NextEnemyAction == EnemyActionTypes.Attack)
                {
                    //both player and enemy deal equal damage
                    PlayerMP.DecreaseValue(LowMPCostValue);
                    EnemyMP.DecreaseValue(LowMPCostValue);

                    PlayerHP.DecreaseValue(LowHPCostValue);
                    EnemyHP.DecreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //player gains magic points additionally
                    PlayerMP.DecreaseValue(LowMPCostValue);
                    EnemyMP.DecreaseValue(MidMPCostValue);

                    PlayerMP.IncreaseValue(MidMPCostValue);
                    EnemyHP.IncreaseValue(LowHPCostValue);
                    EnemyHP.DecreaseValue(MidHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //enemy cancels player's ultimate and gains magic points 
                    PlayerMP.DecreaseValue(LowMPCostValue);
                    EnemyHP.DecreaseValue(LowHPCostValue);

                    EnemyMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //both deal damage, but enemy deals more and gains magic points
                    PlayerMP.DecreaseValue(LowMPCostValue);
                    EnemyMP.DecreaseValue(HighMPCostValue);

                    PlayerHP.DecreaseValue(HighHPCostValue);
                    EnemyHP.DecreaseValue(LowHPCostValue);
                    EnemyMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player deals damage, enemy does nothing
                    //enemy gains magic point for rest
                    PlayerMP.DecreaseValue(LowMPCostValue);
                    
                    PlayerHP.DecreaseValue(HighHPCostValue);
                    EnemyMP.IncreaseValue(LowMPCostValue);
                    
                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
            }else if (NextPlayerAction == PlayerActionTypes.Heal)
            {
                if (NextEnemyAction == EnemyActionTypes.Attack)
                {
                    //enemy gains magic points additionally
                    PlayerMP.DecreaseValue(MidMPCostValue);
                    EnemyMP.DecreaseValue(LowMPCostValue);

                    PlayerHP.IncreaseValue(LowHPCostValue);
                    PlayerHP.DecreaseValue(MidHPCostValue);
                    EnemyMP.IncreaseValue(MidMPCostValue);
                    
                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //both enemy and player heal themselves
                    PlayerMP.DecreaseValue(MidMPCostValue);
                    EnemyMP.DecreaseValue(MidMPCostValue);

                    PlayerHP.IncreaseValue(LowHPCostValue);
                    EnemyHP.IncreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;

                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //player heals themselves big
                    PlayerMP.DecreaseValue(MidMPCostValue);
                    EnemyHP.DecreaseValue(LowHPCostValue);

                    PlayerHP.IncreaseValue(MidHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //player heals themselves but enemy deals big damage and gains magic points
                    PlayerMP.DecreaseValue(MidMPCostValue);
                    EnemyMP.DecreaseValue(HighMPCostValue);

                    PlayerHP.IncreaseValue(LowHPCostValue);
                    PlayerHP.DecreaseValue(HighHPCostValue);
                    EnemyMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player heals, enemy does nothing
                    //enemy gains magic point for rest
                    PlayerMP.DecreaseValue(MidMPCostValue);

                    PlayerHP.IncreaseValue(LowHPCostValue);
                    EnemyMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
            }
            else if (NextPlayerAction == PlayerActionTypes.Counter)
            {
                if (NextEnemyAction == EnemyActionTypes.Attack)
                {
                    //enemy deals no damage, player gains magic points
                    PlayerHP.DecreaseValue(LowHPCostValue);
                    EnemyMP.DecreaseValue(LowMPCostValue);

                    PlayerMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //enemy heals themselves big
                    PlayerHP.DecreaseValue(LowHPCostValue);
                    EnemyMP.DecreaseValue(LowMPCostValue);

                    EnemyHP.IncreaseValue(MidHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //both player and enemy fail to counter each other
                    PlayerHP.DecreaseValue(LowHPCostValue);
                    EnemyHP.DecreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //player cancels enemy's ultimate and gains magic points big
                    PlayerHP.DecreaseValue(LowHPCostValue);
                    EnemyMP.DecreaseValue(HighMPCostValue);

                    PlayerMP.IncreaseValue(MidMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player fails to counter as enemy does nothing
                    //enemy gains magic point for rest
                    PlayerHP.DecreaseValue(LowHPCostValue);

                    EnemyMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
            }
            else if (NextPlayerAction == PlayerActionTypes.Ultimate)
            {
                if (NextEnemyAction == EnemyActionTypes.Attack)
                {
                    //both deal damage, but player deals more and gains magic points
                    PlayerMP.DecreaseValue(HighMPCostValue);
                    EnemyMP.DecreaseValue(LowMPCostValue);

                    PlayerHP.DecreaseValue(LowHPCostValue);
                    PlayerMP.IncreaseValue(LowMPCostValue);
                    EnemyHP.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //enemy heals themselves but player deals big damage and gains magic points
                    PlayerMP.DecreaseValue(HighMPCostValue);
                    EnemyMP.DecreaseValue(MidMPCostValue);

                    PlayerMP.IncreaseValue(LowMPCostValue);
                    EnemyHP.IncreaseValue(LowHPCostValue);
                    EnemyHP.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //enemy cancels player's ultimate and gains magic points big
                    PlayerMP.DecreaseValue(HighMPCostValue);
                    EnemyHP.DecreaseValue(LowHPCostValue);

                    EnemyMP.IncreaseValue(MidMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //both deal big damage
                    PlayerMP.DecreaseValue(HighMPCostValue);
                    EnemyMP.DecreaseValue(HighMPCostValue);

                    PlayerHP.DecreaseValue(HighHPCostValue);
                    EnemyHP.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player deals big damage, enemy does nothing
                    //enemy gains magic point for rest
                    PlayerMP.DecreaseValue(HighMPCostValue);

                    EnemyHP.DecreaseValue(HighHPCostValue);
                    EnemyMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
            }
            else if (NextPlayerAction == PlayerActionTypes.Skip)
            {
                if (NextEnemyAction == EnemyActionTypes.Attack)
                {
                    //player does nothing, enemy deals damage
                    //player gains magic points for rest
                    EnemyMP.DecreaseValue(LowMPCostValue);

                    PlayerMP.IncreaseValue(LowMPCostValue);
                    PlayerHP.DecreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //player does nothing, enemy heals themselves
                    //player gains magic points for rest
                    EnemyMP.DecreaseValue(MidMPCostValue);

                    PlayerMP.IncreaseValue(LowMPCostValue);
                    EnemyHP.IncreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //player does nothing, enemy fails to counter
                    //player gains magic points for rest
                    EnemyHP.DecreaseValue(LowHPCostValue);

                    PlayerMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //player does nothing, enemy deals big damage
                    //player gains magic points for rest
                    EnemyMP.DecreaseValue(HighMPCostValue);

                    PlayerMP.IncreaseValue(LowMPCostValue);
                    PlayerHP.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player does nothing, enemy does nothing
                    //both gain magic points for rest
                    PlayerMP.IncreaseValue(LowMPCostValue);
                    EnemyMP.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
            }
        }
        RoundNumber++;
        return success;
    }
    public void ReceiveInputFromPlayer(int Input)
    {
        switch (Input)
        {
            case 1:
                //PlayPlayerAction(PlayerActionTypes.Attack);
                NextPlayerAction = PlayerActionTypes.Attack;
                break;
            case 2:
                //PlayPlayerAction(PlayerActionTypes.Heal);
                NextPlayerAction = PlayerActionTypes.Heal;
                break;
            case 3:
                //PlayPlayerAction(PlayerActionTypes.Counter);
                NextPlayerAction = PlayerActionTypes.Counter;
                break;
            case 4:
                //PlayPlayerAction(PlayerActionTypes.Ultimate);
                NextPlayerAction = PlayerActionTypes.Ultimate;
                break;
            default:
                NextPlayerAction = PlayerActionTypes.Skip;
                break;
        }
        ReceiveInputFromEnemy();
    }
    private void ReceiveInputFromEnemy()
    {
        //6%6=0%6=0
        //6th action is special and skipped unless condition is met
        int ActionIndex;
        if (EnemyMP.GetCurrentValue()>=EnemyMP.MaxValue)
        {
            ActionIndex = 6;
        }
        else
        {
            ActionIndex = RoundNumber % 6;
        }
        //PlayEnemyAction(EnemyControllerComponent.GetEnemyActionByIndex(ActionIndex));
        NextEnemyAction = EnemyControllerComponent.GetEnemyActionByIndex(ActionIndex);

    }
    public bool PlayPlayerAction(PlayerActionTypes SelectedAction)
    {
        bool Success = false;
        if (PlayerControllerComponent != null)
        {
            if (SelectedAction == PlayerActionTypes.Attack)
            {
                PlayerControllerComponent.Attack();
                Success = true;
            }
            else if (SelectedAction == PlayerActionTypes.Heal)
            {
                PlayerControllerComponent.Heal();
                Success = true;
            }
            else if (SelectedAction == PlayerActionTypes.Counter)
            {
                PlayerControllerComponent.Counter();
                Success = true;
            }
            else if (SelectedAction == PlayerActionTypes.Ultimate)
            {
                PlayerControllerComponent.Ultimate();
                Success = true;
            }
        }
        return Success;
    }
    public bool PlayEnemyAction(EnemyActionTypes SelectedAction)
    {
        bool Success = false;
        if (EnemyControllerComponent != null)
        {
            if (SelectedAction == EnemyActionTypes.Attack)
            {
                EnemyControllerComponent.Attack();
                Success = true;
            }
            else if (SelectedAction == EnemyActionTypes.Heal)
            {
                EnemyControllerComponent.Heal();
                Success = true;
            }
            else if (SelectedAction == EnemyActionTypes.Counter)
            {
                EnemyControllerComponent.Counter();
                Success = true;
            }
            else if (SelectedAction == EnemyActionTypes.Ultimate)
            {
                EnemyControllerComponent.Ultimate();
                Success = true;
            }
        }
        return Success;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerCharacter != null)
        {
            PlayerControllerComponent = PlayerCharacter.gameObject.GetComponent<PlayerController>();
            PlayerHP = PlayerCharacter.gameObject.GetComponent<HealthPoints>();
            PlayerMP = PlayerCharacter.gameObject.GetComponent<MagicPoints>();
        }
        if (EnemyCharacter != null)
        {
            EnemyControllerComponent = EnemyCharacter.gameObject.GetComponent<EnemyController>();
            PlayerHP = EnemyCharacter.gameObject.GetComponent<HealthPoints>();
            PlayerMP = EnemyCharacter.gameObject.GetComponent<MagicPoints>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
