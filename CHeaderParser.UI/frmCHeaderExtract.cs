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

using System;
using System.IO;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;
using System.Reflection;

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Data;
using System.Data.OleDb;

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Linq;
using CHeaderParser.Parser;
using CHeaderParser.Extractor;
using CHeaderParser.Data;
using CHeaderParser.Global;

namespace CHeaderParser
{
    public partial class frmCHeaderExtract : Form
    {
        #region Member Variables

        private CancelEvents m_CancelEvents = new CancelEvents();

        #endregion

        #region Member Object Variables
        #endregion

        #region Member Data Object Variables

        private CHeaderDataSet m_dsHeaderData = null;

        private DataAccess m_HeaderAccess = null;

        #endregion

        #region Header Parser Variables

        private HeaderDeclareParser m_Parser = new HeaderDeclareParser();

        #endregion

        #region Header Data Extraction Variables

        private TypeExtractor m_typeExt = null;

        private StructExtractor m_structExt = null;

        #endregion

        #region Construction/Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public frmCHeaderExtract()
        {
            try
            {
                InitializeComponent();

                StructExtractor.EnumSizeBytes = 4;
                StructExtractor.PointerSizeBytes = 4;

                TypeExtractor.EnumSizeBytes = 4;
                TypeExtractor.PointerSizeBytes = 4;

                m_HeaderAccess = new Data.DataAccess();
                m_dsHeaderData = m_HeaderAccess.HeaderDataSet;

                m_typeExt = new Extractor.TypeExtractor(m_HeaderAccess);
                m_structExt = new Extractor.StructExtractor(m_HeaderAccess);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor function of frmCHdrIntExtract form.");
            }
        }

        /// <summary>
        /// Handles the Form Load event of the CHeaderExtract form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCHeaderExtract_Load(object sender, EventArgs e)
        {
            try
            {
                //Sets the default export file name to the ExportData.json and sets the default file path to the path of the CHeaderParser application.
                txtExportServerFileName.Text = General.ApplicationPath + "\\ExportStructData.json";
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in frmCHeaderExtract_Load function of frmCHeaderExtract");
            }
        }

        #endregion

        #region Header File Loading Functions, Event Handlers

        /// <summary>
        /// Displays a file picker dialog that will allow the user to select the header or set of header files to parse in the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeaderFileSearch_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlgFile = new OpenFileDialog();
                dlgFile.Filter = "C/C++ Header Files (*.h;*.hh)|*.h;*.hh|All Files (*.*)|*.*";
                dlgFile.FilterIndex = 0;
                dlgFile.Multiselect = true;
                dlgFile.Title = "Select Header File(s) To Load";
                
