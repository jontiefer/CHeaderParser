/* Copyright © 2016 Jonathan Tiefer - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the GNU Lesser General Public License (LGPL)
 *
 * You should have received a copy of the LGPL license with
 * this file.
 *
 * /

/*  This file is part of CHeaderParser
*
*   CHeaderParser is free software: you can redistribute it and/or modify
*   it under the terms of the GNU Lesser General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*
*   CHeaderParser is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU Lesser General Public License for more details.
*
*  You should have received a copy of the GNU Lesser General Public License
*   along with CHeaderParser.  If not, see <http://www.gnu.org/licenses/>.
*/

using CHeaderParser.Data;
using CHeaderParser.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHeaderParser.Utils
{
    public static class ExtractorUtils
    {
        #region Array Element Information and Manipulation Properties, Functions        

        /// <summary>
        /// Checks to see if the array contained in the declaration passed to the function is composed completely of numeric values.  In the 
        /// case that dynamic or mathematical equations are contained in the array elements, the function will return false.  If every element 
        /// in the array contains a numeric value, then the function will return true.
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static bool IsNumericArray(string strArray)
        {
            try
            {
                //NOTE: If an array contains a numeric value, but has a space, it will possibly result in the array not being properly 
                //calculated and an array field in a structure/union will be improperly split when detecting field names and declarations
                //in structures.  Therefore, it will be neccessary to detect if any spaces are present in the array declarations and treat 
                //them as they are non-numeric values, so the array can be formatted into the proper numeric format without spaces in the 
                //ConvertNonNumericElements function.
                //strArray = strArray.Replace(" ", "");

                int iStartIndex = strArray.IndexOf('[');
                int iEndIndex = strArray.LastIndexOf(']');

                if (iStartIndex == -1 || iEndIndex == -1)
                    return false;

                int iCurPos = iStartIndex + 1;
                string strElement = "";

                int iNextCloseBracketIndex = strArray.IndexOf(']', iStartIndex);

                bool blIsNumeric = true;

                while (iCurPos < iEndIndex && blIsNumeric)
                {
                    strElement = strArray.Substring(iCurPos, iNextCloseBracketIndex - iCurPos);
                    
                    if (!General.IsNumeric(strElement))
                        blIsNumeric = false;
                    else if (iNextCloseBracketIndex != iEndIndex)
                    {
                        iCurPos = strArray.IndexOf('[', iNextCloseBracketIndex) + 1;

                        if (iCurPos == -1)
                            iCurPos = iEndIndex;
                        else
                            iNextCloseBracketIndex = strArray.IndexOf(']', iCurPos);
                    }
                    else
                    {
                        iCurPos = iEndIndex;
                    }//end if           
                }//end while

                return blIsNumeric;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsNumericArray function of ExtractorUtils class.");
                return false;
            }
        }

        /// <summary>
        /// Converts the non-numeric elements contained in the array declaration passed to the function into the appropriate numeric values 
        /// and reconstructs and returns the declaration strings with the converted numeric values in the appropriate elements of the array that 
        /// match the equation or dynamic expression that matches the equivalent numeric value.   Any type of non-numeric expression, 
        /// such as sizeof operators, mathematical equations, bit-shifting and hexadecimal values can be converted into decimal (base-10)
        /// numeric  values.
        /// </summary>
        /// <param name="HeaderAccess"></param>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static string ConvertNonNumericElements(DataAccess HeaderAccess, string strArray)
        {
            try
            {
                bool blConvertComplete = false;
                int iNextOpenBracketIndex = 0;
                int iNextCloseBracketIndex = 0;
                int iCurPos = 0;
                
                string strElement = "";
                
                while (!blConvertComplete)
                {
                    iNextOpenBracketIndex = strArray.IndexOf('[', iNextOpenBracketIndex);
                    iNextCloseBracketIndex = strArray.IndexOf(']', iNextOpenBracketIndex);
                    iCurPos = iNextOpenBracketIndex + 1;

                    strElement = strArray.Substring(iCurPos, iNextCloseBracketIndex - iCurPos);

                    if (!General.IsNumeric(strElement))
                    {
                        strElement = strElement.Trim();
                              
                        //Calculates the data sizes of all data types wrapped in a sizeof operator in the expression contained in the array element.
                        //Each sizeof value in the expression will be calculated into its associated numeric value.
                        if(strElement.Contains("sizeof"))                        
                            strElement = ConvertSizeOfToNumeric(HeaderAccess, strElement);

                        //Calculates the mathematical expression contained in the array and returns the numerical value as a result of the 
                        //expression.  This will also convert and calculate hex values.
                        long lElementVal = ParserUtils.CalculateMathExprString(strElement);

                        if (lElementVal < 0)
                            lElementVal = 0;

                        strElement = lElementVal.ToString();

                        //Removes the non-numerical expression in the current array element and then replaces the calculated element in its 
                        //numerical format into the array element.
                        strArray = strArray.Remove(iNextOpenBracketIndex + 1, iNextCloseBracketIndex - iNextOpenBracketIndex - 1);
                        strArray = strArray.Insert(iNextOpenBracketIndex + 1, strElement);                        
                    }//end if                    

                    iNextOpenBracketIndex = strArray.IndexOf('[', iNextOpenBracketIndex + 1);

                    if (iNextOpenBracketIndex == -1)
                        blConvertComplete = true;
                }//end while                

                return strArray;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CalculateNonNumericElements function of ExtractorUtils class.");
                return "";
            }
        }        

        #endregion

        #region Data and Element Information and Calculation Functions      

        /// <summary>
        /// Gets the size (in bytes) of the data type passed to the function and returns the value.  All data types, including primitive, 
        /// type definitions, enumeations and structures can be evaluated, so it will be neccessary to pass in the HeaderData data set 
        /// to use to check for the appropriate extracted data types, such as type definitions and structures that were defined in the header.
        /// </summary>
        /// <param name="HeaderAccess"></param>
        /// <param name="strDataTypeName"></param>
        /// <returns></returns>
        public static int SizeOfDataType(DataAccess HeaderAccess, string strDataTypeName)
        {
            try
            {
                int iDataSizeBytes = 0;

                if (DataAccess.PrimDataTypes.IsPrimitiveDataType(strDataTypeName))
                    iDataSizeBytes = DataAccess.PrimDataTypes[strDataTypeName];
                else if (HeaderAccess.TypeDefExists(strDataTypeName))
                {
                    CHeaderDataSet.tblTypeDefsRow rowTypeDef = HeaderAccess.GetTypeDef(strDataTypeName);
                    iDataSizeBytes = rowTypeDef.DataSize;
                }
                else if (HeaderAccess.StructExists(strDataTypeName))
                {
                    CHeaderDataSet.tblStructuresRow rowStruct = HeaderAccess.GetStruct(strDataTypeName);
                    iDataSizeBytes = rowStruct.DataSize;
                }//end if

                return iDataSizeBytes;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in SizeOfDataType function of ExtractorUtils class.");
                return 0;
            }
        }

        /// <summary>
        /// Converts an expression that contains one or more sizeof operator evaluation statements and replaces them in the expression
        /// with the evaluated sizeof values (in bytes).  Each sizeof statement will be located in the expression, where then the sizeof 
        /// operator will be evaluated for the specified data type.  Once the value is evaluted, the sizeof statement is removed and replaced 
        /// with the numeric value in the same location.  
        /// </summary>
        /// <param name="HeaderAccess"></param>
        /// <param name="strExpression"></param>
        public static string ConvertSizeOfToNumeric(DataAccess HeaderAccess, string strExpression)
        {
            try
            {
                bool blConvertComplete = false;
                int iSizeOfStartIndex = 0;
                int iSizeOfEndIndex = 0;

                int iCurPos = 0;
                string strDataTypeName = "";
                
                while (!blConvertComplete)
                {
                    iSizeOfStartIndex = strExpression.IndexOf("sizeof", 0);
                    iSizeOfEndIndex = strExpression.IndexOf(")", iSizeOfStartIndex);
                    iCurPos = strExpression.IndexOf("(", iSizeOfStartIndex) + 1;

                    strDataTypeName = strExpression.Substring(iCurPos, iSizeOfEndIndex - iCurPos);
                    if (strDataTypeName.StartsWith("struct"))
                        strDataTypeName = strDataTypeName.Replace("struct", "").Trim();
                    else if (strDataTypeName.StartsWith("union"))
                        strDataTypeName = strDataTypeName.Replace("union", "").Trim();
                    else if (strDataTypeName.StartsWith("enum"))
                        strDataTypeName = strDataTypeName.Replace("enum", "").Trim();
                    else
                        strDataTypeName = strDataTypeName.Trim();

                    int iDataSize = SizeOfDataType(HeaderAccess, strDataTypeName);

                    strExpression = strExpression.Remove(iSizeOfStartIndex, iSizeOfEndIndex - iSizeOfStartIndex + 1);
                    strExpression = strExpression.Insert(iSizeOfStartIndex, iDataSize.ToString());

                    if (!strExpression.Contains("sizeof"))
                        blConvertComplete = true;
                }//end while

                return strExpression;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ConvertSizeOfToNumeric function of ExtractUtils class.");
                return "";                    
            }
        }

        /// <summary>
        /// Calculates the total number of elements in the array.  This will be the number of elements in each dimension multiplied by the number of 
        /// elements in the higher order dimension of the array.
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static int CalculateFieldElements(string strArray)
        {
            try
            {
                strArray = strArray.Replace(" ", "");

                int iStartIndex = strArray.IndexOf('[');
                int iEndIndex = strArray.LastIndexOf(']');

                List<int> lstElements = new List<int>();

                int iCurPos = iStartIndex + 1;
                string strElement = "";

                int iNextCloseBracketIndex = strArray.IndexOf(']', iStartIndex);
                
                while (iCurPos < iEndIndex)
                {
                    strElement = strArray.Substring(iCurPos, iNextCloseBracketIndex - iCurPos);

                    if (General.IsNumeric(strElement))
                    {
                        lstElements.Add(Convert.ToInt32(strElement));
                    }//end if

                    if (iNextCloseBracketIndex != iEndIndex)
                    {
                        iCurPos = strArray.IndexOf('[', iNextCloseBracketIndex) + 1;

                        if (iCurPos == -1)
                            iCurPos = iEndIndex;
                        else
                            iNextCloseBracketIndex = strArray.IndexOf(']', iCurPos);
                    }
                    else
                    {
                        iCurPos = iEndIndex;
                    }//end if                     
                }//end while

                int iTotalElements = lstElements[0];

                if (lstElements.Count > 1)
                {
                    for (int i = 1; i < lstElements.Count; i++)
                        iTotalElements = iTotalElements * lstElements[i];
                }//end if
                
                return iTotalElements;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CalculateFieldElements function of ExtractorUtils class.");
                return 0;
            }
        }

        /*OBSOLETE: Faster and leaner implementation created
        /// <summary>
        /// Calculates the total number of elements in the array.  This will be the number of elements in each dimension multiplied by the number of 
        /// elements in the higher order dimension of the array.
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static int CalculateFieldElements(string strArray)
        {
            try
            {
                strArray = strArray.Replace(" ", "");

                int iStartIndex = strArray.IndexOf('[');
                int iEndIndex = strArray.LastIndexOf(']');

                List<int> lstElements = new List<int>();

                int iCurPos = iStartIndex + 1;
                string strElement = "";

                while (iCurPos <= iEndIndex)
                {
                    if (char.IsNumber(strArray[iCurPos]))
                        strElement += strArray[iCurPos].ToString();                                        
                    else
                    {
                        if (strArray[iCurPos] == ']')
                        {
                            lstElements.Add(Convert.ToInt32(strElement));
                            strElement = "";
                        }//end if
                    }//end if

                    iCurPos++;
                }//end while

                int iTotalElements = lstElements.Reverse<int>().Sum();
                return iTotalElements;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CalculateFieldElements function of ExtractorUtils class.");
                return 0;
            }
        }
        */        

        #endregion
    }
 
    public static class ParserUtils
    {
        #region String Manipulation and Modification Functions

        /// <summary>
        /// Removes chunks of a strings based on a set of start and end indexes from the StringBuilder object passed to the funciton.
        /// This will allow for high speed manipulation of large strings that have several blocks of data that need to be removed.        
        /// </summary>
        /// <param name="sbData"></param>
        /// <param name="lstStartIndexes"></param>
        /// <param name="lstEndIndexes"></param>
        public static void RemoveStringChunks(StringBuilder sbData, List<int> lstStartIndexes, List<int> lstEndIndexes)
        {
            try
            {
                int iPosOffset = 0;

                for (int i = 0; i < lstStartIndexes.Count; i++)
                {
                    int iStartIndex = lstStartIndexes[i] - iPosOffset;
                    int iEndIndex = lstEndIndexes[i] - iPosOffset;

                    int iRemoveLen = (iEndIndex + 1 - iStartIndex);
                    sbData.Remove(iStartIndex, iRemoveLen);

                    iPosOffset += iRemoveLen;
                }//next i
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in RemoveStringChunks function of ParserUtils class.");
            }
        }

        #endregion

        #region Mathematical, Calculation and Math String Related Functions

        /// <summary>
        /// Evaluates and performs the appropriate calculation on the mathematical expression passed to the function.  This order of operation 
        /// will be taken into account and each calculation will be performed acccordingly to the precedence of the operators and parentheses 
        /// contained in the expression.  Once the calculation is performed, a single numerical value will be returned as a result of the calculation 
        /// of the equation contained in the mathematical expression.
        /// </summary>
        /// <param name="strExpression"></param>
        /// <returns></returns>
        public static long CalculateMathExprString(string strExpression)
        {
            try
            {                
                bool blCalcComplete = false;

                long lMathExprVal = 0;                
                int iCalcIndex = 0;

                if (strExpression.Contains("("))
                {
                    while (!blCalcComplete)
                    {
                        int iOpenParenthIndex = -1;
                        int iCloseParenthIndex = strExpression.IndexOf(')', iCalcIndex);

                        bool blMatchParenthFound = false;

                        while (!blMatchParenthFound)
                        {
                            iCalcIndex = strExpression.IndexOf('(', iCalcIndex);

                            if (iCalcIndex == -1 || iCalcIndex > iCloseParenthIndex)
                            {
                                iCalcIndex = iOpenParenthIndex;
                                blMatchParenthFound = true;
                            }
                            else
                            {
                                iOpenParenthIndex = iCalcIndex;
                                iCalcIndex++;
                            }//end if                                                        
                        }//end while

                        string strInnerExpr = strExpression.Substring(iOpenParenthIndex + 1, iCloseParenthIndex - iOpenParenthIndex - 1);
                        long lInnerExprVal = CalculateMathInnerExpr(strInnerExpr);
                        string strMathExprVal = lInnerExprVal.ToString();

                        strExpression = strExpression.Insert(iOpenParenthIndex, strMathExprVal);
                        strExpression = strExpression.Remove(iOpenParenthIndex + strMathExprVal.Length, iCloseParenthIndex - iOpenParenthIndex + 1);

                        if (strExpression.Contains("("))
                            iCalcIndex = 0;
                        else
                            blCalcComplete = true;
                    }//end while                
                }//end if

                lMathExprVal = CalculateMathInnerExpr(strExpression);

                return lMathExprVal;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CalculateMathExprString function of ParserUtils class.");
                return 0;
            }
        }

        /// <summary>
        /// Caclculates an inner block surrounded by parenetheses (or any other bracket) in a mathematical expression.   The mathematical 
        /// expression contained within the inner block (without parentheses/brackets) will be passed to the function which will then perform 
        /// the appropriate calculation.  Once the equation is calculated, the function will return a single numeric value that is the result of the 
        /// calculation.
        /// </summary>
        /// <param name="strExpression"></param>
        /// <returns></returns>
        private static long CalculateMathInnerExpr(string strExpression)
        {
            try
            {                
                string[] aryOperators = { "-", "+", "/", "*", "^", "%", "<<", ">>", "|", "&" };

                strExpression = strExpression.Replace(" ", "");

                List<string> lstComponents = new List<string>();
                string strVal = "";

                for (int i = 0; i < strExpression.Length; i++)
                {
                    char cVal = strExpression[i];

                    if (i == 0 && cVal == '-')
                        strVal += cVal;
                    else if (char.IsNumber(cVal))
                        strVal += cVal;
                    else if (cVal == 'X' || cVal == 'x')
                    {
                        if (!strVal.ToUpper().Contains("X"))
                            strVal += cVal;
                    }
                    else if (!char.IsNumber(cVal) && strVal != "")
                    {
                        if (strVal.ToUpper().Contains("X"))
                            strVal = ConvertHexToDecString(strVal);

                        lstComponents.Add(strVal);
                        strVal = "";
                    }//end if     

                    if (!char.IsNumber(cVal) && !strVal.ToUpper().EndsWith("X") && i > 0)
                    {
                        if (cVal != '<' && cVal != '>')
                            strVal = cVal.ToString();
                        else
                            strVal = strExpression.Substring(i, 2);

                        if (aryOperators.Contains(strVal))
                            lstComponents.Add(strVal);

                        strVal = "";
                    }//end if                    

                    if (i == strExpression.Length - 1)
                    {
                        if (strVal.ToUpper().Contains("X"))
                            strVal = ConvertHexToDecString(strVal);

                        lstComponents.Add(strVal);
                        strVal = "";
                    }//end if
                }//next i

                long lCurVal = 0;
                long lNextOperand = 0;
                string strOperator = "";

                bool blCalcComplete = false;
                bool blCalcReady = false;
                int iCurIndex = 0;

                if (lstComponents.Count == 1)
                {
                    //If only a single number is passed as an expression, then the number expression will be converted to an integer 
                    //and returned.  This can happen, for example, if a single number is wrapped within parentheses or is a hexadecimal 
                    //number.
                    lCurVal = Convert.ToInt64(lstComponents[0]);
                    return lCurVal;
                }//end if

                while (!blCalcComplete)
                {
                    string strComp = "";

                    if (!blCalcReady)
                    {
                        strComp = lstComponents[iCurIndex];

                        if (iCurIndex == 0)
                        {
                            lCurVal = Convert.ToInt64(strComp);                            
                        }
                        else if (aryOperators.Contains(strComp))
                        {
                            strOperator = strComp;                            
                        }
                        else
                        {
                            lNextOperand = Convert.ToInt64(strComp);                            
                            blCalcReady = true;
                        }//end if                

                        iCurIndex++;
                    }
                    else
                    {                        
                        switch (strOperator)
                        {
                            case "<<":
                                lCurVal = lCurVal << Convert.ToInt32(lNextOperand);
                                break;
                            case ">>":
                                lCurVal = lCurVal >> Convert.ToInt32(lNextOperand);
                                break;
                            case "-":
                                lCurVal = lCurVal - lNextOperand;
                                break;
                            case "+":
                                lCurVal = lCurVal + lNextOperand;
                                break;
                            case "/":
                                lCurVal = lCurVal / lNextOperand;
                                break;
                            case "*":
                                lCurVal = lCurVal * lNextOperand;
                                break;
                            case "^":
                                lCurVal = lCurVal ^ lNextOperand;
                                break;
                            case "%":
                                lCurVal = lCurVal % lNextOperand;
                                break;
                            case "|":
                                lCurVal = lCurVal | lNextOperand;
                                break;
                            case "&":
                                lCurVal = lCurVal & lNextOperand;
                                break;
                        }//end switch

                        strOperator = "";
                        blCalcReady = false;

                        if (iCurIndex >= lstComponents.Count)
                            blCalcComplete = true;
                    }//end if
                }//end while

                return lCurVal;
            }
            catch(DivideByZeroException)
            {
                return 0;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CalculateMathInnerExpr function of ParserUtils class.");
                return 0;
            }
        }

        /// <summary>
        /// Converts the hexadecimal string in the format '0x000' or '0X000' passed to the function into the appropriate equivalent decimal 
        /// based numerical value string.
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        public static string ConvertHexToDecString(string strHex)
        {
            try
            {
                strHex = strHex.Replace(" ", "");

                if (strHex.ToUpper().StartsWith("0X"))
                    strHex = strHex.Remove(0, 2);

                string strDec = Int64.Parse(strHex, System.Globalization.NumberStyles.HexNumber).ToString();

                return strDec;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ConvertHexToDecString function of ParserUtils class.");
                return "";
            }
        }

        #endregion

        #region Nested Block Parsing Functions

        /// <summary>
        /// The GetBlockIndexes function will locate the opening and closing bracket of the block of code passed to the function.  This function will traverse 
        /// through the multi-layers of nested code in the top-level code block passed to the function and identify the positions of where the start and
        /// ending brackets are located in the code block.  An integer array will be returned that will contain the index of the starting and ending bracket 
        /// of the code block, respectively.        
        /// </summary>
        /// <param name="strBlock">The top-level block of code to search.</param>        
        /// <param name="iCurPos">The current position in the block of code to start parsing to locate the top-level code block.  This position 
        /// must be before or starting at the position of the top-level code block.</param>
        /// <returns></returns>
        public static int[] GetBlockIndexes(ref string strBlock, int iCurPos = 0)
        {
            try
            {
                int[] aryBlockIndexes = null;

                int iStartIndex = -1;
                int iEndIndex = -1;

                iStartIndex = strBlock.IndexOf('{', iCurPos);
                int iStartOpenPos = iStartIndex;
                //int iCurPos = iStartOpenPos;

                int iOpenBlockCount = 0;
                int iCloseBlockCount = 0;
                bool blEndBlockFound = false;

                while (!blEndBlockFound)
                {
                    int iNextEndBlockIndex = strBlock.IndexOf('}', iCurPos);
                    string strChunk = strBlock.Substring(iStartOpenPos, iNextEndBlockIndex - iStartOpenPos + 1);

                    iOpenBlockCount = strChunk.Count(c => c == '{');
                    iCloseBlockCount = strChunk.Count(c => c == '}');

                    if (iOpenBlockCount == iCloseBlockCount)
                    {
                        blEndBlockFound = true;
                        iEndIndex = iNextEndBlockIndex;
                    }
                    else
                    {
                        iCurPos = iNextEndBlockIndex + 1;
                    }//end if
                }//end while

                aryBlockIndexes = new int[] { iStartIndex, iEndIndex };

                return aryBlockIndexes;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetBlockIndexes function of ParserUtils class.");
                return null;
            }            
        }

        #endregion
    }
}
