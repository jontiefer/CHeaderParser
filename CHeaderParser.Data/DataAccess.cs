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

using CHeaderParser.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CHeaderParser.Data
{
    #region Enumerations

    public enum SortOrderEnum
    {
        None = 0,
        Ascending = 1,
        Descending = 2
    }

    #endregion

    public class DataAccess
    {
        #region Member Variables
        #endregion

        #region Member Object Variables
        #endregion

        #region Member Data Object Variables

        /// <summary>
        /// The DataAccess class will contain its own instantiated CHeaderDataSet object that can be used to load/save header file data in the program 
        /// directly from the DataAccess class.  Optionally, a CHeaderDataSet object can be supplied to the class and used for all data access layer specific
        /// operations within the class.
        /// </summary>
        private CHeaderDataSet m_dsHeaderData = null;
        
        private static PrimitiveDataTypes m_PrimDataTypes = new PrimitiveDataTypes();

        #endregion

        #region Construction/Initialization
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dsHeaderData">The CHeaderDataSet that will be used for all data operations within the class.  If not supplied, a default 
        /// CHeaderDataSet will be instantiated and used in the DataAccess class.</param>
        public DataAccess(CHeaderDataSet dsHeaderData = null)
        {
            try
            {
                if (dsHeaderData != null)
                    m_dsHeaderData = dsHeaderData;
                else
                    m_dsHeaderData = new Data.CHeaderDataSet();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor function of DataAccess class.");
            }
        }

        #endregion

        #region Header Data Set/Data Table Access Properties

        /// <summary>
        /// Gets/Sets a DataSet object that can be used to load/save header file data in the program directly from the DataAccess class.   This dataset 
        /// will be used for all data access layer specific operations within the class.
        /// </summary>
        public CHeaderDataSet HeaderDataSet
        {
            get
            {
                return m_dsHeaderData;
            }
            set
            {
                m_dsHeaderData = value;
            }
        }

        /// <summary>
        /// Gets a reference to the Structures data table contained in the HeaderData data set object linked to the DataAccess class.
        /// </summary>
        public CHeaderDataSet.tblStructuresDataTable StructuresTable
        {
            get
            {
                return m_dsHeaderData.tblStructures;
            }
        }

        /// <summary>
        /// Gets a reference to the TypeDefs data table contained in the HeaderData data set object linked to the DataAccess class.
        /// </summary>
        public CHeaderDataSet.tblTypeDefsDataTable TypeDefsTable
        {
            get
            {
                return m_dsHeaderData.tblTypeDefs;
            }
        }

        /// <summary>
        /// Gets a reference to the Fields data table contained in the HeaderData data set object linked to the DataAccess class.
        /// </summary>
        public CHeaderDataSet.tblFieldsDataTable FieldsTable
        {
            get
            {
                return m_dsHeaderData.tblFields;
            }
        }

        #endregion

        #region Primitive and Other Non CHeaderDataSet Data Object Properties

        /// <summary>
        /// Contains all the information about primitive data types.  Each primitive data type will have its data size (in bytes) stored and have its declarator 
        /// used as the key to identify the data type.
        /// </summary>
        public static PrimitiveDataTypes PrimDataTypes
        {
            get
            {
                return m_PrimDataTypes;
            }
        }

        #endregion

        #region Table Updating Functions

        /// <summary>
        /// Adds a row to the Structures data table in the Header Data Set object linked to the DataAccess class using the data contained in the
        /// StructData object passed to the function.  The StructData object will contain all the required information needed to create a structure record
        /// in the Structures table. 
        /// </summary>
        /// <param name="sdStruct"></param>
        /// <returns></returns>
        public bool AddStructRow(StructData sdStruct)
        {
            try
            {
                CHeaderDataSet.tblStructuresRow rowStruct = StructuresTable.NewtblStructuresRow();

                rowStruct.StructName = sdStruct.StructName;
                rowStruct.StructUnion = (byte)sdStruct.StructUnion;
                rowStruct.FieldCount = sdStruct.Fields.Count;
                rowStruct.Elements = sdStruct.Elements;
                rowStruct.DataSize = sdStruct.DataSize;

                StructuresTable.AddtblStructuresRow(rowStruct);

                return true;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in AddStructRow function of DataAccess class.");
                return false;
            }
        }

        /// <summary>
        /// Adds a row to the TypeDefs data table in the Header Data Set object linked to the DataAccess class using the data contained in the
        /// TypeDefData object passed to the function.  The TypeDefData object will contain all the required information needed to create a type 
        /// definition record in the TypeDefs table. 
        /// </summary>
        /// <param name="tdTypeDef"></param>
        /// <returns></returns>
        public bool AddTypeDefRow(TypeDefData tdTypeDef)
        {
            try
            {
                CHeaderDataSet.tblTypeDefsRow rowTypeDef = TypeDefsTable.NewtblTypeDefsRow();

                rowTypeDef.TypeDefName = tdTypeDef.TypeDefName;
                rowTypeDef.Elements = tdTypeDef.Elements;
                rowTypeDef.DataSize = tdTypeDef.DataSize;

                TypeDefsTable.AddtblTypeDefsRow(rowTypeDef);

                return true;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in AddTypeDefRow function of DataAccess class.");
                return false;
            }
        }

        /// <summary>
        /// Adds a set of rows to the Fields data table in the Header Data Set object linked to the DataAccess class using the data contained in the
        /// list of FieldData objects passed to the function.  The FieldData object will contain all the required information needed to create a field record
        /// in the Fields table. 
        /// </summary>
        /// <param name="lstFields"></param>
        /// <returns></returns>
        public bool AddFieldRows(List<FieldData> lstFields)
        {
            try
            {
                CHeaderDataSet.tblFieldsRow rowField = null;

                foreach (FieldData fdField in lstFields)
                {
                    rowField = FieldsTable.NewtblFieldsRow();

                    rowField.FieldKey = fdField.FieldKey;                    
                    rowField.FieldName = fdField.FieldName;
                    rowField.ParentName = fdField.ParentName;
                    rowField.FieldIndex = fdField.FieldIndex;
                    rowField.FieldType = (int)fdField.FieldType;
                    rowField.FieldTypeName = fdField.FieldTypeName;
                    rowField.Elements = fdField.Elements;
                    rowField.DataSize = fdField.DataSize;

                    if (fdField.Bits > 0)
                        rowField.FieldName += "<bit: " + fdField.Bits.ToString() + ">";

                    rowField.FieldByteOffset = fdField.FieldByteOffset;

                    FieldsTable.AddtblFieldsRow(rowField);
                }//next fdField

                return true;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in AddFieldRows function of DataAccess class.");
                return false;
            }
        }

        #endregion

        #region TypeDef Record Loading Functions

        /// <summary>
        /// Checks to see if the TypeDef with the name specified in the function parameters exists in the TypeDefs table contained within 
        /// the HeaderData dataset object linked to the DataAccess class.
        /// </summary>
        /// <param name="strTypeDefName"></param>
        /// <returns></returns>
        public bool TypeDefExists(string strTypeDefName)
        {
            try
            {
                if (m_dsHeaderData.tblTypeDefs.FindByTypeDefName(strTypeDefName) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in TypeDefExists function of DataAccess class.");
                return false;
            }
        }

        /// <summary>
        /// Retrieves the row in the TypeDefs data table in the Header Data Set object linked to the DataAccess class with the name specified
        /// in the function's TypeDefName parameter.
        /// </summary>
        /// <param name="strTypeDefName"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblTypeDefsRow GetTypeDef(string strTypeDefName)
        {
            try
            {
                CHeaderDataSet.tblTypeDefsRow rowTypeDef = TypeDefsTable.FindByTypeDefName(strTypeDefName);

                return rowTypeDef;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetTypeDef function of DataAccess class.");
                return null;
            }
        }

        #endregion

        #region Structure Record Loading Functions

        /// <summary>
        /// Checks to see if the structure with the name specified in the function parameters exists in the Structures table contained within 
        /// the HeaderData dataset object linked to the DataAccess class.
        /// </summary>
        /// <param name="strStructName"></param>
        /// <returns></returns>
        public bool StructExists(string strStructName)
        {
            try
            {
                if (m_dsHeaderData.tblStructures.FindByStructName(strStructName) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in StructExists function of DataAccess class.");
                return false;
            }
        }

        /// <summary>
        /// Retrieves the row in the Structures data table in the Header Data Set object linked to the DataAccess class with the name specified
        /// in the function's StructName parameter.
        /// </summary>
        /// <param name="strStructName"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblStructuresRow GetStruct(string strStructName)
        {
            try
            {
                CHeaderDataSet.tblStructuresRow rowStruct = StructuresTable.FindByStructName(strStructName);

                return rowStruct;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetStruct function of DataAccess class.");
                return null;
            }
        }

        #endregion

        #region Field Record Loading Functions

        /// <summary>
        /// Retrieves a set of FieldData rows in the Fields data table in the Header Data Set object linked to the DataAccess class that are contained 
        /// in the structure/union specified in the function's StructName parameter.  Each field in a structure/union will be keyed by the name of the 
        /// structure/union concatenated with the index of the field.
        /// </summary>
        /// <param name="strStructName"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblFieldsRow[] GetStructFields(string strStructName)
        {
            try
            {
                CHeaderDataSet.tblStructuresRow rowStruct = GetStruct(strStructName);

                if (rowStruct == null)
                    return null;

                CHeaderDataSet.tblFieldsRow[] rowFields = new CHeaderDataSet.tblFieldsRow[rowStruct.FieldCount];

                for (int iFieldIndex = 0; iFieldIndex < rowFields.Length; iFieldIndex++)
                {
                    rowFields[iFieldIndex] = FieldsTable.FindByFieldKey(rowStruct.StructName + "_" + iFieldIndex.ToString());
                }//next iFieldIndex

                return rowFields;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetStructFields function of DataAccess class.");
                return null;
            }
        }

        #endregion

        #region Data Table Querying Functions

        /// <summary>
        /// Queries all structures from the Structure data table contained in the linked HeaderData data set that are within a specified range of data sizes. 
        /// The structure rows will be queried and sorted in ascending or descending order.  This function can be used to query either sets of structures or unions.
        /// </summary>
        /// <param name="iMinSize"></param>
        /// <param name="iMaxSize"></param>
        /// <param name="sortOrder"></param>
        /// <param name="structOrUnion"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblStructuresRow[] QueryStructsBySize(int iMinSize, int iMaxSize = -1,
                                                                                        SortOrderEnum sortOrder = SortOrderEnum.Ascending,
                                                                                        StructUnionEnum structOrUnion = StructUnionEnum.Structure)
        {
            try
            {
                if (iMaxSize == -1)
                    iMaxSize = Int32.MaxValue;

                IEnumerable<CHeaderDataSet.tblStructuresRow> qryStructRows = null;
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                qryStructRows = StructuresTable.Where(s => s.DataSize >= iMinSize && s.DataSize <= iMaxSize);

                if (structOrUnion != StructUnionEnum.Both)
                {
                    if (structOrUnion == StructUnionEnum.Structure)
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 1);
                    else
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 2);
                }//end if

                if (sortOrder == SortOrderEnum.Ascending)
                    qryStructRows = qryStructRows.OrderBy(s => s.DataSize);
                else
                    qryStructRows = qryStructRows.OrderByDescending(s => s.DataSize);

                aryStructRows = qryStructRows.ToArray();

                return aryStructRows;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsBySize function of DataAccess class.");

                return null;
            }
        }

        /* NOT USED: Structures and Unions can be queried specifically by the StructOrUnion parameter.
        /// <summary>
        /// Queries all unions from the Structure data table contained in the linked HeaderData data set that are within a specified range of data sizes. 
        /// The union rows will be queried and sorted in ascending or descending order. 
        /// </summary>
        /// <param name="iMinSize"></param>
        /// <param name="iMaxSize"></param>
        /// <param name="sortOrder"></param>
        /// <param name="structOrUnion"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblStructuresRow[] QueryUnionsBySize(int iMinSize, int iMaxSize = -1,
                                                                                        SortOrderEnum sortOrder = SortOrderEnum.Ascending)
        {
            try
            {
                return QueryStructsBySize(iMinSize, iMaxSize, sortOrder, StructUnionEnum.Union);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryUnionsBySize function of DataAccess class.");

                return null;
            }
        }
        */

        /// <summary>
        /// Queries all structures contained in the Structures data table that match either a portion or the entire name of the structure query string passed 
        /// to the function.  The structure query can also be case-sensitive depending on the parameters that are set in the function.  
        /// NOTE: This version of the function does not use wildcards, but will just match a portion or the entire name of each structure stored in the 
        /// Structures data table. 
        /// </summary>
        /// <param name="strStructQuery"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblStructuresRow[] QueryStructsByName(
                                                string strStructQuery, bool blMatchAny = true, bool blMatchExact = false, bool blMatchCase = false,
                                                SortOrderEnum sortOrder = SortOrderEnum.Ascending, StructUnionEnum structOrUnion = StructUnionEnum.Both)
        {
            try
            {
                IEnumerable<CHeaderDataSet.tblStructuresRow> qryStructRows = null;
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                if (!blMatchExact)
                {
                    if (!blMatchCase)
                    {
                        qryStructRows = StructuresTable.Where(s => blMatchAny ? s.StructName.ToUpper().Contains(strStructQuery.ToUpper()) :
                                                                                                              s.StructName.ToUpper().StartsWith(strStructQuery.ToUpper()));
                    }
                    else
                    {
                        qryStructRows = StructuresTable.Where(s => blMatchAny ? s.StructName.Contains(strStructQuery) :
                                                                                                              s.StructName.StartsWith(strStructQuery));
                    }//end if
                }
                else
                {
                    if (!blMatchCase)
                    {
                        qryStructRows = StructuresTable.Where(s => s.StructName.ToUpper() == strStructQuery.ToUpper());
                    }
                    else
                    {
                        qryStructRows = StructuresTable.Where(s => s.StructName == strStructQuery);
                    }//end if
                }//end if

                if (structOrUnion != StructUnionEnum.Both)
                {
                    if (structOrUnion == StructUnionEnum.Structure)
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 1);
                    else
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 2);
                }//end if

                if (sortOrder == SortOrderEnum.Ascending)
                    qryStructRows = qryStructRows.OrderBy(s => s.StructName);
                else
                    qryStructRows = qryStructRows.OrderByDescending(s => s.StructName);

                aryStructRows = qryStructRows.ToArray();

                return aryStructRows;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsByName function of DataAccess class.");
                return null;
            }
        }

        /// <summary>
        /// Queries all structures contained in the Structures data table using a wildcard query.  The '?' and '*' wildcard characters can be used to 
        /// query the structures in the function.  The '?' character will serve as a single character wildcard in the string, whereas the '*' character 
        /// will serve as a wildcard for any number of characters proceeding the wildcard symbol.  If the MatchCase parameter is set to true, then 
        /// the wildcard query will be case-sensitive.  
        /// </summary>
        /// <param name="strExpression"></param>
        /// <param name="blMatchCase"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblStructuresRow[] QueryStructsByWildcard(
                                                                string strExpression, bool blMatchCase = false,
                                                                SortOrderEnum sortOrder = SortOrderEnum.Ascending,
                                                                StructUnionEnum structOrUnion = StructUnionEnum.Both)
        {
            try
            {
                IEnumerable<CHeaderDataSet.tblStructuresRow> qryStructRows = null;
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                strExpression = "^" + strExpression.Replace('?', '.').Replace("*", "\\w*") + "\\z";

                if (!blMatchCase)
                {
                    strExpression = "(?i)" + strExpression;

                    qryStructRows = StructuresTable.Where(s =>
                                                                        Regex.IsMatch(s.StructName, strExpression));
                }
                else
                {
                    strExpression = "(?-i)" + strExpression;

                    qryStructRows = StructuresTable.Where(s =>
                                                                            Regex.IsMatch(s.StructName, strExpression));
                }//end if

                if (structOrUnion != StructUnionEnum.Both)
                {
                    if (structOrUnion == StructUnionEnum.Structure)
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 1);
                    else
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 2);
                }//end if

                if (sortOrder == SortOrderEnum.Ascending)
                    qryStructRows = qryStructRows.OrderBy(s => s.StructName);
                else
                    qryStructRows = qryStructRows.OrderByDescending(s => s.StructName);

                aryStructRows = qryStructRows.ToArray();

                return aryStructRows;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsByWildcard function of DataAccess class.");
                return null;
            }
        }

        /// <summary>
        /// Queries all structures contained in the Structures data table using a regular expression query.  The results will match exactly to criteria  
        /// specified in the regular expression and will be case and space sensitive.
        /// </summary>
        /// <param name="strRegex"></param>
        /// <returns></returns>
        public CHeaderDataSet.tblStructuresRow[] QueryStructsByRegex(string strRegex, 
                                                                                                      SortOrderEnum sortOrder = SortOrderEnum.Ascending,
                                                                                                      StructUnionEnum structOrUnion = StructUnionEnum.Both)
        {
            try
            {
                IEnumerable<CHeaderDataSet.tblStructuresRow> qryStructRows = null;
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                qryStructRows = StructuresTable.Where(s => Regex.IsMatch(s.StructName, strRegex));

                if (structOrUnion != StructUnionEnum.Both)
                {
                    if (structOrUnion == StructUnionEnum.Structure)
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 1);
                    else
                        qryStructRows = qryStructRows.Where(s => s.StructUnion == 2);
                }//end if

                if (sortOrder == SortOrderEnum.Ascending)
                    qryStructRows = qryStructRows.OrderBy(s => s.StructName);
                else
                    qryStructRows = qryStructRows.OrderByDescending(s => s.StructName);

                aryStructRows = qryStructRows.ToArray();

                return aryStructRows;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsByRegex function of DataAccess class.");
                return null;
            }
        }

        /// <summary>
        /// Queries the data type associated with the field with the specified key passed to the function.  The linked data type record will be returned 
        /// by the function.   Depending on what type of data type is associated with the field, will determine whether a structure data row, typedef data row
        /// or primitive data object is returned by the function.  Primtive data types will not be stored in the Header Data Set and will have its data returned 
        /// from the Global Primitive data object.
        /// </summary>
        /// <param name="strFieldKey"></param>
        /// <returns></returns>
        public object QueryFieldTypeDataByKey(string strFieldKey)
        {
            try
            {                
                CHeaderDataSet.tblFieldsRow rowField = FieldsTable.FindByFieldKey(strFieldKey);

                if (rowField == null)
                    return null;
                
                switch ((FieldTypeEnum)rowField.FieldType)
                {
                    case FieldTypeEnum.Primitive:
                    case FieldTypeEnum.Enum:
                    case FieldTypeEnum.Pointer:
                        object[] aryPrimTypeData = new object[] { rowField.FieldTypeName, DataAccess.PrimDataTypes[rowField.FieldTypeName] };

                        return aryPrimTypeData;                        
                    case FieldTypeEnum.TypeDef:
                        CHeaderDataSet.tblTypeDefsRow rowTypeDef = TypeDefsTable.FindByTypeDefName(rowField.FieldTypeName);

                        return rowTypeDef;
                    case FieldTypeEnum.Structure:
                        CHeaderDataSet.tblStructuresRow rowStruct = StructuresTable.FindByStructName(rowField.FieldTypeName);

                        return rowStruct;
                };

                return null;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryFieldTypeDataByKey function of DataAccess class.");

                return null;
            }
        }

        /// <summary>
        /// Queries the data type associated with the field with the specified field name passed to the function.  The linked data type record will be returned 
        /// by the function.   Depending on what type of data type is associated with the field, will determine whether a structure data row, typedef data row
        /// or primitive data object is returned by the function.  Primtive data types will not be stored in the Header Data Set and will have its data returned 
        /// from the Global Primitive data object.
        /// </summary>
        /// <param name="strFieldKey"></param>
        /// <returns></returns>
        public object QueryFieldTypeDataByName(string strFieldName)
        {
            try
            {
                CHeaderDataSet.tblFieldsRow rowField = FieldsTable.Where(f => f.FieldName == strFieldName).FirstOrDefault();

                if (rowField == null)
                    return null;

                return QueryFieldTypeDataByKey(rowField.FieldKey);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryFieldTypeDataByName function of DataAccess class.");

                return null;
            }
        }

        #endregion

        #region Data Export Functions
        #endregion
    }        
}
