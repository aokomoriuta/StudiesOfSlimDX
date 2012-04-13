namespace LWisteria.StudiesOfSlimDX.HelloWorld
{
	/// <summary>
	/// メインクラス
	/// </summary>
	partial class HelloWorldMain : System.Windows.Application
	{
		/// <summary>
		/// 最上位ウインドウ
		/// </summary>
		MainWindow mainWindow;

		/// <summary>
		/// メインクラスを開始
		/// </summary>
		public HelloWorldMain()
		{
			// 最上位ウインドウを作成して設定
			this.mainWindow = new MainWindow();
			base.MainWindow = this.mainWindow;
			base.MainWindow.Show();
		}
	}
}