                if (dlgFile.ShowDialog() == DialogResult.OK)
                {
                    string strHeaderFileNames = "";

                    foreach (string strFileName in dlgFile.FileNames)
                    {
                        if(!txtHeaderFileNames.Text.ToUpper().Contains(strFileName.ToUpper() + "\r\n") &&
                            !txtHeaderFileNames.Text.ToUpper().Contains(strFileName.ToUpper() + ";") &&
                            !txtHeaderFileNames.Text.ToUpper().Contains(strFileName.ToUpper() + ","))                                                        
                            strHeaderFileNames += strFileName + "\r\n";
                    }//next strFileName
                    
                    txtHeaderFileNames.Text += strHeaderFileNames; 
                }//end if
            }
            catch(Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in btnHeaderFileSearch_Click function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Clears all header file names currently contained in the HeaderFileNames textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFileNames_Click(object sender, EventArgs e)
        {
            try
            {
                txtHeaderFileNames.Text = "";
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in btnClearFileNames_Click function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Loads the data of all the header files set in the header file names textbox into the HeaderDeclareParser object in the form,
        /// which will then make the header data files available for parsing and extracting their type definition and structure data.
        /// </summary>
        private bool LoadHeaderFiles()
        {
            try
            {                
                if (txtHeaderFileNames.Text.Trim() == "")
                {
                    MessageBox.Show("No header files set to be loaded!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }//end if

                m_Parser.ClearHeaderData();

                string[] aryHeaderFileNames = txtHeaderFileNames.Text.
                                                                Split(new string[] { "\r\n", ",", ";" }, StringSplitOptions.RemoveEmptyEntries)
                                                                .Select(filename => filename.Trim())
                                                                .Where(filename => File.Exists(filename))
                                                                .ToArray();

                foreach (string strHeaderFileName in aryHeaderFileNames)
                {
                    m_Parser.AppendHeaderFromFile(strHeaderFileName);
                }//next strHeaderFileName
                
                return true;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in LoadHeaderFiles function of frmCHeaderExtract form.");
                return false;
            }
        }

        /*OBSOLETE: Header Files will be loaded and parsed in the Parse button click event handler.
        /// <summary>
        /// Loads the data of all the header files set in the header file names textbox into the HeaderDeclareParser object in the form,
        /// which will then make the header data files available for parsing and extracting their type definition and structure data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadHeaderFiles_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datStart = DateTime.Now;

                if (txtHeaderFileNames.Text.Trim() == "")
                {
                    MessageBox.Show("No header files set to be loaded!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;                        
                }//end if

                m_Parser.ClearHeaderData();

                string[] aryHeaderFileNames = txtHeaderFileNames.Text.
                                                                Split(new string[] { "\r\n", ",", ";" }, StringSplitOptions.RemoveEmptyEntries)
                                                                .Select(filename => filename.Trim())                                                                 
                                                                .Where(filename => File.Exists(filename))
                                                                .ToArray();
                
                foreach (string strHeaderFileName in aryHeaderFileNames)
                {
                    m_Parser.AppendHeaderFromFile(strHeaderFileName);                   
                }//next strHeaderFileName

                btnParse.Enabled = true;

                TimeSpan tsElapsed = DateTime.Now.Subtract(datStart);

                MessageBox.Show("Header files loaded into CHeaderDeclareParser object!  The header files can now be parsed.", "Headers Loaded",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (chbShowHdrLoadTime.Checked)
                    MessageBox.Show("Header Data File Loading Time: " + tsElapsed.TotalSeconds.ToString() + " seconds");
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in btnLoadHeaderFiles_Click function of frmCHeaderExtract form.");
            }
        }
        */

        #endregion

        #region Header Parsing and Extraction Functions, Event Handlers

        /// <summary>
        /// When the Parse button is clicked, all header data files will be loaded into the HeaderDeclareParser object of the form and then 
        /// will be parsed and have the desired information extracted from the file into data objects.   The parser will extract all type definition, 
        /// enumerations, structure and union information from the file and load them into their associated data objects in the data tables of a 
        /// CHeaderDataSet data object.   Once all the data in the header data file (or set of files) is parsed, the extracted data can be 
        /// queried and displayed in the form or extracted to other data file formats.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParse_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datStartTime = DateTime.Now;

                if (!LoadHeaderFiles())
                    return;

                this.Cursor = Cursors.WaitCursor;
                
                m_Parser.CurrentPos = 0;

                m_HeaderAccess.TypeDefsTable.Rows.Clear();
                m_HeaderAccess.StructuresTable.Rows.Clear();
                m_HeaderAccess.FieldsTable.Rows.Clear();

                TypeExtractor.PointerSizeBytes = Convert.ToInt32(txtPointerSize.Text);
                TypeExtractor.EnumSizeBytes = Convert.ToInt32(txtEnumSize.Text);

                StructExtractor.PointerSizeBytes = Convert.ToInt32(txtPointerSize.Text);
                StructExtractor.EnumSizeBytes = Convert.ToInt32(txtEnumSize.Text);

                do
                {                    
                    if (m_Parser.IsTypeDef())
                    {
                        string strTypeDef = m_Parser.GetTypeDef();
                        TypeDefData tdData = m_typeExt.ExtractTypeDefData(strTypeDef);

                        if(tdData != null)
                            m_HeaderAccess.AddTypeDefRow(tdData);
                    }
                    else if (m_Parser.IsEnum())
                    {
                        string strEnum = m_Parser.GetEnum();
                        TypeDefData tdEnum = m_typeExt.ExtractEnumData(strEnum);

                        if(tdEnum != null)
                            m_HeaderAccess.AddTypeDefRow(tdEnum);
                    }
                    else if (m_Parser.IsStruct())
                    {
                        string strStruct = m_Parser.GetStructure();
                        StructData sdData = m_structExt.ExtractStructData(strStruct);

                        if (sdData != null)
                        {
                            m_HeaderAccess.AddStructRow(sdData);
                            m_HeaderAccess.AddFieldRows(sdData.Fields);
                        }//end if
                    }
                    else
                    {
                        m_Parser.MoveNextItem();
                    }//end if
                } while (!m_Parser.EndOfFile);

                TimeSpan tsElapsed = DateTime.Now.Subtract(datStartTime);

                //Once the parsing operation completes successfully, all the structures/unions extracted from the header files will be initially 
                //queried and loaded in the form.
                rbQryDisplayAll.Checked = true;
                btnQuery_Click(btnQuery, EventArgs.Empty);

                this.Cursor = Cursors.Default;
                MessageBox.Show("Header File Parsing Completed!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if(chbShowParseTime.Checked)
                    MessageBox.Show("Parsing Time: " + tsElapsed.TotalSeconds.ToString() + " seconds");
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                ErrorHandler.ShowErrorMessage(err, "Error in btnParse_Click function of frmCHeaderExtract form.");
            }
        }

        #endregion

        #region Header Query Tab Control Functions, Event Handlers

        /// <summary>
        /// Handles the event of when the check of one of the header query radio buttons in the query tab are changed.  The associated 
        /// panel of the query radio button will be enabled/disabled depending on whether it is selected/unselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbQry_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_CancelEvents.RadioButtonCheckedChanged)
                    return;

                if (!((RadioButton)sender).Checked)
                    return;

                pnlQryName.Enabled = false;
                pnlQryWildcard.Enabled = false;
                pnlQryRegex.Enabled = false;
                pnlQryDataSize.Enabled = false;

                if (sender == rbQryName)
                    pnlQryName.Enabled = true;
                else if (sender == rbQryWildcard)
                    pnlQryWildcard.Enabled = true;
                else if (sender == rbQryRegex)
                    pnlQryRegex.Enabled = true;
                else if (sender == rbQryDataSize)
                    pnlQryDataSize.Enabled = true;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in rbQry_CheckedChanged function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Handles the Key Press event of all the various structure query textboxes in the Query tab of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQry_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    //Pressing the Enter key in a query textbox in the query tab will perform the same function as clicking the Query button
                    //in the Query tab.
                    btnQuery.PerformClick();
                    e.Handled = true;
                }
                else
                {
                    if (sender == txtQryDataSizeMinSize || sender == txtQryDataSizeMaxSize)
                    {
                        if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back &&
                            e.KeyChar != (char)Keys.Left && e.KeyChar != (char)Keys.Right)
                            e.Handled = true;
                    }//end if
                }//end if
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in txtQry_KeyPress function of frmCHeaderExtract form.");
            }
        }
        
