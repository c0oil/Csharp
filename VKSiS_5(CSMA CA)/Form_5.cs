using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace VKSiS_5
{
	public partial class VKSiS_5 : Form
	{
		const Char JAM_SIGNAL = '♂';
		const int MAX_STRING_SIZE = 55;
		const int DEFAULT_DELAY = 10;

		private delegate void OutputDelegate(string message);
		private delegate void DebugDelegate(string message);

        private MemoryMappedFile sharedFile_ = MemoryMappedFile.CreateOrOpen(@"MYSHARE", 8, MemoryMappedFileAccess.ReadWrite);
		private int collisionCount_ = 0;
		private Thread readLoopThread_;
		private Thread writeLoopThread_;
		private int delay_;

		public VKSiS_5()
		{
			InitializeComponent();

			this.CalculateDelay();

            using (MemoryMappedViewAccessor fileMap = sharedFile_.CreateViewAccessor())
            {
                fileMap.Flush();
            }
		}

		private void CalculateDelay()
		{
			delay_ = DEFAULT_DELAY * (int)Math.Pow(2, collisionCount_ + 1);
		}

		public Boolean CheckMessage(String message, String template)
		{
			int length = message.Length;
			Boolean result = true;
            for (int i = 0; i < length; i++)
            {
                if (message[i] == JAM_SIGNAL)
                {
                    result = false;
                }
            }
			return result;
		}

		public void CollisionDetected()
		{
			collisionCount_ += 1;
			this.CalculateDelay();
			this.DebugMessage("Collision! [" + collisionCount_ + "]\n");
		}

        public void WareBusy()
        {
            collisionCount_ += 1;
            this.CalculateDelay();
            this.DebugMessage("Ware busy! [" + collisionCount_ + "]\n");
        }

		public bool Write(String message)
		{
			using (MemoryMappedViewAccessor fileMap = sharedFile_.CreateViewAccessor())
			{
			    string memory = this.Read(1);
                if (!this.CheckMessage(memory, null))
                {
                    this.WareBusy();
                    Thread.Sleep(delay_);
                    return false;
                }

                fileMap.Write(0, JAM_SIGNAL);
                this.DebugMessage("Take ware.\n");
                Thread.Sleep(DEFAULT_DELAY);

                fileMap.Flush();
				int length = message.Length;
				for (int i = 0; i < length; i++)
				{
				    fileMap.Write(i * sizeof(char), message[i]);
                    Thread.Sleep(DEFAULT_DELAY);
                    memory = this.Read(1);
                    if (!this.CheckMessage(memory, null))
                    {
                        this.CollisionDetected();
                        Thread.Sleep(delay_);
                        return false;
                    }
				}
			}
		    return true;
		}

		private void WriteLoop()
		{
			String message = this.inputBox.Text;
			do
			{
				if(this.Write(message))
				    this.DebugMessage("Message \'" + message + "\' was successfully sent!\n");
			}
			while (this.loopCheckBox.Checked);
		}

		public String Read(int length)
		{
			Char[] answer = new Char[length];
			using (MemoryMappedViewAccessor fileMap = sharedFile_.CreateViewAccessor())
			{
				fileMap.ReadArray<Char>(0, answer, 0, length);
			}
			return new String(answer);
		}

		private void ReadLoop()
		{
			String message = "";
			do
			{
				this.OutputMessage(message);
				message = this.Read(MAX_STRING_SIZE);
			}
			while (this.CheckMessage(message, null));
			this.CollisionDetected();
			this.DebugMessage("Memory: " + message + "\n ");
		}

		private void OutputMessage(String message)
		{
			if (this.outputBox.InvokeRequired)
			{
				OutputDelegate del = new OutputDelegate(OutputMessage);
				this.outputBox.Invoke(del, message);
			}
			else
			{
				this.outputBox.Text = message;
			}
		}

		private void DebugMessage(string message)
		{
			if (this.outputBox.InvokeRequired)
			{
				DebugDelegate del = new DebugDelegate(DebugMessage);
				this.debugBox.Invoke(del, message);
			}
			else
			{
				this.debugBox.Text += message;
                this.debugBox.SelectionStart = this.debugBox.Text.Length;
                this.debugBox.ScrollToCaret();
			}
            
		}

		private void writeButton_Click(object sender, EventArgs e)
		{
			if (this.loopCheckBox.Checked)
			{
				if (writeLoopThread_ == null || writeLoopThread_.ThreadState != ThreadState.Running)
				{
					ThreadStart startWrite = new ThreadStart(WriteLoop);
					writeLoopThread_ = new Thread(startWrite);
					writeLoopThread_.Start();
				}
			}
			else
			{
				String message = this.inputBox.Text;
				this.Write(message);
				this.debugBox.Text += "Message \'" + message + "\' was successfully sent!\n";
			}
		}

		private void readButton_Click(object sender, EventArgs e)
		{
			if (this.loopCheckBox.Checked)
			{
				if (readLoopThread_ == null || readLoopThread_.ThreadState != ThreadState.Running)
				{
					ThreadStart startRead = new ThreadStart(ReadLoop);
					readLoopThread_ = new Thread(startRead);
					readLoopThread_.Start();
				}
			}
			else
			{
				String message = this.Read(MAX_STRING_SIZE);
				if (!this.CheckMessage(this.outputBox.Text, null))
				{
					this.CollisionDetected();
					this.DebugMessage("Memory: " + message + "\n");
				}
				else
					this.outputBox.Text = message;
			}
		}
	}
}
