using System;
using Enumerators;
using Events;
using UnityEngine;

namespace Interface
{
    public class PlayPauseImageHandler : MonoBehaviour
    {
        [SerializeField] private Animator playAnimator;
        [SerializeField] private Animator pauseAnimator;

        private const string WINDOW_IN = "WindowIn";
        private const string WINDOW_OUT = "WindowOut";

        public PlayPauseImageHandler()
        {
            ClockEvents.ChangeClockStateEvent.AddListener(ClockChangeState);
        }

        private void ClockChangeState(ClockState clockState)
        {
            switch (clockState)
            {
                case ClockState.Play:
                    SetPause();
                    break;
                case ClockState.Pause:
                    SetPlay();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(clockState), clockState, "ClockState not implemented");
            }
        }

        private void SetPlay()
        {
            playAnimator.Play(WINDOW_IN);
            pauseAnimator.Play(WINDOW_OUT);
        }

        private void SetPause()
        {
            playAnimator.Play(WINDOW_OUT);
            pauseAnimator.Play(WINDOW_IN);
        }
    }
}