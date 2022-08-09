using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject playerObj;    // 움직일 캐릭터 링크
    private Animator animator;

    private Command keyQ, keyW, keyE, upArrow;

    private List<Command> commands = new List<Command>();
    private Coroutine replayCoroutine;
    private bool isReplaying = false;
    private bool canStartReplaying = true;

    void Start()
    {
        keyQ = new PerformJump();
        keyW = new PerformKick();
        keyE = new PerformPunch();
        upArrow = new MoveForward();

        animator = playerObj.GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow360>().player = playerObj.transform;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (isReplaying) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExecuteCommand(keyQ);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ExecuteCommand(keyW);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ExecuteCommand(keyE);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ExecuteCommand(upArrow);
        }

        // Replay
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canStartReplaying && commands.Count > 0)
            {
                canStartReplaying = false;

                if (replayCoroutine != null)
                {
                    StopCoroutine(replayCoroutine);
                }

                replayCoroutine = StartCoroutine(Replay());
            }
        }

        // Undo
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (commands.Count > 0)
            {
                Command cmd = commands[commands.Count - 1];
                cmd.Execute(animator, true);

                commands.RemoveAt(commands.Count - 1);
            }
        }
    }

    private void ExecuteCommand(Command command)
    {
        command.Execute(animator, true);
        commands.Add(command);
    }

    private IEnumerator Replay()
    {
        isReplaying = true;

        for(int i = 0; i < commands.Count; i++)
        {
            commands[i].Execute(animator, false);
            yield return new WaitForSeconds(1f);
        }

        isReplaying = false;
        canStartReplaying = true;
    }
}
