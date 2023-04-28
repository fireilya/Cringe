namespace Assets.scripts.Interfaces
{
    public interface IAttack
    {
        void StartAttack();
        void EndAttack();
        void EndPhase();

        void ReturnToZero();
        bool isAttackStarted { get; set; }
        bool isOnZero { get; set; }
        int neededPhaseCount { get; set; }
        public int currentPhaseCount { get; set; }

    }
}
