/* using System.Numerics; */
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public enum InputPhase
{
    Started,
    Performed,
    Cancled
}

[CreateAssetMenu(fileName = "New InputReader", menuName = "Input/InputReader")]
public class InputReader : MonoBehaviour, GameInput.IGameplayActions, GameInput.IUIActions
{
    private GameInput _gameInput;

    public delegate void OnGameInput_Single(Vector3 _target);
    public static event OnGameInput_Single onGameInput_SingleClick;

    public delegate void OnGameInput_Double(Vector3 _target);
    public static event OnGameInput_Double onGameInput_DoubleClick;

    public delegate void OnGameInput_Move(InputPhase phase, Vector3 _target);
    public static event OnGameInput_Move onGameInput_Move;

    public delegate void OnUIInput_Click();
    public static event OnUIInput_Click onUIInput_Click;

    private Camera _cam;
    private Vector3 _targetPosition;

    void OnEnable(){
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(instance: this);
            _gameInput.UI.SetCallbacks(instance: this);

            SetGameplay();
        }

        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void SetGameplay()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.UI.Disable();
    }

    public void SetUI()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.UI.Enable();
    }

    public void OnUI_Interaction(InputAction.CallbackContext context)
    {
        onUIInput_Click?.Invoke();
        
    }

    public void OnGame_Interaction(InputAction.CallbackContext context)
    {
        if (context.performed){
            RayCastMouseHit();
            if (context.interaction is MultiTapInteraction){
                    onGameInput_DoubleClick?.Invoke(_targetPosition);
                }else
                {
                    onGameInput_SingleClick?.Invoke(_targetPosition);
                }
            if (context.interaction is HoldInteraction)
            {

            }
        }
        if (context.canceled){
            if (context.interaction is HoldInteraction)
            {

            }
        }
    }

    public void OnGame_Move(InputAction.CallbackContext context)
    {
        if (context.started){
            onGameInput_Move?.Invoke(InputPhase.Performed, _targetPosition);
        }
        if (context.canceled){
            onGameInput_Move?.Invoke(InputPhase.Cancled, _targetPosition);
        }
    }

    private void RayCastMouseHit()
    {
        Ray _ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit, 100)){
            // TO DO: Adjust so Buttons are excluded

            _targetPosition = _hit.point;
        }

        /* _targetPosition = _cam.ScreenToWorldPoint(Input.mousePosition); */

        /* return Vec; */
    }
}