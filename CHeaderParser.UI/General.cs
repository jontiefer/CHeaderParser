/* Copyright (C) 2016 Jonathan Tiefer - All Rights Reserved
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

//#define _TESTING
//#define _MOUNTTEST

using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Threading;
using Microsoft.Win32;
using System.Linq;
using System.Text;
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
using CHeaderParser.Data;

namespace CHeaderParser
{
    #region Global Enumerations

    #endregion

    #region Event Management Classses

    public class CancelEvents
    {
        public bool ControlEnter = false;
        public bool ControlLeave = false;
        public bool ControlLocationChanged = false;
        public bool ButtonClick = false;
        public bool ButtonDoubleClick = false;
        public bool LabelClick = false;
        public bool LabelMouseDown = false;
        public bool LabelMouseMove = false;
        public bool LabelMouseUp = false;
        public bool ComboBoxValueChanged = false;
        public bool ComboBoxRowSelected = false;
        public bool ListBoxIndexChanged = false;
        public bool RadioButtonGroupValueChanged = false;
        public bool RadioButtonCheckedChanged = false;
        public bool CheckBoxCheckStateChanged = false;
        public bool CheckBoxClicked = false;
        public bool PerioDataEntryCellValueChanged = false;
        public bool TextBoxValidate = false;
        public bool TextBoxValueChanged = false;
        public bool NumBoxValidate = false;
        public bool NumBoxValueChanged = false;
        public bool NumBoxEditorSpinBtnClick = false;
        public bool DateTimePickerValueChanged = false;
        public bool DateTimePickerValidate = false;
        public bool GridCellChange = false;
        public bool GridSelChange = false;
        public bool GridMouseDown = false;
        public bool GridMouseUp = false;
        public bool GridBeforeCellActivate = false;
        public bool GridAfterCellActivate = false;
        public bool GridBeforeRowActivate = false;
        public bool GridAfterRowActivate = false;
        public bool GridClick = false;
        public bool GridKeyPress = false;
        public bool GridBeforeRowRegionScroll = false;
        public bool GridAfterRowRegionScroll = false;
        public bool TreeListAfterSelect = false;
        public bool VisibleChanged = false;
        public bool FormResize = false;
        public bool FormClosing = false;
        public bool UserControlResize = false;
        public bool ControlValueChanged = false;
        public bool ControlValidate = false;
        public bool ToolbarManagerToolClick = false;
        public bool ToolbarManagerToolValueChanged = false;
        public bool ToolbarManagerAfterToolExitEditMode = false;
        public bool ScrollbarScroll = false;
        public bool ScrollbarValueChanged = false;
        public bool NotifySelect = false;
        public bool ListSelectItemSelected = false;
        public bool ListSelectOkClicked = false;
        public bool ListSelectPopupClosed = false;
        public bool ListviewSelectionChanging = false;
        public bool ListviewSelectionChanged = false;
        public bool TrackbarValueChanged = false;
        public bool GdViewerPageDisplayed = false;

        //TODO: Add to this class as needed.
    }

    #endregion

    #region Error Handling Classes/Functions

    public static class ErrorHandler
    {
        /// <summary>
        /// Displays Error Message to User or Debugger, depending on whether it is called in debug or run-time mode.
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strMessage"></param>
        /// <param name="e"></param>
        /// <param name="blHandleInvalidNetwork"></param>
        /// <param name="blWriteLog"></param>
        /// <param name="blHideMsg"></param>
        public static void ShowErrorMessage(Exception e, string strMessage = "", string strTitle = "", bool blHideMsg = false)
        {
            if (e != null)
            {
                strMessage += "\r\n" +
                    "Error Message: " + e.Message +
                    "\r\n\r\nSource: " + e.Source +
                    "\r\n\r\nStack Trace: " + e.StackTrace +
                    "\r\n\r\nException Type: " + e.GetType().ToString();

                if (e.TargetSite != null)
                    strMessage += "\r\n\r\nTarget Site: " + e.TargetSite.Name;
            }

            if (strTitle == "")
                strTitle = "Error";

#if DEBUG
            Debug.WriteLine("-------------------------------------------------------------------------------------------------");
            Debug.WriteLine(strMessage);
            Debug.WriteLine("-----------------------------------------------------------------------------------------------");


            Debugger.Break();
#else
            if (!blHideMsg)
                MessageBox.Show(strMessage, strTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
        }
    }

    #endregion

    #region Object Comparer Classes

    /// <summary>
    /// This class will compare two objects of the sample type with the use of a delegate method supplied to the constructor of the comparer
    /// class.  Any type of simple comparisons between two objects can be easily performed with this class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> m_Comparer = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="funcComparer">Function containing lambda expression used to compare the equality of two similiar objects.</param>
        public ObjectComparer(Func<T, T, bool> funcComparer)
        {
            m_Comparer = funcComparer;
        }

        public bool Equals(T x, T y)
        {
            return m_Comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }

    #endregion

    #region Global Static Class

    public static class Global
    {
        #region Member Variables

#if DEBUG
        /// <summary>
        /// The path to the executable or assembly file of the application that is currently running.
        /// NOTE: This variable is only used in Debug mode and will be hardcoded to the StartupPath of the executing applicaiton 
        /// during run-time.
        /// </summary>
        private static string m_strAppPath = "";
#endif

        #endregion

        #region Member Object Variables

        #endregion

        #region Member Data Object Variables

        private static PrimitiveDataTypes m_PrimDataTypes = new PrimitiveDataTypes();

        #endregion

        #region Static Constructor

        static Global()
        {
            Stream stream = null;

            try
            {
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in Constructor function of Global class.");
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        #endregion       

        #region Global Path Properties       

        /// <summary>
        /// Path to the executable of the program.  This will return the correct path, regardless of the program is in debug or run-time
        /// mode.
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
#if DEBUG               
                return m_strAppPath;
#else
                return Application.StartupPath;
#endif
            }
#if DEBUG
            set
            {
                m_strAppPath = value;
            }
#endif
        }

        /// <summary>
        /// Path to where temporary files in the program are contained.
        /// </summary>
        public static string TempFilesPath
        {
            get
            {
                return Global.ApplicationPath + @"\TempFiles";
            }
        }

        #endregion                 

        #region Data-Object Properties, Functions

        /* NOT USED: With separate dll libraries, circular dependencies must be avoided.
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
        */

        #endregion

        #region Memory/Stream Related Properties, Functions

        /// <summary>
        /// Serializes the ISerializable object passed to the function into a buffer.
        /// Returns: Success = Serialized Buffer, Failure = Null.
        /// </summary>
        /// <param name="data">The object containing the data to serialize.</param>
        /// <param name="blEncryptData">Indicates if the buffer will be encrypted after it is serialized with the object data.</param>
        /// <returns></returns>
        public static byte[] SerializeObject(object data/*, bool blEncryptData = false*/)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();

                byte[] bufData = null;

                bf.Serialize(ms, data);
                bufData = ms.GetBuffer();

                /*NO ENCRYPTION
                if (blEncryptData)
                {                    
                    byte[] bufDataEnc = DDSCrypt.EncryptBuffer(bufData);

                    if (bufDataEnc == null)
                        throw new Exception("Error attempting to encrypt data buffer.");

                    bufData = bufDataEnc;
                }//end if
                */

                return bufData;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in SerializeObject function of Global class.");
                return null;
            }
        }

        /// <summary>
        /// Deserializes a serialized buffer into an object.
        /// Returns: Success = Deserialized object, False = Null.
        /// </summary>
        /// <param name="bufData">Serialized data to deserialize.</param>
        /// <param name="blDecryptData">Indicates if the data contained in the buffer must be decrypted before beiong deserialized.</param>
        /// <returns></returns>
        public static object DeserializeObject(byte[] bufData/*, bool blDecryptData = false*/)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = null;

            try
            {
                /*NO ENCRYPTION
                if (blDecryptData)
                {
                    byte[] bufDecryptData = DDSCrypt.DecryptBuffer(bufData);
                    bufData = bufDecryptData;
                }//end if
                */

                ms = new MemoryStream(bufData);

                object data = bf.Deserialize(ms);

                return data;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in DeserializeObject Overload 1 function of Global class.");
                return null;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
        }

        /// <summary>
        /// Deserializes a serialized buffer into a strong-typed, ISerializable object.
        /// Returns: Success = Deserialized object, False = Default Value of the Object Type, which should be null or blank.
        /// </summary>
        /// <typeparam name="ObjType">The type of object to deserialize.</typeparam>
        /// <param name="bufData">Serialized data to deserialize.</param>
        /// <returns></returns>
        public static ObjType DeserializeObject<ObjType>(byte[] bufData)
        {
            try
            {
                ObjType data = (ObjType)DeserializeObject(bufData);

                return data;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in DeserializeObject Overload 2 function of Global class.");
                return default(ObjType);
            }
        }

        #endregion

        #region File Related Properties, Functions

        /// <summary>
        /// Saves a binary buffer to a file on disk with the full path and name specified in the funciton's FileName parameter.  Any existing file with the same name
        /// will be overwritten.
        /// </summary>
        /// <param name="strFileName">The full name and path of the file containing the buffer to write to disk.</param>
        /// <param name="bufData">The buffer that will be saved to a file on disk.</param>
        /// <param name="iOffset">The zero-based byte offset in array at which to begin copying bytes to the current stream.</param>
        /// <param name="iCount">The number of bytes to be written to the current stream.</param>
        /// <param name="blEncryptData">Indicates if the buffer will be encrypted after it is serialized with the object data.</param>
        public static void SaveBufferToFile(string strFileName, byte[] bufData, int iOffset, int iCount/*, bool blEncryptData = false*/)
        {
            FileStream fstream = null;

            try
            {
                /*NO ENCRYPTION
                if (blEncryptData)
                {
                    byte[] bufDataEnc = DDSCrypt.EncryptBuffer(bufData);

                    if (bufDataEnc == null)
                        throw new Exception("Error attempting to encrypt data buffer.");

                    bufData = bufDataEnc;
                }//end if
                */

                fstream = File.Create(strFileName);
                fstream.Write(bufData, iOffset, iCount);
                fstream.Close();
                fstream = null;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in SaveBufferToFile Overload 1 function of Global class.");
            }
            finally
            {
                if (fstream != null)
                    fstream.Close();
            }
        }

        /// <summary>
        /// Saves a binary buffer to a file on disk with the full path and name specified in the funciton's FileName parameter.  Any existing file with the same name
        /// will be overwritten.
        /// </summary>
        /// <param name="strFileName">The full name and path of the file containing the buffer to write to disk.</param>
        /// <param name="bufData">The buffer that will be saved to a file on disk.</param>
        /// <param name="blEncryptData">Indicates if the buffer will be encrypted after it is serialized with the object data.</param>
        public static void SaveBufferToFile(string strFileName, byte[] bufData/*, bool blEncryptData = false*/)
        {
            try
            {
                SaveBufferToFile(strFileName, bufData, 0, bufData.Length/*, blEncryptData*/);
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in SaveBufferToFile Overload 2 function of Global class.");
            }
        }

        /// <summary>
        /// Generates a random file name for a new document that is created in the program and being saved to disk for the first time.  Each temporary file in the program
        /// will contain a file name that starts with the letter "T" and then is followed by a hexadecimal formatted guid that is converted to decimal format, which is then
        /// concatenated with the minute and seconds of the current time of the computer.  This pattern of naming temporary files will guarantee they are unique.  
        /// </summary>
        /// <param name="strFileExt">The file extension to add to the end of temporary file name generated.  This will default to a TMP extension.</param>
        /// <param name="iGuidLen">The length of the Guid value that is generated and will be used as part of the name of the temporary file.</param>
        /// <returns></returns>
        public static string GenerateTempFileName(string strFileExt = "TMP", int iGuidLen = 8)
        {
            try
            {
                string strGuidHex = Guid.NewGuid().ToString().Substring(0, iGuidLen).Replace("-", "");
                long lGuidDecVal = Math.Abs(Int64.Parse(strGuidHex, System.Globalization.NumberStyles.HexNumber));

                string strFileName = "";

                if (lGuidDecVal.ToString().Length >= (iGuidLen - 1))
                    strFileName = "T" + lGuidDecVal.ToString().Substring(0, (iGuidLen - 1)) + DateTime.Now.ToString("mmss") + "." + strFileExt;
                else
                    strFileName = "T" + lGuidDecVal.ToString().Substring(0, lGuidDecVal.ToString().Length) + DateTime.Now.ToString("mmss") + "." + strFileExt;

                return strFileName;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GenerateTempFileName function of Global class.");
                return "";
            }
        }

        /// <summary>
        /// Creates a temporary file with the binary data passed to the function's FileData parameter.  A random temporary file name will be generated in this function 
        /// using the file extension and Guid length passed to the function.  If the temporary file is successfully created, the full name and path of the temporary file 
        /// will be returned by the function.  In the case it fails to be created, a blank string is returned.
        /// </summary>
        /// <param name="bufFileData">Buffer containing all the data to store in the temporary file.</param>
        /// <param name="strFileExt">The file extension to add to the end of temporary file name generated.  This will default to a TMP extension.</param>
        /// <param name="iGuidLen">The length of the Guid value that is generated and will be used as part of the name of the temporary file.</param>
        /// <param name="strTempFilePath">The path where temporary file will be created. If this parameter is left blank, it will default to the TempFilesPath used in the 
        /// program.</param>
        /// <returns></returns>
        public static string CreateTempFile(byte[] bufFileData, string strFileExt = "TMP", int iGuidLen = 8, string strTempFilePath = "")
        {
            FileStream fs = null;

            try
            {
                string strTempFileName = "";

                if (strTempFilePath == "")
                    strTempFilePath = Global.TempFilesPath;

                //Performs a loop to generate the unique temporary file name that will prevent multiple threads from created a duplicate temporary
                //file.  A check will be made to see if the current temporary file exists on disk and if not it the name will be guaranteed as 
                //unique.
                do
                {
                    strTempFileName = strTempFilePath + "\\" + GenerateTempFileName(strFileExt, iGuidLen);
                } while (File.Exists(strTempFileName));

                fs = File.Create(strTempFileName);
                fs.Write(bufFileData, 0, bufFileData.Length);
                fs.Close();
                fs = null;

                return strTempFileName;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CreateTempFile Overload 1 function of Global class.");
                return "";
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// Creates a temporary file that contains the same data as the file specified in the FileName parameter of the function.  A random temporary file name will be 
        /// generated in this function using the file extension associated with the specified file and Guid length passed to the function.  If the temporary file is 
        /// successfully created, the full name and path of the temporary file will be returned by the function.  In the case it fails to be created, a blank string is 
        /// returned.
        /// </summary>
        /// <param name="strFileName">The full name and path of the file to have its contents copied into a temporary file.</param>        
        /// <param name="iGuidLen">The length of the Guid value that is generated and will be used as part of the name of the temporary file.</param>
        /// <param name="strTempFilePath">The path where temporary file will be created. If this parameter is left blank, it will default to the TempFilesPath used in the 
        /// program.</param>
        /// <returns></returns>
        public static string CreateTempFile(string strFileName, int iGuidLen = 8, string strTempFilePath = "")
        {
            try
            {
                FileInfo fi = new FileInfo(strFileName);
                string strTempFileName = "";

                if (strTempFilePath == "")
                    strTempFilePath = Global.TempFilesPath;

                do
                {
                    strTempFileName = strTempFilePath + "\\" + GenerateTempFileName(fi.Extension.Replace(".", ""), iGuidLen);
                } while (File.Exists(strTempFileName));

                File.Copy(strFileName, strTempFileName);

                return strTempFileName;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CreateTempFile Overload 2 function of Global class.");
                return "";
            }
        }

        /// <summary>
        /// Generates a random file name for a file that is created in the program.  The random file will have a name generated that can be concatenated with a prefix name.
        /// The geneated file name can either be concatenated with a random generated date, a guid or a combination of both.  A default extension of TMP will be used, if 
        /// an extension is not provided in the FilExt parameter of the function.  If a guid is used, then it will be necessary to choose the number of digits of the guid 
        /// to display in the file name, in the GuidLength parameter.
        /// </summary>
        /// <param name="bType">Indicates the method used to generated the file name.  1 = Guid, 2 = Current Date/Time String, 3 = Combination of Both.</param>
        /// <param name="strPrefix">The text to display before and concatenate to the generated file name.</param>
        /// <param name="strFileExt">The extension of the generated file name.</param>
        /// <param name="iGuidLen">Number of digits to display for the guid generated in the file name.</param>
        /// <returns></returns>
        public static string GenerateFileName(byte bType, string strPrefix = "", string strFileExt = "TMP", int iGuidLen = 7)
        {
            try
            {
                string strFileName = strPrefix;
                string strGuid = "";
                string strDate = "";

                if (bType == 1 || bType == 3)
                {
                    string strGuidHex = Guid.NewGuid().ToString().Substring(0, iGuidLen).Replace("-", "");
                    long lGuidDecVal = Math.Abs(Int64.Parse(strGuidHex, System.Globalization.NumberStyles.HexNumber));

                    if (lGuidDecVal.ToString().Length >= iGuidLen)
                        strGuid = lGuidDecVal.ToString().Substring(0, iGuidLen);
                    else
                        strGuid = lGuidDecVal.ToString();
                }//end if

                if (bType == 2 || bType == 3)
                    strDate = DateTime.Now.ToString("yyyyMMddHHmmss");

                strFileName += strGuid + strDate + "." + strFileExt;

                return strFileName;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GenerateFileName function of Global class.");
                return "";
            }
        }

        /// <summary>
        /// Calculates the total size in bytes of all files in the directory, including all the files contained in subdirectories, and returns the total.
        /// </summary>
        /// <param name="strDirectory">The full path and name of the directory.</param>
        /// <param name="strSearchPattern">Calculate files only that match the specified search pattern.</param>
        /// <param name="blSupressErrMsg">Indicates if an error message will be displayed if an exception is thrown in the function.</param>
        /// <returns></returns>
        public static long GetDirSize(string strDirectory, string strSearchPattern = "", bool blSupressErrMsg = false)
        {
            try
            {
                long lDirSize = 0;

                DirectoryInfo di = new DirectoryInfo(strDirectory);

                FileInfo[] aryFiles = di.GetFiles(strSearchPattern);
                lDirSize += aryFiles.Sum(f => f.Length);

                foreach (DirectoryInfo diSubDir in di.GetDirectories())
                    lDirSize += GetDirSize(diSubDir.FullName, strSearchPattern);

                return lDirSize;
            }
            catch (Exception err)
            {
                if (!blSupressErrMsg)
                    ErrorHandler.ShowErrorMessage(err, "Error in GetDirSize function of Global class.");

                return 0;
            }
        }


        #endregion

        #region Operating System Properties/Functions

        /// <summary>
        /// Indicates whether the computer is running under a x86 (32-bit) operating system or an x64 (64-bit)
        /// operating system.
        /// Returns a numeric value represents the bits of the operating system that is being runned.
        /// </summary>
        public static int OSBits
        {
            get
            {
                if (IntPtr.Size == 4)
                    return 32;
                else if (IntPtr.Size == 8)
                    return 64;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the path to the 32-bit Program Files directory on the computer which the Diamond Dental Software program
        /// is contained.  On 32-bit machines, this would be Program Files and on 64-bit machines, this will be Program Files (x86).
        /// </summary>
        public static string ProgramFiles32Path
        {
            get
            {
                if (OSBits == 32)
                    return Environment.GetEnvironmentVariable("ProgramFiles");
                else if (OSBits == 64)
                    return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets the path to the 32-bit Common Program Files directory on the computer where certain shared data files between the Diamomd Dental and other
        /// programs are contained.  On 32-bit machines, this would be Program Files\Common Program Files and on 64-bit machines, this will be 
        /// Program Files (x86)\Common Program Files.
        /// </summary>
        public static string CommonProgramFiles32Path
        {
            get
            {
                if (OSBits == 32)
                    return Environment.GetEnvironmentVariable("CommonProgramFiles");
                else if (OSBits == 64)
                    return Environment.GetEnvironmentVariable("CommonProgramFiles(x86)");
                else
                    return "";
            }
        }

        #endregion

        #region Global Information Functions

        /// <summary>
        /// Retrieves the current version number of the EMS program that is stored in the executing assembly.  The version will be returned as a string in the format of 
        /// x.x.x.x.
        /// </summary>
        /// <returns></returns>
        public static string GetAppVersion()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                return fvi.FileVersion;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetAppVersion function of Global class.");
                return "";
            }
        }

        /// <summary>
        /// Gets the Mac Address of the network card of the current computer on the network running the Paperless program.
        /// </summary>
        /// <param name="blFormatted">Indicates if the Mac Address will be formatted in the standard two-digit hexadecimal format separated by dashes.</param>
        public static string MacAddress(bool blFormatted = false)
        {
            try
            {
                PhysicalAddress MacAddr =
                    (
                        from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress()
                    ).FirstOrDefault();

                string strMacAddr = "";

                if (blFormatted)
                {
                    byte[] bufMacAddr = MacAddr.GetAddressBytes();

                    for (int i = 0; i < bufMacAddr.Length; i++)
                        strMacAddr += bufMacAddr[i].ToString("X2").ToUpper() + "-";

                    strMacAddr = strMacAddr.Remove(strMacAddr.Length - 1);
                }
                else
                {
                    strMacAddr = MacAddr.ToString();
                }//end if

                return strMacAddr;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in MacAddress function of Global class.");
                return "";
            }
        }

        #endregion

        #region Global Setting Properties

        #endregion

        #region Global Color-Related Functions

        public static int ToRGBColor(int Argb)
        {
            string strHexRgb = "";

            string strHexArgb = String.Format("{0:X}", Argb);

            if (strHexArgb.Length == 1 || strHexArgb.Length == 3 || strHexArgb.Length == 5)
                strHexArgb = "0" + strHexArgb;

            if (strHexArgb.Length >= 2)
                strHexRgb = strHexArgb.Substring(strHexArgb.Length - 2, 2);

            if (strHexArgb.Length >= 4)
                strHexRgb += strHexArgb.Substring(strHexArgb.Length - 4, 2);

            if (strHexArgb.Length >= 6)
                strHexRgb += strHexArgb.Substring(strHexArgb.Length - 6, 2);

            int iRGB = Convert.ToInt32(strHexRgb, 16);

            return iRGB;
        }

        public static int ToRGBColor(Color color)
        {
            return ToRGBColor(color.ToArgb());
        }

        /// <summary>
        /// Converts a .NET Color to an RGB Hex string usuable by HTML document.
        /// </summary>
        /// <param name="color">Color to convert.</param>
        /// <returns></returns>
        public static string ToHexRGBColor(Color color)
        {
            try
            {
                string strHexColor = "";

                int iRed = color.R;
                int iGreen = color.G;
                int iBlue = color.B;

                strHexColor = "#" + iRed.ToString("X2") + iGreen.ToString("X2") + iBlue.ToString("X2");

                return strHexColor;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in ToHexColor function of Global class.");
                return "";
            }
        }

        #endregion

        #region Global String-Related Functions

        public static bool IsNumeric(string strVal)
        {
            if (strVal.Trim() == "")
                return false;

            //bool blIsNumeric = true;
            char[] chrAry = strVal.ToCharArray();
            bool blHasDecimal = false;
            bool blHasComma = false;

            if ((char.IsNumber(chrAry[0]) && (chrAry[0] < (char)188 || chrAry[0] > (char)190)) || (chrAry[0] == 36 && chrAry.GetLength(0) > 1) || (chrAry[0] == 45 && chrAry.GetLength(0) > 1))
            {
                for (int i = 1; i <= chrAry.GetUpperBound(0); i++)
                {
                    if (!char.IsNumber(chrAry[i]))
                    {
                        if (!blHasComma && !blHasDecimal && chrAry[i] == 44)
                        {
                            blHasComma = true;
                        }
                        else if (!blHasDecimal && chrAry[i] == 46)
                        {
                            blHasDecimal = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if ((blHasComma && strVal.Length < 4) ||
                    (blHasDecimal) && strVal.Length < 3)
                    return false;

                return true;
            }
            else
                return false;
        }        
        
        #endregion

        #region Global Process Related Functions

        /// <summary>
        /// This function will check to see if another instance of a specified program is currently running
        /// on the computer.  
        /// Returns: True = Another instance of program is running on the system, False = No other instances of
        /// program are running.
        /// </summary>
        /// <returns></returns>
        public static bool CheckExistingProcess(string strProcName)
        {
            try
            {
                int iProcCount = 0;
                bool blProcFound = false;

                //Enumerates all processes on the system and checks to see if another instance of the specified process is found.
                foreach (Process proc in Process.GetProcesses())
                {
                    if (proc.ProcessName.ToUpper() == strProcName.ToUpper())
                    {
                        iProcCount++;

                        if (iProcCount > 1)
                        {
                            blProcFound = true;
                            break;
                        }//end if
                    }//end if
                }//next proc  

                return blProcFound;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CheckExistingProcess function of Global class.");

                return false;
            }
        }

        #endregion

        #region General Extension Methods

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }

        }
        
        #endregion

        #region ADO.NET Extension Properties, Functions

        /// <summary>
        /// Returns the value of a specific field of an ADO data object record.  This function will return a default value if the current field contains a null value, according to the 
        /// data type of the field.
        /// </summary>
        /// <param name="oValue">Value of Field.</param>
        /// <param name="dataType">Data type of the field.</param>
        /// <returns></returns>
        public static object GetADOValue(object oValue, OleDbType dataType)
        {
            try
            {
                if (oValue == null || oValue == DBNull.Value)
                {
                    switch (dataType)
                    {
                        case OleDbType.TinyInt:
                        case OleDbType.UnsignedTinyInt:
                            return (byte)0;
                        case OleDbType.SmallInt:
                        case OleDbType.UnsignedSmallInt:
                            return (short)0;
                        case OleDbType.Integer:
                        case OleDbType.UnsignedInt:
                            return (int)0;
                        case OleDbType.BigInt:
                        case OleDbType.UnsignedBigInt:
                            return (long)0;
                        case OleDbType.Single:
                            return (float)0;
                        case OleDbType.Double:
                            return (double)0;
                        case OleDbType.Currency:
                        case OleDbType.Decimal:
                            return 0m;
                        case OleDbType.Date:
                        case OleDbType.DBDate:
                        case OleDbType.DBTime:
                        case OleDbType.DBTimeStamp:
                            return new DateTime(1753, 1, 1);
                        case OleDbType.Boolean:
                            return false;
                        case OleDbType.LongVarChar:
                        case OleDbType.LongVarWChar:
                        case OleDbType.BSTR:
                        case OleDbType.VarChar:
                        case OleDbType.VarWChar:
                            return "";
                    }//end switch
                }//end if

                return oValue;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetADOValue function of Global class.");
                return null;
            }
        }

        /// <summary>
        /// This function will automate the closing and memory cleanup of an ADO.NET SQL Connection object to reduce
        /// the cleanup code needed in each function.
        /// </summary>
        /// <param name="connObj">DbConnection object to close and dispose.</param>
        public static void CloseADODataObject(DbConnection connObj)
        {
            try
            {
                if (connObj != null)
                {
                    if (connObj.State == ConnectionState.Open)
                        connObj.Close();

                    connObj.Dispose();
                }
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CloseADODataObject Overload 1 function of Global class.");
            }
        }

        /// <summary>
        /// This function will automate the closing and memory cleanup of an ADO.NET SQL Command object to reduce
        /// the cleanup code needed in each function.
        /// </summary>
        /// <param name="cmdObj">DbCommand object to close and dispose.</param>
        public static void CloseADODataObject(DbCommand cmdObj)
        {
            try
            {
                if (cmdObj != null)
                {
                    cmdObj.Dispose();
                }
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CloseADODataObject Overload 2 function of Global class.");
            }
        }

        /// <summary>
        /// This function will automate the closing and memory cleanup of an ADO.NET SQL Data Reader object to reduce
        /// the cleanup code needed in each function.
        /// </summary>
        /// <param name="drdObj">OleDbDataReader object to close and dispose.</param>
        public static void CloseADODataObject(DbDataReader drdObj)
        {
            try
            {
                if (drdObj != null)
                {
                    if (!drdObj.IsClosed)
                        drdObj.Close();

                    drdObj.Dispose();
                }
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in CloseADODataObject Overload 3 function of Global class.");
            }
        }

        #endregion

        #region Form Focus Functions/Event Handlers

        /// <summary>
        /// This is a special version of the SetFocus function for .NET which will guarantee a control will gain focus.  The SetFocus function 
        /// in the Global class will be used during Form_Load events, since a problem seems to occur when setting the focus to a control
        /// using regular SetFocus functions in Form_Load.  This version of SetFocus will launch a System.Windows.Form Timer that will launch an
        /// handler event almost immediately after it is created to give the control focus.  This will prevent the control from not retaining focus
        /// during the Form_Load.
        /// </summary>
        /// <param name="ctrl">Control to receive the focus.</param>
        public static void SetFocus(Control ctrl)
        {
            try
            {
                System.Windows.Forms.Timer FocusTimer = new System.Windows.Forms.Timer();
                FocusTimer.Tick += new EventHandler(FocusTimer_Tick);
                FocusTimer.Interval = 25;
                FocusTimer.Start();

                FocusTimer.Tag = ctrl;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in SetFocus function of Global class.");
            }
        }

        /// <summary>
        /// Tick event handler of the Focus Timer object that will give focus to the specified control in the SetFocus function.
        /// </summary>
        /// <param name="sender">Focus Timer object.</param>
        /// <param name="e">EventArgs</param>
        private static void FocusTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Timer FocusTimer = (System.Windows.Forms.Timer)sender;
                Control ctrl = (Control)FocusTimer.Tag;

                FocusTimer.Stop();
                FocusTimer.Dispose();

                ctrl.Focus();
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in FocusTimer_Tick event handler of Global class.");
            }
        }

        #endregion

        #region .NET GDI+ Imaging Properties, Functions

        /// <summary>
        /// Saves the bitmap image passed to the function to a JPEG buffer with the compression set in the quality parameter.  This function will save the JPEG
        /// image using .NET GDI+ functions.
        /// </summary>
        /// <param name="bmpImage">Bitmap to save to JPEG buffer.</param>
        /// <param name="iQuality">Quality of JPEG image (0-100).</param>
        /// <returns></returns>
        public static byte[] SaveBitmapToJPEG(Bitmap bmpImage, int iQuality)
        {
            byte[] bufImage = null;
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream();

                ImageCodecInfo jpgEncoder = GetGDIEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID for the Quality parameter category.
                System.Drawing.Imaging.Encoder qualEncoder = System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  An EncoderParameters object has an array of EncoderParameter objects. In this case, 
                //there is only one EncoderParameter object in the array.
                EncoderParameters qualEncParams = new EncoderParameters(1);

                EncoderParameter qualEncParam = new EncoderParameter(qualEncoder, Convert.ToInt64(iQuality));
                qualEncParams.Param[0] = qualEncParam;

                bmpImage.Save(ms, jpgEncoder, qualEncParams);
                bufImage = ms.ToArray();
                ms.Close();
                ms = null;

                return bufImage;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in SaveBitmapToJPEG Overload 1 function of Global class.");
                return null;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
        }

        /// <summary>
        /// Saves the bitmap image passed to the function to a JPEG file with the compression set in the quality parameter.  This function will save the JPEG
        /// image using .NET GDI+ functions.
        /// </summary>
        /// <param name="strFileName">The full name and path on disk where to save the image file.</param>
        /// <param name="bmpImage">Bitmap to save to JPEG buffer.</param>        
        /// <param name="iQuality">Quality of JPEG image (0-100).</param>
        public static void SaveBitmapToJPEG(string strFileName, Bitmap bmpImage, int iQuality)
        {
            FileStream fs = null;

            try
            {
                byte[] bufImage = SaveBitmapToJPEG(bmpImage, iQuality);

                if (bufImage == null)
                    return;

                fs = File.Create(strFileName);
                fs.Write(bufImage, 0, bufImage.Length);
                fs.Close();
                fs = null;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in SaveBitmapToJPEG Overload 2 function of Global class.");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// Gets the GDI+ image encoder object that is associated with the image format that is specified in the format parameter of the function.
        /// </summary>
        /// <param name="format">The image format associated with the codec to return.</param>
        /// <returns></returns>
        public static ImageCodecInfo GetGDIEncoder(ImageFormat format)
        {
            try
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == format.Guid)
                    {
                        return codec;
                    }
                }
                return null;
            }
            catch (Exception err)
            {
                ErrorHandler.ShowErrorMessage(err, "Error in GetGDIEncoder function of Global class.");
                return null;
            }
        }

        #endregion

        #region Data Type Specific Properties

        [DefaultValue(4)]
        public static int EnumSizeBytes { get; set; }

        [DefaultValue(4)]
        public static int PointerSizeBytes { get; set; }

        #endregion        
    }

    #endregion
   
}
