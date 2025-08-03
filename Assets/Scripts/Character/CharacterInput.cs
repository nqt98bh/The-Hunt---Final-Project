using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public float HorizontalInput {  get; private set; }
    public bool JumpPressed { get; private set; }
    public bool RollPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool BlockPressed { get; private set; }
    public bool UnBlockPressed { get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        RollPressed = Input.GetKeyDown(KeyCode.S);
        AttackPressed = Input.GetMouseButtonDown(0);
        BlockPressed = Input.GetMouseButtonDown(1);
        UnBlockPressed = Input.GetMouseButtonUp(1);


    }
}
