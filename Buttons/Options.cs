
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

namespace Buttons
{
	[Activity (Label = "Options",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class Options : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Options);

			CheckBox checkVibrace = FindViewById<CheckBox> (Resource.Id.checkBoxVibrace);
			ToggleButton toggle = FindViewById<ToggleButton> (Resource.Id.toggleButton);

			SaveOtions.ReadFromFile ();

			if (OptionsHodnoty.Vibrace) 
			{
				checkVibrace.Checked = true;
			} else {
				checkVibrace.Checked = false;
			}

			checkVibrace.CheckedChange += delegate {
				if (checkVibrace.Checked)
				{
					OptionsHodnoty.Vibrace = true;
					SaveOtions.WriteToFile();
				}else{
					OptionsHodnoty.Vibrace = false;
					SaveOtions.WriteToFile();
				}
			};

			if (OptionsHodnoty.TouchMode) 
			{
				toggle.Checked = true;
			} else {
				toggle.Checked = false;
			}

			toggle.CheckedChange += delegate {
				if (toggle.Checked)
				{
					OptionsHodnoty.TouchMode = true;
					SaveOtions.WriteToFile();
				}else{
					OptionsHodnoty.TouchMode = false;
					SaveOtions.WriteToFile();
				}
			};

			Button resetScore = FindViewById<Button> (Resource.Id.buttonResetScore);
			resetScore.LongClick += delegate {
				SeznamVysledku.ResetVysledku();
			};

			Button zpet = FindViewById<Button> (Resource.Id.buttonZpet);
			zpet.Click += delegate {
				Finish();
			};

		}
	}
}

