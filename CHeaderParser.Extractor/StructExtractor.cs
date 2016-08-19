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
//using CHeaderParser.Parser;
using CHeaderParser.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CHeaderParser.Extractor
{    
    public class StructExtractor
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
        public StructExtractor(DataAccess HeaderAccess)
        {
            try
            {
                m_HeaderAccess = HeaderAccess;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor function of StructExtractor class.");
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

        #region Structure/Union Analyzation and Field Extraction Functions

        /// <summary>
        /// Extracts all data contained within the structure/union block passed in the Struct parameter of the function and loads the data into a StructData data 
        /// object.  The structure/union block will be parsed and each field in the structure/union, including sub-structures and unions will be extracted 
        /// and loaded into the appropriate FieldData data class object's and added to the Fields collection in the StructData object, thereby linking each 
        /// field to the structure.   If nested structure/union declarations are detected, then the ExtractStructData function will be called recursively 
        /// from the ExtractFieldData function to extract the appropriate data out of the nested structure/union and generate a child StructData object 
        /// that will be linked as a field to the parent structure/union.
        /// </summary>
        /// <param name="strStruct">Complete structure/union block of code to extract.  This will contain the beginning to the end of the structure/union 
        /// declaration.</param>
        public StructData ExtractStructData(string strStruct)
        {            
            try
            {                
                StructData sdStructure = new StructData();
                
                //Checks to see if the structure is defined as an array and verifies that the array contains only numeric values.  If 
                //non-numeric values are presented in the structure array declaration, they are first converted to their numeric equivalents 
                //before proceeding with extracting the type definition declaration.  This operation will both convert the non-numeric array
                //elements of the fields contained in the structure, as well as the structure, itself.
                if (strStruct.Contains('[') && strStruct.Contains(']'))
                {                    
                    if (!ExtractorUtils.IsNumericArray(strStruct))
                        strStruct = ExtractorUtils.ConvertNonNumericElements(m_HeaderAccess, strStruct);
                }//end if        
                
                int iStartBracketIndex = strStruct.IndexOf('{', 0);
                int iEndBracketIndex = strStruct.LastIndexOf('}');

                string strDeclarator = strStruct.Substring(0, iStartBracketIndex).Trim();

                if (strDeclarator.Contains("struct"))
                    sdStructure.StructUnion = StructUnionEnum.Structure;
                else
                    sdStructure.StructUnion = StructUnionEnum.Union;

                if (strDeclarator.Contains("typedef"))
                {
                    //Extracts name of structure after ending of typedef struct closing bracket.
                    string strStructName = strStruct.Substring(iEndBracketIndex + 1, strStruct.Length - iEndBracketIndex - 1).Split(';')[0].Trim();

                    if (strStructName.Contains('['))                    
                        strStructName = strStructName.Substring(0, strStructName.IndexOf('[')).Trim();

                    sdStructure.StructName = strStructName;
                }
                else
                {
                    int iStartNameIndex = 0;

                    if (sdStructure.StructUnion == StructUnionEnum.Structure)                    
                        iStartNameIndex = strStruct.IndexOf("struct") + "struct".Length;
                    else
                        iStartNameIndex = strStruct.IndexOf("union") + "union".Length;

                    //Extracts name of the structure or union in the structure/union declaration before opening bracket.
                    sdStructure.StructName = strStruct.Substring(iStartNameIndex, iStartBracketIndex - iStartNameIndex).Trim();                    

                    if (sdStructure.StructName.Trim() == "")
                    {
                        //Extracts name of structure after ending of struct closing bracket.
                        string strStructName = strStruct.Substring(iEndBracketIndex + 1, strStruct.Length - iEndBracketIndex - 1).Split(';')[0].Trim();

                        if (strStructName.Contains('['))
                            strStructName = strStructName.Substring(0, strStructName.IndexOf('['));

                        sdStructure.StructName = strStructName;
                    }//end if
                }//end if

                //Parses body of structure and extracts each field (including sub-structures/unions) and loads them into FieldData objects which 
                //then are linked to the structure data class.
                int iCurPos = iStartBracketIndex + 1;

                bool blDeclaratorFound = false;
                FieldData[] aryFields = null;
                int iCurFieldIndex = 0;
                int iFieldByteOffset = 0;

                bool blBitDetected = false;
                int iBitFieldDataSize = 0;
                int iBitCount = 0;
                string strBitFieldTypeName = "";

                while (iCurPos < iEndBracketIndex)
                {
                    if (char.IsLetter(strStruct[iCurPos]) || strStruct[iCurPos] == '*')
                        blDeclaratorFound = true;
                    else
                        iCurPos++;

                    if (blDeclaratorFound)
                    {
                        aryFields = ExtractStructFieldData(strStruct, sdStructure.StructName, sdStructure.StructUnion,
                                                                           ref iCurPos, ref iCurFieldIndex, ref iFieldByteOffset,
                                                                           ref blBitDetected, ref iBitFieldDataSize, ref iBitCount, 
                                                                           ref strBitFieldTypeName);

                        if(aryFields != null)
                            sdStructure.Fields.AddRange(aryFields);

                        blDeclaratorFound = false;                     
                    }//end if
                }//end while     

                //If the structure is declared as an array, then the total number of elements declared for the structure 
                //will be calculated.  
                int iTotalElements = 1;
                string strAnalyze = strStruct.Substring(iEndBracketIndex + 1, strStruct.Length - iEndBracketIndex - 1);

                if (strAnalyze.Contains('[') && strAnalyze.Contains(']'))
                    iTotalElements = ExtractorUtils.CalculateFieldElements(strAnalyze);

                sdStructure.Elements = iTotalElements;

                if (sdStructure.StructUnion == StructUnionEnum.Structure)
                    //Calculates the total number of bytes of each field in the structure.  Once the total number of bytes 
                    //of all fields are calculated, the Data Size property of the structure data object will be assigned by 
                    //multiplying the total number of elements of the structure by the calculated number of bytes of each field.
                    sdStructure.DataSize = sdStructure.Fields.Sum(fld => fld.DataSize) * iTotalElements;
                else
                {
                    //If a union is being extracted, then the union's data size will be set to the field with the maximum data size 
                    //multiplied by the total number of elements of the union.
                    sdStructure.DataSize = sdStructure.Fields.Max(fld => fld.DataSize) * iTotalElements;                    
                }//end if
                
                return sdStructure;                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetStructData function of StructExtractor class.");
                return null;
            }
        }

        /// <summary>
        /// Extracts the next detected field (or set of fields) contained within a structure/union block.  The fields will be extracted from the structure/union within the code block 
        /// passed in the Struct parameter of the function.  The next field to be extracted in the structure block will be determined by the position marker passed by reference
        /// in the CurPos parameter.  In the case that a set of fields are declared in one line and separated by commas, a set of fields for the declared 
        /// data type will be extracted and loaded into the FieldData array.  Once a field or set of fields are detected and extracted they will be loaded 
        /// into a FieldData array that will be returned by the function and can be added to the associated StructData data object.  The function will also 
        /// update the current position marker of the structure body, the current field index and byte offset.  
        /// </summary>
        /// <param name="strStruct">Complete structure/union block of code to extract.  This will contain the beginning to the end of the structure/union 
        /// declaration.</param>
        /// <param name="strStructName">The name of the structure whose fields are to be extracted.</param>
        /// <param name="structOrUnion">Indicates if the block of code represents a structure or union.</param>
        /// <param name="iCurPos">The current position in the structure/union code block.</param>
        /// <param name="iCurFieldIndex">The next available field index in the structure to be assigned to the next extracted field.</param>
        /// <param name="iFieldByteOffset">The current byte offset to be assigned to the next field in the structure to be extracted.</param>
        /// <param name="blBitDetected">Indicates if the previous field extracted from the structure was a field with a bit declaration that has not currently 
        /// exceeded the boundary of its associated data type.</param>
        /// <param name="iBitFieldDataSize">If bit fields are currently being extracted from the structure, then this field must be set to size 
        /// of the data type associated with the bit fields that are being extracted.  The BitFieldDataSize will be used to increment the byte offset 
        /// of the structure after the end of the bit field declaration is detected.</param>
        /// <param name="iBitCount">The total number of bits that are currently calculated for the current set of bit fields being extracted from 
        /// the structure.  If the bit count surpasses the data size, then it indicates the end of the current set of bit fields being extracted and 
        /// the beginning of a new set of bit fields in the structure.</param>
        /// <param name="strBitFieldTypeName">If bit fields are currently being extracted from the structure, then this field must be set with the 
        /// previous data type name associated with the bit field declaration being extracted.  Once a new field type is detected, the current bit field 
        /// extraction will stop for that field declaration.</param>
        /// <returns></returns>
        private FieldData[] ExtractStructFieldData(string strStruct, string strStructName, StructUnionEnum structOrUnion, 
                                                                     ref int iCurPos, ref int iCurFieldIndex, ref int iFieldByteOffset, ref bool blBitDetected,
                                                                     ref int iBitFieldDataSize, ref int iBitCount, ref string strBitFieldTypeName)
        {
            try
            {                
                FieldData[] aryFields = null;
                FieldData fdFieldSchema = new FieldData();                                                
                
                bool blIsStructDeclaration = false;
                
                if (strStruct.IndexOf("struct", iCurPos) == iCurPos || strStruct.IndexOf("union", iCurPos) == iCurPos)
                {
                    int iBracketIndex = strStruct.IndexOf('{', iCurPos);
                    if (iBracketIndex != -1)
                    {
                        if(iBracketIndex< strStruct.IndexOf(';', iCurPos))
                            blIsStructDeclaration = true;
                    }//end if                                       
                }//end if                                

                int iFieldEndPos = -1;
                string strFieldData = "";
                bool blIsEnum = false;
                bool blIsStruct = false;
                bool blIsPointer = false;
                bool blIsFunction = false;
                bool blNewFieldDetected = true;

                if (blBitDetected)
                    blNewFieldDetected = false;

                if (!blIsStructDeclaration)
                {
                    iFieldEndPos = strStruct.IndexOf(';', iCurPos);
                    strFieldData = strStruct.Substring(iCurPos, iFieldEndPos - iCurPos).Trim();
                }//end if

                if (strStruct.IndexOf("enum", iCurPos) == iCurPos)
                {
                    fdFieldSchema.FieldType = FieldTypeEnum.Enum;

                    iCurPos += "enum".Length;
                    blIsEnum = true;
                    blNewFieldDetected = true;
                }
                else if (strStruct.IndexOf("struct", iCurPos) == iCurPos)
                {
                    fdFieldSchema.FieldType = FieldTypeEnum.Structure;

                    if (!blIsStructDeclaration)
                        iCurPos += "struct".Length;

                    blIsStruct = true;
                    blNewFieldDetected = true;
                }
                else if (strStruct.IndexOf("union", iCurPos) == iCurPos)
                {
                    fdFieldSchema.FieldType = FieldTypeEnum.Structure;

                    if (!blIsStructDeclaration)
                        iCurPos += "union".Length;

                    blIsStruct = true;
                    blNewFieldDetected = true;
                }//end if
                
                if (strFieldData.Contains("*") || (strFieldData.Contains("(") && strFieldData.EndsWith(")")))
                {
                    if (blIsStruct)
                        blIsStruct = false;

                    fdFieldSchema.FieldType = FieldTypeEnum.Pointer;
                    fdFieldSchema.DataSize = PointerSizeBytes;
                    blIsPointer = true;
                    blNewFieldDetected = true;

                    if (strFieldData.Contains("(") && strFieldData.EndsWith(")"))
                    {
                        blIsFunction = true;
                        fdFieldSchema.FieldTypeName = "function";
                    }//end if
                }//end if                

                
                if (!blIsStructDeclaration)
                {                    
                    strFieldData = strStruct.Substring(iCurPos, iFieldEndPos - iCurPos).Trim();
                    
                    //int iFieldCurPos = 0;

                    if (!blIsFunction)
                    {                        
                        int iFieldCount = 0;
                        List<string> lstFieldNames = new List<string>();

                        string[] aryFieldSections = null;
                        string strFieldTypeName = "";
                        int iFieldNameStartIndex = 0;
                        
                        //The field names will begin either at the last element in the split field sections array string or when the first element
                        //containing a comma (which indicates the beginning of a multiple single field type declaration) is located.
                        if (!strFieldData.Contains(','))
                        {
                            aryFieldSections = strFieldData.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            iFieldNameStartIndex = aryFieldSections.Length - 1;
                            
                            for (int i = 0; i < aryFieldSections.Length - 1; i++)
                            {
                                strFieldTypeName += aryFieldSections[i];
                            }//next i

                            iFieldNameStartIndex = aryFieldSections.Length - 1;
                            lstFieldNames.Add(aryFieldSections[iFieldNameStartIndex].Trim().Replace(";", ""));
                        }
                        else
                        {                            
                            string[] aryFieldCommaSections = strFieldData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            string strSection = aryFieldCommaSections[0];

                            if (strSection.IndexOf(':') == -1)
                                aryFieldSections = strSection.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            else
                            {                                
                                int iColonIndex = strSection.IndexOf(':');
                                string strSectionSub = strSection.Substring(iColonIndex, strSection.Length - iColonIndex);
                                strSection = strSection.Remove(iColonIndex);
                                strSection += strSectionSub.Replace(" ", "");

                                aryFieldSections = strSection.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                                for (int i = 1; i < aryFieldCommaSections.Length; i++)
                                {
                                    aryFieldCommaSections[i] = aryFieldCommaSections[i].Replace(" ", "").Replace("\n", "");
                                }//next i
                            }//end if

                            for (int i = 0; i < aryFieldSections.Length - 1; i++)
                            {
                                strFieldTypeName += aryFieldSections[i];
                            }//next i

                            iFieldNameStartIndex = aryFieldSections.Length - 1;
                            lstFieldNames.Add(aryFieldSections[iFieldNameStartIndex].Trim().Replace(";", ""));

                            for(int i =1; i < aryFieldCommaSections.Length; i++)
                                lstFieldNames.Add(aryFieldCommaSections[i].Trim().Replace(";", ""));
                        }//end if

                        fdFieldSchema.FieldTypeName = strFieldTypeName;

                        if (!blIsPointer && !blIsStruct && !blIsEnum)
                            fdFieldSchema.FieldType = GetVarFieldType(fdFieldSchema.FieldTypeName);
                        
                                                
                        //If fields in the structure are declared with bit values, then a check will be made to determine if any of the bit fields 
                        //in the bit field set are declared without field names.  Bit fields declared without field names indicate either unused 
                        //bits in the field or the repositioning to a new byte in the bit declared field set.  If an empty bit field name is declared 
                        //with a bit value of zero, it indicates that the new bit field declared in the set will be positioned at a beginning of a new
                        //byte.  
                        if (lstFieldNames[0].Contains(":"))
                        {
                            int iCheckBitFieldCounter = 0;
                            int iFieldNameCountBitCheck = lstFieldNames.Count;
                            int iCheckBitIndex = 0;
                            int iFieldBits = 0;

                            while (iCheckBitFieldCounter < iFieldNameCountBitCheck)
                            {
                                string strFieldName = lstFieldNames[iCheckBitIndex].Replace(" ", "");
                                if (strFieldName.StartsWith(":"))
                                {
                                    iFieldBits = Convert.ToInt32(strFieldName.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                    
                                    if (iFieldBits == 0)
                                    {
                                        //If a blank field with a zero value is detected, then advance the bit field counter to the beginning position 
                                        //of the next byte of the field.
                                        int iByteAlignedBits = Convert.ToInt32(Math.Truncate(iBitFieldDataSize / 8d));                                        
                                        iBitCount = 8 + iByteAlignedBits;                                        
                                    }
                                    else
                                    {
                                        //If a blank field with a bit value declaration is detected, then the bit field counter is advanced to the number 
                                        //of bits specified in the field.
                                        iBitCount += iFieldBits;
                                    }//end if

                                    //Since blank name bit field declarations are used only for repositioning the bit fields, these fields will be removed
                                    //from the field data list and not added to the structure.  
                                    lstFieldNames.RemoveAt(iCheckBitIndex);
                                }
                                else
                                    iCheckBitIndex++;                                

                                iCheckBitFieldCounter++;
                            }//end while

                            if (lstFieldNames.Count == 0)
                                return null;
                        }//end if


                        iFieldCount = lstFieldNames.Count;
                        aryFields = new FieldData[iFieldCount];

                        int iFieldTypeDataSize = 0;

                        if (!blIsPointer)
                            iFieldTypeDataSize = GetVarFieldDataSize(fdFieldSchema.FieldTypeName);
                        else
                        {
                            iFieldTypeDataSize = PointerSizeBytes;

                            if (!fdFieldSchema.FieldTypeName.Contains("*"))
                                fdFieldSchema.FieldTypeName += "*";
                        }//end if

                        for (int i = 0; i < lstFieldNames.Count; i++)
                        {
                            if (blBitDetected && i > 0)
                                blNewFieldDetected = false;

                            aryFields[i] = new FieldData();

                            string[] aryFieldNameRaw = lstFieldNames[i].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            string strFieldName = aryFieldNameRaw[0].Trim();
                            int iFieldBits = 0;

                            //FOR DEBUGGING
                            //if (aryFieldNameRaw.Length > 1)
                            //    Debugger.Break();
                            //////////////

                            if(blBitDetected)
                            {
                                bool blBitFieldEndDetect = false;

                                //If a bit field declaration extraction is currently in progress in the structure, then a check will be made to see if 
                                //another field of the same bit declaration is detected or if a new field outside the set of fields of the bit declaration 
                                //is detected, which will signal an end to the extraction of the previous set of bit fields.
                                if (aryFieldNameRaw.Length == 1)
                                {
                                    if (blBitDetected)
                                        blBitFieldEndDetect = true;
                                }
                                else if (fdFieldSchema.FieldTypeName != strBitFieldTypeName)
                                {
                                    //A field with a new data type, even being the beginning of a new bit field declaration, will result in the ending 
                                    //of the previous set of bit field declarations, thereby advancing the offset position to the boundary of the 
                                    //previous bit field declaration's data type.                                    
                                    blBitFieldEndDetect = true;
                                }
                                else if(iBitCount >= (iBitFieldDataSize * 8))
                                {
                                    //If the previous set of bit fields that were extracted have been calculated to equal the size of the data type 
                                    //associated with the set of bit fields then it can be determine that the end of the declaration for the set of 
                                    //bit fields has been reached.  If a new set of bit field declarations are detected, then a new extraction operation 
                                    //will be performed for the new set of bit fields.  The offset position will be advanced to the boundary of the 
                                    //previous bit fields declaration's data type.
                                    blBitFieldEndDetect = true;
                                }//end if

                                if (blBitFieldEndDetect)
                                {
                                    //Once a new field is detected in the structure, the byte offset will be advanced in the structure.  The byte offset 
                                    //will be advanced to a position that is equal to the size of the previous data type associated with the bit field 
                                    //declaration.
                                    blNewFieldDetected = true;
                                    iFieldByteOffset += iBitFieldDataSize;
                                    blBitDetected = false;
                                }//end if
                            }//end if

                            if (blNewFieldDetected && aryFieldNameRaw.Length > 1)
                            {
                                //If a new bit field declaration is detected in the structure, then the appropriate variables will be set to indicate 
                                //that the next set of fields being extracted in the structure are part of a bit field declaration, until the end of the 
                                //bit field declaration (or structure declaration) is detected.  
                                blBitDetected = true;
                                iBitFieldDataSize = iFieldTypeDataSize;
                                strBitFieldTypeName = strFieldTypeName;
                            }//end if

                            if (aryFieldNameRaw.Length > 1)
                            {
                                iFieldBits = Convert.ToInt32(aryFieldNameRaw[1].Trim());
                                iBitCount += iFieldBits;
                            }//end if

                            int iTotalElements = 1;
                            
                            if (strFieldName.Contains("["))
                            {
                                iTotalElements = ExtractorUtils.CalculateFieldElements(strFieldName);
                                strFieldName = strFieldName.Split('[')[0].Trim();
                            }//end if

                            if (strFieldName.Contains("*"))                            
                                strFieldName = strFieldName.Replace("*", "").Trim();                                                            

                            aryFields[i].FieldName = strFieldName;
                            aryFields[i].Elements = iTotalElements;

                            if (blNewFieldDetected)
                                aryFields[i].DataSize = iFieldTypeDataSize * iTotalElements;
                            else
                                //Bit field declarations will only have their data size set to the field's data size for the first field of the bit 
                                //declaration that is extracted.  This will also allow for the structure's total data size to be properly calculated.
                                aryFields[i].DataSize = 0;

                            if (blBitDetected)
                                aryFields[i].Bits = iFieldBits;
                        }//next strFieldName              

                        foreach (FieldData fdField in aryFields)
                        {
                            fdField.FieldIndex = iCurFieldIndex;
                            iCurFieldIndex++;

                            fdField.FieldKey = strStructName + "_" + fdField.FieldIndex;
                            fdField.ParentName = strStructName;

                            fdField.FieldType = fdFieldSchema.FieldType;
                            fdField.FieldTypeName = fdFieldSchema.FieldTypeName;

                            fdField.FieldByteOffset = iFieldByteOffset;

                            //Field byte offsets will only be incremented during the extraction of fields if the fields are being extracted from a structure.  If a union 
                            //block is having its fields, extracted, the byte offset of all fields will be the same (all fields occupy shared memory space within a union)
                            //and the field byte offset variable will be incremented only after the entire union's data is extracted.
                            if (structOrUnion == StructUnionEnum.Structure)
                            {
                                //Field byte offsets will only be advanced if the field being extracted is not part of a bit declaration.  Byte offsets will
                                //be the same for each bit declaration and each declared bit variable will be treated as a single field of its associated 
                                //data type.
                                if(!blBitDetected)
                                    //Advances byte offset position in structure to next field after the total bytes of the current field are calculated.
                                    iFieldByteOffset += fdField.DataSize;
                            }//end if
                        }//next fdField
                    }
                    else
                    {
                        //If the current field being extracted in the structure is a pointer to a function, then the function field will be extracted 
                        //as a pointer.   In addition, to being identified as a pointer field, the function will have a field name set to the 
                        //word "Function" concatenated with the index of the function field in the structure.  This way the function
                        //field can be identified in the structure.  All additional settings of the function, such as field type name will have been
                        //set previously in the code.                                              
                        fdFieldSchema.FieldIndex = iCurFieldIndex;
                        iCurFieldIndex++;

                        fdFieldSchema.FieldName = "Function" + "_" + fdFieldSchema.FieldIndex.ToString();
                        fdFieldSchema.FieldKey = strStructName + "_" + fdFieldSchema.FieldIndex.ToString();

                        fdFieldSchema.ParentName = strStructName;

                        fdFieldSchema.FieldByteOffset = iFieldByteOffset;

                        //Field byte offsets will only be incremented during the extraction of fields if the fields are being extracted from a structure.  If a union 
                        //block is having its fields, extracted, the byte offset of all fields will be the same (all fields occupy shared memory space within a union)
                        //and the field byte offset variable will be incremented only after the entire union's data is extracted.
                        if (structOrUnion == StructUnionEnum.Structure)
                        {
                            //Advances byte offset position in structure to next field after the total bytes of the current field are calculated.
                            iFieldByteOffset += fdFieldSchema.DataSize;
                        }//end if

                        aryFields = new FieldData[] { fdFieldSchema };
                    }//end if

                    //Advances the cursor of the structure to the end of the current field data that was extracted and to the beginning of the 
                    //next set of field data to be extracted or to the end of the structure, if it has been reached.
                    iCurPos = iFieldEndPos + 1;
                }
                else
                {
                    string strNestedStruct = "";
                    int[] aryNestedBlockIndexes = ParserUtils.GetBlockIndexes(ref strStruct, iCurPos);

                    if (aryNestedBlockIndexes == null)
                        return null;

                    int iEndNestStructIndex = strStruct.IndexOf(';', aryNestedBlockIndexes[1]);
                    strNestedStruct = strStruct.Substring(iCurPos, iEndNestStructIndex + 1 - iCurPos);

                    StructData sdChild = ExtractStructData(strNestedStruct);

                    if (sdChild == null)
                        return null;

                    FieldData fdField = new FieldData();
                    fdField.FieldType = FieldTypeEnum.Structure;
                    fdField.FieldTypeName = sdChild.StructName;
                    fdField.FieldName = sdChild.StructName;

                    if (fdField.FieldName.Trim() == "")
                    {
                        //If a nested structure or union is unnamed, then a field type name of "struct" or "union" will be set as the field type 
                        //name of the field.
                        if (sdChild.StructUnion == StructUnionEnum.Structure)
                            fdField.FieldTypeName = "struct";
                        else
                            fdField.FieldTypeName = "union";

                        //If a nested structure or union is unnamed, then a generated field name of "Structure" or "Union" concatenated 
                        //with the field index will be set as the name of the field in the structure.
                        if (sdChild.StructUnion == StructUnionEnum.Structure)
                            fdField.FieldName = "Structure_" + iCurFieldIndex.ToString();
                        else
                            fdField.FieldName = "Union_" + iCurFieldIndex.ToString();
                    }
                    else
                    {
                        //Since there will be no way of identifying if nested structures/unions contained within structures and unions are either 
                        //a structure or union type (as of the current version), it will be neccessary to concatenate to the name of the 
                        //field, whether it is a nested structure or union.
                        if(sdChild.StructUnion == StructUnionEnum.Structure)
                            fdField.FieldName += " <struct>";
                        else
                            fdField.FieldName += " <union>";
                    }//end if

                    fdField.Elements = sdChild.Elements;
                    fdField.DataSize = sdChild.DataSize;

                    fdField.FieldIndex = iCurFieldIndex;
                    iCurFieldIndex++;

                    fdField.FieldKey = strStructName + "_" + fdField.FieldIndex;
                    fdField.ParentName = strStructName;

                    fdField.FieldByteOffset = iFieldByteOffset;

                    //Field byte offsets will only be incremented during the extraction of fields if the fields are being extracted from a structure.  If a union 
                    //block is having its fields, extracted, the byte offset of all fields will be the same (all fields occupy shared memory space within a union)
                    //and the field byte offset variable will be incremented only after the entire union's data is extracted.
                    if (structOrUnion == StructUnionEnum.Structure)
                    {
                        //Advances byte offset position in structure to next field after the total bytes of the current field are calculated.
                        iFieldByteOffset += fdField.DataSize;
                    }//end if

                    aryFields = new FieldData[] { fdField };

                    //Advances the cursor of the structure to the end of the current field data that was extracted and to the beginning of the 
                    //next set of field data to be extracted or to the end of the structure, if it has been reached.
                    iCurPos = iEndNestStructIndex + 1;
                }//end if

                return aryFields;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ExtractStructFieldData function of StructExtractor class.");
                return null;
            }
        }

        /// <summary>
        /// Returns a value which indicates the data type associated with the field/variable name passed to the function.
        /// </summary>
        /// <param name="strFieldTypeName">Name of the field/variable.</param>
        /// <returns></returns>
        private FieldTypeEnum GetVarFieldType(string strFieldTypeName)
        {
            try
            {                
                if (DataAccess.PrimDataTypes.IsPrimitiveDataType(strFieldTypeName))
                    return FieldTypeEnum.Primitive;
                else if (m_HeaderAccess.TypeDefExists(strFieldTypeName))
                    return FieldTypeEnum.TypeDef;
                else if (m_HeaderAccess.StructExists(strFieldTypeName))
                    return FieldTypeEnum.Structure;
                else if (strFieldTypeName.Contains("*"))
                    return FieldTypeEnum.Pointer;
                else
                    return FieldTypeEnum.Pointer;                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetVarFieldType function of StructExtractor class.");
                return FieldTypeEnum.Pointer;
            }
        }

        /// <summary>
        /// Gets the size of the data type associated with the field passed to the function in bytes.
        /// </summary>
        /// <param name="strFieldTypeName"></param>
        /// <returns></returns>
        private int GetVarFieldDataSize(string strFieldTypeName)
        {
            try
            {
                if (DataAccess.PrimDataTypes.IsPrimitiveDataType(strFieldTypeName))
                    return DataAccess.PrimDataTypes[strFieldTypeName];
                else if (m_HeaderAccess.TypeDefExists(strFieldTypeName))
                    return m_HeaderAccess.GetTypeDef(strFieldTypeName).DataSize;
                else if (m_HeaderAccess.StructExists(strFieldTypeName))
                    return m_HeaderAccess.GetStruct(strFieldTypeName).DataSize;
                else if (strFieldTypeName.Contains("*"))
                    return PointerSizeBytes;
                else
                    return PointerSizeBytes;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetVarFieldDataSize function of StructExtractor class.");
                return 0;
            }
        }        

        #endregion
    }

}
