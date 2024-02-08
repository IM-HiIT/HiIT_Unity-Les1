using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cikoria.EggTimer
{
    [AddComponentMenu("")]
    public class EggTimer : MonoBehaviour
    {
        List<EggTimerAction> scaledActions = new List<EggTimerAction>();
        List<EggTimerAction> unscaledActions = new List<EggTimerAction>();
        List<EggTimerAction> fixedActions = new List<EggTimerAction>();
        List<EggTimerAction> fixedUnscaledActions = new List<EggTimerAction>();

        private static EggTimer instance;
        public static EggTimer Instance
        {
            get
            {
                if (!instance)
                {
                    var go = new GameObject("EggTimer");
                    instance = go.AddComponent<EggTimer>();
                    DontDestroyOnLoad(go);
                }

                return instance;
            }
        }

        /// <summary>
        /// Execute an action using a specific time style.
        /// If you specify a duration, the time style will
        /// affect how that duration is evaluted.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="timeStyle"></param>
        /// <returns></returns>
        public EggTimerAction Execute(Action action, TimeStyle timeStyle = TimeStyle.Scaled)
        {
            var eggTimerAction = new EggTimerAction(action, timeStyle);

            switch (timeStyle)
            {
                case TimeStyle.Scaled:
                    scaledActions.Add(eggTimerAction);
                    break;
                case TimeStyle.Unscaled:
                    unscaledActions.Add(eggTimerAction);
                    break;
                case TimeStyle.FixedScaled:
                    fixedActions.Add(eggTimerAction);
                    break;
                case TimeStyle.FixedUnscaled:
                    fixedUnscaledActions.Add(eggTimerAction);
                    break;
            }

            return eggTimerAction;
        }

        /// <summary>
        /// Remove/interrupt an action.
        /// </summary>
        /// <param name="eggTimerAction"></param>
        public void Remove(EggTimerAction eggTimerAction)
        {
            scaledActions.Remove(eggTimerAction);
        }

        /// <summary>
        /// Update all of scaled actions.
        /// </summary>
        private void UpdateScaledActions() 
        {
            for (int i = scaledActions.Count - 1; i >= 0; --i)
            {
                var action = scaledActions[i];

                var elapsedTime = (Time.time - action.StartTime) - action.Delay;
                if (elapsedTime < 0f) continue;

                action.Invoke();

                if (elapsedTime >= action.Duration)
                {
                    action.OnFinishAction?.Invoke();
                    scaledActions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Update all of the unscaled actions.
        /// </summary>
        private void UpdateUnscaledActions() 
        {
            for (int i = unscaledActions.Count - 1; i >= 0; --i)
            {
                var action = unscaledActions[i];

                var elapsedTime = (Time.unscaledTime - action.StartTime) - action.Delay;
                if (elapsedTime < 0f) continue;

                action.Invoke();

                if (elapsedTime >= action.Duration)
                {
                    action.OnFinishAction?.Invoke();
                    unscaledActions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Update all of the fixed and scaled actions.
        /// </summary>
        private void UpdateFixedScaledActions() 
        {
            for (int i = fixedActions.Count - 1; i >= 0; --i)
            {
                var action = fixedActions[i];

                var elapsedTime = (Time.fixedTime - action.StartTime) - action.Delay;
                if (elapsedTime < 0f) continue;

                action.Invoke();

                if (elapsedTime >= action.Duration)
                {
                    action.OnFinishAction?.Invoke();
                    fixedActions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Update all of the fixed and unscaled actions.
        /// </summary>
        private void UpdateFixedUnscaledActions()
        {
            for (int i = fixedUnscaledActions.Count - 1; i >= 0; --i)
            {
                var action = fixedUnscaledActions[i];

                var elapsedTime = (Time.fixedUnscaledTime - action.StartTime) - action.Delay;
                if (elapsedTime < 0f) continue;

                action.Invoke();

                if (elapsedTime >= action.Duration)
                {
                    action.OnFinishAction?.Invoke();
                    fixedUnscaledActions.RemoveAt(i);
                }
            }
        }

        private void Update()
        {
            UpdateScaledActions();
            UpdateUnscaledActions();
        }

        private void FixedUpdate()
        {
            UpdateFixedScaledActions();
            UpdateFixedUnscaledActions();
        }
    }
}