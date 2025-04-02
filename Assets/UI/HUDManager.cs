using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections; 
using static HUDManager;

public class HUDManager : MonoBehaviour
{
    //variables:
    public GameObject PlayerCharacter, EnemyCharacter;
    public enum HUDStates
    {
        CombatScreen,
        GameOverScreen
    }
    /*[Serializable]
    public class ActionButtonAndCostValueTypePair
    {
        public enum ActionCostTypes
        {
            HealthPoints,
            MagicPoints
        }
        public struct ActionCost_ValueAndType
        {
            public ActionCost_ValueAndType(float InActionCostValue, ActionCostTypes InActionCostType)
            {
                ActionCostValue = InActionCostValue;
                ActionCostType = InActionCostType;
            }
            public float ActionCostValue { get; }
            public ActionCostTypes ActionCostType { get; }

            public override string ToString() => $"({ActionCostValue},{ActionCostType})";
        }
        public Button ActionButton;
        public ActionCost_ValueAndType CostValueAndType;

    }*/
    public enum ActionCostTypes
    {
        HealthPoints,
        MagicPoints
    }
    private string GameOverText;
    private HUDStates Current;
    [SerializeField]
    private Slider PlayerHPBar, PlayerMPBar, EnemyHPBar;
    [SerializeField]
    private GameObject GameOverScreen, CombatScreen, PlayerWinsText, EnemyWinsText;
    /*[SerializeField]
    private ActionButtonAndCostValueTypePair[] PlayerActionsAndMagicPointsCost;*/
    [SerializeField]
    private Button[] PlayerActionButtons;
    [SerializeField]
    private float[] PlayerActionCostValues;
    [SerializeField]
    private ActionCostTypes[] PlayerActionsCostTypes;
    private bool IsPlayerWon=false;
    //delegates:
    private void HandleOnPlayerHealthPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef) {
        //
        UpdatePlayerHPBar(DeltaKoef);
        UpdatePlayerActionButtons(ActionCostTypes.HealthPoints, CurrentValue);
    }
    private void HandleOnPlayerHealthPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef) {
        //
        UpdatePlayerHPBar(DeltaKoef);
        UpdatePlayerActionButtons(ActionCostTypes.HealthPoints, CurrentValue);
    }
    private void HandleOnPlayerHealthPointsReachedMinimum() {
        //
        IsPlayerWon = false;
        UpdateHUDState(HUDStates.GameOverScreen);
    }
    private void HandleOnPlayerMagicPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef) {
        //
        UpdatePlayerMPBar(DeltaKoef);
        UpdatePlayerActionButtons(ActionCostTypes.MagicPoints, CurrentValue);
    }
    private void HandleOnPlayerMagicPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef) {
        //
        UpdatePlayerMPBar(DeltaKoef);
        UpdatePlayerActionButtons(ActionCostTypes.MagicPoints, CurrentValue);
    }
    private void HandleOnPlayerMagicPointsReachedMinimum() { 
        //
    }
    private void HandleOnEnemyHealthPointsIncreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
        UpdateEnemyHPBar(DeltaKoef);
    }
    private void HandleOnEnemyHealthPointsDecreased(float CurrentValue, float MaxValue, float DeltaKoef)
    {
        //
        UpdateEnemyHPBar(DeltaKoef);
    }
    private void HandleOnEnemyHealthPointsReachedMinimum()
    {
        IsPlayerWon = true;
        UpdateHUDState(HUDStates.GameOverScreen);
    }
    //methods:
    private bool UpdatePlayerHPBar(float NewValue)
    {
        //
        bool Success = true;
        PlayerHPBar.value = NewValue;
        return Success;
    }
    private bool UpdatePlayerMPBar(float NewValue)
    {
        //
        bool Success = true;
        PlayerMPBar.value = NewValue;
        return Success;
    }
    private bool UpdateEnemyHPBar(float NewValue)
    {
        //
        bool Success = true;
        EnemyHPBar.value = NewValue;
        return Success;
    }
    private bool UpdateHUDState(HUDStates NewState)
    {
        //
        bool Success = false;
        Debug.Log("Starting Changing UI's state..");
        if (NewState == HUDStates.CombatScreen)
        {
            StartCoroutine(SetUIStateToCombatScreen());
            Success = true;
        }
        else if (NewState == HUDStates.GameOverScreen)
        {
            StartCoroutine(SetUIStateToGameOverScreen());
            Success = true;
        }
        return Success;
    }
    private IEnumerator SetUIStateToCombatScreen()
    {
        CombatScreen.SetActive(true);
        yield return new WaitForSeconds(0);
        GameOverScreen.SetActive(false);
        Debug.Log("..Changed UI's state to CombatScreen");
    }
    private IEnumerator SetUIStateToGameOverScreen()
    {
        CombatScreen.SetActive(false);
        yield return new WaitForSeconds(5);
        GameOverScreen.SetActive(true);
        if (IsPlayerWon)
        {
            PlayerWinsText.SetActive(true);
            EnemyWinsText.SetActive(false);
        }
        else
        {
            PlayerWinsText.SetActive(false);
            EnemyWinsText.SetActive(true);
        }
            Debug.Log("..Changed UI's state to GameOverScreen");
    }
    private bool UpdatePlayerActionButtons(ActionCostTypes CheckedResourceType, float ControlValue)
    {
        //
        bool Success = false;
        if(PlayerActionButtons.Length == PlayerActionCostValues.Length && PlayerActionCostValues.Length == PlayerActionsCostTypes.Length)
        {
            for (int i = 0; i < PlayerActionButtons.Length; i++)
            {
                if (PlayerActionsCostTypes[i] == CheckedResourceType)
                {
                    if (PlayerActionCostValues[i] > ControlValue)
                    {
                        //disable button
                        PlayerActionButtons[i].interactable = false;
                        Success = true;
                    }
                    else
                    {
                        //enable button
                        PlayerActionButtons[i].interactable = true;
                        Success = true;
                    }
                }
            }
        }
        return Success;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerCharacter != null)
        {
            var PlayerHPComponent = PlayerCharacter.gameObject.GetComponent<HealthPoints>();
            var PlayerMPComponent = PlayerCharacter.gameObject.GetComponent<MagicPoints>();
            if (PlayerHPComponent != null)
            {
                PlayerHPComponent.OnValueIncreased += HandleOnPlayerHealthPointsIncreased;
                PlayerHPComponent.OnValueDecreased += HandleOnPlayerHealthPointsDecreased;
                PlayerHPComponent.OnValueReachedMinimum += HandleOnPlayerHealthPointsReachedMinimum;

            }
            if (PlayerMPComponent != null)
            {
                PlayerMPComponent.OnValueIncreased += HandleOnPlayerMagicPointsIncreased;
                PlayerMPComponent.OnValueDecreased += HandleOnPlayerMagicPointsDecreased;
                PlayerMPComponent.OnValueReachedMinimum += HandleOnPlayerMagicPointsReachedMinimum;
            }
        }
        if (EnemyCharacter != null)
        {
            var EnemyHPComponent = EnemyCharacter.gameObject.GetComponent<HealthPoints>();
            //var EnemyMPComponent = EnemyCharacter.gameObject.GetComponent<MagicPoints>();
            if (EnemyHPComponent != null)
            {
                EnemyHPComponent.OnValueIncreased += HandleOnEnemyHealthPointsIncreased;
                EnemyHPComponent.OnValueDecreased += HandleOnEnemyHealthPointsDecreased;
                EnemyHPComponent.OnValueReachedMinimum += HandleOnEnemyHealthPointsReachedMinimum;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
