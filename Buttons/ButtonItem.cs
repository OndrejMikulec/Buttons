using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Buttons
{
	public class ButtonItem
	{

		static Random nahodnik = new Random ();

		public ImageButton oImageButton;

		Activity oActivity;
		Game oGame;

		public static List<ButtonItem> seznamVisibleButtons = new List<ButtonItem>();

		public ButtonItem (Activity oActivity, Game oGame)
		{
			this.oActivity = oActivity;
			this.oGame = oGame;
			oImageButton = oActivity.FindViewById<ImageButton> (Int32.Parse( Game.buttons[nahodnik.Next(0,Game.buttons.Length-1)].ToString()));
			seznamVisibleButtons.Add (this);

			oActivity.RunOnUiThread (() => oImageButton.Visibility = ViewStates.Visible);
			oActivity.RunOnUiThread (() => oImageButton.Enabled = true);

			if (OptionsHodnoty.TouchMode) 
			{
				//oImageButton.Touch += HandleClick;
				//oImageButton.SetOnTouchListener (this);
			} else {
				oImageButton.Click += HandleClick;
			}
		}

		void HandleClick (object sender, EventArgs e)
		{

			killThis ();

		}

		public void killThis()
		{
			oActivity.RunOnUiThread (() => oGame.upgradeScore ());
			oActivity.RunOnUiThread (() => oImageButton.Visibility = ViewStates.Invisible);
			oActivity.RunOnUiThread (() => oImageButton.Enabled = false);
			seznamVisibleButtons.Remove (this);
			oImageButton.Click -= HandleClick;

		}

		/*public void upgradeScore()
		{
			oActivity.score++;
			oActivity.RunOnUiThread (() => textScore.Text = "Score: " + score);
		}*/

		/*public bool OnTouch(View v, MotionEvent e)
		{

			if (e.Action == MotionEventActions.Move) 
			{
				float[] touchPosition = { e.RawX, e.RawY };

				foreach (ButtonItem bt in seznamVisibleButtons) 
				{
					if (bt.oImageButton.Visibility == ViewStates.Visible 
						&& touchPosition [0] > (bt.oImageButton.Left - 10) 
						&& touchPosition [0] < (bt.oImageButton.Right + 10) 
						&& touchPosition [1] > (bt.oImageButton.Top - 10) 
						&& touchPosition [1] < (bt.oImageButton.Bottom + 10)) 
					{
						bt.killThis ();

					}
				}
			}
			return true;

		}*/

	}
}

