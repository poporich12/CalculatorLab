using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class RPNCalculatorEngine :CalculatorEngine
    {
        public String Process(string str)
        {
            String result="0";
            List<string> parts = str.Split(' ').ToList<string>();
            Stack rpnStack = new Stack();

                for(int i = 0; i < parts.Count; i++)
            {
                if (isNumber(parts[i]))
                    rpnStack.Push(parts[i]);
                
                else if(isOperator(parts[i]))
                {
                    String SeconOperand = rpnStack.Pop().ToString();
                    String FirtOperand = rpnStack.Pop().ToString();
                    result = calculate(parts[i], FirtOperand, SeconOperand, 4);
                    rpnStack.Push(result);
                }
                   
            }

            return result;
        }
    }
}
