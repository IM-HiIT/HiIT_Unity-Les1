using System;
using UnityEngine;

namespace Cikoria.EggTimer
{
    public class EggTimerAction
    {
        public float Delay { get; private set; }
        public float Duration { get; private set; }
        public float StartTime { get; private set; }
        public Action OnFinishAction { get; private set; }
        Action action;

        public EggTimerAction(Action action, TimeStyle timeStyle)
        {
            this.action = action;

            // Determine the time this action was created based on
            // the time style.
            switch (timeStyle)
            {
                case TimeStyle.Scaled:
                    StartTime = Time.time;
                    break;
                case TimeStyle.Unscaled:
                    StartTime = Time.unscaledTime;
                    break;
                case TimeStyle.FixedScaled:
                    StartTime = Time.fixedTime;
                    break;
                case TimeStyle.FixedUnscaled:
                    StartTime = Time.fixedUnscaledTime;
                    break;
            }
        }

        /// <summary>
        /// Invoke this action.
        /// </summary>
        public void Invoke() 
        {
            action.Invoke();
        }

        /// <summary>
        /// Specify for how many seconds this action should be
        /// delayed before it is invoked.
        /// </summary>
        /// <param name="delay">The delay in seconds</param>
        /// <returns></returns>
        public EggTimerAction WithDelay(float delay)
        {
            this.Delay = delay;
            return this;
        }

        /// <summary>
        /// Specify for how many seconds this action should be invoked.
        /// </summary>
        /// <param name="duration">The duration in seconds</param>
        /// <returns></returns>
        public EggTimerAction ForDuration(float duration)
        {
            this.Duration = duration;
            return this;
        }

        /// <summary>
        /// Specify an action that should be invoked when this
        /// action is finished.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public EggTimerAction OnFinish(Action action)
        {
            OnFinishAction = action;
            return this;
        }
    }
}