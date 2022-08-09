using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class TurnLeftCommand : Command
    {
        private MoveObject moveObj;

        public TurnLeftCommand(MoveObject moveObject)
        {
            this.moveObj = moveObject;
        }

        public override void Execute()
        {
            moveObj.TurnLeft();
        }

        public override void Undo()
        {
            moveObj.TurnRight();
        }
    }
}
