using System.Windows.Input;

namespace WPFFramework
{
	public interface IDelegateCommand : ICommand
	{
		/// <summary>
		/// Updates the command by raising the CanExecuteChanged event,
		/// causing re-evaluation of the CanExecute method.
		/// </summary>
		void Update();
	}
}