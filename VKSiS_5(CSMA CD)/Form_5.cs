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
		const Char JAM_SIGNAL = '◙';
		const int MAX_STRING_SIZE = 55;
		const int DEFAULT_DELAY = 1;

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
					break;
				}
				if (template != null && message[i] != template[i])
				{
					using (MemoryMappedViewAccessor fileMap = sharedFile_.CreateViewAccessor())
					{
						fileMap.Write(i * sizeof(char), JAM_SIGNAL);
					}
					result = false;
					break;
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

		public void Write(String message)
		{
			Thread.Sleep(delay_);
			Boolean success = true;
			Char[] packet = message.ToCharArray();
			using (MemoryMappedViewAccessor fileMap = sharedFile_.CreateViewAccessor())
			{
				int length = message.Length;
				for (int i = 0; i < length; i++)
				{
					fileMap.Write(i * sizeof(char), message[i]);

					Thread.Sleep(DEFAULT_DELAY);

					String fromMemory = this.Read(i + 1);
					if (!this.CheckMessage(fromMemory, message))
					{
						this.CollisionDetected();
						success = false;
						break;
					}
				}
			}
			if (!success)
			{
				this.Write(message);
			}
		}

		private void WriteLoop()
		{
			String message = this.inputBox.Text;
			do
			{
				this.Write(message);
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
