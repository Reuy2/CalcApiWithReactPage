namespace WebCalcApi.Evaluations
{
    public static class StringExpCheck
    {
        public static (bool,string?) Check(string exp)
        {
            exp = exp.Replace(".", ",");
            var strArr = exp.Split(new char[] {'*','/','-','+','^','(',')',' '});
            char[] possibleChars = new char[] {'*','^','/',',','.','+','-','(',')','1','2','3','4','5','6','7','8','9','0',' '};


            foreach (var ch in exp)
            {

                if (!possibleChars.Contains(ch)) return (false, $"{ch} недопустимый символ");
            }

            foreach (var str in strArr)
            {
                if (str == "") continue;
                if (str.Length >1 && str[0] == '0' && str[1] != ',') return (false, $"{str} не число");
                if(!float.TryParse(str, out _))
                {
                    return (false,$"{str} не число");
                }
            }

            return (true,"");
        }
    }
}
