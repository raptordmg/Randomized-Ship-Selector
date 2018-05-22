using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Randomized_Ship_Selector
{
    public class Logger
    {
        private readonly RichTextBox _output;

        /// <summary>
        /// Logs objects to the given text box.
        /// </summary>
        /// <param name="outputRichTextBox">The text box to log text to.</param>
        public Logger(RichTextBox outputRichTextBox)
        {
            _output = outputRichTextBox;
        }

        // Logs a string to the rich text box
        public void Log(string text)
        {
            _output.AppendText(Environment.NewLine + text);
        }

        public void LogError(string text)
        {
            _output.AppendText(Environment.NewLine + "ERROR: " + text);
            _output.AppendText(", this is an error. Please message me or open a issue on github:");
            _output.AppendText(Environment.NewLine + "https://github.com/DInbound/Randomized-Ship-Selector/issues");
        }

        private void LogWebError(string text)
        {
            _output.AppendText(Environment.NewLine + "ERROR: " + text);
            _output.AppendText(Environment.NewLine + "Check your internet connection and try again later.");
        }

        public void CatchWebEx(WebException ex)
        {
            switch (ex.Status)
            {
                case WebExceptionStatus.ConnectFailure:
                    LogWebError("Could not connect to server.");
                    break;
                case WebExceptionStatus.Timeout:
                    LogWebError("Request timed out.");
                    break;
                case WebExceptionStatus.SendFailure:
                    LogWebError("Failed to send data.");
                    break;
                case WebExceptionStatus.NameResolutionFailure:
                    LogWebError("Failed to connect to DNS.");
                    break;
                default:
                    LogWebError(ex.Status.ToString());
                    break;
            }
        }
    }
}
