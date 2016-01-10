using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Service;

using System.Collections.Generic;
using System.Timers;
using System.Linq;

namespace Buttons
{
	[Activity (Label = "Buttons", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
		TextView scoreText;
		public static string vysledkyPath;
		public static string optionsPath;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Button start = FindViewById<Button> (Resource.Id.buttonStartGame);
			start.Click += delegate {
				StartActivity(typeof(Game));
			};

			Button options = FindViewById<Button> (Resource.Id.buttonOptions);
			options.Click += delegate {
				StartActivity(typeof(Options));
			};

			Button about = FindViewById<Button> (Resource.Id.buttonAbout);
			about.Click += delegate {
				StartActivity(typeof(About));
			};

			Button quit = FindViewById<Button> (Resource.Id.buttonQuit);
			quit.Click += delegate {
				Finish();
			};

			scoreText = FindViewById<TextView> (Resource.Id.textViewScore);

			vysledkyPath = BaseContext.FilesDir.AbsolutePath + "/vysledky.dat";
			optionsPath = BaseContext.FilesDir.AbsolutePath + "/options.dat";
		}

		protected override void OnResume()
		{
			base.OnResume ();

			scoreText.Text = null;
			SeznamVysledku.ReadFromBinaryFile();
			if (SeznamVysledku.seznam != null) 
			{
				foreach (Vysledek x in SeznamVysledku.seznam.OrderByDescending(p => p.Score)) {
					scoreText.Text += "\n" + x.Score + "     " + x.Cas;

				}
			}

			SaveOtions.ReadFromFile ();

		}

	}
}


