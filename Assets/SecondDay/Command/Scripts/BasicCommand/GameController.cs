using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class GameController : MonoBehaviour
    {
        public MoveObject moveObj;  // 움직여야 할 객체 링크용

        // 커맨드로 쓸 키들
        private Command buttonW;
        private Command buttonA;
        private Command buttonS;
        private Command buttonD;

        private Stack<Command> undoCommands = new Stack<Command>();
        private Stack<Command> redoCommands = new Stack<Command>();

        private bool isReplaying = false;
        private Vector3 startPos = Vector3.zero;
        private const float REPLAY_PAUSE_TIME = 0.3f;

        private void Start()
        {
            // 키 바인딩
            startPos = moveObj.transform.position;

            buttonW = new MoveForwardCommand(moveObj);
            buttonA = new TurnLeftCommand(moveObj);
            buttonS = new MoveBackCommand(moveObj);
            buttonD = new TurnRightCommand(moveObj);
        }

        private void Update()
        {
            if (isReplaying) return;

            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("W");
                ExecuteNewCommand(buttonW);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A");
                ExecuteNewCommand(buttonA);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("S");
                ExecuteNewCommand(buttonS);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D");
                ExecuteNewCommand(buttonD);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwapKey(ref buttonW, ref buttonS);
                SwapKey(ref buttonA, ref buttonD);
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                Undo();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Redo();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(ReplayCoroutine());
                isReplaying = true;
            }
        }

        private void ExecuteNewCommand(Command command)
        {
            undoCommands.Push(command);
            redoCommands.Clear();

            command.Execute();
        }

        private void SwapKey(ref Command command1, ref Command command2)
        {
            Command temp = command1;
            command2 = command1;
            command1 = temp;
        }

        private IEnumerator ReplayCoroutine()
        {
            moveObj.transform.position = startPos;

            Command[] oldCommands = undoCommands.ToArray();

            for(int i = oldCommands.Length - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(REPLAY_PAUSE_TIME);

                Command curCommand = oldCommands[i];
                curCommand.Execute();
            }

            undoCommands.Clear();
            redoCommands.Clear();
            startPos = transform.position;
            isReplaying = false;
        }

        private void Undo()
        {
            if (undoCommands.Count == 0)
            {
                print("No Undo");
            }
            else
            {
                Command lastCommand = undoCommands.Pop();
                lastCommand.Undo();
                redoCommands.Push(lastCommand);
            }
        }

        private void Redo()
        {
            if (redoCommands.Count == 0)
            {
                print("No Redo");
            }
            else
            {
                Command nextCommand = redoCommands.Pop();
                nextCommand.Execute();
                undoCommands.Push(nextCommand);
            }
        }
    }
}

// 1. Undo      U
// 2. Redo      R
// 3. Replay    ENTER

// Undo, Redo => Stack