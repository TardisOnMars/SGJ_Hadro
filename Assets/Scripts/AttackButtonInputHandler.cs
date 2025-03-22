using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AttackButtonInputHandler : MonoBehaviour
{
    public InputActionReference shoutAttackAction;
    public Button attackButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shoutAttackAction.action.performed += ClicAttackButton;
    }

    private void ClicAttackButton(InputAction.CallbackContext context)
    {
        attackButton.onClick.Invoke();
    }

}
