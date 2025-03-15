using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables:
    public GameObject PlayerCharacter, EnemyCharacter;
    public enum PlayerActionTypes
    {
        Attack,
        Heal,
        Counter,
        Ultimate
    }
    public enum EnemyActionTypes
    {
        Attack,
        Heal,
        Counter,
        Ultimate
    }
    private EnemyController EnemyControllerComponent;
    private PlayerController PlayerControllerComponent;
    //delegates: 

    //methods: 
    public void ReceiveInputFromPlayer(int Input)
    {
        switch (Input)
        {
            case 1:
                PlayerAction(PlayerActionTypes.Attack);
                break;
            case 2:
                PlayerAction(PlayerActionTypes.Heal);
                break;
            case 3:
                PlayerAction(PlayerActionTypes.Counter);
                break;
            case 4:
                PlayerAction(PlayerActionTypes.Ultimate);
                break;
        }
    }
    public bool PlayerAction(PlayerActionTypes SelectedAction)
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
    public bool EnemyAction(EnemyActionTypes SelectedAction)
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
        }
        if (EnemyCharacter != null)
        {
            EnemyControllerComponent = EnemyCharacter.gameObject.GetComponent<EnemyController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
