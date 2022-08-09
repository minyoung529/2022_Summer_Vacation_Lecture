using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class TurnRightCommand : Command
    {
        private MoveObject moveObj;

        public TurnRightCommand(MoveObject moveObject)
        {
            this.moveObj = moveObject;
        }

        public override void Execute()
        {
            moveObj.TurnRight();
        }

        public override void Undo()
        {
            moveObj.TurnLeft();
        }
    }
}
