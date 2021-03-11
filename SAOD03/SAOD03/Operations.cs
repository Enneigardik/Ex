using System;
using System.Collections.Generic;

namespace SAOD03
{

	public class Operation
	{
		public static Dictionary<string, Operation> Operations = new Dictionary<string, Operation>()
		{
			{ "+", new Operation(2, binaryFunction: new Func<double, double, double>((n1, n2) => n1 + n2)) },
			{ "-", new Operation(2, binaryFunction: new Func<double, double, double>((n1, n2) => n1 - n2)) },
			{ "*", new Operation(3, binaryFunction: new Func<double, double, double>((n1, n2) => n1 * n2)) },
			{ "/", new Operation(3, binaryFunction: new Func<double, double, double>((n1, n2) => n1 / n2)) },
			{ "^", new Operation(4, binaryFunction: new Func<double, double, double>((n1, n2) => Math.Pow(n1, n2))) },
			{ "qr", new Operation(4, binaryFunction: new Func<double, double, double>((n1, n2) => Math.Pow(n1, 1 / n2))) },
			{ "log", new Operation(4, binaryFunction: new Func<double, double, double>((n1, n2) => Math.Log(n2, n1))) },
			{ "lg", new Operation(4, ordinaryFunction: new Func<double, double>((n) => Math.Log10(n))) },
			{ "ln", new Operation(4, ordinaryFunction: new Func<double, double>((n) => Math.Log(n))) },
			{ "sqrt", new Operation(4, ordinaryFunction: new Func<double, double>((n) => Math.Sqrt(n))) },
			{ "!", new Operation(4, ordinaryFunction: new Func<double, double>((n) => Factorial((int)n))) },
			{ "cos", new Operation(4, ordinaryFunction: new Func<double, double>((n) => Math.Cos(n / 180 * Math.PI))) },
			{ "sin", new Operation(4, ordinaryFunction: new Func<double, double>((n) => Math.Sin(n / 180 * Math.PI))) },
			{ "tg", new Operation(4, ordinaryFunction: new Func<double, double>((n) => Math.Tan(n / 180 * Math.PI))) },
		};

		public int Rating;
		public bool IsOrdinary = false;

		protected Func<double, double, double> binary;
		protected Func<double, double> ordinary;


		public Operation(int rating, Func<double, double, double> binaryFunction = null, Func<double, double> ordinaryFunction = null) 
		{
			Rating = rating;
			binary = binaryFunction;
			ordinary = ordinaryFunction;
			if (binary == null) IsOrdinary = true;
		}
		public double OperationFunction(params double[] nums) =>
			IsOrdinary ? ordinary(nums[0]) : binary(nums[0], nums[1]);
		private static double Factorial(int n, int res = 1)
		{
			if (n > 1) return Factorial(n - 1, res * n);
			return res;
		}
	}
}
