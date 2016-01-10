
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
	[Activity (Label = "About",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class About : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.About);
			LinearLayout aboutLayout = FindViewById<LinearLayout> (Resource.Id.layoutAbout);
			aboutLayout.LongClick += HandleLongClick;

			TextView text = FindViewById<TextView> (Resource.Id.textViewNavod);
			text.Text = 
				@"Návod:
Účel hry je v časovém intervalu kliknutím skrýt zobrazenou kachličku.

Hra má dva herní módy:
1 - Klik - kachlička se skryje kliknutím.
2 - Dotyk - kachlička se skryje pohybováním prstem po displeji. Pozor! pokud uděláte klik, je neplatný.";
		}

		void HandleLongClick (object sender, View.LongClickEventArgs e)
		{
			Finish ();
		}
	}
}

