using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveForwardCommand : Command
    {
        private MoveObject moveObj;

        public MoveForwardCommand(MoveObject moveObject)
        {
            this.moveObj = moveObject;
        }

        public override void Execute()
        {
            moveObj.MoveForward();
        }

        public override void Undo()
        {
            moveObj.MoveBack();
        }
    }
}
