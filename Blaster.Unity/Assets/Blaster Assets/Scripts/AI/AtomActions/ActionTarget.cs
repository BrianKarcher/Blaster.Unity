using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public enum ActionTarget
    {
        Target = 0,
        Self = 1,
        MainCharacter = 2,
        Companion = 3,
        Waypoint1 = 4,
        Waypoint2 = 5,
        MCJointUnit = 6,
        Waypoint3 = 7,
        ManualVector = 8,
        Parent = 9
    }
}
