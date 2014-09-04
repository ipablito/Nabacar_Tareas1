Namespace Microsoft.Crm.Sdk.Samples
	Partial Public Class WinCRUDOperations
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.btnExit = New Button()
			Me.cbxServerList = New ComboBox()
			Me.btnConnect = New Button()
			Me.lblMulConn = New Label()
			Me.lblOutMsg = New Label()
			Me.SuspendLayout()
			' 
			' btnExit
			' 
			Me.btnExit.Location = New Point(475, 323)
			Me.btnExit.Name = "btnExit"
			Me.btnExit.Size = New Size(75, 23)
			Me.btnExit.TabIndex = 0
			Me.btnExit.Text = "Exit"
			Me.btnExit.UseVisualStyleBackColor = True
'			Me.btnExit.Click += New System.EventHandler(Me.btnExit_Click)
			' 
			' cbxServerList
			' 
			Me.cbxServerList.FormattingEnabled = True
			Me.cbxServerList.Location = New Point(30, 37)
			Me.cbxServerList.Name = "cbxServerList"
			Me.cbxServerList.Size = New Size(436, 21)
			Me.cbxServerList.TabIndex = 5
			' 
			' btnConnect
			' 
			Me.btnConnect.Location = New Point(475, 37)
			Me.btnConnect.Name = "btnConnect"
			Me.btnConnect.Size = New Size(75, 23)
			Me.btnConnect.TabIndex = 6
			Me.btnConnect.Text = "Connect"
			Me.btnConnect.UseVisualStyleBackColor = True
'			Me.btnConnect.Click += New System.EventHandler(Me.btnConnect_Click)
			' 
			' lblMulConn
			' 
			Me.lblMulConn.AutoSize = True
			Me.lblMulConn.Font = New Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, (CByte(0)))
			Me.lblMulConn.Location = New Point(27, 9)
			Me.lblMulConn.Name = "lblMulConn"
			Me.lblMulConn.Size = New Size(444, 17)
			Me.lblMulConn.TabIndex = 7
			Me.lblMulConn.Text = "Choose a server configuration from the list, and then select [Connect]"
			' 
			' lblOutMsg
			' 
			Me.lblOutMsg.BackColor = Color.SeaShell
			Me.lblOutMsg.BorderStyle = BorderStyle.FixedSingle
			Me.lblOutMsg.Location = New Point(30, 73)
			Me.lblOutMsg.Name = "lblOutMsg"
			Me.lblOutMsg.Size = New Size(520, 236)
			Me.lblOutMsg.TabIndex = 8
			' 
			' WinCRUDOperations
			' 
			Me.AutoScaleDimensions = New SizeF(6F, 13F)
			Me.AutoScaleMode = AutoScaleMode.Font
			Me.ClientSize = New Size(581, 373)
			Me.Controls.Add(Me.lblOutMsg)
			Me.Controls.Add(Me.lblMulConn)
			Me.Controls.Add(Me.btnConnect)
			Me.Controls.Add(Me.cbxServerList)
			Me.Controls.Add(Me.btnExit)
			Me.Name = "WinCRUDOperations"
			Me.Text = "QuickStart using Windows Forms"
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents btnExit As Button

		Private cbxServerList As ComboBox
		Private WithEvents btnConnect As Button
		Private lblMulConn As Label
		Private lblOutMsg As Label
	End Class
End Namespace