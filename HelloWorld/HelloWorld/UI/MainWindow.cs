using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using CompositionTarget = System.Windows.Media.CompositionTarget;

using System.Drawing;

using SlimDX;
using SlimDX.Direct3D9;  
namespace LWisteria.StudiesOfSlimDX.HelloWorld
{
	/// <summary>
	/// 最上位ウインドウ
	/// </summary>
	partial class MainWindow : System.Windows.Window
	{
		/// <summary>
		/// 最上位ウインドウを作成
		/// </summary>
		public MainWindow()
		{
			// コンポーネント初期化
			InitializeComponent();


			// デバイスを作成
			Device device = new Device(
				new Direct3D(),
				0,
				DeviceType.Hardware,
				IntPtr.Zero,
				CreateFlags.HardwareVertexProcessing,
				new PresentParameters());

			// 光源を設定して有効化
			device.SetLight(0, new Light()
			{
				Type = LightType.Directional,
				Diffuse = Color.White,
				Ambient = Color.GhostWhite,
				Direction = new Vector3(0, -1, 1)
			});
			device.EnableLight(0, true);

			//射影変換を設定
			device.SetTransform(TransformState.Projection,
				Matrix.PerspectiveFovLH((float)(Math.PI / 4),
				(float)(this.Width / this.Height),
				0.1f, 20.0f));

			//ビューを設定
			device.SetTransform(TransformState.View,
				Matrix.LookAtLH(new Vector3(3, 2, -3),
				Vector3.Zero,
				new Vector3(0, 1, 0)));

			//マテリアル設定
			device.Material = new Material()
			{
				Diffuse = new Color4(Color.GhostWhite)
			};


			// 全面灰色でクリア
			device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, new Color4(0.2f, 0.2f, 0.2f), 1.0f, 0);

			// シーン開始
			device.BeginScene();

			//ティーポットを描画
			device.SetTransform(TransformState.World,
				Matrix.Translation(-3, 0, 5));
			Mesh.CreateTeapot(device).DrawSubset(0);

			//文字列を描画
			device.SetTransform(TransformState.World,
				Matrix.Translation(-4, -1, 0));
			Mesh.CreateText(device, new System.Drawing.Font("Arial", 10), "Hello, world!", 0.001f, 0.001f).DrawSubset(0);

			// シーン終了
			device.EndScene();


			// 画像をロック
			this.directXImage.Lock();

			// バックバッファー領域を指定
			this.directXImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, device.GetBackBuffer(0, 0).ComPointer);
			this.directXImage.AddDirtyRect(new Int32Rect(0, 0, directXImage.PixelWidth, directXImage.PixelHeight));

			// ロック解除
			this.directXImage.Unlock();
		}
	}
}