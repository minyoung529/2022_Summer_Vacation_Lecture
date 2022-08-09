using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveBackCommand : Command
    {
        private MoveObject moveObj;

        public MoveBackCommand(MoveObject moveObject)
        {
            this.moveObj = moveObject;
        }

        public override void Execute()
        {
            moveObj.MoveBack();
        }

        public override void Undo()
        {
            moveObj.MoveForward();
        }
    }
}
