using UnityEngine;
using UnityEngine.SceneManagement;

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
    private HealthPoints PlayerHPComponent, EnemyHPComponent;
    private MagicPoints PlayerMPComponent, EnemyMPComponent;
    public float LowHPCostValue, MidHPCostValue, HighHPCostValue, LowMPCostValue, MidMPCostValue, HighMPCostValue;
    //delegates: 

    //methods: 
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    private bool CalculateCurrentRound()
    {
        bool success = false;
        if ((NextPlayerAction != null && NextEnemyAction != null) && (PlayerHPComponent != null && PlayerMPComponent != null) && (EnemyHPComponent!=null && EnemyMPComponent!=null))
        {
            if(NextPlayerAction == PlayerActionTypes.Attack)
            {
                if(NextEnemyAction == EnemyActionTypes.Attack)
                {
                    //both player and enemy deal equal damage
                    PlayerMPComponent.DecreaseValue(LowMPCostValue);
                    EnemyMPComponent.DecreaseValue(LowMPCostValue);

                    PlayerHPComponent.DecreaseValue(LowHPCostValue);
                    EnemyHPComponent.DecreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //player gains magic points additionally
                    PlayerMPComponent.DecreaseValue(LowMPCostValue);
                    EnemyMPComponent.DecreaseValue(MidMPCostValue);

                    PlayerMPComponent.IncreaseValue(MidMPCostValue);
                    EnemyHPComponent.IncreaseValue(LowHPCostValue);
                    EnemyHPComponent.DecreaseValue(MidHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //enemy cancels player's ultimate and gains magic points 
                    PlayerMPComponent.DecreaseValue(LowMPCostValue);
                    EnemyHPComponent.DecreaseValue(LowHPCostValue);

                    EnemyMPComponent.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //both deal damage, but enemy deals more and gains magic points
                    PlayerMPComponent.DecreaseValue(LowMPCostValue);
                    EnemyMPComponent.DecreaseValue(HighMPCostValue);

                    PlayerHPComponent.DecreaseValue(HighHPCostValue);
                    EnemyHPComponent.DecreaseValue(LowHPCostValue);
                    EnemyMPComponent.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player deals damage, enemy does nothing
                    //enemy gains magic point for rest
                    PlayerMPComponent.DecreaseValue(LowMPCostValue);
                    
                    PlayerHPComponent.DecreaseValue(HighHPCostValue);
                    EnemyMPComponent.IncreaseValue(LowMPCostValue);
                    
                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
            }else if (NextPlayerAction == PlayerActionTypes.Heal)
            {
                if (NextEnemyAction == EnemyActionTypes.Attack)
                {
                    //enemy gains magic points additionally
                    PlayerMPComponent.DecreaseValue(MidMPCostValue);
                    EnemyMPComponent.DecreaseValue(LowMPCostValue);

                    PlayerHPComponent.IncreaseValue(LowHPCostValue);
                    PlayerHPComponent.DecreaseValue(MidHPCostValue);
                    EnemyMPComponent.IncreaseValue(MidMPCostValue);
                    
                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //both enemy and player heal themselves
                    PlayerMPComponent.DecreaseValue(MidMPCostValue);
                    EnemyMPComponent.DecreaseValue(MidMPCostValue);

                    PlayerHPComponent.IncreaseValue(LowHPCostValue);
                    EnemyHPComponent.IncreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;

                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //player heals themselves big
                    PlayerMPComponent.DecreaseValue(MidMPCostValue);
                    EnemyHPComponent.DecreaseValue(LowHPCostValue);

                    PlayerHPComponent.IncreaseValue(MidHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //player heals themselves but enemy deals big damage and gains magic points
                    PlayerMPComponent.DecreaseValue(MidMPCostValue);
                    EnemyMPComponent.DecreaseValue(HighMPCostValue);

                    PlayerHPComponent.IncreaseValue(LowHPCostValue);
                    PlayerHPComponent.DecreaseValue(HighHPCostValue);
                    EnemyMPComponent.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player heals, enemy does nothing
                    //enemy gains magic point for rest
                    PlayerMPComponent.DecreaseValue(MidMPCostValue);

                    PlayerHPComponent.IncreaseValue(LowHPCostValue);
                    EnemyMPComponent.IncreaseValue(LowMPCostValue);

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
                    PlayerHPComponent.DecreaseValue(LowHPCostValue);
                    EnemyMPComponent.DecreaseValue(LowMPCostValue);

                    PlayerMPComponent.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //enemy heals themselves big
                    PlayerHPComponent.DecreaseValue(LowHPCostValue);
                    EnemyMPComponent.DecreaseValue(LowMPCostValue);

                    EnemyHPComponent.IncreaseValue(MidHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //both player and enemy fail to counter each other
                    PlayerHPComponent.DecreaseValue(LowHPCostValue);
                    EnemyHPComponent.DecreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //player cancels enemy's ultimate and gains magic points big
                    PlayerHPComponent.DecreaseValue(LowHPCostValue);
                    EnemyMPComponent.DecreaseValue(HighMPCostValue);

                    PlayerMPComponent.IncreaseValue(MidMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player fails to counter as enemy does nothing
                    //enemy gains magic point for rest
                    PlayerHPComponent.DecreaseValue(LowHPCostValue);

                    EnemyMPComponent.IncreaseValue(LowMPCostValue);

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
                    PlayerMPComponent.DecreaseValue(HighMPCostValue);
                    EnemyMPComponent.DecreaseValue(LowMPCostValue);

                    PlayerHPComponent.DecreaseValue(LowHPCostValue);
                    PlayerMPComponent.IncreaseValue(LowMPCostValue);
                    EnemyHPComponent.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //enemy heals themselves but player deals big damage and gains magic points
                    PlayerMPComponent.DecreaseValue(HighMPCostValue);
                    EnemyMPComponent.DecreaseValue(MidMPCostValue);

                    PlayerMPComponent.IncreaseValue(LowMPCostValue);
                    EnemyHPComponent.IncreaseValue(LowHPCostValue);
                    EnemyHPComponent.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //enemy cancels player's ultimate and gains magic points big
                    PlayerMPComponent.DecreaseValue(HighMPCostValue);
                    EnemyHPComponent.DecreaseValue(LowHPCostValue);

                    EnemyMPComponent.IncreaseValue(MidMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //both deal big damage
                    PlayerMPComponent.DecreaseValue(HighMPCostValue);
                    EnemyMPComponent.DecreaseValue(HighMPCostValue);

                    PlayerHPComponent.DecreaseValue(HighHPCostValue);
                    EnemyHPComponent.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player deals big damage, enemy does nothing
                    //enemy gains magic point for rest
                    PlayerMPComponent.DecreaseValue(HighMPCostValue);

                    EnemyHPComponent.DecreaseValue(HighHPCostValue);
                    EnemyMPComponent.IncreaseValue(LowMPCostValue);

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
                    EnemyMPComponent.DecreaseValue(LowMPCostValue);

                    PlayerMPComponent.IncreaseValue(LowMPCostValue);
                    PlayerHPComponent.DecreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Heal)
                {
                    //player does nothing, enemy heals themselves
                    //player gains magic points for rest
                    EnemyMPComponent.DecreaseValue(MidMPCostValue);

                    PlayerMPComponent.IncreaseValue(LowMPCostValue);
                    EnemyHPComponent.IncreaseValue(LowHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);
                }
                else if (NextEnemyAction == EnemyActionTypes.Counter)
                {
                    //player does nothing, enemy fails to counter
                    //player gains magic points for rest
                    EnemyHPComponent.DecreaseValue(LowHPCostValue);

                    PlayerMPComponent.IncreaseValue(LowMPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Ultimate)
                {
                    //player does nothing, enemy deals big damage
                    //player gains magic points for rest
                    EnemyMPComponent.DecreaseValue(HighMPCostValue);

                    PlayerMPComponent.IncreaseValue(LowMPCostValue);
                    PlayerHPComponent.DecreaseValue(HighHPCostValue);

                    PlayPlayerAction(NextPlayerAction);
                    PlayEnemyAction(NextEnemyAction);

                    success = true;
                }
                else if (NextEnemyAction == EnemyActionTypes.Skip)
                {
                    //player does nothing, enemy does nothing
                    //both gain magic points for rest
                    PlayerMPComponent.IncreaseValue(LowMPCostValue);
                    EnemyMPComponent.IncreaseValue(LowMPCostValue);

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
        if (EnemyMPComponent.GetCurrentValue()>=EnemyMPComponent.MaxValue)
        {
            ActionIndex = 6;
        }
        else
        {
            ActionIndex = RoundNumber % 6;
        }
        //PlayEnemyAction(EnemyControllerComponent.GetEnemyActionByIndex(ActionIndex));
        NextEnemyAction = EnemyControllerComponent.GetEnemyActionByIndex(ActionIndex);
        CalculateCurrentRound();
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
            PlayerHPComponent = PlayerCharacter.gameObject.GetComponent<HealthPoints>();
            PlayerMPComponent = PlayerCharacter.gameObject.GetComponent<MagicPoints>();
        }
        if (EnemyCharacter != null)
        {
            EnemyControllerComponent = EnemyCharacter.gameObject.GetComponent<EnemyController>();
            EnemyHPComponent = EnemyCharacter.gameObject.GetComponent<HealthPoints>();
            EnemyMPComponent = EnemyCharacter.gameObject.GetComponent<MagicPoints>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
