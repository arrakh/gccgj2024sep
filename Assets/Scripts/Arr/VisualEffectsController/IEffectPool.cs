namespace Arr.VisualEffectsController
{
    public interface IEffectPool
    {
        public UnityEffect Get();

        public void Return(UnityEffect effect);
    }
}