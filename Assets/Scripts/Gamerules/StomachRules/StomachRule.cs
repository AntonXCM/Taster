namespace Taster.Gameplay.Rules
{
	public abstract class StomachRule : GameRule
	{
		public abstract void EatenIngredientsChanged(Stomach stomach);
	}
}