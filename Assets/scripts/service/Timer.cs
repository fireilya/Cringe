using UnityEngine;

namespace Assets.scripts
{
    public class Timer : MonoBehaviour

    {
        private float _neededTime;
        private bool isStarted;
        private float timer;
        public bool IsEnded { get; private set; }
        public float Progress { get; private set; }

        private void OnEnable()
        {
            IsEnded = true;
        }

        private void Update()
        {
            if (!isStarted) return;
            timer += Time.deltaTime;
            Progress = Mathf.Clamp(timer / _neededTime, 0, 1);
            if (!(timer > _neededTime)) return;
            isStarted = false;
            IsEnded = true;
        }

        public void StartTimer(float neededTime)
        {
            isStarted = true;
            timer = 0;
            IsEnded = false;
            _neededTime = neededTime;
        }
    }
}