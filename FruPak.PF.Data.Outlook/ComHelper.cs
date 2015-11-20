//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PF.Common.Code
//{
//    class ComHelper
//    {
//    }
//}

using NLog;
using System;
using System.Runtime.InteropServices;
using ComTypes = System.Runtime.InteropServices.ComTypes;

namespace PF.Data.Outlook.ComUtils
{
    public class ComHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Returns a string value representing the type name of the specified COM object.
        /// </summary>
        /// <param name="comObj">A COM object the type name of which to return.</param>
        /// <returns>A string containing the type name.</returns>
        public static string GetTypeName(object comObj)
        {
            if (comObj == null)
                return String.Empty;

            if (!Marshal.IsComObject(comObj))
                //The specified object is not a COM object
                return String.Empty;

            IDispatch dispatch = comObj as IDispatch;
            if (dispatch == null)
                //The specified COM object doesn't support getting type information
                return String.Empty;

            ComTypes.ITypeInfo typeInfo = null;
            try
            {
                try
                {
                    // obtain the ITypeInfo interface from the object
                    dispatch.GetTypeInfo(0, 0, out typeInfo);
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Info, ("Cannot get the ITypeInfo interface for the specified COM object: " + ex.Message));

                    //Cannot get the ITypeInfo interface for the specified COM object
                    return String.Empty;
                }

                string typeName = "";
                string documentation, helpFile;
                int helpContext = -1;

                try
                {
                    //retrieves the documentation string for the specified type description
                    typeInfo.GetDocumentation(-1, out typeName, out documentation,
                        out helpContext, out helpFile);
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Info, ("Cannot extract ITypeInfo information: " + ex.Message));
                    // Cannot extract ITypeInfo information
                    return String.Empty;
                }
                return typeName;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Info, ("Weird Unexpected Error: " + ex.Message));
                // Unexpected error
                return String.Empty;
            }
            finally
            {
                if (typeInfo != null) Marshal.ReleaseComObject(typeInfo);
            }
        }
    }

    /// <summary>
    /// Exposes objects, methods and properties to programming tools and other
    /// applications that support Automation.
    /// </summary>
    [ComImport()]
    [Guid("00020400-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDispatch
    {
        [PreserveSig]
        int GetTypeInfoCount(out int Count);

        [PreserveSig]
        int GetTypeInfo(
            [MarshalAs(UnmanagedType.U4)] int iTInfo,
            [MarshalAs(UnmanagedType.U4)] int lcid,
            out ComTypes.ITypeInfo typeInfo);

        [PreserveSig]
        int GetIDsOfNames(
            ref Guid riid,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)]
            string[] rgsNames,
            int cNames,
            int lcid,
            [MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);

        [PreserveSig]
        int Invoke(
            int dispIdMember,
            ref Guid riid,
            uint lcid,
            ushort wFlags,
            ref ComTypes.DISPPARAMS pDispParams,
            out object pVarResult,
            ref ComTypes.EXCEPINFO pExcepInfo,
            IntPtr[] pArgErr);
    }
}