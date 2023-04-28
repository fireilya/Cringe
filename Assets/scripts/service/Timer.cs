using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public class Timer : MonoBehaviour

    {
        public bool IsEnded { get; private set; }
        private float timer;
        private float _neededTime;
        private bool isStarted;
        public float Progress { get; private set; }

        private void OnEnable()
        {
            IsEnded=true;
        }
        private void Update()
        {
            if (!isStarted) return;
            timer += Time.deltaTime;
            Progress = Mathf.Clamp(timer / _neededTime, 0, 1) ;
            if (!(timer > _neededTime)) return;
            isStarted = false;
            IsEnded = true;
        }

        public void StartTimer(float neededTime)
        {
            isStarted=true;
            timer = 0;
            IsEnded=false;
            _neededTime = neededTime;
        }
    }
}
