//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Scripts/Input/GameInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""57496d2a-ab17-4f30-acc8-0b09d97e891b"",
            ""actions"": [
                {
                    ""name"": ""Game_Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""54acfb14-ed06-4d0f-81e1-7829cfef50d1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Game_Move"",
                    ""type"": ""Button"",
                    ""id"": ""a006d76d-3972-43b5-ac31-8f8a82ae254b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b9b33d7f-c20a-46ee-a30f-6a8ae738f6ec"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""MultiTap(tapDelay=0.2),Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Game_Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ba57e41-a146-4d20-8506-b267123658c9"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Game_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""828eb728-670b-41c6-89f6-035359a0c258"",
            ""actions"": [
                {
                    ""name"": ""UI_Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""29ffd165-15b7-4293-9679-9f061b318d48"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""82ef0e5e-f112-46ee-a1d8-15a8f87ed072"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UI_Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Game_Interaction = m_Gameplay.FindAction("Game_Interaction", throwIfNotFound: true);
        m_Gameplay_Game_Move = m_Gameplay.FindAction("Game_Move", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_UI_Interaction = m_UI.FindAction("UI_Interaction", throwIfNotFound: true);
    }

    /* ~@GameInput()
    {
        UnityEngine.Debug.Assert(!m_Gameplay.enabled, "This will cause a leak and performance issues, GameInput.Gameplay.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_UI.enabled, "This will cause a leak and performance issues, GameInput.UI.Disable() has not been called.");
    } */

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_Game_Interaction;
    private readonly InputAction m_Gameplay_Game_Move;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Game_Interaction => m_Wrapper.m_Gameplay_Game_Interaction;
        public InputAction @Game_Move => m_Wrapper.m_Gameplay_Game_Move;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @Game_Interaction.started += instance.OnGame_Interaction;
            @Game_Interaction.performed += instance.OnGame_Interaction;
            @Game_Interaction.canceled += instance.OnGame_Interaction;
            @Game_Move.started += instance.OnGame_Move;
            @Game_Move.performed += instance.OnGame_Move;
            @Game_Move.canceled += instance.OnGame_Move;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @Game_Interaction.started -= instance.OnGame_Interaction;
            @Game_Interaction.performed -= instance.OnGame_Interaction;
            @Game_Interaction.canceled -= instance.OnGame_Interaction;
            @Game_Move.started -= instance.OnGame_Move;
            @Game_Move.performed -= instance.OnGame_Move;
            @Game_Move.canceled -= instance.OnGame_Move;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_UI_Interaction;
    public struct UIActions
    {
        private @GameInput m_Wrapper;
        public UIActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @UI_Interaction => m_Wrapper.m_UI_UI_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @UI_Interaction.started += instance.OnUI_Interaction;
            @UI_Interaction.performed += instance.OnUI_Interaction;
            @UI_Interaction.canceled += instance.OnUI_Interaction;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @UI_Interaction.started -= instance.OnUI_Interaction;
            @UI_Interaction.performed -= instance.OnUI_Interaction;
            @UI_Interaction.canceled -= instance.OnUI_Interaction;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IGameplayActions
    {
        
        void OnGame_Interaction(InputAction.CallbackContext context);
        void OnGame_Move(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnUI_Interaction(InputAction.CallbackContext context);
    }
}
