using System.Collections.Generic;
using System.Linq;

namespace SAOD03
{
	public class Expression
	{
		public List<ExpressionPart> Elements = new List<ExpressionPart>();

		public Expression(string s)
		{
			var t = "";
			var isNumber = false;
			foreach (var c in s)
			{
				if (c == ' ')
					continue;
				if ("1234567890.".Contains(c))
				{
					if (!isNumber && t != "") 
					{
						Elements.Add(new ExpressionPart(t));
						t = "";
					}
					isNumber = true;
					t += c;
					continue;
				}
				if ("()".Contains(c))
				{
					if (t != "")
						Elements.Add(new ExpressionPart(t));
					Elements.Add(new ExpressionPart(c.ToString()));
					isNumber = false;
					t = "";
					continue;
				} 
				if (c == ',')
				{
					if (t != "") Elements.Add(new ExpressionPart(t));
					isNumber = false;
					t = "";
					continue;
				}
				if (isNumber)
				{
					Elements.Add(new ExpressionPart(t));
					t = "";
					isNumber = false;
				}
				t += c;
				if (Operation.Operations.ContainsKey(t))
				{
					Elements.Add(new ExpressionPart(t));
					t = "";
				}
			}
			if (t != "")
				Elements.Add(new ExpressionPart(t));
		}
		private Expression() { }
		public Expression ToPolskExpression()
		{
			var res = new Expression();
			var stack = new YourStack<ExpressionPart>(Elements.Count);
			foreach (var el in Elements)
			{
				switch (el.Type)
				{
					case ExpressionPartType.OpenBracket:
						stack.Push(el);
						continue;
					case ExpressionPartType.CloseBracket:
						while (!stack.IsEmpty)
						{
							if (stack.Peek().Type == ExpressionPartType.OpenBracket)
							{
								stack.Pop();
								break;
							}
							res.Elements.Add(stack.Pop());
						}
						continue;
					case ExpressionPartType.Number:
						res.Elements.Add(el);
						continue;
					case ExpressionPartType.OrdinaryOperation:
					case ExpressionPartType.BinaryOperation:
						if (stack.IsEmpty)
							stack.Push(el);
						else
						{
							while (true)
							{
								if (stack.IsEmpty || el.Rating > stack.Peek().Rating)
								{
									stack.Push(el);
									break;
								}
								else
									res.Elements.Add(stack.Pop());
							}
						}
						continue;
				}
			}
			while (!stack.IsEmpty)
				res.Elements.Add(stack.Pop());
			return res;
		}
		public double Calculate()
		{
			var exp = ToPolskExpression();
			var stack = new YourStack<ExpressionPart>(exp.Elements.Count);
			foreach (var el in exp.Elements)
			{
				if (el.Type == ExpressionPartType.Number)
				{
					stack.Push(el);
				}
				else if (el.Type == ExpressionPartType.BinaryOperation)
				{
					var e2 = stack.Pop().Number;
					var e1 = stack.Pop().Number;
					stack.Push(new ExpressionPart(el.Operation.OperationFunction(e1, e2)));
				}
				else if (el.Type == ExpressionPartType.OrdinaryOperation)
				{
					stack.Push(new ExpressionPart(el.Operation.OperationFunction(stack.Pop().Number)));
				}
			}
			return stack.Pop().Number;
		}

		public Expression Clone()
		{
			var res = new Expression();
			for (var i = 0; i < Elements.Count; i++) 
				res.Elements.Add(Elements[i]);
			return res;
		}

		public override string ToString() =>
			string.Join(" ", string.Join(" ", Elements.Select(e => e.StringValue)));
	}
}