        #endregion

        #region Header Data Object Query Functions, Event Handlers      

        /// <summary>
        /// Queries all structures/unions extracted from the header data files parsed in the form, according to the selected structure/union 
        /// query that is set in the Query tab of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                if (rbQryDisplayAll.Checked)
                    aryStructRows = QueryAllStructs();
                else if (rbQryName.Checked)
                    aryStructRows = QueryStructsByName();
                else if (rbQryWildcard.Checked)
                    aryStructRows = QueryStructsByWildcard();
                else if (rbQryRegex.Checked)
                    aryStructRows = QueryStructsByRegex();
                else if (rbQryDataSize.Checked)
                    aryStructRows = QueryStructsBySize();

                DisplayQuery(aryStructRows);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in btnQuery_Click function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Queries all structures and/or unions extracted from the selected header data files and loads them into the Structures listbox in the form.
        /// When the Query all structures filter is selected, every structure and/or union that was extracted from the header data or set of data 
        /// files will be loaded into the form and available for viewing.  If sorting is to be used, the structures will be sorted by name, according 
        /// to the selected sort order selected.
        /// </summary>
        private CHeaderDataSet.tblStructuresRow[] QueryAllStructs()
        {
            try
            {                
                CHeaderDataSet.tblStructuresDataTable dtStruct = m_HeaderAccess.StructuresTable;
                
                IEnumerable<CHeaderDataSet.tblStructuresRow> query = null;

                if (rbSUStruct.Checked)
                    query = dtStruct.Where(r => r.StructUnion == 1);
                else if (rbSUUnion.Checked)
                    query = dtStruct.Where(r => r.StructUnion == 2);
                else
                    query = dtStruct.Select(r => r);

                if(rbSOAscending.Checked)
                    query = query.OrderBy(r => r.StructName);
                else if(rbSODescending.Checked)
                    query = query.OrderByDescending(r => r.StructName);

                return query.ToArray();                                              
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryAllStructs function of frmCHeaderExtract form.");
                return null;
            }
        }

