// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Runner"",
            ""id"": ""047a2e42-913f-41af-88ed-fe3ab3645ecc"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d02587c7-1a84-47c7-90fa-7320dd9291bb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9e745d83-befe-4f29-a603-04ca5dd8fa00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e1f2c9e4-3047-49d6-b4c9-2c7a868ad9f1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false,invertY=false)"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cd4e86f4-5526-4ab7-92eb-020ee21d49a2"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1f1f28bb-8131-426e-939d-3223fd8886ba"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ae3bbda-cf66-4f3f-b617-43fbcc942557"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0d4ff01-03e4-4541-bb14-791cd23885c0"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ac903bff-efa4-46db-8eb4-71dbdca3bd07"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Creator"",
            ""id"": ""1bccef08-fe31-44f1-b574-4283b40c9379"",
            ""actions"": [
                {
                    ""name"": ""ClickCreate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b2e665f1-4813-402d-bd9a-e2669ee91f6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Press"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0d6e4b21-e840-4bff-8454-669152cdb4a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b18084ed-fd38-4237-bbae-102381b89116"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""ClickCreate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a16508cb-e451-4749-b015-028b2f9bf164"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Pad"",
            ""bindingGroup"": ""Pad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyMouse"",
            ""bindingGroup"": ""KeyMouse"",
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
        // Runner
        m_Runner = asset.FindActionMap("Runner", throwIfNotFound: true);
        m_Runner_Move = m_Runner.FindAction("Move", throwIfNotFound: true);
        m_Runner_Jump = m_Runner.FindAction("Jump", throwIfNotFound: true);
        // Creator
        m_Creator = asset.FindActionMap("Creator", throwIfNotFound: true);
        m_Creator_ClickCreate = m_Creator.FindAction("ClickCreate", throwIfNotFound: true);
        m_Creator_Press = m_Creator.FindAction("Press", throwIfNotFound: true);
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

    // Runner
    private readonly InputActionMap m_Runner;
    private IRunnerActions m_RunnerActionsCallbackInterface;
    private readonly InputAction m_Runner_Move;
    private readonly InputAction m_Runner_Jump;
    public struct RunnerActions
    {
        private @PlayerControls m_Wrapper;
        public RunnerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Runner_Move;
        public InputAction @Jump => m_Wrapper.m_Runner_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Runner; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RunnerActions set) { return set.Get(); }
        public void SetCallbacks(IRunnerActions instance)
        {
            if (m_Wrapper.m_RunnerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_RunnerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_RunnerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_RunnerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_RunnerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public RunnerActions @Runner => new RunnerActions(this);

    // Creator
    private readonly InputActionMap m_Creator;
    private ICreatorActions m_CreatorActionsCallbackInterface;
    private readonly InputAction m_Creator_ClickCreate;
    private readonly InputAction m_Creator_Press;
    public struct CreatorActions
    {
        private @PlayerControls m_Wrapper;
        public CreatorActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ClickCreate => m_Wrapper.m_Creator_ClickCreate;
        public InputAction @Press => m_Wrapper.m_Creator_Press;
        public InputActionMap Get() { return m_Wrapper.m_Creator; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CreatorActions set) { return set.Get(); }
        public void SetCallbacks(ICreatorActions instance)
        {
            if (m_Wrapper.m_CreatorActionsCallbackInterface != null)
            {
                @ClickCreate.started -= m_Wrapper.m_CreatorActionsCallbackInterface.OnClickCreate;
                @ClickCreate.performed -= m_Wrapper.m_CreatorActionsCallbackInterface.OnClickCreate;
                @ClickCreate.canceled -= m_Wrapper.m_CreatorActionsCallbackInterface.OnClickCreate;
                @Press.started -= m_Wrapper.m_CreatorActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_CreatorActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_CreatorActionsCallbackInterface.OnPress;
            }
            m_Wrapper.m_CreatorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ClickCreate.started += instance.OnClickCreate;
                @ClickCreate.performed += instance.OnClickCreate;
                @ClickCreate.canceled += instance.OnClickCreate;
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
            }
        }
    }
    public CreatorActions @Creator => new CreatorActions(this);
    private int m_PadSchemeIndex = -1;
    public InputControlScheme PadScheme
    {
        get
        {
            if (m_PadSchemeIndex == -1) m_PadSchemeIndex = asset.FindControlSchemeIndex("Pad");
            return asset.controlSchemes[m_PadSchemeIndex];
        }
    }
    private int m_KeyMouseSchemeIndex = -1;
    public InputControlScheme KeyMouseScheme
    {
        get
        {
            if (m_KeyMouseSchemeIndex == -1) m_KeyMouseSchemeIndex = asset.FindControlSchemeIndex("KeyMouse");
            return asset.controlSchemes[m_KeyMouseSchemeIndex];
        }
    }
    public interface IRunnerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ICreatorActions
    {
        void OnClickCreate(InputAction.CallbackContext context);
        void OnPress(InputAction.CallbackContext context);
    }
}
