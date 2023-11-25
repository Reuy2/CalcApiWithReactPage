using System.Linq;
using System.Text.RegularExpressions;
using WebCalcApi.Extentions;

namespace WebCalcApi.Evaluations
{
    public static class StringExpEval
    {
        public static float Eval(string str)
        {
            str.Trim();
            str = str.Replace(" ", "");
            str = str.Replace(".", ",");
            while (str.Contains('('))
            {
                int FirstBraceIndex = str.IndexOf('(');
                int LastBraceIndex = FindLastBrace(str, FirstBraceIndex);

                if (FirstBraceIndex != -1 && LastBraceIndex != -1)
                {
                    string expInBrace = str.Substring(FirstBraceIndex + 1, LastBraceIndex - 1 - FirstBraceIndex);
                    str = str.Replace($"({expInBrace})", Eval(expInBrace).ToString());
                }
            }

            while (str.Contains('^'))
            {
                int i = str.IndexOf('^');

                float operand1 = FindLeftOperand(i, str);
                float operand2 = FindRightOperand(i, str);

                str = str.Replace($"{operand1}^{operand2}", Math.Pow(operand1, operand2).ToString(), true);


            }

            while (str.Contains('/'))
            {
                int i = str.IndexOf('/');
                float operand1 = FindLeftOperand(i, str);
                float operand2 = FindRightOperand(i, str);

                if (operand2 == 0) throw new DivideByZeroException();

                str = str.Replace($"{operand1}/{operand2}", (operand1 / operand2).ToString(), true);
            }

            while (str.Contains('*'))
            {
                int i = str.IndexOf('*');
                float operand1 = FindLeftOperand(i, str);
                float operand2 = FindRightOperand(i, str);

                str = str.Replace($"{operand1}*{operand2}", (operand1 * operand2).ToString(), true);
            }

            while (str.Contains('+'))
            {
                int i = str.IndexOf('+');
                float operand1 = FindLeftOperand(i, str);
                float operand2 = FindRightOperand(i, str);

                str = str.Replace($"{operand1}+{operand2}", (operand1 + operand2).ToString(), true);
            }

            while (str.Contains('-'))
            {
                int i = str.IndexOf('-', 1);
                if (i == -1) break;
                float operand1 = FindLeftOperand(i, str);
                float operand2 = FindRightOperand(i, str);

                str = str.Replace($"{operand1}-{operand2}", (operand1 - operand2).ToString(), true);
            }

            return float.Parse(str);
        }

        private static int FindLastBrace(string str, int FirstBraceIndex)
        {
            if (FirstBraceIndex == -1) return -1;
            int openedBrace = 1;
            int startIndex = FirstBraceIndex+1;
            while (openedBrace > 0 && startIndex < str.Length)
            {
                startIndex++;
                if (str[startIndex] == '(') openedBrace += 1;
                if (str[startIndex] == ')') openedBrace -= 1;
            }
            return startIndex;
        }

        private static float FindRightOperand(int i, string str)
        {
            string res = "";
            for (int j = i + 1; j < str.Length; j++)
            {
                if (int.TryParse(str[j].ToString(), out _) || str[j] == '.')
                {
                    res += str[j];
                }
                else return float.Parse(res);
            }
            return float.Parse(res);
        }

        private static float FindLeftOperand(int i, string str)
        {
            string res = "";
            for (int j = i - 1; j >= 0; j--)
            {
                if (int.TryParse(str[j].ToString(), out _) || str[j] == '.' || (j == 0 && str[j] == '-'))
                {
                    res = str[j] + res;
                }
                else return float.Parse(res);
            }
            return float.Parse(res);
        }
    }
}