        /// <summary>
        /// Queries all qualified structures and/or unions by the name set in the Name Query textbox in the Query tab of the form.  When 
        /// a structure name query is to be used, then all structures and/or unions with a name that matches the specified criteria will 
        /// be queried from the Structures DataTable which contains all extracted structures from the Header data file loaded in the form.
        /// The structures that will be queried will depend on the criteria set for the structure name query.  A name query can be set to 
        /// match the beginning of the structure name, any part of the structure name or to exactly match the structure.  The query can 
        /// also be made to be case-sensitive.   If sorting is to be used, the structures will be sorted by name, according to the selected 
        /// sort order selected.
        /// </summary>
        private CHeaderDataSet.tblStructuresRow[] QueryStructsByName()
        {
            try
            {                
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;                

                 aryStructRows =  m_HeaderAccess.QueryStructsByName(txtQryName.Text, chbQryNameMatchAny.Checked,
                                                                             chbQryNameExactMatch.Checked, chbQryNameMatchCase.Checked,
                                                                             GetSortOrderVal(), GetStructUnionVal());

                return aryStructRows;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsByName function of frmCHeaderExtract form.");
                return null;
            }
        }

        /// <summary>
        /// Queries all qualified structures and/or unions by the wild card search set in the Wildcard Query textbox in the Query tab of
        /// the form.  When a structure wildcard query is to be used, then all structures and/or unions with a name that matches the specified
        /// wildcard will be queried from the Structures DataTable which contains all extracted structures from the Header data file loaded 
        /// in the form.  The wildcard query will use the SQL like wildcard characters of '?' and '*', where '?' represents a single wildcard 
        /// character and '*' matches any number of wildcard characters.  The structures/unions that will be queried will depend on the criteria 
        /// set for the wildcard query.  This query can also be made to be case-sensitive.   If sorting is to be used, the structures will be 
        /// sorted by name, according to the selected sort order selected.
        /// </summary>
        private CHeaderDataSet.tblStructuresRow[] QueryStructsByWildcard()
        {
            try
            {                
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                aryStructRows = m_HeaderAccess.QueryStructsByWildcard(txtQryWildcard.Text, chbQryWildcardMatchCase.Checked,
                                                                                                      GetSortOrderVal(), GetStructUnionVal());

                return aryStructRows;                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsByWildcard function of frmCHeaderExtract form.");
                return null;
            }
        }

        /// <summary>
        /// Queries all qualified structures and/or unions by the regular expression set in the Regular Expression Query textbox in the
        /// Query tab of the form.  When a structure regular expression query is to be used, then all structures and/or unions with a name
        /// that matches the specified regular expression will be queried from the Structures DataTable which contains all extracted 
        /// structures from the Header data file loaded in the form.  The regular expression query will use a qualified regular expression to 
        /// perform a query on the structures/unions.  The structures/unions that will be queried will depend on the criteria 
        /// set for the wildcard query.  This query can be case-sensitive, depending on the regular expression criteria.  If sorting is to be 
        /// used, the structures will be sorted by name, according to the selected sort order selected.
        /// </summary>
        private CHeaderDataSet.tblStructuresRow[] QueryStructsByRegex()
        {
            try
            {                
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                aryStructRows = m_HeaderAccess.QueryStructsByRegex(txtQryRegEx.Text, GetSortOrderVal(), GetStructUnionVal());                

                return aryStructRows;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsByWildcard function of frmCHeaderExtract form.");
                return null;
            }
        }

