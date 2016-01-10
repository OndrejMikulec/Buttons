
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.Timers;

namespace Buttons
{
	[Activity (Label = "Game",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,Theme = "@style/CustomTheme")]		
	public class Game : Activity , View.IOnTouchListener
	{
		//const int malaPausa = 100;

		int gameInterval;
		int gameIntervalBase = 150;

		public Vibrator mvibrator;

		public static int score = 0;
		public int nahodneCislo = 0;
		public TextView textScore;
		public ImageView obrazekSmile;

		static Random nahodnik = new Random();
		public static System.Timers.Timer casovac;

		public RelativeLayout layMain;

		public static float screenWidthInDp;
		public static float screenHeightInDp;

		bool gameFailed = false;

		public static object[] buttons = new object[] {
			Resource.Id.button1,
			Resource.Id.button2,
			Resource.Id.button3,
			Resource.Id.button4,
			Resource.Id.button5,
			Resource.Id.button6,
			Resource.Id.button7,
			Resource.Id.button8,
			Resource.Id.button9,
			Resource.Id.button10,
			Resource.Id.button11,
			Resource.Id.button12,
			Resource.Id.button13,
			Resource.Id.button14,
			Resource.Id.button15,
			Resource.Id.button16,
			Resource.Id.button17,
			Resource.Id.button18,
			Resource.Id.button19,
			Resource.Id.button20,
			Resource.Id.button21,
			Resource.Id.button22,
			Resource.Id.button23,
			Resource.Id.button24,
			Resource.Id.button25,
			Resource.Id.button26,
			Resource.Id.button27,
			Resource.Id.button28,
			Resource.Id.button29,
			Resource.Id.button30};

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Game);

			layMain = FindViewById<RelativeLayout> (Resource.Id.LayoutMain);
			layMain.SetOnTouchListener (this);


			SaveOtions.ReadFromFile ();

			textScore = FindViewById<TextView> (Resource.Id.textView1);

			obrazekSmile = FindViewById<ImageView> (Resource.Id.imageView1);

			casovac = new System.Timers.Timer();
			casovac.AutoReset = true;
			casovac.Elapsed += HandleElapsed;


		}


		protected override void OnResume()
		{
			base.OnResume ();
			var metrics = Resources.DisplayMetrics;

			foreach (object obj in buttons) {
				ImageButton oImageButton = FindViewById<ImageButton> (Int32.Parse( obj.ToString()));
				oImageButton.LayoutParameters.Width = ((int)metrics.WidthPixels / 5);
				oImageButton.LayoutParameters.Height = oImageButton.LayoutParameters.Width;
			}

			reset ();
		}

		protected override void OnPause()
		{
			base.OnPause ();
			casovac.Stop ();
		}

		protected override void OnStop()
		{
			base.OnStop ();
			casovac.Stop ();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy ();
			casovac.Stop ();
		}

		void reset() 
		{
			score = 0;
			RunOnUiThread (() => obrazekSmile.SetImageResource (Resource.Drawable.wow));
			RunOnUiThread (() => textScore.Text = "Score: " + score);
			casovac.Interval = 10;
			gameInterval = gameIntervalBase;
			casovac.Start();
		}

		void HandleElapsed (object sender, ElapsedEventArgs e)
		{
			if (gameInterval <= 0) {
				if (ButtonItem.seznamVisibleButtons.Count != 0) {
					casovac.Stop ();
					ButtonItem.seznamVisibleButtons.Clear ();
					gameFail ();
	
				} else {
					gameInterval = gameIntervalBase;
					timeSucccess ();
					ButtonItem oButtonItem = new ButtonItem (this,this);
					gameIntervalBase--;

				}
			} else {
				gameInterval--;
			}
		}



		public void timeSucccess()
		{
			casovac.Stop ();

			foreach (ButtonItem bt in ButtonItem.seznamVisibleButtons) {
				bt.killThis ();
			}

			if (gameInterval >= 100 && gameInterval < 130) 
			{
				RunOnUiThread (() => obrazekSmile.SetImageResource (Resource.Drawable.smile));
			}
			if (gameInterval < 100)
			{
				RunOnUiThread (() => obrazekSmile.SetImageResource (Resource.Drawable.lol));
			}
			if (OptionsHodnoty.Vibrace)
			{
				mvibrator = (Vibrator)this.GetSystemService (Context.VibratorService);
				mvibrator.Vibrate (10);
			}
			//System.Threading.Thread.Sleep (malaPausa);

			gameIntervalBase--;
			gameInterval = gameIntervalBase;

			casovac.Start ();

		}

		public void gameFail() 
		{
			RunOnUiThread (() => obrazekSmile.SetImageResource (Resource.Drawable.angry));

			if (OptionsHodnoty.Vibrace) {
				mvibrator = (Vibrator)this.GetSystemService (Context.VibratorService);
				mvibrator.Vibrate (50);
			}

			Vysledek vysledek = new Vysledek (score, DateTime.Now.ToString ());
			SeznamVysledku.seznam.Add (vysledek);
			SeznamVysledku.WriteToBinaryFile ();
			gameFailed = true;
		}



		public bool OnTouch(View v, MotionEvent e)
		{
			if (!gameFailed) {
				if (e.Action == MotionEventActions.Move) 
				{
					float[] touchPosition = { e.RawX, e.RawY };

					for (int i = 0; i <= ButtonItem.seznamVisibleButtons.Count-1; i++) {
						if (ButtonItem.seznamVisibleButtons[i].oImageButton.Visibility == ViewStates.Visible 
							&& touchPosition [0] > (ButtonItem.seznamVisibleButtons[i].oImageButton.Left - 10) 
							&& touchPosition [0] < (ButtonItem.seznamVisibleButtons[i].oImageButton.Right + 10) 
							&& touchPosition [1] > (ButtonItem.seznamVisibleButtons[i].oImageButton.Top - 10) 
							&& touchPosition [1] < (ButtonItem.seznamVisibleButtons[i].oImageButton.Bottom + 10)) 
						{
							
							upgradeScore ();
							ButtonItem.seznamVisibleButtons[i].killThis ();

						}					
					}
				}				
			} else {
				if (e.Action == MotionEventActions.Down) {
					Finish();
				}
			}

			return true;

		}

		public void upgradeScore()
		{
			score++;
			RunOnUiThread (() => textScore.Text = "Score: " + score);
		}

	}
}

