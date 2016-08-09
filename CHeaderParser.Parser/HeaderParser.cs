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

//using CHeaderParser.Extractor;
using CHeaderParser.Global;
using CHeaderParser.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHeaderParser.Parser 
{
    /// <summary>
    /// Handles all parsing of C Header files, identifying each type of declaration and parsing through the nested declarations of structure/union declarations.
    /// </summary>
    public class HeaderDeclareParser
    {
        #region Member Variables

        private string m_strHeaderData = "";
        
        #endregion

        #region Member Object Variables
        #endregion

        #region Construction/Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public HeaderDeclareParser(string strHeaderData = "")
        {
            try
            {
                LoadHeaderFromString(strHeaderData);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor of HeaderDeclareParser class.");
            }
        }

        #endregion

        #region Header File Data Loading and Updating Properties, Functions

        /// <summary>
        /// Clears the current header data string that is set in the HeaderDeclareParser class.  This will allow for new header files to be loaded and 
        /// parsed in the class.
        /// </summary>
        public void ClearHeaderData()
        {
            try
            {
                m_strHeaderData = "";                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ClearHeaderData function of HeaderDeclareParser class.");
            }
        }

        /// <summary>
        /// Appends another header file to the end of the header data string currently loaded in the HeaderDeclareParser class.  By appending header files 
        /// you will be able to parse and extract type defintions, structures and other relevant information from each header file and all header files linked 
        /// to the header file.   The top header file in the chain must be loaded first and each header file linked and dependent to that header file must be
        /// appended after the source header file, so the files can be parsed in the appropriate order.  A newline character will be added as a separator 
        /// between the end of the current header data string loaded in the class and the new header data string to be appended.
        /// </summary>
        /// <param name="strHeaderData"></param>
        /// <param name="blRemoveComments"></param>
        /// <param name="blRemovePreProcCmds"></param>
        /// <param name="blRemoveInlineFuncs"></param>
        /// <param name="blRemoveFuncDeclares"></param>
        /// <param name="blRemoveExternVars"></param>
        public void AppendHeaderFromString(string strHeaderData,
                                                               bool blRemoveComments = true, bool blRemovePreProcCmds = true, bool blRemoveInlineFuncs = true,
                                                               bool blRemoveExternVars = true, bool blRemoveFuncDeclares = false)
        {
            try
            {
                if (m_strHeaderData != "")
                    m_strHeaderData += "\n";

                m_strHeaderData += strHeaderData;
                
                if (m_strHeaderData != "")
                    CurrentPos = 0;
                else
                    CurrentPos = -1;

                //Removes all comments, pre-processor commands, inline functions, function declarations and external variable declarations, if they are 
                //specified to be removed in the appropriate function parameters.
                if (blRemoveComments)
                    RemoveComments();

                if (blRemovePreProcCmds)
                    RemovePreProcessorCmds();

                if (blRemoveInlineFuncs)
                    RemoveInlineFuncs();

                if (blRemoveFuncDeclares)
                    RemoveFuncDeclares();

                if (blRemoveExternVars)
                    RemoveExternVars();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in AppendHeaderFromString function of HeaderDeclareParser class.");
            }
        }

        /// <summary>
        /// Directly loads the header data from a string into the header data string of the class to make it available for parsing.  If a header file is to be 
        /// loaded from a string, then any previous header strings loaded in the class will be removed.
        /// </summary>
        /// <param name="strHeaderData"></param>
        /// <param name="blRemoveComments"></param>
        /// <param name="blRemovePreProcCmds"></param>
        /// <param name="blRemoveInlineFuncs"></param>
        /// <param name="blRemoveFuncDeclares"></param>
        /// <param name="blRemoveExternVars"></param>
        public void LoadHeaderFromString(string strHeaderData,
                    bool blRemoveComments = true, bool blRemovePreProcCmds = true, bool blRemoveInlineFuncs = true,
                    bool blRemoveExternVars = true, bool blRemoveFuncDeclares = false)
        {
            try
            {
                ClearHeaderData();

                if(strHeaderData != "")
                    AppendHeaderFromString(strHeaderData, blRemoveComments, blRemovePreProcCmds, blRemoveInlineFuncs,
                                                          blRemoveFuncDeclares, blRemoveExternVars);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in LoadHeaderFromString function of HeaderDeclareParser class.");
            }
        }

        /// <summary>
        /// Appends the data from the specified file into the current header data string of the class to make it available for parsing.  By appending header files 
        /// you will be able to parse and extract type defintions, structures and other relevant information from each header file and all header files linked 
        /// to the header file.   The top header file in the chain must be loaded first and each header file linked and dependent to that header file must be
        /// appended after the source header file, so the files can be parsed in the appropriate order.  A newline character will be added as a separator 
        /// between the end of the current header file data loaded in the class and the new header data file data to be appended.
        /// </summary>
        /// <param name="strHeaderFile"></param>
        public void AppendHeaderFromFile(string strHeaderFile)
        {
            FileStream fs = null;

            try
            {
                fs = File.Open(strHeaderFile, FileMode.Open, FileAccess.Read);
                StreamReader srdr = new StreamReader(fs);
                AppendHeaderFromString(srdr.ReadToEnd());

                fs.Close();
                fs = null;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in AppendHeaderFromFile function of HeaderDeclareParser class.");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// Loads the data from the specified file into the header data string of the class to make it available for parsing.
        /// </summary>
        /// <param name="strHeaderFile"></param>
        public void LoadHeaderFromFile(string strHeaderFile)
        {
            FileStream fs = null;

            try
            {
                fs = File.Open(strHeaderFile, FileMode.Open, FileAccess.Read);
                StreamReader srdr = new StreamReader(fs);
                LoadHeaderFromString(srdr.ReadToEnd());

                fs.Close();
                fs = null;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in LoadHeaderFromFile function of HeaderDeclareParser class.");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        #endregion

        #region Header Data Information Properties, Functions

        /// <summary>
        /// The length in bytes of the header data string that is loaded in the HeaderDeclareParser class.
        /// </summary>
        public int HeaderLength
        {
            get
            {
                return m_strHeaderData.Length;
            }
        }

        /// <summary>
        /// The current header data string linked to the HeaderDeclareParser class.   The header data string can only be read and not modified through 
        /// this property.
        /// </summary>
        public string HeaderData
        {
            get
            {
                return m_strHeaderData;
            }
        }

        #endregion

        #region Header String Processing Functions

        /// <summary>
        /// Identifies all comments in the header data string and removes the comments from the header string linked to the 
        /// HeaderDeclareParser class.
        /// </summary>
        public void RemoveComments()
        {
            try
            {
                List<int> lstCommentStartIndexes = new List<int>();
                List<int> lstCommentEndIndexes = new List<int>();

                int iCurPos = 0;
                int iCommentIndex = 0;

                while (iCurPos < m_strHeaderData.Length && iCommentIndex != -1)
                {
                    iCommentIndex = m_strHeaderData.IndexOf("# ", iCurPos);

                    if (iCommentIndex > -1)
                    {
                        if (char.IsDigit(m_strHeaderData[iCommentIndex + 2]))
                        {
                            lstCommentStartIndexes.Add(iCommentIndex);
                            iCurPos = iCommentIndex + 1;
                        }//end if
                    }//end if
                }//end while

                iCurPos = 0;
                iCommentIndex = 0;

                while (iCurPos < m_strHeaderData.Length && iCommentIndex != -1)
                {
                    iCommentIndex = m_strHeaderData.IndexOf("/*");

                    if (iCommentIndex > -1)
                    {
                        lstCommentStartIndexes.Add(iCommentIndex);
                        iCurPos = iCommentIndex + 1;
                    }//end if
                }//end while

                iCurPos = 0;
                iCommentIndex = 0;

                while (iCurPos < m_strHeaderData.Length && iCommentIndex != -1)
                {
                    iCommentIndex = m_strHeaderData.IndexOf("//");

                    if (iCommentIndex > -1)
                    {
                        lstCommentStartIndexes.Add(iCommentIndex);
                        iCurPos = iCommentIndex + 1;
                    }//end if
                }//end while


                StringBuilder sbHeaderData = new StringBuilder(m_strHeaderData);
                string strPeekData = "";

                for (int i = 0; i < lstCommentStartIndexes.Count; i++)
                {
                    int iStartIndex = lstCommentStartIndexes[i];
                    int iEndIndex = -1;

                    strPeekData = m_strHeaderData.Substring(iStartIndex, 3);

                    if (strPeekData.Contains("#"))
                    {
                        iEndIndex = m_strHeaderData.IndexOf('\n', iStartIndex);
                        if (iEndIndex == -1)
                            iEndIndex = iStartIndex;

                        lstCommentEndIndexes.Add(iEndIndex);
                    }
                    else if (strPeekData.Contains("/*"))
                    {
                        iEndIndex = m_strHeaderData.IndexOf("*/", iStartIndex);
                        lstCommentEndIndexes.Add(iEndIndex);
                    }
                    else if (strPeekData.Contains("//"))
                    {
                        iEndIndex = m_strHeaderData.IndexOf("\n", iStartIndex);
                        if (iEndIndex == -1)                            
                            iEndIndex = iStartIndex + 1;

                        lstCommentEndIndexes.Add(iEndIndex);
                    }//end if
                }//next i

                ParserUtils.RemoveStringChunks(sbHeaderData, lstCommentStartIndexes, lstCommentEndIndexes);
                m_strHeaderData = sbHeaderData.ToString();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in RemoveComments function of HeaderDeclareParser class.");
            }
        }

        /// <summary>
        /// Identifies all pre-processor commands in the header data string and removes them from the header string linked to the 
        /// HeaderDeclareParser class.
        /// </summary>
        public void RemovePreProcessorCmds()
        {
            try
            {
                List<int> lstPreProcStartIndexes = new List<int>();
                List<int> lstPreProcEndIndexes = new List<int>();

                int iCurPos = 0;
                int iPreProcIndex = 0;

                while (iCurPos < m_strHeaderData.Length && iPreProcIndex != -1)
                {
                    iPreProcIndex = m_strHeaderData.IndexOf("#", iCurPos);

                    if (iPreProcIndex > -1)
                    {
                        if (iPreProcIndex < HeaderLength - 1)
                        {
                            if (char.IsLetter(m_strHeaderData[iPreProcIndex + 1]))
                            {
                                lstPreProcStartIndexes.Add(iPreProcIndex);

                                int iEndIndex = m_strHeaderData.IndexOf('\n', iPreProcIndex);

                                if (iEndIndex == -1)
                                    iEndIndex = iPreProcIndex + 1;

                                lstPreProcEndIndexes.Add(iEndIndex);
                                iCurPos = iEndIndex + 1;
                            }
                            else
                                iCurPos++;
                        }
                        else
                            iCurPos = iPreProcIndex + 1;
                    }//end if
                }//end while


                StringBuilder sbHeaderData = new StringBuilder(m_strHeaderData);
                ParserUtils.RemoveStringChunks(sbHeaderData, lstPreProcStartIndexes, lstPreProcEndIndexes);
                m_strHeaderData = sbHeaderData.ToString();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in RemovePreProcessorCmds function of HeaderDeclareParser class.");
            }
        }

        public void RemoveInlineFuncs()
        {
            try
            {
                List<int> lstFuncStartIndexes = new List<int>();
                List<int> lstFuncEndIndexes = new List<int>();

                int iCurPos = 0;
                int iFuncIndex = 0;

                while (iCurPos < m_strHeaderData.Length && iFuncIndex != -1)
                {
                    //Static Inline functions always start with static inline declarator.
                    iFuncIndex = m_strHeaderData.IndexOf("static inline", iCurPos);

                    if (iFuncIndex > -1)
                    {
                        lstFuncStartIndexes.Add(iFuncIndex);
                        iCurPos = iFuncIndex + 1;
                    }//end if
                }//end while


                StringBuilder sbHeaderData = new StringBuilder(m_strHeaderData);

                for (int i = 0; i < lstFuncStartIndexes.Count; i++)
                {
                    int iStartIndex = lstFuncStartIndexes[i];
                    int iEndIndex = -1;

                    int iFirstOpenBracketIndex = m_strHeaderData.IndexOf('{', iStartIndex);

                    if (m_strHeaderData.IndexOf(';', iStartIndex) > iFirstOpenBracketIndex && iFirstOpenBracketIndex != -1)
                    {
                        //Static Inline function contains a body enclosed within brackets.
                        int[] aryBlockIndexes = GetBlockIndexes(iStartIndex);

                        if (aryBlockIndexes == null || aryBlockIndexes[1] == -1)
                            iEndIndex = iStartIndex + "static inline".Length;
                        else
                        {
                            iEndIndex = aryBlockIndexes[1];

                            //If the inline function is terminated with a semi-colon after the close bracket, then the end index of the 
                            //inline function is positioned after the semi-colon.
                            if (m_strHeaderData.IndexOf("};", iEndIndex) == iEndIndex)
                                iEndIndex++;
                        }//end if
                    }
                    else
                    {
                        //Static Inline function contains only a definition without a body enclosed within brackets.
                        iEndIndex = m_strHeaderData.IndexOf(';', iStartIndex);
                    }//end if

                    lstFuncEndIndexes.Add(iEndIndex);
                }//next i    

                ParserUtils.RemoveStringChunks(sbHeaderData, lstFuncStartIndexes, lstFuncEndIndexes);
                m_strHeaderData = sbHeaderData.ToString();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in RemoveInlineFuncs function of HeaderDeclareParser class.");
            }
        }

        /// <summary>
        /// Identifies all function declarations in the header data string and removes them from the header string linked to the 
        /// HeaderDeclareParser class.
        /// NOTE: As of now this function performs slowly and will need to be updated before it can be used effectively.
        /// </summary>
        public void RemoveFuncDeclares()
        {
            try
            {
                List<int> lstFuncStartIndexes = new List<int>();
                List<int> lstFuncEndIndexes = new List<int>();

                int iCurPos = 0;
                int iEndIndex = 0;                
                int[] aryBlockIndexes = null;
                //string strPeek = "";

                while (iCurPos < m_strHeaderData.Length)
                {
                    if (!IsStruct() || !IsEnum())
                    {
                        //////For Debugging 
                        //if (m_strHeaderData.Substring(iCurPos, "".Length) == "")
                        //    Debugger.Break();

                        if (IsFunctionDecl(iCurPos))
                        {
                            lstFuncStartIndexes.Add(iCurPos);

                            iEndIndex = m_strHeaderData.IndexOf(");", iCurPos) + 1;
                            lstFuncEndIndexes.Add(iEndIndex);

                            iCurPos = iEndIndex + 1;
                        }
                        else
                        {
                            iCurPos = m_strHeaderData.IndexOf('\n', iCurPos) + 1;                            
                        }//end if
                    }
                    else
                    {
                        aryBlockIndexes = GetBlockIndexes(iCurPos);
                        iCurPos = aryBlockIndexes[1] + 1;                        
                    }//end if
                }//end while

                StringBuilder sbHeaderData = new StringBuilder(m_strHeaderData);

                ParserUtils.RemoveStringChunks(sbHeaderData, lstFuncStartIndexes, lstFuncEndIndexes);
                m_strHeaderData = sbHeaderData.ToString();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in RemoveFuncDeclares function of HeaderDeclareParser class.");
            }
        }

        /* NOT USED
        /// <summary>
        /// Identifies all function declarations in the header data string and removes them from the header string linked to the 
        /// HeaderDeclareParser class.
        /// </summary>
        public void RemoveFuncDeclares()
        {
            try
            {
                List<int> lstFuncStartIndexes = new List<int>();
                List<int> lstFuncEndIndexes = new List<int>();

                int iCurPos = 0;
                int iStartIndex = 0;
                int iDeclWordCount = 0;
                bool blOpenFuncBracketDetected = false;
                bool blDeclWordDetected = false;
                bool blTypeCastDetected = false;

                while (iCurPos < m_strHeaderData.Length && iStartIndex != -1)
                {
                    iStartIndex = m_strHeaderData.IndexOf(");", iCurPos);

                    if (iStartIndex > -1)
                    {
                        int iEndIndex = iStartIndex + 1;
                        iCurPos = iStartIndex;

                        while (m_strHeaderData[iCurPos] != '\n' && iCurPos >= 0)
                        {
                            if (!blTypeCastDetected)
                            {
                                if (m_strHeaderData[iCurPos] == ')')
                                    blTypeCastDetected = true;

                                if (blDeclWordDetected)
                                {
                                    if (m_strHeaderData[iCurPos] == ' ' || m_strHeaderData[iCurPos] == '\n')
                                    {
                                        iDeclWordCount++;
                                        blDeclWordDetected = false;
                                    }//end if
                                }
                                else
                                {
                                    if (m_strHeaderData[iCurPos] != ' ' && m_strHeaderData[iCurPos] != '\n' && blOpenFuncBracketDetected)
                                        blDeclWordDetected = true;
                                }//end if
                            }
                            else
                            {
                                if (m_strHeaderData[iCurPos] == '(' && !blOpenFuncBracketDetected)
                                    blOpenFuncBracketDetected = true;
                                else
                                    blTypeCastDetected = false;
                            }//end if

                            iCurPos--;
                        }//end while

                        iCurPos++;

                        bool blValidDecl = false;

                        if (iDeclWordCount == 2)
                            blValidDecl = true;
                        else if (iDeclWordCount == 3)
                        {
                            if (m_strHeaderData.Substring(iCurPos, "struct".Length) == "struct")
                                blValidDecl = true;
                        }//end if

                        if (blValidDecl)
                        {
                            lstFuncStartIndexes.Add(iStartIndex);
                            lstFuncEndIndexes.Add(iEndIndex);
                            iCurPos = iEndIndex + 1;
                        }
                        else
                        {
                            iCurPos = iStartIndex + 1;
                        }//end if                        
                    }//end if
                }//end while


                StringBuilder sbHeaderData = new StringBuilder(m_strHeaderData);

                ParserUtils.RemoveStringChunks(sbHeaderData, lstFuncStartIndexes, lstFuncEndIndexes);
                m_strHeaderData = sbHeaderData.ToString();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in RemoveFuncDeclares function of HeaderDeclareParser class.");
            }
        }
        */

        /// <summary>
        /// Identifies all external variable declarations in the header data string and removes them from the header string linked to the 
        /// HeaderDeclareParser class.
        /// </summary>
        public void RemoveExternVars()
        {
            try
            {
                List<int> lstExternStartIndexes = new List<int>();
                List<int> lstExternEndIndexes = new List<int>();

                int iCurPos = 0;
                int iExternIndex = 0;

                while (iCurPos < m_strHeaderData.Length && iExternIndex != -1)
                {
                    iExternIndex = m_strHeaderData.IndexOf("extern ", iCurPos);

                    if (iExternIndex > -1)
                    {
                        if (iExternIndex < HeaderLength - 1)
                        {
                            lstExternStartIndexes.Add(iExternIndex);

                            int iEndIndex = m_strHeaderData.IndexOf('\n', iExternIndex);

                            if (iEndIndex == -1)
                                iEndIndex = iExternIndex + 1;

                            lstExternEndIndexes.Add(iEndIndex);
                            iCurPos = iEndIndex + 1;
                        }
                        else
                            iCurPos++;
                    }//end if
                }//end while


                StringBuilder sbHeaderData = new StringBuilder(m_strHeaderData);
                ParserUtils.RemoveStringChunks(sbHeaderData, lstExternStartIndexes, lstExternEndIndexes);
                m_strHeaderData = sbHeaderData.ToString();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in RemoveExternVars function of HeaderDeclareParser class.");
            }
        }

        #endregion

        #region Parsing Position and Information Properties

        /// <summary>
        /// The current position (in bytes) of the cursor in the header file.
        /// </summary>        
        public int CurrentPos { get; set; }

        /// <summary>
        /// Indicates if the cursor has gone past the last position of the file.
        /// </summary>
        public bool EndOfFile
        {
            get
            {
                if (CurrentPos < HeaderLength)
                    return false;
                else
                    return true;
            }
        }

        #endregion

        #region General File Parsing Functions        
    
        /// <summary>
        /// Moves the position of the parsing cursor to the next position in the header data string linked to the class.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            try
            {
                CurrentPos++;

                if (!EndOfFile)
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in MoveNext function of HeaderDeclareParser class.");
                return false;
            }
        }

        /// <summary>
        /// Moves the position of the parsing cursor to the next extractable declaration in the header data string linked to the class.        
        /// </summary>
        /// <returns></returns>
        public bool MoveNextItem()
        {
            try
            {
                bool blItemFound = false;
                int[] aryBlockIndexes = null;
                
                while (!blItemFound && !EndOfFile)
                {
                    if (CurrentPos > -1)
                    {                        
                        if (IsStruct() || IsEnum())
                        {
                            aryBlockIndexes = GetBlockIndexes();
                            CurrentPos = aryBlockIndexes[1] + 1;
                        }
                        else if (IsTypeDef())
                        {
                            CurrentPos = m_strHeaderData.IndexOf(';', CurrentPos) + 1;
                        }
                        else if (m_strHeaderData[CurrentPos] == '\n' || m_strHeaderData[CurrentPos] == '\t' ||
                                   m_strHeaderData[CurrentPos] == ' ')
                        {
                            CurrentPos++;
                        }                        
                        else if (m_strHeaderData.IndexOf('\n', CurrentPos) != -1)
                            CurrentPos = m_strHeaderData.IndexOf('\n', CurrentPos) + 1;
                        //ERROR: Results in parse going to following semi-colon, bypassing important data variables and declarations, will
                        //remove.
                        //else if (m_strHeaderData.IndexOf(';', CurrentPos) != -1)
                        //    CurrentPos = m_strHeaderData.IndexOf(';', CurrentPos) + 1;
                        else
                            CurrentPos++;
                    }
                    else
                        CurrentPos = 0;                    

                    if (IsTypeDef() || IsEnum() || IsStruct())
                        blItemFound = true;                    
                }//end while

                if (!EndOfFile)
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in MoveNextItem function of HeaderDeclareParser class.");
                return false;
            }
        }

        #endregion

        #region Nested Block Parsing Functions               

        /// <summary>
        /// The GetBlockIndexes function will locate the opening and closing bracket of the block of code passed to the function.  This function will traverse 
        /// through the multi-layers of nested code in the top-level code block passed to the function and identify the positions of where the start and
        /// ending brackets are located in the code block.  An integer array will be return that will contain the index of the starting and ending bracket 
        /// of the code block, respectively.
        /// 
        /// This version of the function will examine the top-level block of code in that starts at either the position in the header specified in 
        /// CurPos paramter or the current parsing position of the header data string loaded in an instantiated HeaderDeclareParser object.        
        /// </summary>
        /// <param name="iCurPos">The current position of the header data string to parse code block.  If the CurPos parameter is 
        /// set to -1, then the current position parser set in the class will be used.</param>
        /// <returns></returns>
        public int[] GetBlockIndexes(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;

                int[] aryBlockIndexes = ParserUtils.GetBlockIndexes(ref m_strHeaderData, iCurPos);

                if (aryBlockIndexes == null || (aryBlockIndexes[0] == -1 || aryBlockIndexes[1] == -1))
                    return null;
                
                return aryBlockIndexes;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetBlockIndexes Overload 2 function of HeaderDeclareParser class.");
                return null;                    
            }
        }

        #endregion

        #region Function Parsing Functions

        /* NOT USED: Invalid logic.. Function declarations will be checked for existence instead.
        /// <summary>
        /// The GetFuncDeclareEndIndex function will locate the opening and closing parentheses at the position in the header data string 
        /// file specified in the function's parameters.  This function will traverse through the multi-layers of nested parentheses in the function 
        /// declaration and identify the ending parentheses position of where the function declaration ends is located in the declaration block.
        /// An integer will be returned that will contain the index of the ending semi-colon of the function declaration.  If is determined that 
        /// the current data being parsed is not a function declaration or if an error occurs, a value of -1 will be returned instead.
        /// </summary>
        /// <param name="strBlock">The top-level block of code to search.</param>        
        /// <param name="iCurPos">The current position in the block of code to start parsing to locate the top-level code block.  This position 
        /// must be before or starting at the position of the top-level code block.</param>
        /// <returns></returns>
        public int GetFuncDeclareEndIndex(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;
                                
                int iStartIndex = m_strHeaderData.IndexOf('(', iCurPos);
                int iEndIndex = m_strHeaderData.IndexOf(");", iCurPos);

                int iStartOpenPos = iStartIndex;

                int iOpenParenthCount = 0;
                int iCloseParenthCount = 0;
                bool blEndParenthFound = false;

                int iCheckIndex = 0;

                if (iStartIndex == -1 || iEndIndex == -1)
                    return -1;

                if (iEndIndex < iStartIndex)
                    return -1;

                //If a bracket is detected before a parentheses, then the block of code being parsed is not a function declaration.
                iCheckIndex = m_strHeaderData.IndexOf('{', iCurPos);
                if (iCheckIndex < iStartIndex)
                    return -1;

                //If a bracket is detected between the first detected opening and closed parentheses, the code being parsed is not a function 
                //declaration.
                iCheckIndex = m_strHeaderData.IndexOf('{', iStartIndex);
                if (iCheckIndex < iEndIndex)
                    return -1;

                iEndIndex = -1;

                while (!blEndParenthFound && !EndOfFile)
                {
                    int iNextEndParenthIndex =  m_strHeaderData.IndexOf(')', iCurPos);
                    string strChunk = m_strHeaderData.Substring(iStartOpenPos, iNextEndParenthIndex - iStartOpenPos + 1);

                    iOpenParenthCount = strChunk.Count(c => c == '(');
                    iCloseParenthCount = strChunk.Count(c => c == ')');

                    if (iOpenParenthCount == iCloseParenthCount)
                    {
                        blEndParenthFound = true;

                        if (m_strHeaderData.Substring(iNextEndParenthIndex, 2) != ");")
                            //If the function declaration does not end with a closing parentheses terminated by a semi-colon, then it can be determined 
                            //that the data parsed was not a function declaration.
                            iEndIndex = -1;
                        else
                            iEndIndex = iNextEndParenthIndex + 1;
                    }
                    else
                    {
                        iCurPos = iNextEndParenthIndex + 1;
                    }//end if
                }//end while

                return iEndIndex;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetFuncDeclareEndIndex function of HeaderDeclareParser class.");
                return -1;
            }
        }
        */

        #endregion

        #region Specific Declaration Parsing Functions        

        /// <summary>
        /// Parses the type definition declaration from the header data string and returns a string that contains the 
        /// full declaration of the type definition declaration for the current line in the header file.  The cursor will be 
        /// advanced to the end of the declaration after the appropriate data is parsed.
        /// NOTE: Typedef structures will not be included in typedef declarations, but typedef enums can.  All
        /// structures will use the GetStruct parsing function instead.  Typedef array declarations also will be parsed 
        /// by this function.
        /// </summary>
        /// <param name="blMoveNextItemAfterParse"></param>
        /// <returns></returns>
        public string GetTypeDef(bool blMoveNextItemAfterParse = true)
        {
            try
            {
                int iStartIndex = CurrentPos;
                int iEndIndex = m_strHeaderData.IndexOf(';', iStartIndex);

                string strTypeDef = m_strHeaderData.Substring(iStartIndex, iEndIndex + 1 - iStartIndex);                

                if (blMoveNextItemAfterParse)
                    MoveNextItem();
                else
                    CurrentPos += iEndIndex + 1;

                return strTypeDef;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetTypeDef function of HeaderDeclareParser class.");
                return "";
            }
        }

        /* NOT USED: GetTYpeDef function can handle parsing typedef arrays.
        /// <summary>
        /// Parses the type definition array declaration from the header data string and returns a string that contains the 
        /// full declaration of the type definition array declaration for the current line in the header file.  The cursor will be 
        /// advanced to the end of the declaration after the appropriate data is parsed.
        /// NOTE: Typedef structure arrays will not be included in typedef declarations, but typedef enum arrays will be.  All
        /// structures will use the GetStruct parsing function instead.
        /// </summary>
        /// <returns></returns>
        public string GetTypeDefArray()
        {
            try
            {
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetTypeDefArray function of HeaderDeclareParser class.");
            }
        }
        */

        /// <summary>
        /// Parses the enumeration declaration from the header data string and returns a string that contains the 
        /// full declaration of the enumeration declaration for the current line in the header file.  The cursor will be 
        /// advanced to the end of the declaration after the appropriate data is parsed.
        /// NOTE: The GetTypeDef function will work for parsing enumerations as well.
        /// </summary>
        /// <param name="blMoveNextItemAfterParse"></param>
        /// <returns></returns>
        public string GetEnum(bool blMoveNextItemAfterParse = true)
        {
            try
            {
                int iStartIndex = CurrentPos;
                int iEndIndex = m_strHeaderData.IndexOf(';', iStartIndex);

                string strEnum = m_strHeaderData.Substring(iStartIndex, iEndIndex + 1 - iStartIndex);

                if (blMoveNextItemAfterParse)
                    MoveNextItem();
                else
                    CurrentPos += iEndIndex + 1;

                return strEnum;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetEnum function of HeaderDeclareParser class.");
                return "";
            }
        }

        /// <summary>
        /// Parses the structure or union declaration from the header data string and returns a string that contains the full
        /// declaration of the structure or union declaration for the current line in the header file.  The end of the structure 
        /// or union will be detected by traversing through the nested structures/unions within the structure/union in the header string.  
        /// The cursor will be advanced to the position after the end of the declaration once the appropriate data is parsed.
        /// NOTE: If nested structures and unions are to be parsed as top-level structures, then the nested structure/union declaration, itself, 
        /// should be linked directly to the HeaderDeclareParser class as the header data string.
        /// NOTE: Typedef structures will also be parsed in the GetStructure function.
        /// </summary>
        /// <param name="blMoveNextItemAfterParse"></param>
        /// <returns></returns>
        public string GetStructure(bool blMoveNextItemAfterParse = true)
        {
            try
            {
                int iStartIndex = CurrentPos;
                int iEndIndex = -1;

                string strStruct = "";
                
                int[] aryBlockIndexes = GetBlockIndexes();

                if (aryBlockIndexes == null)
                    return null;

                int iEndStructIndex = m_strHeaderData.IndexOf(';', aryBlockIndexes[1]);
                strStruct = m_strHeaderData.Substring(CurrentPos, iEndStructIndex + 1 - CurrentPos);                

                if (blMoveNextItemAfterParse)
                    MoveNextItem();
                else
                    CurrentPos = iEndIndex + 1;

                return strStruct;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetStructure function of HeaderDeclareParser class.");
                return "";
            }
        }

        /* NOT USED: Non-Optimal logic, with better implementation used instead.
        /// <summary>
        /// Parses the structure or union declaration from the header data string and returns a string that contains the full
        /// declaration of the structure or union declaration for the current line in the header file.  The end of the structure 
        /// or union will be detected by traversing through the nested structures/unions within the structure/union in the header string.  
        /// The cursor will be advanced to the position after the end of the declaration once the appropriate data is parsed.
        /// NOTE: If nested structures and unions are to be parsed as top-level structures, then the nested structure/union declaration, itself, 
        /// should be linked directly to the HeaderDeclareParser class as the header data string.
        /// NOTE: Typedef structures will also be parsed in the GetStructure function.
        /// </summary>
        /// <param name="blMoveNextItemAfterParse"></param>
        /// <returns></returns>
        public string GetStructure(bool blMoveNextItemAfterParse = true)
        {
            try
            {
                int iStartIndex = CurrentPos;
                int iEndIndex = -1;
                
                string strStruct = "";
                bool blStructEndDetected = false;

                int iNextStructChildIndex = -1;
                int iNextUnionChildIndex = -1;

                int iNextChildIndex = -1;
                int iNextCloseIndex = -1;

                int iNestedStructCount = 0;

                int iStructCurPos = iStartIndex;

                while (!blStructEndDetected)
                {
                    iNextStructChildIndex = m_strHeaderData.IndexOf("struct", iStructCurPos);
                    iNextUnionChildIndex = m_strHeaderData.IndexOf("union", iStructCurPos);

                    if (iNextStructChildIndex < iNextUnionChildIndex && iNextStructChildIndex != -1)
                    {
                        iNextChildIndex = iNextStructChildIndex;
                        iStructCurPos += iNextChildIndex + "struct".Length;
                    }
                    else if (iNextUnionChildIndex != -1)
                    {
                        iNextChildIndex = iNextUnionChildIndex;
                        iStructCurPos += iNextChildIndex + "union".Length;
                    }
                    else
                        iNextChildIndex = -1;

                    if (iNextChildIndex == -1)
                        blStructEndDetected = true;
                    else
                    {
                        iNestedStructCount++;                        
                    }//end if
                }//end while

                iNextCloseIndex = iStartIndex;
                for (int i = 0; i < iNestedStructCount + 1; i++)
                {
                    iNextCloseIndex = m_strHeaderData.IndexOf("}", iNextCloseIndex);
                }//next i

                iEndIndex = m_strHeaderData.IndexOf(';', iNextCloseIndex);

                strStruct = m_strHeaderData.Substring(iStartIndex, iEndIndex - iStartIndex);

                if (blMoveNextItemAfterParse)
                    MoveNextItem();
                else
                    CurrentPos = iEndIndex + 1;

                return strStruct;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetStructure function of HeaderDeclareParser class.");
                return "";
            }
        }
        */

        #endregion

        #region Declaration Identification Functions

        /// <summary>
        /// Indicates if the next character in the parsed header is the beginning of a comment.
        /// </summary>
        /// <param name="iCurPos"></param>
        public bool IsComment(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;

                if (iCurPos > m_strHeaderData.Length - 3)
                    return false;

                string strPeekData = m_strHeaderData.Substring(iCurPos, 3);

                if ((strPeekData.StartsWith("#") && strPeekData[1] == ' ' && char.IsDigit(strPeekData[2])) ||
                    strPeekData.Contains("//") || strPeekData.Contains("/*"))
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsComment function of HeaderDeclareParser class.");
                return false;
            }
        }

        /// <summary>
        /// Indicates if the next character in the parsed header is the beginning of a type definition declaration.   This will also include
        /// typedef structure and enumeration declarations.
        /// </summary>
        /// <param name="iCurPos"></param>
        public bool IsTypeDef(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;

                if (iCurPos > m_strHeaderData.Length - 15)
                    return false;

                string strPeekData = m_strHeaderData.Substring(iCurPos, 15);

                if (strPeekData.StartsWith("typedef"))
                {
                    if (!strPeekData.StartsWith("typedef struct") && !strPeekData.StartsWith("typedef union") && 
                        !IsFunctionDecl(iCurPos))
                        return true;                    
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsTypeDef function of HeaderDeclareParser class.");
                return false;
            }
        }

        /* NOT USED: TypeDefArrays will now be joined into the identified as type definitions.
        /// <summary>
        /// Indicates if the next character in the parsed header is the beginning of a type definition array declaration.
        /// </summary>
        private bool IsTypeDefArray()
        {
            try
            {
                if (!IsTypeDef())
                    return false;

                int iTypeDefEndPos = m_strHeaderData.IndexOf(';', CurrentPos);

                if (iTypeDefEndPos == -1)
                    return false;

                string strPeekData = m_strHeaderData.Substring(CurrentPos, iTypeDefEndPos - CurrentPos);

                if (strPeekData.Contains(']'))
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsTypeDef function of HeaderDeclareParser class.");
                return false;
            }
        }
        */

        /// <summary>
        /// Indicates if the next character in the parsed header is the beginning of an enumeration.
        /// </summary>
        /// <param name="iCurPos"></param>
        public bool IsEnum(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;

                if (iCurPos > m_strHeaderData.Length - 12)  //typedef enum string length is 12 bytes
                    return false;

                string strPeekData = m_strHeaderData.Substring(iCurPos, 12);
                int iEndIndex = -1;

                if (strPeekData == "typedef enum" || strPeekData.StartsWith("enum"))
                {
                    iEndIndex = m_strHeaderData.IndexOf(';', iCurPos);
                    strPeekData = m_strHeaderData.Substring(iCurPos, iEndIndex - iCurPos);

                    if (strPeekData.Contains('{'))
                        return true;
                    else
                        return false;                    
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsEnum function of HeaderDeclareParser class.");
                return false;
            }
        }

        /// <summary>
        /// Indicates if the next character in the parsed header is the beginning of a structure or union.
        /// </summary>
        /// <param name="iCurPos"></param>
        public bool IsStruct(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;

                if (iCurPos > m_strHeaderData.Length - 15)
                    return false;

                string strPeekData = m_strHeaderData.Substring(iCurPos, 15);
                int iEndIndex = -1;                
                
                if (strPeekData.StartsWith("struct") || strPeekData.StartsWith("typedef struct") ||
                    strPeekData.StartsWith("union") || strPeekData.StartsWith("typedef union"))
                {                                                                              
                    iEndIndex = m_strHeaderData.IndexOf(';', iCurPos);
                    strPeekData = m_strHeaderData.Substring(iCurPos, iEndIndex - iCurPos);

                    if (strPeekData.Contains('{'))
                        return true;
                    else
                        return false;                    
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsStruct function of HeaderDeclareParser class.");
                return false;
            }
        }

        /// <summary>
        /// Indicates if the next character in the parsed header is the beginning of a function declaration.
        /// NOTE: As of now, the function declaration check performs slowly and is best to avoid checking function declarations unless 
        /// neccessary. 
        /// </summary>
        /// <param name="iCurPos"></param>
        /// <returns></returns>
        public bool IsFunctionDecl(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;

                int iStartIndex = iCurPos;
                int iEndIndex = m_strHeaderData.IndexOf(");", iStartIndex);

                if (iEndIndex == -1)
                    return false;

                if (m_strHeaderData.IndexOf('(', iCurPos) > iEndIndex)
                    return false;

                string strChunk = m_strHeaderData.Substring(iStartIndex, iEndIndex - iStartIndex);

                if (!strChunk.Contains('{') && !strChunk.Contains('}'))
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsFunctionDecl function of HeaderDeclareParser class.");
                return false;
            }
        }

        /// <summary>
        /// Indicates if the next character in the parsed header is the beginning of an inline function.
        /// </summary>
        /// <returns></returns>
        public bool IsInlineFunc(int iCurPos = -1)
        {
            try
            {
                if (iCurPos == -1)
                    iCurPos = CurrentPos;

                if (iCurPos > m_strHeaderData.Length - 13)  //Static Inline string is 13 bytes.
                    return false;

                if (m_strHeaderData.Substring(iCurPos, 13) == "static inline")
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in IsInlineFunc function of HeaderDeclareParser class.");
                return false;
            }
        }

        #endregion
    }
}
