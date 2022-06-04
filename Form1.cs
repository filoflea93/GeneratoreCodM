using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace GeneratoreCodM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        public string FilesCreatedString = "Files created";
        public string FileReadingErrorString = "File reading error";
        public string FileWritingErrorString = "File writing error";

        public struct CodM
        {

            public string codM;
            public string descrUp;
            public string descrDown;
            public string timerUp;
            public string inputUp;
            public string inputDown;
            public string outputUp;
            public string outputDown;
            public string msgNumberUp;

            public CodM(string _n, string _descrUp, string _descrDown,
                        string _timerUp, string _inputUp, string _inputDown,
                        string _outputUp, string _outputDown, string _msgNumberUp)
            {

                codM = _n;
                descrUp = _descrUp;
                descrDown = _descrDown;
                timerUp = _timerUp;
                inputUp = _inputUp;
                inputDown = _inputDown;
                outputUp = _outputUp;
                outputDown = _outputDown;
                msgNumberUp = _msgNumberUp;

            }

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            string fileName;
            string codM;
            string descrUp;
            string descrDown;
            string timerUp;
            string inputUp;
            string inputDown;
            string outputUp;
            string outputDown;
            string msgNumber;

            fileName = fileNameBox.Text + ".csv";

            string dirpath = Directory.GetCurrentDirectory();
            string path = dirpath + "\\" + fileName;

            fileNameBox.Text = path;

            try
            {

                using (TextFieldParser csvReader = new TextFieldParser(path))
                {

                    csvReader.SetDelimiters(new string[] { ";" });

                    // Skip the row with the column names
                    csvReader.ReadLine();

                    while (!csvReader.EndOfData)
                    {
                        // Read current line fields, pointer moves to the next line.
                        string[] fields = csvReader.ReadFields();
                        codM = fields[0];
                        descrUp = fields[1];
                        descrDown = fields[2];
                        timerUp = fields[3];
                        inputUp = fields[4];
                        inputDown = fields[5];
                        outputUp = fields[6];
                        outputDown = fields[7];
                        msgNumber = fields[8];

                        CodM newCodM = new CodM(codM, descrUp, descrDown,
                                                timerUp, inputUp, inputDown,
                                                outputUp, outputDown, msgNumber);

                        Write_FbU_Attuatori_File(newCodM, dirpath);

                    }

                    fileNameBox.Text = FilesCreatedString;

                }

            }
            catch (Exception)
            {

                fileNameBox.Text = FileReadingErrorString;

            }
        }

        public void Write_FbU_Attuatori_File(CodM _codM, string _dirpath)
        {

            int intCodMUp = int.Parse(_codM.codM);
            int intTimerUp = int.Parse(_codM.timerUp);
            int intMsgNumberUp = int.Parse(_codM.msgNumberUp);

            int codMUp = intCodMUp;
            int codMDown = intCodMUp + 1;

            int timerDown = intTimerUp + 1;
            int msgNumberDown = intMsgNumberUp + 1;

            string inputUp;
            string inputDown;
            string outputUp;
            string outputDown;
            string msgUp;
            string msgDown;

            string pType = "";
            string evType;

            string FbU_Attuatore_outputFilePath = _dirpath + "\\FbU_Attuatore.txt";
            string Anomalie_outputFilePath = _dirpath + "\\Anomalie.txt";
            string AttuatoreHeader_outputFilePath = _dirpath + "\\AttuatoreHeader.txt";
            string ResetValvole_outputFilePath = _dirpath + "\\ResetValvole.txt";
            string Timer_outputFilePath = _dirpath + "\\Timer.txt";

            if (intMsgNumberUp < 100)
            {
                msgUp = "0" + intMsgNumberUp;
            }
            else
            {
                msgUp = intMsgNumberUp.ToString();
            }

            if (msgNumberDown < 100)
            {
                msgDown = "0" + msgNumberDown;
            }
            else
            {
                msgDown = msgNumberDown.ToString();
            }

            if (_codM.inputUp != "")
            {
                inputUp = "I_" + _codM.inputUp;
            }
            else
            {
                inputUp = "true";
            }

            if (_codM.inputDown != "")
            {
                inputDown = "I_" + _codM.inputDown;
            }
            else
            {
                inputDown = "true";
            }

            if (_codM.outputUp != "")
            {
                outputUp = "O_" + _codM.outputUp;
            }
            else
            {
                outputUp = "li_localNotUsed";
            }

            if (_codM.outputDown != "")
            {
                outputDown = "O_" + _codM.outputDown;
            }
            else
            {
                outputDown = "li_localNotUsed";
            }

            if (_codM.inputUp != "" && _codM.inputDown != "")
                pType = "01";

            if (_codM.inputUp == "" && _codM.inputDown == "")
                pType = "11";

            if (_codM.inputUp != "" && _codM.inputDown == "")
                pType = "21";

            if (_codM.inputUp == "" && _codM.inputDown != "")
                pType = "31";

            if (_codM.outputUp != "" && _codM.outputDown != "")
                evType = "2";
            else
                if (_codM.outputUp == "" || _codM.outputDown == "")
                evType = "1";
            else
                evType = "???";

            try
            {
                Write_HeaderAttuatori_File(codMUp, codMDown, AttuatoreHeader_outputFilePath);
                Write_Anomalie_File(_codM.inputUp, _codM.inputDown, _codM.descrUp, _codM.descrDown, msgUp, msgDown, Anomalie_outputFilePath);
                Write_ResetValvole_File(_codM.outputUp, _codM.outputDown, ResetValvole_outputFilePath);
                Write_Timer_File(intTimerUp, timerDown, _codM.descrUp, _codM.descrDown, Timer_outputFilePath);

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(FbU_Attuatore_outputFilePath, true);

                //Write a line of text
                sw.WriteLine("(* M" + codMUp + ": " + _codM.descrUp + " *)");
                sw.WriteLine("(* M" + codMDown + ": " + _codM.descrDown + " *)");
                sw.WriteLine("FbU_AttuatoreM" + codMUp + "(ii_proc := ii_proc,");
                sw.WriteLine("                  ioi_ManualCmd   := li_localNotUsed,");
                sw.WriteLine("                  i_EnabManCmd    := 0,");
                sw.WriteLine("                  i_EnableUP      := true,");
                sw.WriteLine("                  i_EnableDN      := true,");
                sw.WriteLine("                  io_CommandUP    := GstU_ProcFunction[ii_proc].Req_M[" + codMUp + "],");
                sw.WriteLine("                  io_CommandDN    := GstU_ProcFunction[ii_proc].Req_M[" + codMDown + "],");
                sw.WriteLine("                  io_AckUP        := GstU_ProcFunction[ii_proc].Ack_M[" + codMUp + "],");
                sw.WriteLine("                  io_AckDN        := GstU_ProcFunction[ii_proc].Ack_M[" + codMDown + "],");
                sw.WriteLine("                  i_DlyUp         := GI_Timer[" + intTimerUp + "],");
                sw.WriteLine("                  i_DlyDn         := GI_Timer[" + timerDown + "],");
                sw.WriteLine("                  i_TimeOut       := gTimeOutCilinder,");
                sw.WriteLine("                  i_Ptype         := " + pType + ",");
                sw.WriteLine("                  i_EVType        := " + evType + ",");
                sw.WriteLine("                  i_InputUP       := " + inputUp + ",");
                sw.WriteLine("                  i_InputDN       := " + inputDown + ",");
                sw.WriteLine("                  io_OutputUP     := " + outputUp + ",");
                sw.WriteLine("                  io_OutputDN     := " + outputDown + ",");
                sw.WriteLine("                  io_MsgErrorEnab := li_localNotUsed,");
                sw.WriteLine("                  o_MsgErrorUP    => M_UPMsg" + msgUp + ",");
                sw.WriteLine("                  o_MsgErrorDN    => M_UPMsg" + msgDown + ");");
                sw.WriteLine("");

                //Close the file
                sw.Close();

            }
            catch (Exception e)
            {
                fileNameBox.Text = FileWritingErrorString;
            }
        }

        public void Write_HeaderAttuatori_File(int _codMUp, int _codMDown, string _outputFilePath)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(_outputFilePath, true);

            sw.WriteLine("FbU_AttuatoreM" + _codMUp + "   : FbU_AttuatoreNew_v1_1; (* FUNCTION BLOCK PER CODICI " + _codMUp + " - " + _codMDown + " *)");

            //Close the file
            sw.Close();
        }

        public void Write_Anomalie_File(string _inputUp, string _inputDown, string _descUp, string _descDown, string _msgUp, string _msgDown, string _outputFilePath)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(_outputFilePath, true);

            if(_inputUp != "")
                sw.WriteLine("Anom. " + _inputUp + " - " + _descUp);
            else
                if (_inputDown != "")
                    sw.WriteLine("Anom. not " + _inputDown + " - " + _descUp);

            if (_inputDown != "")
                sw.WriteLine("Anom. " + _inputDown + " - " + _descDown);
            else
                if (_inputUp != "")
                sw.WriteLine("Anom. not " + _inputUp + " - " + _descDown);

            //Close the file
            sw.Close();
        }

        public void Write_ResetValvole_File(string _outputUp, string _outputDown, string _outputFilePath)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(_outputFilePath, true);

            if(_outputUp != "")
                sw.WriteLine("O_" + _outputUp + " := false;");

            if(_outputDown != "")
                sw.WriteLine("O_" + _outputDown + " := false;");

            //Close the file
            sw.Close();

        }

        public void Write_Timer_File(int _timerUp, int _timerDown, string _descUp, string _descDown, string _outputFilePath)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(_outputFilePath, true);

            sw.WriteLine("T" + _timerUp + ": " + _descUp + "");
            sw.WriteLine("T" + _timerDown + ": " + _descDown + "");

            //Close the file
            sw.Close();

        }
    }
}