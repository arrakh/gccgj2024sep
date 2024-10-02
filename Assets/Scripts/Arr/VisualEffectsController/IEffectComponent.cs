namespace Arr.VisualEffectsController
{
    public interface IEffectComponent
    {
        public void Initialize(UnityEffect effect, IEffectPool pool);
    }
}