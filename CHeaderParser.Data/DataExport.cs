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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHeaderParser.Data
{
    #region Enumerations

    public enum WebExportTypeEnum
    {
        Local = 1,
        Server = 2
    }

    #endregion

    public class DataExport
    {
        #region Member Variables
        #endregion

        #region Member Object Variables
        #endregion

        #region Member Data Object Variables

        //NOTE: Only data access objects will be linked to other classes in the CHeaderParser library, instead of direct links to data objects.
        /*
        /// <summary>
        /// A reference to the HeaderDataSet data object that contains all extracted structure and associated typedef and field information.
        /// The DataExport class will require a linked HeaderDataSet data object to perform its export operations and will use the dataset 
        /// as the source of information containing all information that will be used to export structures and their associated field information.
        /// </summary>
        private CHeaderDataSet m_dsHeaderData = null;
        */

        /// <summary>
        /// A reference to the HeaderDataSet data access object that will provide a wrapper around the backend data source and will be used to 
        /// perform all CRUD operations.  The data source will contain all extracted structure and associated typedef and field information 
        /// and be accessed bia the data access layer object.
        /// 
        /// The DataExport class will require a linked data access object to perform its export operations and will use the data source
        /// linked to the data access object as the source of information containing all information that will be used to export structures 
        /// and their associated field information.  The Header Access object will allow for special operations and queries to be performed
        /// upon the linked header data.
        /// </summary>
        private DataAccess m_HeaderAccess = null;

        #endregion

        #region Construction/Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HeaderAccess">A reference to the HeaderDataSet data access object that will provide a wrapper around the backend data source and will be used to 
        /// perform all CRUD operations.  The data source will contain all extracted structure and associated typedef and field information 
        /// and be accessed bia the data access layer object.</param>
        public DataExport(DataAccess HeaderAccess = null)
        {
            try
            {
                if (HeaderAccess != null)
                    LinkHeaderData(HeaderAccess);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor of DataExport class.");
            }
        }

        #endregion

        #region Export Link and Header Data Source Related Properties, Functions

        /// <summary>
        /// Links the HeaderDataSet data object that contains all extracted structure and associated typedef and field information.
        /// The DataExport class will require a linked HeaderDataSet data object to perform its export operations and will use the linked 
        /// data source as the source of information containing all information that will be used to export structures and their associated
        /// field information.
        /// </summary>
        /// <param name="HeaderAccess"></param>
        public void LinkHeaderData(DataAccess HeaderAccess)
        {
            try
            {
                m_HeaderAccess = HeaderAccess;                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in LinkHeaderData function of DataExport class.");
            }
        }

        #endregion

        #region Structure Query Export Functions

        /// <summary>
        /// Generates JSON formatted data structures a set of structures and their associated fields and exports the data to a JSON file 
        /// specified in the FileName parameter.  This export file will contain the relevant information for each structure and be in a format
        /// that will allow for it to be loaded into a web page (such as the StructInfo.html page of the CHeaderParser program) or any
        /// other program that can consume and process JSON formatted data. 
        /// The Structure/Union JSON export file will be in the following JSON format:
        /// "structs": [ { "structname",  "structdata": { "datasize", "fieldcount", "elements",
        ///                                                                 "fields": [ { "offset", "fieldtypename", "fieldname",
        ///                                                                                "fieldindex", "datasize", "elements" } ] } }]         
        /// </summary>
        /// <param name="aryStructs">Array of structures to export into JSON formatted export data file.</param>
        /// <param name="strFileName">The full name and path of the struture/union export data file.  It will either be a JSON file or JS file 
        /// containing all the JSON data assigned to a global variable.</param>
        /// <param name="exportType">Indicates whether the export file will be generated so that it can be loaded into a page hosted 
        /// either locally or on a web server.   If an export data file is to be generated that is to be used in a locally hosted web file, then 
        /// a javascript file will be generated that will load the entire exported JSON data structure into a global variable.  When the a web  
        /// file hosted on a server is used, the data will be exported to a standard JSON formatted data file that can be consumed by the 
        /// server hosted  web application.</param>
        public bool ExportStructs(CHeaderDataSet.tblStructuresRow[] aryStructs, string strFileName, WebExportTypeEnum exportType)
        {
            FileStream fs = null;
            
            try
            {
                fs = File.Create(strFileName);
                StreamWriter swrt = new StreamWriter(fs);

                string strJSONExportData = "";

                if (exportType == WebExportTypeEnum.Local)
                {
                    //If a locally hosted web page is to consume the exported JSON data, the file will be saved as a javascript file that will 
                    //have all the exported JSON data assigned to a global variable named jsonExportData.  The locally hosted web page 
                    //can then add a source to the script to allow for the global variable to be loaded statically along with the web page.
                    StringBuilder sbJSONExportData = new StringBuilder(GenerateStructJSONExportData(aryStructs));
                    sbJSONExportData.Insert(0, "var jsonExportData = ");
                    sbJSONExportData.Append(";");

                    strJSONExportData = sbJSONExportData.ToString();                    
                }
                else
                {
                    strJSONExportData = GenerateStructJSONExportData(aryStructs);
                }//end if

                swrt.Write(strJSONExportData);
                swrt.Flush();
                swrt.Close();

                fs = null;

                return true;
            }
            catch(UnauthorizedAccessException)
            {
                MessageBox.Show("Access to the directory is denied!", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ExportStructs Overload 1 function of DataExport class.");
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// Generates JSON formatted data structures a set of structures and their associated fields and exports the data to a JSON file 
        /// specified in the FileName parameter.  This export file will contain the relevant information for each structure and be in a format
        /// that will allow for it to be loaded into a web page (such as the StructInfo.html page of the CHeaderParser program) or any
        /// other program that can consume and process JSON formatted data.  
        /// The Structure/Union JSON export file will be in the following JSON format:
        /// "structs": [ { "structname",  "structdata": { "datasize", "fieldcount", "elements",
        ///                                                                 "fields": [ { "offset", "fieldtypename", "fieldname",
        ///                                                                                "fieldindex", "datasize", "elements" } ] } }]         
        /// </summary>
        /// <param name="aryStructNames">An array of strings, containing the names of each struct to export.</param>
        /// <param name="strFileName">The full name and path of the struture/union export data file.  It will either be a JSON file or JS file 
        /// containing all the JSON data assigned to a global variable.</param>
        /// <param name="exportType">Indicates whether the export file will be generated so that it can be loaded into a page hosted 
        /// either locally or on a web server.   If an export data file is to be generated that is to be used in a locally hosted web file, then 
        /// a javascript file will be generated that will load the entire exported JSON data structure into a global variable.  When the a web  
        /// file hosted on a server is used, the data will be exported to a standard JSON formatted data file that can be consumed by the 
        /// server hosted  web application.</param>
        public bool ExportStructs(string[] aryStructNames, string strFileName, WebExportTypeEnum exportType)
        {
            try
            {
                CHeaderDataSet.tblStructuresRow[] aryStructs = new CHeaderDataSet.tblStructuresRow[aryStructNames.Length];

                for (int i = 0; i < aryStructNames.Length; i++)
                    aryStructs[i] = m_HeaderAccess.GetStruct(aryStructNames[i]);

                return ExportStructs(aryStructs, strFileName, exportType);                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ExportStructs Overload 2 function of DataExport class.");
                return false;
            }
        }

        #endregion

        #region JSON Format Export Generation Functions

        /// <summary>
        /// Generates a JSON formatted string containing relevant export information for the set of structures and their associated fields
        /// This string can then be exported to a JSON file or database and be processed by other applications.  
        /// The Structure/Union JSON export string will be in the following JSON format:
        /// "structs": [ { "structname",  "structdata": { "datasize", "fieldcount", "elements",
        ///                                                                 "fields": [ { "offset", "fieldtypename", "fieldname",
        ///                                                                                "fieldindex", "datasize", "elements" } ] } }]         
        /// </summary>
        /// <param name="aryStructs"></param>
        /// <returns></returns>
        public string GenerateStructJSONExportData(CHeaderDataSet.tblStructuresRow[] aryStructs)
        {
            try
            {
                string strJSONExportData = "{\r\n" +
                                                         AddIdent(1) + "\"structs\": [\r\n";                                                         

                foreach (CHeaderDataSet.tblStructuresRow rowStruct in aryStructs)
                {
                    string strJSONItemData = "";

                    strJSONItemData += AddIdent(2) + "{\r\n";
                    strJSONItemData += AddIdent(3) + "\"structname\": \"" + rowStruct.StructName + "\",\r\n";
                    strJSONItemData += AddIdent(3) + "\"structdata\":  {\r\n";
                    strJSONItemData += AddIdent(4) + "\"datasize\": " + rowStruct.DataSize.ToString() + ",\r\n";
                    strJSONItemData += AddIdent(4) + "\"fieldcount\": " + rowStruct.FieldCount.ToString() + ",\r\n";
                    strJSONItemData += AddIdent(4) + "\"elements\": " + rowStruct.Elements.ToString() + ",\r\n";                    
                    strJSONItemData += AddIdent(4) + "\"fields\": [ \r\n";

                    //Loads each field contained in the structure and adds them to the fields sub-node of the fields node of the struct node.
                    CHeaderDataSet.tblFieldsRow[] aryFields = m_HeaderAccess.GetStructFields(rowStruct.StructName);

                    foreach (CHeaderDataSet.tblFieldsRow rowField in aryFields)
                    {
                        strJSONItemData += AddIdent(5) + "{ \r\n";
                        strJSONItemData += AddIdent(6) + "\"offset\": " + rowField.FieldByteOffset.ToString() + ",\r\n";
                        strJSONItemData += AddIdent(6) + "\"fieldtypename\": \"" + rowField.FieldTypeName + "\",\r\n";
                        strJSONItemData += AddIdent(6) + "\"fieldname\": \"" + rowField.FieldName + "\",\r\n";
                        strJSONItemData += AddIdent(6) + "\"fieldindex\": " + rowField.FieldIndex.ToString() + ",\r\n";
                        strJSONItemData += AddIdent(6) + "\"datasize\": " + rowField.DataSize.ToString() + ",\r\n";
                        strJSONItemData += AddIdent(6) + "\"elements\": " + rowField.Elements.ToString() + "\r\n";
                        strJSONItemData += AddIdent(5) + "},\r\n";
                    }//next rowField

                    strJSONItemData = strJSONItemData.Remove(strJSONItemData.Length - 3, 1);

                    strJSONItemData += AddIdent(4) + "]\r\n";
                    strJSONItemData += AddIdent(3) + "}\r\n";
                    strJSONItemData += AddIdent(2) + "},\r\n";

                    strJSONExportData += strJSONItemData;
                }//next 
                
                strJSONExportData = strJSONExportData.Remove(strJSONExportData.Length - 3, 1);
                strJSONExportData += AddIdent(1) + "]\r\n";
                strJSONExportData += "}";

                return strJSONExportData;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GenerateStructJSONExportData function of DataExport class.");
                return "";
            }
        }

        /* NOT USED: This function is unnecessary, the GenerateStructJSONExportData function will handle this processing.
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="rowStruct"></param>
        /// <returns></returns>
        private string GenerateStructJSONItem(CHeaderDataSet.tblStructuresRow rowStruct)
        {
            try
            {
                

                return strJSONData;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GenerateStructJSONItem function of DataExport class.");
                return "";
            }
        }
        */

        /// <summary>
        /// Returns a string that contains a set of identation spaces, based on the nest index and the identation character string specified 
        /// in the function.
        /// </summary>
        /// <param name="iNestIndex"></param>
        /// <returns></returns>
        private string AddIdent(int iNestIndex, string strIdentChar = "    ")
        {
            try
            {                
                string strIdentOutput = "";

                for (int i = 0; i < iNestIndex; i++)
                    strIdentOutput += strIdentChar;

                return strIdentOutput;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in AddIdent function of DataExport class.");
                return "";
            }
        }

        #endregion
    }
}
