namespace SAOD03
{
	public enum ExpressionPartType
	{
		Number,
		OpenBracket,
		CloseBracket,
		BinaryOperation,
		OrdinaryOperation
	}

	public class ExpressionPart
	{
		public string StringValue;
		public ExpressionPartType Type;
		public int Rating;
		public double Number;
		public Operation Operation;

		public ExpressionPart(double num)
		{
			StringValue = num.ToString();
			Type = ExpressionPartType.Number;
			Number = num;
		}

		public ExpressionPart(string stringValue)
		{
			StringValue = stringValue;
			if (double.TryParse(stringValue, out var num))
			{
				Type = ExpressionPartType.Number;
				Number = num;
			}
			else if (stringValue == "(")
			{
				Type = ExpressionPartType.OpenBracket;
				Rating = 0;
			}
			else if (stringValue == ")")
			{
				Type = ExpressionPartType.CloseBracket;
				Rating = 1;
			}
			else
			{
				Type = Operation.Operations[stringValue].IsOrdinary ? 
					ExpressionPartType.OrdinaryOperation : ExpressionPartType.BinaryOperation;
				Operation = Operation.Operations[stringValue];
				Rating = Operation.Rating;
			}
		}
	}
}
