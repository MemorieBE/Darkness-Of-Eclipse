//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Inputs/GameInputs.inputactions
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

public partial class @GameInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputs"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""e97d5d57-1c93-4199-942b-785721a7eafa"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0ccc6aad-7cd3-4c9e-9d06-8d95356e2dbf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d0feac90-05ea-466e-b466-592b0a167e43"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sneak"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dc2afbaf-91fc-4ae4-b443-ee09c1f8da17"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b2fb9b04-daa0-4058-a85c-7ab9b9db5b4b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""39178d42-3f90-4078-8b8f-2ea9174cead1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f6215a1d-487e-448f-aa68-e4a5e5bcadf2"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""969f1bc9-6731-46a3-81b0-4dbef4a65731"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""477597d4-8156-4d94-a184-70d1f57a1654"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fb956773-b645-411d-9738-55c72f4706ae"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c2646ecb-66df-4323-9e55-13f1df100a63"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6d3d362d-e272-41fa-865a-872c2692f371"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""629c99bb-6384-4572-a92a-8a19a3cbae6b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b121ce72-ee4f-485a-a27a-ca5622547bcc"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e4a4360-258a-4754-b561-a3ca61b6d05e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mechanics"",
            ""id"": ""f078df48-260c-4857-8491-cc7810fc71d6"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""4c03b76a-c767-40f5-b992-02827a9a65de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toggle Flashlight"",
                    ""type"": ""Button"",
                    ""id"": ""ef3da80c-239a-4701-aadc-fb5737d8db06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toggle Equippable"",
                    ""type"": ""Button"",
                    ""id"": ""73dfd315-8e48-43e3-95fc-41bfca5618ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Trigger Equippable"",
                    ""type"": ""Button"",
                    ""id"": ""253cf9cc-f497-412d-a229-08ad0edcd945"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Use Wisp"",
                    ""type"": ""Button"",
                    ""id"": ""8ee170fe-0643-4d81-babd-7f8828162ca9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Exit Action"",
                    ""type"": ""Button"",
                    ""id"": ""501db987-1a35-4fd6-8f25-a48267785a79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d71532b5-8cf7-4b0f-8a49-eb14a38937d0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27d28266-a620-4809-b389-9302571cd135"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Toggle Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""300c34fb-846d-4235-b0d9-dd3084ca16f6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Toggle Equippable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""990a364c-2eb2-45d8-9cc3-61f0fb72951c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Trigger Equippable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18760f7f-7e52-4059-b501-5d2e71edb390"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Use Wisp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""621bd3be-2fa9-41e1-a5cb-dacb0c1cb5ac"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""db341a73-1afb-419f-ada6-b0c213a751ce"",
            ""actions"": [
                {
                    ""name"": ""Escape/Pause"",
                    ""type"": ""Button"",
                    ""id"": ""feb30374-24c7-4959-94bb-bf13bf5f12aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""04ab5923-bae0-48be-a005-c48fc68a78cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cheat Input Field"",
                    ""type"": ""Button"",
                    ""id"": ""77818b5e-a3f2-432c-977c-7be053aa8e0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Game Menu"",
                    ""type"": ""Button"",
                    ""id"": ""bdef1bd4-793b-4e17-be41-597c9b3ed50f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""27e8751e-f9d2-4377-96e9-3bddd25387e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Notepad"",
                    ""type"": ""Button"",
                    ""id"": ""6edbdccd-561a-41d1-a3ec-18f75fb1a42f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Last Command"",
                    ""type"": ""Button"",
                    ""id"": ""2b452ce3-0451-4253-bed6-c41c864373b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d8483465-1d5a-48d8-a2f0-f816ce6ecace"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Escape/Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a6be312-d1c9-40b3-8b0f-b3cd31bf9e08"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Cheat Input Field"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34b3b945-2dcb-48d8-9667-72d45bb4736f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Game Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8502387b-945d-4ef7-90f7-d5837cdc54d2"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6741405b-5fa2-4b8e-a8a2-135b7b38bf76"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Notepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b4857c6-0842-4e14-b034-6370e1ec634f"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e887376-8210-44e3-b251-fc0a492fb149"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Last Command"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_Sprint = m_Movement.FindAction("Sprint", throwIfNotFound: true);
        m_Movement_Sneak = m_Movement.FindAction("Sneak", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_Look = m_Movement.FindAction("Look", throwIfNotFound: true);
        // Mechanics
        m_Mechanics = asset.FindActionMap("Mechanics", throwIfNotFound: true);
        m_Mechanics_Interact = m_Mechanics.FindAction("Interact", throwIfNotFound: true);
        m_Mechanics_ToggleFlashlight = m_Mechanics.FindAction("Toggle Flashlight", throwIfNotFound: true);
        m_Mechanics_ToggleEquippable = m_Mechanics.FindAction("Toggle Equippable", throwIfNotFound: true);
        m_Mechanics_TriggerEquippable = m_Mechanics.FindAction("Trigger Equippable", throwIfNotFound: true);
        m_Mechanics_UseWisp = m_Mechanics.FindAction("Use Wisp", throwIfNotFound: true);
        m_Mechanics_ExitAction = m_Mechanics.FindAction("Exit Action", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_EscapePause = m_Menu.FindAction("Escape/Pause", throwIfNotFound: true);
        m_Menu_Confirm = m_Menu.FindAction("Confirm", throwIfNotFound: true);
        m_Menu_CheatInputField = m_Menu.FindAction("Cheat Input Field", throwIfNotFound: true);
        m_Menu_GameMenu = m_Menu.FindAction("Game Menu", throwIfNotFound: true);
        m_Menu_Inventory = m_Menu.FindAction("Inventory", throwIfNotFound: true);
        m_Menu_Notepad = m_Menu.FindAction("Notepad", throwIfNotFound: true);
        m_Menu_LastCommand = m_Menu.FindAction("Last Command", throwIfNotFound: true);
    }

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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_Sprint;
    private readonly InputAction m_Movement_Sneak;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_Look;
    public struct MovementActions
    {
        private @GameInputs m_Wrapper;
        public MovementActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @Sprint => m_Wrapper.m_Movement_Sprint;
        public InputAction @Sneak => m_Wrapper.m_Movement_Sneak;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @Look => m_Wrapper.m_Movement_Look;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Sneak.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSneak;
                @Sneak.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSneak;
                @Sneak.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSneak;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Sneak.started += instance.OnSneak;
                @Sneak.performed += instance.OnSneak;
                @Sneak.canceled += instance.OnSneak;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Mechanics
    private readonly InputActionMap m_Mechanics;
    private IMechanicsActions m_MechanicsActionsCallbackInterface;
    private readonly InputAction m_Mechanics_Interact;
    private readonly InputAction m_Mechanics_ToggleFlashlight;
    private readonly InputAction m_Mechanics_ToggleEquippable;
    private readonly InputAction m_Mechanics_TriggerEquippable;
    private readonly InputAction m_Mechanics_UseWisp;
    private readonly InputAction m_Mechanics_ExitAction;
    public struct MechanicsActions
    {
        private @GameInputs m_Wrapper;
        public MechanicsActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Mechanics_Interact;
        public InputAction @ToggleFlashlight => m_Wrapper.m_Mechanics_ToggleFlashlight;
        public InputAction @ToggleEquippable => m_Wrapper.m_Mechanics_ToggleEquippable;
        public InputAction @TriggerEquippable => m_Wrapper.m_Mechanics_TriggerEquippable;
        public InputAction @UseWisp => m_Wrapper.m_Mechanics_UseWisp;
        public InputAction @ExitAction => m_Wrapper.m_Mechanics_ExitAction;
        public InputActionMap Get() { return m_Wrapper.m_Mechanics; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MechanicsActions set) { return set.Get(); }
        public void SetCallbacks(IMechanicsActions instance)
        {
            if (m_Wrapper.m_MechanicsActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnInteract;
                @ToggleFlashlight.started -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.performed -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.canceled -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnToggleFlashlight;
                @ToggleEquippable.started -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnToggleEquippable;
                @ToggleEquippable.performed -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnToggleEquippable;
                @ToggleEquippable.canceled -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnToggleEquippable;
                @TriggerEquippable.started -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnTriggerEquippable;
                @TriggerEquippable.performed -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnTriggerEquippable;
                @TriggerEquippable.canceled -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnTriggerEquippable;
                @UseWisp.started -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnUseWisp;
                @UseWisp.performed -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnUseWisp;
                @UseWisp.canceled -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnUseWisp;
                @ExitAction.started -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnExitAction;
                @ExitAction.performed -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnExitAction;
                @ExitAction.canceled -= m_Wrapper.m_MechanicsActionsCallbackInterface.OnExitAction;
            }
            m_Wrapper.m_MechanicsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @ToggleFlashlight.started += instance.OnToggleFlashlight;
                @ToggleFlashlight.performed += instance.OnToggleFlashlight;
                @ToggleFlashlight.canceled += instance.OnToggleFlashlight;
                @ToggleEquippable.started += instance.OnToggleEquippable;
                @ToggleEquippable.performed += instance.OnToggleEquippable;
                @ToggleEquippable.canceled += instance.OnToggleEquippable;
                @TriggerEquippable.started += instance.OnTriggerEquippable;
                @TriggerEquippable.performed += instance.OnTriggerEquippable;
                @TriggerEquippable.canceled += instance.OnTriggerEquippable;
                @UseWisp.started += instance.OnUseWisp;
                @UseWisp.performed += instance.OnUseWisp;
                @UseWisp.canceled += instance.OnUseWisp;
                @ExitAction.started += instance.OnExitAction;
                @ExitAction.performed += instance.OnExitAction;
                @ExitAction.canceled += instance.OnExitAction;
            }
        }
    }
    public MechanicsActions @Mechanics => new MechanicsActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_EscapePause;
    private readonly InputAction m_Menu_Confirm;
    private readonly InputAction m_Menu_CheatInputField;
    private readonly InputAction m_Menu_GameMenu;
    private readonly InputAction m_Menu_Inventory;
    private readonly InputAction m_Menu_Notepad;
    private readonly InputAction m_Menu_LastCommand;
    public struct MenuActions
    {
        private @GameInputs m_Wrapper;
        public MenuActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @EscapePause => m_Wrapper.m_Menu_EscapePause;
        public InputAction @Confirm => m_Wrapper.m_Menu_Confirm;
        public InputAction @CheatInputField => m_Wrapper.m_Menu_CheatInputField;
        public InputAction @GameMenu => m_Wrapper.m_Menu_GameMenu;
        public InputAction @Inventory => m_Wrapper.m_Menu_Inventory;
        public InputAction @Notepad => m_Wrapper.m_Menu_Notepad;
        public InputAction @LastCommand => m_Wrapper.m_Menu_LastCommand;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @EscapePause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnEscapePause;
                @EscapePause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnEscapePause;
                @EscapePause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnEscapePause;
                @Confirm.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @CheatInputField.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCheatInputField;
                @CheatInputField.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCheatInputField;
                @CheatInputField.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCheatInputField;
                @GameMenu.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnGameMenu;
                @GameMenu.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnGameMenu;
                @GameMenu.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnGameMenu;
                @Inventory.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnInventory;
                @Notepad.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnNotepad;
                @Notepad.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnNotepad;
                @Notepad.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnNotepad;
                @LastCommand.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnLastCommand;
                @LastCommand.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnLastCommand;
                @LastCommand.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnLastCommand;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EscapePause.started += instance.OnEscapePause;
                @EscapePause.performed += instance.OnEscapePause;
                @EscapePause.canceled += instance.OnEscapePause;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @CheatInputField.started += instance.OnCheatInputField;
                @CheatInputField.performed += instance.OnCheatInputField;
                @CheatInputField.canceled += instance.OnCheatInputField;
                @GameMenu.started += instance.OnGameMenu;
                @GameMenu.performed += instance.OnGameMenu;
                @GameMenu.canceled += instance.OnGameMenu;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Notepad.started += instance.OnNotepad;
                @Notepad.performed += instance.OnNotepad;
                @Notepad.canceled += instance.OnNotepad;
                @LastCommand.started += instance.OnLastCommand;
                @LastCommand.performed += instance.OnLastCommand;
                @LastCommand.canceled += instance.OnLastCommand;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnSneak(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
    public interface IMechanicsActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnToggleFlashlight(InputAction.CallbackContext context);
        void OnToggleEquippable(InputAction.CallbackContext context);
        void OnTriggerEquippable(InputAction.CallbackContext context);
        void OnUseWisp(InputAction.CallbackContext context);
        void OnExitAction(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnEscapePause(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnCheatInputField(InputAction.CallbackContext context);
        void OnGameMenu(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnNotepad(InputAction.CallbackContext context);
        void OnLastCommand(InputAction.CallbackContext context);
    }
}
