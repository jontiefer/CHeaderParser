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
using CHeaderParser.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHeaderParser.Extractor
{
    public class TypeExtractor
    {
        #region Member Variables
        #endregion

        #region Member Object Variables
        #endregion

        #region Member Data Object Variables

        /// <summary>
        /// Reference to Data Access layer object that will used to perform all data agnostic CRUD operations with the data backend.
        /// </summary>
        private DataAccess m_HeaderAccess = null;

        //NOTE: Only data access objects will be linked to other classes in the CHeaderParser library, instead of direct links to data objects.
        //private CHeaderDataSet m_dsHeaderData = null;

        #endregion

        #region Construction/Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HeaderAccess"></param>
        public TypeExtractor(DataAccess HeaderAccess)
        {
            try
            {
                m_HeaderAccess = HeaderAccess;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor function of TypeExtractor class.");
            }
        }

        #endregion

        #region Data Type Size Specific Properties

        /// <summary>
        /// Gets/Sets the size (in bytes) of enumerations to be used on the system.
        /// </summary>
        public static int EnumSizeBytes { get; set; }

        /// <summary>
        /// Gets/Sets the size (in bytes) of pointer to be used on the system.
        /// </summary>
        public static int PointerSizeBytes { get; set; }        

        #endregion              

        #region TypeDef Data Type Extraction and Processing Functions

        /// <summary>
        /// Examines a type definition declaration and extracts the appropriate information about the definition into a TypeDefData data object.  The 
        /// ExtractTypeDefData function will be used to extract type defintions derived from primitive data types and other type definitions, 
        /// pointers (including functions) and enumerations.  Union and Structure type definitions will be extracted using the ExtractStructData function 
        /// of the StructExtractor class instead.  Once the appropriate data is extracted from the type definition string supplied to the function, the data
        ///  can be loaded into the appropriate data table and have their data types be identified in declared variables in structures. 
        /// </summary>
        /// <param name="strTypeDef"></param>
        /// <returns></returns>
        public TypeDefData ExtractTypeDefData(string strTypeDef)
        {
            try
            {                
                TypeDefData tdTypeDef = new TypeDefData();
                
                //Checks to see if the type definition is defined as an array and verifies that the array contains only numeric values.  If 
                //non-numeric values are presented in the type definition array declaration, they are first converted to their numeric equivalents 
                //before proceeding with extracting the type definition declaration.
                if (!strTypeDef.StartsWith("typedef enum") && !strTypeDef.StartsWith("enum"))
                {                    
                    if (strTypeDef.Contains('[') && strTypeDef.Contains(']'))
                    {
                        if (!ExtractorUtils.IsNumericArray(strTypeDef))
                            strTypeDef = ExtractorUtils.ConvertNonNumericElements(m_HeaderAccess, strTypeDef);
                    }//end if                    

                    string strAnalyze = strTypeDef.Trim();
                    strAnalyze = strAnalyze.Replace("typedef", "").Replace(";", "").Replace("\n", "").Trim();

                    string[] aryTypeDefStr = strAnalyze.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    //Extract Primitive C++ Data Type Name
                    string strTypeDefDataType = "";

                    for (int i = 0; i < aryTypeDefStr.Length - 1; i++)
                        strTypeDefDataType += aryTypeDefStr[i] + " ";

                    strTypeDefDataType = strTypeDefDataType.TrimEnd();

                    //Extract Typedef Declaration Name
                    string strTypeDefName = aryTypeDefStr[aryTypeDefStr.Length - 1];

                    //Set the number of bytes associated with the primitive C++ data type which will then be linked to the typedef declaration name.
                    int iDataSizeBytes = 0;

                    if (!strTypeDefDataType.Contains("*") && !strTypeDefName.Contains("*"))
                    {
                        if (DataAccess.PrimDataTypes.IsPrimitiveDataType(strTypeDefDataType))
                        {
                            //Locates the associated primitive data type associated with the type definition and set the data size of the type definition, according 
                            //to the data type.
                            switch (strTypeDefDataType)
                            {
                                case "RMascii":
                                case "RMbool":
                                case "RMuint8":
                                case "RMint8":
                                case "char":
                                case "unsigned char":
                                case "signed char":
                                case "bool":                                
                                    iDataSizeBytes = 1;
                                    break;
                                case "RMuint16":
                                case "RMint16":
                                case "short":
                                case "short int":
                                case "unsigned short":
                                case "unsigned short int":
                                case "signed short":
                                case "signed short int":
                                case "wchar_t":                                
                                    iDataSizeBytes = 2;
                                    break;
                                case "RMuint32":
                                case "RMint32":
                                case "RMnewOperatorSize":                                    
                                case "int":
                                case "unsigned int":
                                case "signed int":
                                case "long":
                                case "long int":
                                case "signed long":
                                case "signed long int":
                                case "unsigned long":
                                case "unsigned long int":
                                case "float":                                
                                    iDataSizeBytes = 4;
                                    break;
                                case "RMint64":
                                case "RMuint64":
                                case "RMreal":
                                case "long long":
                                case "unsigned long long":
                                case "double":
                                case "long double":                                
                                    iDataSizeBytes = 8;
                                    break;
                            };//end switch
                        }
                        else
                        {
                            //If the data type of the type definition is associated with another type definition, then the type definition will be located and the 
                            //data size of the type definition which is the associated data type of the type definition will be assigned to the type definition being
                            //extracted.
                            CHeaderDataSet.tblTypeDefsRow rowTypeDef = m_HeaderAccess.GetTypeDef(strTypeDefDataType);

                            if (rowTypeDef == null)
                                return null;

                            iDataSizeBytes = rowTypeDef.DataSize;
                        }//end if
                    }
                    else
                    {
                        if (strTypeDefName.Contains('*'))
                            strTypeDefName = strTypeDefName.Replace("*", "");

                        //Pointers variables will always be set to the pre-defined size of the pointer length on the operating system, which is preset in the 
                        //program's settings by the user.
                        iDataSizeBytes = PointerSizeBytes;
                    }//end if

                    int iTotalElements = 1;

                    //If the type definition is declared as an array, then the total number of elements in the array and data size of the type definition array
                    //will be calculated.  The data size of a type definition will be the data size of the data type associated with the type definition multiplied 
                    //by the number of elements in the array.                    
                    if (strTypeDefName.Contains('[') && strTypeDefName.Contains(']'))
                    {
                        iTotalElements = ExtractorUtils.CalculateFieldElements(strTypeDefName);
                        strTypeDefName = strTypeDefName.Split('[')[0].Trim();
                    }//end if

                    tdTypeDef.TypeDefName = strTypeDefName;
                    tdTypeDef.Elements = iTotalElements;
                    tdTypeDef.DataSize = iDataSizeBytes * iTotalElements;                    
                }
                else
                {
                    //Enumerations and Enumeration TypeDefs handled by ExtractEnumData function.
                    tdTypeDef = ExtractEnumData(strTypeDef);
                }//end if                 
                
                return tdTypeDef;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ExtractTypeDefData function of TypeExtractor class.");
                return null;
            }
        }

        /* NOT USED: Type Definition Arrays will now be handled in the same manner as non-array type definition declarations
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="strTypeDefAry"></param>
        /// <returns></returns>
        public object[] ExtractTypeDefArrayData(string strTypeDefAry)
        {
            try
            {
                CHeaderDataSet.tblTypeDefsDataTable dtTypeDefs = m_dsHeaderData.tblTypeDefs;
                                
                object[] aryTypeDefAryVarData = null;

                string strAnalyze = strTypeDefAry.Trim();
                strAnalyze = strAnalyze.Replace("typedef", "").Replace(";", "").Trim();

                string[] aryTypeDefAryStr = strAnalyze.Split(' ');

                //Extract TypeDef Variable Name
                string strTypeDefName = aryTypeDefAryStr[0];

                //Extract Typedef Array Declaration Name and Total Number of elements in the array.
                int iTotalAryElements = ExtractorUtils.CalculateFieldElements(aryTypeDefAryStr[aryTypeDefAryStr.Length - 1]);
                string strTypeDefAryName = aryTypeDefAryStr[aryTypeDefAryStr.Length - 1].Split('[')[0].Trim();                 

                //Set the number of bytes associated with the array type definition which will then be linked to the array typedef declaration name.
                //The number of bytes will be calculated by multiplying the number of bytes associated with the primitive derived type definition associated 
                //with the array type definition.
                CHeaderDataSet.tblTypeDefsRow rowTypeDef = dtTypeDefs.FindByTypeDefName(strTypeDefName);

                if (rowTypeDef == null)                
                    return null;                

                int iSrcTypeDefBytes = rowTypeDef.DataSize;
                int iDataSizeBytes = iTotalAryElements * iSrcTypeDefBytes;
               
                aryTypeDefAryVarData = new object[] { strTypeDefAryName, strTypeDefName, iTotalAryElements, iDataSizeBytes };

                return aryTypeDefAryVarData;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ExtractTypeDefArrayData function of TypeExtractor class.");
                return null;
            }
        }       
        */

        #endregion

        #region Enumeration Extraction Functions

        /// <summary>
        /// Examines an enumeration declaration and extracts the appropriate information about the definition into a TypeDefData data object.  Once 
        /// the appropriate data is extracted from the enumeration string supplied to the function, the data can be loaded into the appropriate data table
        /// and have their data types be identified in declared variables in structures. 
        /// </summary>
        /// <param name="strEnumDef"></param>
        /// <returns></returns>
        public TypeDefData ExtractEnumData(string strEnumDef)
        {
            try
            {
                TypeDefData tdEnum = null;
                
                string[] aryEnumDefStr = null;
                string strEnumName = "";

                if (strEnumDef.StartsWith("enum"))
                {
                    strEnumDef = strEnumDef.Replace("enum", "").Trim();
                    aryEnumDefStr = strEnumDef.Split('{');
                    strEnumName = aryEnumDefStr[0].Trim();
                }
                else
                {
                    aryEnumDefStr = strEnumDef.Split('}');

                    //Extract Enumeration Typedef Declaration Name
                    strEnumName = aryEnumDefStr[aryEnumDefStr.Length - 1];
                    strEnumName = strEnumName.Replace(";", "").Trim();
                }//end if

                int iTotalElements = 1;

                //If the enumeration is declared as an array, then the total number of elements in the array and data size of the enumeration array
                //will be calculated.  The data size of an enumeration will be the data size of enumerations on the system multiplied by the number
                //of elements in the array.                    
                if (strEnumName.Contains('[') && strEnumName.Contains(']'))
                {
                    iTotalElements = ExtractorUtils.CalculateFieldElements(strEnumName);
                    strEnumName = strEnumName.Split('[')[0].Trim();
                }//end if
                
                tdEnum = new TypeDefData() { TypeDefName = strEnumName, Elements = iTotalElements, DataSize = EnumSizeBytes * iTotalElements  };

                return tdEnum;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ExtractEnumData function of TypeExtractor class.");
                return null;
            }
        }

        #endregion

    }
}
