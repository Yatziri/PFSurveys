﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Survey.Business
{
	[Serializable]
	public class Encriptar
	{
		public static string SHA256Encrypt(string input)
		{
			SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

			byte[] inputBytes = Encoding.UTF8.GetBytes(input);
			byte[] hashedBytes = provider.ComputeHash(inputBytes);

			StringBuilder output = new StringBuilder();

			for (int i = 0; i < hashedBytes.Length; i++)
				output.Append(hashedBytes[i].ToString("x2").ToLower());

			return output.ToString();
		}
	}
}
