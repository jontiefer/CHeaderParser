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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHeaderParser.Data
{
    #region Enumerations

    public enum StructUnionEnum
    {        
        Both = 0,
        Structure = 1,
        Union = 2
    }

    public enum FieldTypeEnum
    {
        Primitive = 1,
        Enum = 2,
        TypeDef = 3,        
        Structure = 4,
        Pointer = 5
    }

    #endregion

    #region Struct Data Classes  

    /// <summary>
    /// 
    /// </summary>
    public class StructData
    {
        private List<FieldData> m_Fields = new List<FieldData>();

        public string StructName { get; set; }

        public StructUnionEnum StructUnion { get; set; }

        public int Elements { get; set; }

        public int DataSize { get; set; }

        public List<FieldData> Fields
        {
            get
            {
                return m_Fields;
            }
        }
    }

    #endregion

    #region Field Data Classes

    /// <summary>
    /// 
    /// </summary>
    public class FieldData
    {
        public string FieldKey { get; set; }

        public string FieldName { get; set; }

        public string ParentName { get; set; }

        public int FieldIndex { get; set; }

        public FieldTypeEnum FieldType { get; set; }

        public string FieldTypeName { get; set; }

        public int Elements { get; set; }

        public int DataSize { get; set; }

        public int Bits { get; set; }        

        public int FieldByteOffset { get; set; }
    }

    #endregion

    #region Type Definition Data Classes

    /// <summary>
    /// 
    /// </summary>
    public class TypeDefData
    {
        public TypeDefData()
        {            
        }

        public string TypeDefName { get; set; }

        public int Elements { get; set; }

        public int DataSize { get; set; }
    }

    #endregion

    #region Data Type Classes

    /// <summary>
    /// Contains all the information about primitive data types.  Each primitive data type will have its data size (in bytes) stored and have its declarator 
    /// used as the key to identify the data type.
    /// </summary>
    public class PrimitiveDataTypes
    {
        private SortedList<string, int> m_slDataTypes = new SortedList<string, int>();

        /// <summary>
        /// Returns the size (in bytes) of the primitive data type with the name of the primitive type declarator serving as the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int this[string key]
        {
            get
            {
                return (int)m_slDataTypes[key];
            }
        }
        
        public SortedList<string, int> DataTypes
        {
            get
            {
                return m_slDataTypes;
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PrimitiveDataTypes()
        {
            try
            {
                m_slDataTypes.Add("RMascii", 1);
                m_slDataTypes.Add("RMbool", 1);
                m_slDataTypes.Add("RMuint8", 1);
                m_slDataTypes.Add("RMint8", 1);
                m_slDataTypes.Add("char", 1);
                m_slDataTypes.Add("unsigned char", 1);
                m_slDataTypes.Add("signed char", 1);
                m_slDataTypes.Add("bool", 1);

                m_slDataTypes.Add("RMuint16", 2);
                m_slDataTypes.Add("RMint16", 2);                
                m_slDataTypes.Add("short", 2);
                m_slDataTypes.Add("short int", 2);
                m_slDataTypes.Add("unsigned short", 2);
                m_slDataTypes.Add("unsigned short int", 2);
                m_slDataTypes.Add("signed short", 2);
                m_slDataTypes.Add("signed short int", 2);
                m_slDataTypes.Add("wchar_t", 2);

                m_slDataTypes.Add("RMuint32", 4);
                m_slDataTypes.Add("RMint32", 4);
                m_slDataTypes.Add("RMnewOperatorSize", 4);
                m_slDataTypes.Add("int", 4);
                m_slDataTypes.Add("unsigned int", 4);
                m_slDataTypes.Add("signed int", 4);
                m_slDataTypes.Add("long", 4);
                m_slDataTypes.Add("long int", 4);
                m_slDataTypes.Add("signed long", 4);
                m_slDataTypes.Add("signed long int", 4);
                m_slDataTypes.Add("unsigned long", 4);
                m_slDataTypes.Add("unsigned long int", 4);
                m_slDataTypes.Add("float", 4);

                m_slDataTypes.Add("RMint64", 8);
                m_slDataTypes.Add("RMuint64", 8);
                m_slDataTypes.Add("RMreal", 8);
                m_slDataTypes.Add("long long", 8);
                m_slDataTypes.Add("unsigned long long", 8);
                m_slDataTypes.Add("double", 8);
                m_slDataTypes.Add("long double", 8);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor function of PrimitiveDataTypes class.");
            }
        }

        /// <summary>
        /// Checks to see if the data type in the supplied string is a primitive C data type. 
        /// </summary>
        /// <param name="strDataTypeName"></param>
        /// <returns></returns>
        public bool IsPrimitiveDataType(string strDataTypeName)
        {
            try
            {
                return m_slDataTypes.ContainsKey(strDataTypeName);
            }
            catch (Exception err)
            {                
                ErrorHandler.ShowErrorMessage(err, "Error in IsPrimitiveDataType function of PrimitiveDataTypes class.");
                return false;
            }
        }
    }

    #endregion   
}
