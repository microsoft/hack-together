using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace FileSystemAbstraction.Exceptions
{
    //
    // Summary:
    //     The exception that is thrown when an attempt to access a file that does not exist
    //     on disk fails.
    //     Source based on System.IO.FileNotFoundExpection
    [Serializable]
    public class FileNotFoundException : Exception
    {
        private string _fileName;
        private string _message;
        internal int _HResult;

        //
        // Summary:
        //     Gets the error message that explains the reason for the exception.
        //
        // Returns:
        //     The error message.
        public override string Message
        {
            get
            {
                SetMessageField();
                return _message;
            }
        }

        //
        // Summary:
        //     Gets the name of the file that cannot be found.
        //
        // Returns:
        //     The name of the file, or null if no file name was passed to the constructor for
        //     this instance.
        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        private void SetErrorCode(int hresult)
        {
            base.HResult = hresult;
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.FileNotFoundException class with
        //     its message string set to a system-supplied message and its HRESULT set to COR_E_FILENOTFOUND.
        public FileNotFoundException()
        {
            SetErrorCode(-2147024894);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.FileNotFoundException class with
        //     its message string set to message and its HRESULT set to COR_E_FILENOTFOUND.
        //
        // Parameters:
        //   message:
        //     A description of the error. The content of message is intended to be understood
        //     by humans. The caller of this constructor is required to ensure that this string
        //     has been localized for the current system culture.
        
        public FileNotFoundException(string message)
            : base(message)
        {
            SetErrorCode(-2147024894);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.FileNotFoundException class with
        //     a specified error message and a reference to the inner exception that is the
        //     cause of this exception.
        //
        // Parameters:
        //   message:
        //     A description of the error. The content of message is intended to be understood
        //     by humans. The caller of this constructor is required to ensure that this string
        //     has been localized for the current system culture.
        //
        //   innerException:
        //     The exception that is the cause of the current exception. If the innerException
        //     parameter is not null, the current exception is raised in a catch block that
        //     handles the inner exception.
        
        public FileNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            SetErrorCode(-2147024894);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.FileNotFoundException class with
        //     its message string set to message, specifying the file name that cannot be found,
        //     and its HRESULT set to COR_E_FILENOTFOUND.
        //
        // Parameters:
        //   message:
        //     A description of the error. The content of message is intended to be understood
        //     by humans. The caller of this constructor is required to ensure that this string
        //     has been localized for the current system culture.
        //
        //   fileName:
        //     The full name of the file with the invalid image.
        
        public FileNotFoundException(string message, string fileName)
            : base(message)
        {
            SetErrorCode(-2147024894);
            _fileName = fileName;
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.FileNotFoundException class with
        //     a specified error message and a reference to the inner exception that is the
        //     cause of this exception.
        //
        // Parameters:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   fileName:
        //     The full name of the file with the invalid image.
        //
        //   innerException:
        //     The exception that is the cause of the current exception. If the innerException
        //     parameter is not null, the current exception is raised in a catch block that
        //     handles the inner exception.
        
        public FileNotFoundException(string message, string fileName, Exception innerException)
        {
            _message = message;
            _fileName = fileName;
        }

        private void SetMessageField()
        {
            if (_message == null)
            {
                if (_fileName != null)
                {
                    _message = _fileName +" not found.";
                }
                else
                {
                    _message = "FileNotFound";
                }
            }
        }

        //
        // Summary:
        //     Returns the fully qualified name of this exception and possibly the error message,
        //     the name of the inner exception, and the stack trace.
        //
        // Returns:
        //     The fully qualified name of this exception and possibly the error message, the
        //     name of the inner exception, and the stack trace.
        
        public override string ToString()
        {
            string text = GetType().FullName + ": " + Message;
            if (_fileName != null && _fileName.Length != 0)
            {
                text = text + Environment.NewLine + "FileName_Name:"+ _fileName;
            }

            if (base.InnerException != null)
            {
                text = text + " ---> " + base.InnerException.ToString();
            }

            if (StackTrace != null)
            {
                text = text + Environment.NewLine + StackTrace;
            }

            return text;
        }


        private FileNotFoundException(string fileName, int hResult)
            : base(null)
        {
            SetErrorCode(hResult);
            _fileName = fileName;
            SetMessageField();
        }

        //
        // Summary:
        //     Sets the System.Runtime.Serialization.SerializationInfo object with the file
        //     name and additional exception information.
        //
        // Parameters:
        //   info:
        //     The object that holds the serialized object data about the exception being thrown.
        //
        //   context:
        //     The object that contains contextual information about the source or destination.
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("FileNotFound_FileName", _fileName, typeof(string));
            
        }
    }
}