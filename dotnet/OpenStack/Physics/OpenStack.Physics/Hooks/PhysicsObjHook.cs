using System;

namespace OpenStack.Physics.Hooks
{
    public class PhysicsObjHook
    {
        public PhysicsHookType HookType;
        public double TimeCreated;
        public double InterpolationTime;
        public Object UserData;

        public virtual bool Execute(PhysicsObj obj)
        {
            return false;
        }
    }
}
