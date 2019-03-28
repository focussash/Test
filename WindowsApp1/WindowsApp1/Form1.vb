Imports System.Windows.Forms
Public Class Form
    'Declarations'
    'Pumps'
    Dim pumps(2) As Pump
    Dim Pump1 As New Pump()
    Dim Pump2 As New Pump()
    Dim Pump3 As New Pump()
    Dim Pump4 As New Pump()
    Dim Pump5 As New Pump()
    'Solenoid Valves'
    Dim Valve1 As New Valve()
    Dim Valve2 As New Valve()
    Dim Valve3 As New Valve()
    Dim Valve4 As New Valve()
    Dim Valve5 As New Valve()
    Dim Valve6 As New Valve()
    'PH sensors'
    Dim Sensor1 As New pHsensor()
    Dim Sensor2 As New pHsensor()
    Dim Sensor3 As New pHsensor()
    'Serial Ports'
    Dim com(2) As IO.Ports.SerialPort
    Dim portCount As Integer

    'Random stuff for troubleshooting'
    Dim a As Integer


    Private Sub Base_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox15.Text = "Not initialized!"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Pump1.State = 0) Then
            Pump1.State += 1
        Else
            Pump1.State *= 0
        End If
        Try
            ListBox1.SetSelected(0, True)
            com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
            com(0).WriteLine(Pump1.State.ToString & "1011") ' The 1,1,1 here indicates that this is a pump(1) numbered 01 with digital control （1）
            ListBox1.SetSelected(0, False)
            com(0).Close()
        Catch
        End Try
        TextBox1.Text = Pump1.State.ToString & "1011"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click 'Initialize!'
        'Confirm that the form is initialized'
        TextBox15.Text = "Initialized!" & vbCrLf & "Do Not Click Again!"

        'Initialize the serial port connection with Arduinos'
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ListBox1.Items.Add(sp)
            Try
                com(portCount) = My.Computer.Ports.OpenSerialPort(sp)
                com(portCount).ReadTimeout = 10000
                com(portCount).Close()
            Catch ex As TimeoutException
                MsgBox("Error: Serial Port read timed out.")
            End Try
            portCount += 1
            'I dont think there is an easy way to detect which Arduino connects which so I will just have to remember them'
        Next
        'Alternatively,manually assign/exchange order as needed'





        'This is the initialization button which initializes all variables'
        Pump1.Name = "Food Media"
        Pump2.Name = "Pancreatic"
        Pump3.Name = "Transfer 1"
        Pump4.Name = "Transfer 2"
        Pump5.Name = "Transfer 3"
        Valve1.Name = "Stomach Acid"
        Valve2.Name = "Stomach Base"
        Valve3.Name = "Small Intestine Acid"
        Valve4.Name = "Small Intestine Base"
        Valve5.Name = "Colon Acid"
        Valve6.Name = "Colon Base"
        Sensor1.Name = "Stomach PH"
        Sensor2.Name = "Small Intestine PH"
        Sensor3.Name = "Colon PH"






    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        If (Pump1.State = 1) Then
            Pump1.Voltage = TrackBar1.Value
            Try
                ListBox1.SetSelected(0, True)
                com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
                com(0).WriteLine(Pump1.Voltage.ToString & "1012") ' The 1,1,2 here indicates that this is a pump(1) in area 01 with analog control (2)
                ListBox1.SetSelected(0, False)
                com(0).Close()
                TextBox1.Text = Pump1.Voltage.ToString & "1012"
            Catch
            End Try
        Else
            MsgBox("Pump not on!")
        End If
    End Sub
End Class

