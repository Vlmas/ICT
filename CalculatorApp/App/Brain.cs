using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public delegate void DisplayMessage(string text);

    public enum State
    {
        Zero,
        AccumulateDigits,
        AccumulateDigitsWithDecimal,
        ComputePending,
        Compute
    }

    public class Brain
    {
        private readonly DisplayMessage display;
        private readonly string[] nonZeroDigits;
        private readonly string[] allDigits;
        private readonly string[] zeroDigit;
        private readonly string[] commonOperations;
        private readonly string[] equal;
        private readonly string[] separator;
        private State currentState;
        private double previousNumber;
        private string currentNumber;
        private string currentOperation;

        public Brain(DisplayMessage display) 
        {
            this.display = display;
            currentState = State.Zero;
            nonZeroDigits = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            allDigits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            zeroDigit = new string[] { "0" };
            commonOperations = new string[] { "+", "-", "*", "/" };
            equal = new string[] { "=" };
            separator = new string[] { "," };
            previousNumber = 0;
            currentNumber = "";
            currentOperation = "";
        }

        public void ProcessSignal(string message)
        {
            switch(message)
            {
                case "C":
                    ProcessClear();
                    break;
                case "+/-":
                    ProcessChangeSign();
                    break;
                case "x":
                    ProcessEraseOneDigit();
                    break;
                case "^2":
                    ProcessSquare();
                    break;
                case "1/":
                    ProcessOneOver();
                    break;
            }

            switch(currentState)
            {
                case State.Zero:
                    ProcessZeroState(message, false);
                    break;
                case State.AccumulateDigits:
                    ProcessAccumulateState(message, false);
                    break;
                case State.AccumulateDigitsWithDecimal:
                    ProcessAccumulateDecimalState(message, false);
                    break;
                case State.ComputePending:
                    ProcessComputePendingState(message, false);
                    break;
                case State.Compute:
                    ProcessComputeState(message, false);
                    break;
                default:
                    break;
            }
        }

        private void ProcessOneOver()
        {
            double currentNumberWrapper = double.Parse(currentNumber);
            currentNumberWrapper = 1 / currentNumberWrapper;
            currentNumber = currentNumberWrapper.ToString();
            display(currentNumber);
        }

        private void ProcessEraseOneDigit()
        {
            try
            {
                if(currentNumber.Length == 0)
                {
                    currentNumber = "0";
                }
                currentNumber = currentNumber.Remove(currentNumber.Length - 1);
                if (currentNumber.Length == 0)
                {
                    currentNumber = "0";
                }
            }
            catch(IndexOutOfRangeException) { }   
            catch(Exception) { }

            display(currentNumber);
        }

        public void ProcessSquare()
        {
            double currentNumberWrapper = double.Parse(currentNumber);
            currentNumberWrapper *= currentNumberWrapper;
            currentNumber = currentNumberWrapper.ToString();
            display(currentNumber);
        }

        public void ProcessClear()
        {
            previousNumber = 0;
            currentNumber = "";
            currentOperation = "";
            currentState = State.Zero;
            display("0");
        }

        public void ProcessChangeSign()
        {
            double currentNumberWrapper = double.Parse(currentNumber);
            currentNumberWrapper = -currentNumberWrapper;
            currentNumber = currentNumberWrapper.ToString();
            display(currentNumber);
        }

        public void ProcessZeroState(string message, bool incoming)
        {
            if(incoming)
            {
                currentState = State.Zero;
            }
            else
            {
                if(nonZeroDigits.Contains(message))
                {
                    ProcessAccumulateState(message, true);
                }
                else if(separator.Contains(message))
                {
                    ProcessAccumulateDecimalState(message, true);
                }
            }
        }

        public void ProcessAccumulateDecimalState(string message, bool incoming)
        {
            if(incoming)
            {
                currentState = State.AccumulateDigitsWithDecimal;

                if (!currentNumber.Contains(","))
                {
                    currentNumber += message;
                }

                display(currentNumber);
            }
            else
            {
                ProcessAccumulateState(message, false);
            }
        }

        public void ProcessAccumulateState(string message, bool incoming)
        {
            if(incoming)
            {
                currentState = State.AccumulateDigits;

                if(zeroDigit.Contains(currentNumber))
                {
                    currentNumber = message;
                }
                else
                {
                    currentNumber += message;
                }

                display(currentNumber);
            }
            else
            {
                if(allDigits.Contains(message))
                {
                    ProcessAccumulateState(message, true);
                }
                else if(commonOperations.Contains(message))
                {
                    ProcessComputePendingState(message, true);
                }
                else if(separator.Contains(message))
                {
                    ProcessAccumulateDecimalState(message, true);
                }
                else if(equal.Contains(message))
                {
                    ProcessComputeState(message, true);
                }
            }
        }

        public void ProcessComputePendingState(string message, bool incoming)
        {
            if(incoming)
            {
                currentState = State.ComputePending;
                currentOperation = message;

                switch(currentOperation)
                {
                    case "+":
                        previousNumber += double.Parse(currentNumber);
                        break;
                    case "*":
                        previousNumber = (previousNumber == 0) ? 1 : previousNumber;
                        previousNumber *= double.Parse(currentNumber);
                        break;
                    default:
                        previousNumber = double.Parse(currentNumber);
                        break;
                }

                currentNumber = "";
            }
            else
            {
                if(allDigits.Contains(message))
                {
                    ProcessAccumulateState(message, true);
                }
            }
        }

        public void ProcessComputeState(string message, bool incoming)
        {
            if(incoming)
            {
                currentState = State.Compute;
                double n1 = previousNumber;
                double n2 = double.Parse(currentNumber);

                switch(currentOperation)
                {
                    case "+":
                        currentNumber = (n1 + n2).ToString();
                        break;
                    case "-":
                        currentNumber = (n1 - n2).ToString();
                        break;
                    case "*":
                        currentNumber = (n1 * n2).ToString();
                        break;
                    case "/":
                        currentNumber = (n1 / n2).ToString();
                        break;
                }

                previousNumber = double.Parse(currentNumber);
                display(currentNumber);
                currentNumber = "";
                currentOperation = "";
            }
            else
            {
                if(zeroDigit.Contains(message))
                {
                    ProcessZeroState(message, true);
                }
                else if(nonZeroDigits.Contains(message))
                {
                    ProcessAccumulateState(message, true);
                }
            }
        }
    }
}