using System;
using System.Collections.Generic;
using System.IO;

namespace Buttons
{
	[Serializable]
	public class Vysledek
	{
		public int Score { get; private set; }
		public string Cas { get; private set; } 

		public Vysledek(int score, string cas)
		{
			Score = score;
			Cas = cas;
		}
	}

	public static class OptionsHodnoty
	{
		public static bool Vibrace = true;
		public static bool TouchMode = true; 


	}

	public static class SeznamVysledku
	{
		public static List<Vysledek> seznam = new List<Vysledek>();

		public static void WriteToBinaryFile()
		{
			using (Stream stream = File.Open(MainActivity.vysledkyPath,FileMode.Create))
			{
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				binaryFormatter.Serialize(stream, seznam);
			}
		}

		public static void ReadFromBinaryFile()
		{
			if (File.Exists(MainActivity.vysledkyPath))
			{
				using (Stream stream = File.Open (MainActivity.vysledkyPath, FileMode.Open)) 
				{
					var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ();
					seznam = (List<Vysledek>)binaryFormatter.Deserialize (stream);
				}
			}
		}

		public static void ResetVysledku()
		{
			seznam.Clear();
			WriteToBinaryFile();
		}
	}

	public static class SaveOtions
	{

		public static void WriteToFile()
		{
			using (FileStream stream = new FileStream(MainActivity.optionsPath,FileMode.Create))
			{
				using (BinaryWriter zapis = new BinaryWriter (stream))
				{
					zapis.Write (OptionsHodnoty.Vibrace);
					zapis.Write (OptionsHodnoty.TouchMode);
				}
			}
		}

		public static void ReadFromFile()
		{
			if (File.Exists(MainActivity.optionsPath))
			{
				using (FileStream stream = new FileStream(MainActivity.optionsPath,FileMode.Open)) 
				{
					using (BinaryReader cteni = new BinaryReader (stream)) 
					{
						try{
							OptionsHodnoty.Vibrace = cteni.ReadBoolean();
							OptionsHodnoty.TouchMode = cteni.ReadBoolean();
						}catch{};
					}
				}
			}
		}
	}



}