        /// <summary>
        /// Queries all structures that are within a range of data sizes (in bytes) specified in the minimum and maximum data size query 
        /// controls in the Query tab of the form.   When a data size query is to be used, all structures and/or unions that are within the 
        /// specified data size range will be queried from the Structures Dataable which contains all extracted dstructures from the Header 
        /// data file loaded in the form.  If sorting is to be used, the structures will be sorted by their data size, according to the selected
        /// sort order selected.
        /// </summary>
        private CHeaderDataSet.tblStructuresRow[] QueryStructsBySize()
        {
            try
            {
                if(txtQryDataSizeMinSize.Text.Trim() == "")
                {
                    MessageBox.Show("A minimum data size is required to perform this query.", "Minimum Data Size Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQryDataSizeMinSize.Focus();
                    return null;
                }//end if

                if (txtQryDataSizeMaxSize.Text.Trim() == "")
                {
                    MessageBox.Show("A maximum data size is required to perform this query.", "Maximum Data Size Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQryDataSizeMaxSize.Focus();
                    return null;
                }//end if

                if (Convert.ToInt32(txtQryDataSizeMinSize.Text) > Convert.ToInt32(txtQryDataSizeMaxSize.Text))
                {
                    MessageBox.Show("The minimum data size cannot be larger than the maximum data size.", "Invalid Query Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQryDataSizeMinSize.Focus();
                    return null;
                }//end if
                
                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                aryStructRows = m_HeaderAccess.QueryStructsBySize(
                                                          Convert.ToInt32(txtQryDataSizeMinSize.Text), Convert.ToInt32(txtQryDataSizeMaxSize.Text), 
                                                          GetSortOrderVal(), GetStructUnionVal());

                return aryStructRows;                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in QueryStructsBySize function of frmCHeaderExtract form.");
                return null;
            }
        }

        #endregion

        #region Header DisplayQuery Tab Control/UI Functions, Event Handlers

        /// <summary>
        /// If a structure query is performed that is to have its results displayed in the CHeaderExtract form UI, then the DisplayQuery 
        /// function will take the qualified structures from the query and load all queried them into the appropriate controls of the form.        
        /// </summary>
        /// <param name="aryStructRows"></param>
        private void DisplayQuery(CHeaderDataSet.tblStructuresRow[] aryStructRows)
        {
            try
            {
                lbStructs.Items.Clear();
                ClearStructInfo();

                m_CancelEvents.ListBoxIndexChanged = true;

                foreach (CHeaderDataSet.tblStructuresRow rowStruct in aryStructRows)
                {
                    lbStructs.Items.Add(rowStruct.StructName);
                }//next rowStruct

                m_CancelEvents.ListBoxIndexChanged = false;

                if(lbStructs.Items.Count > 0)
                    lbStructs.SelectedIndex = 0;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in DisplayQuery function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Retrieves the numerical sort order enumeration value, based on which sort order radio button is selected in the query tab 
        /// of the form.
        /// </summary>
        /// <returns></returns>
        private SortOrderEnum GetSortOrderVal()
        {
            try
            {
                if (rbSOAscending.Checked)
                    return SortOrderEnum.Ascending;
                else if (rbSODescending.Checked)
                    return SortOrderEnum.Descending;
                else
                    return SortOrderEnum.None;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetSortOrderVal function of frmCHeaderExtract form.");
                return SortOrderEnum.None;
            }
        }

        /// <summary>
        /// Retrieves the numerical StructUnion enumeration value, based on which StructUnion radio button is selected in the query tab 
        /// of the form.
        /// </summary>
        /// <returns></returns>
        private StructUnionEnum GetStructUnionVal()
        {
            try
            {
                if (rbSUStruct.Checked)
                    return StructUnionEnum.Structure;
                else if (rbSUUnion.Checked)
                    return StructUnionEnum.Union;
                else
                    return StructUnionEnum.Both;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetStructUnionVal function of frmCHeaderExtract form.");
                return StructUnionEnum.Both;
            }
        }

        #endregion

        #region Header Data Object Export Functions, Event Handlers   

        /// <summary>
        /// Handles the event of when one of the data export radio buttons is changed in the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbExport_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbExportLocalData.Checked)
                {
                    pnlExportServerFile.Enabled = false;
                }
                else
                {
                    pnlExportServerFile.Enabled = true;
                }//end if
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in rbExport_CheckedChanged function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Exports the data associated with all structures/unions extracted from the header data files parsed in the form, according to the
        /// selected structure/union  query that is set in the Query tab of the form.  All qualified structures/unions in the query will be 
        /// exported into JSON formatted file so it can be imported into other applications and web pages, including the CHeaderParser.Web 
        /// web application.  The structure/union JSON formatted export file will be exported to the path and file specified in the export
        /// file name textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbExportServerData.Checked)
                {
                    if (txtExportServerFileName.Text == "")
                    {
                        MessageBox.Show("A file name is required before proceeding with exporting the query.", "File Name Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtExportServerFileName.Focus();
                        return;
                    }
                    else if (!txtExportServerFileName.Text.ToUpper().EndsWith(".JSON"))
                    {
                        txtExportServerFileName.Text += ".JSON";
                    }//end if
                }//end if

                this.Cursor = Cursors.WaitCursor;

                CHeaderDataSet.tblStructuresRow[] aryStructRows = null;

                if (rbQryDisplayAll.Checked)
                    aryStructRows = QueryAllStructs();
                else if (rbQryName.Checked)
                    aryStructRows = QueryStructsByName();
                else if (rbQryWildcard.Checked)
                    aryStructRows = QueryStructsByWildcard();
                else if (rbQryRegex.Checked)
                    aryStructRows = QueryStructsByRegex();
                else if (rbQryDataSize.Checked)
                    aryStructRows = QueryStructsBySize();

                DataExport export = new DataExport(m_HeaderAccess);

                bool blRtnVal = false;

                if(rbExportLocalData.Checked)
                    blRtnVal = export.ExportStructs(aryStructRows, General.ApplicationPath + "//ExportStructData.js", WebExportTypeEnum.Local);
                else
                    blRtnVal = export.ExportStructs(aryStructRows, txtExportServerFileName.Text, WebExportTypeEnum.Server);

                this.Cursor = Cursors.Default;

                if (blRtnVal)
                    MessageBox.Show("Structure/Union query successfully exported!", "Data Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to export Structure/Union query.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                ErrorHandler.ShowErrorMessage(err, "Error in btnExport_Click function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Displays a file picker dialog to allow the user to specifiy the full path and name to use to save the export file.  All file extensions 
        /// must end with JSON.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportServerFileNameSearch_Click(object sender, EventArgs e)
        {
            try
            {                
                SaveFileDialog dlgFile = new SaveFileDialog();
                dlgFile.Filter = "Javascript Object Notation (JSON) Files (*.JSON)|*.JSON|All Files (*.*)|*.*";
                dlgFile.FilterIndex = 0;
                dlgFile.DefaultExt = "JSON";
                dlgFile.AddExtension = true;
                dlgFile.Title = "Save Structure Data Export File As";

                if (dlgFile.ShowDialog() == DialogResult.OK)
                {                    
                    txtExportServerFileName.Text = dlgFile.FileName;

                    if (!txtExportServerFileName.Text.ToUpper().EndsWith(".JSON"))
                        txtExportServerFileName.Text += ".JSON";
                }//end if
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in btnExportServerFileNameSearch_Click function of frmCHeaderExtract form.");
            }
        }

        #endregion

        #region Structures and Structure Fields Display Control Functions, Event Handlers

        private void lbStructs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_CancelEvents.ListBoxIndexChanged)
                    return;

                if (lbStructs.SelectedIndex != -1)
                {
                    //Loads all the information of the fields contained in the selected structure into the fields grid control.                    
                    CHeaderDataSet.tblFieldsViewDataTable tblFieldsView = dsHeaderDataView.tblFieldsView;
                    tblFieldsView.Rows.Clear();                    
                    
                    CHeaderDataSet.tblFieldsRow[] aryStructFields =
                                        m_HeaderAccess.GetStructFields(lbStructs.Items[lbStructs.SelectedIndex].ToString());
                    
                    foreach (CHeaderDataSet.tblFieldsRow rowField in aryStructFields)
                    {
                        CHeaderDataSet.tblFieldsViewRow rowFieldView = tblFieldsView.NewtblFieldsViewRow();

                        for (int i = 0; i < tblFieldsView.Columns.Count; i++)
                        {
                            if (m_HeaderAccess.FieldsTable.Columns.Contains(tblFieldsView.Columns[i].ColumnName))
                            {
                                rowFieldView[i] = rowField[tblFieldsView.Columns[i].ColumnName];
                            }
                            else if (tblFieldsView.Columns[i].ColumnName == "DataType")
                            {
                                switch ((FieldTypeEnum)rowField.FieldType)
                                {
                                    case FieldTypeEnum.Enum:
                                        rowFieldView[i] = "enum";
                                        break;
                                    case FieldTypeEnum.TypeDef:
                                        rowFieldView[i] = "typedef";
                                        break;
                                    case FieldTypeEnum.Structure:
                                        CHeaderDataSet.tblStructuresRow rowStruct = m_HeaderAccess.GetStruct(rowFieldView.FieldTypeName);

                                        if (rowStruct != null)
                                        {
                                            if (m_HeaderAccess.GetStruct((rowFieldView.FieldTypeName)).StructUnion == 1)
                                                rowFieldView[i] = "struct";
                                            else
                                                rowFieldView[i] = "union";
                                        }
                                        else
                                            rowFieldView[i] = "struct/union";

                                        break;
                                    case FieldTypeEnum.Primitive:
                                        rowFieldView[i] = "primitive";
                                        break;
                                    case FieldTypeEnum.Pointer:
                                        rowFieldView[i] = "pointer";
                                        break;
                                };//end switch    
                            }//end if                           
                        }//next i

                        tblFieldsView.AddtblFieldsViewRow(rowFieldView);                                                                          
                    }//next rowField     

                    //Loads all controls in the form that display information specific to the selected structure in the Structures 
                    //listbox.                       
                    LoadStructInfo();
                }//end if                                    
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in lbStructs_SelectedIndexChanged function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Loads all controls in the form that display information specific to the selected structure in the Structures 
        /// listbox.        
        /// </summary>
        private void LoadStructInfo()
        {
            try
            {
                CHeaderDataSet.tblStructuresRow rowStruct = m_HeaderAccess.GetStruct(lbStructs.Items[lbStructs.SelectedIndex].ToString());
                lblStructName.Text = rowStruct.StructName;                
                lblStructSize.Text = rowStruct.DataSize.ToString();
                lblStructFieldCount.Text = rowStruct.FieldCount.ToString();
                lblStructElements.Text = rowStruct.Elements.ToString();

                if (rowStruct.StructUnion == 1)
                {
                    lblStructNameHdr.Text = "Structure Name";
                    lblStructSizeHdr.Text = "Structure Size (Bytes)";
                }
                else
                {
                    lblStructNameHdr.Text = "Union Name";
                    lblStructSizeHdr.Text = "Union Size (Bytes)";
                }//end if
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in LoadStructInfo function of frmCHeaderExtract form.");
            }
        }

        /// <summary>
        /// Clears all controls in the form that display information specific to the selected structure in the Structures 
        /// listbox when no structure or union is selected in the form.
        /// </summary>
        private void ClearStructInfo()
        {
            try
            {
                dsHeaderDataView.tblFields.Rows.Clear();

                lblStructName.Text = "";
                lblStructSize.Text = "";
                lblStructFieldCount.Text = "";
                lblStructElements.Text = "";                
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ClearStructInfo function of frmCHeaderExtract form.");
            }
        }





        #endregion

        #region General Functions, Event Handlers

        #endregion

        #region Test Functions, Event Handlers      
        #endregion

        
    }
}
