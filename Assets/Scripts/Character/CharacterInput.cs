using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public float HorizontalInput {  get; private set; }
    public bool JumpPressed { get; private set; }
    public bool RollPressed { get; private set; }
    public bool AttackPressed { get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        RollPressed = Input.GetKeyDown(KeyCode.S);
        AttackPressed = Input.GetMouseButtonDown(0);
    }
}
