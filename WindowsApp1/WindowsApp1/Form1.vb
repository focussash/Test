Imports System.Windows.Forms
Public Class Form
    'Declarations'
    Dim InitializeStatus As Integer
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
    Dim FlashTime As Integer
    'PH sensors'
    Dim Sensor1 As New pHsensor()
    Dim Sensor2 As New pHsensor()
    Dim Sensor3 As New pHsensor()
    'Serial Ports'
    Dim com(4) As IO.Ports.SerialPort
    Dim portCount As Integer

    'Random stuff for troubleshooting'
    Dim a As Integer


    Private Sub Base_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox15.Text = "Not initialized!"
        InitializeStatus = 0
    End Sub

    'Buttons 1 - 5 are for pumps'
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Pump1.State = 0) Then
            Pump1.State += 1
        Else
            Pump1.State *= 0
        End If

        If (Pump1.State = 1) Then
            Pump1.StateStr = "On"
        Else
            Pump1.StateStr = "Off"
            TrackBar1.Value = 0 'In this case reset the trackbar'
        End If

        Try
            InputPort1.WriteLine(Pump1.State.ToString & "1011")
        Catch
        End Try
        TextBox1.Text = "Digital" & ", " & Pump1.StateStr
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (Pump2.State = 0) Then
            Pump2.State += 1
        Else
            Pump2.State *= 0
        End If

        If (Pump2.State = 1) Then
            Pump2.StateStr = "On"
        Else
            Pump2.StateStr = "Off"
            TrackBar2.Value = 0 'In this case reset the trackbar'
        End If
        Try
            InputPort1.WriteLine(Pump2.State.ToString & "1021") ' The 1,02,1 here indicates that this is a pump(1) numbered 02 with digital control （1）
        Catch
        End Try
        TextBox2.Text = "Digital" & ", " & Pump2.StateStr
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (Pump3.State = 0) Then
            Pump3.State += 1
        Else
            Pump3.State *= 0
        End If

        If (Pump3.State = 1) Then
            Pump3.StateStr = "On"
        Else
            Pump3.StateStr = "Off"
            TrackBar3.Value = 0 'In this case reset the trackbar'
        End If

        Try
            InputPort1.WriteLine(Pump3.State.ToString & "1031") ' The 1,03,1 here indicates that this is a pump(1) numbered 03 with digital control （1）
        Catch
        End Try
        TextBox3.Text = "Digital" & ", " & Pump3.StateStr
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If (Pump4.State = 0) Then
            Pump4.State += 1
        Else
            Pump4.State *= 0
        End If

        If (Pump4.State = 1) Then
            Pump4.StateStr = "On"
        Else
            Pump4.StateStr = "Off"
            TrackBar4.Value = 0 'In this case reset the trackbar'
        End If

        Try
            InputPort1.WriteLine(Pump4.State.ToString & "1041") ' The 1,04,1 here indicates that this is a pump(1) numbered 04 with digital control （1）
        Catch
        End Try
        TextBox4.Text = "Digital" & ", " & Pump4.StateStr
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If (Pump5.State = 0) Then
            Pump5.State += 1
        Else
            Pump5.State *= 0
        End If

        If (Pump5.State = 1) Then
            Pump5.StateStr = "On"
        Else
            Pump5.StateStr = "Off"
            TrackBar5.Value = 0 'In this case reset the trackbar'
        End If

        Try
            InputPort1.WriteLine(Pump5.State.ToString & "1051") ' The 1,05,1 here indicates that this is a pump(1) numbered 05 with digital control （1）
        Catch
        End Try
        TextBox5.Text = "Digital" & ", " & Pump5.StateStr
    End Sub
    'Buttons 1 - 5 are for pumps'

    'Buttons 6 - 17 are for solenoid valves'
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click 'Stomach Acid, On/Off'
        If (Valve1.State = 0) Then
            Valve1.State += 1
        Else
            Valve1.State *= 0
        End If

        If (Valve1.State = 1) Then
            Valve1.StateStr = "On"
        Else
            Valve1.StateStr = "Off"
        End If

        Try
            InputPort1.WriteLine(Valve1.State.ToString & "2011") ' The 2,01,1 here indicates that this is a valve(2) numbered 01 with digital control （1）
        Catch
        End Try
        TextBox6.Text = Valve1.StateStr
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click 'Stomach Acid, flash'
        If (Valve1.State = 0) Then
            Try
                InputPort1.WriteLine(FlashTime.ToString & "2012") ' The 2,01,1 here indicates that this is a valve(2) numbered 01 with flash control (2)
            Catch
            End Try
            Valve1.StateStr = CDbl(FlashTime.ToString) * 0.5
            TextBox6.Text = Valve1.StateStr & "s"
        Else
            MsgBox("Valve already on!")
        End If
    End Sub


    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        If (Pump1.State = 1) Then
            Pump1.Voltage = TrackBar1.Value
            Try
                InputPort1.WriteLine(Pump1.Voltage.ToString & "1012") ' The 1,01,2 here indicates that this is a pump(1) numbered 01 with analog control (2)
            Catch
            End Try
            'Now convert the voltage to real output voltage to print'
            Pump1.Voltage = Format(Pump1.Voltage / 9 * 100, 0.0)
            Pump1.StateStr = Pump1.Voltage.ToString
            TextBox1.Text = "Analog" & ", " & Pump1.Voltage.ToString & "%"
        Else
            MsgBox("Pump not on!")
            TrackBar1.Value = 0
        End If
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        If (Pump2.State = 1) Then
            Pump2.Voltage = TrackBar2.Value
            Try
                InputPort1.WriteLine(Pump2.Voltage.ToString & "1022") ' The 1,02,2 here indicates that this is a pump(1) numbered 02 with analog control (2)
            Catch
            End Try
            'Now convert the voltage to real output voltage to print'
            Pump2.Voltage = Format(Pump2.Voltage / 9 * 100, 0.0)
            Pump2.StateStr = Pump2.Voltage.ToString
            TextBox2.Text = "Analog" & ", " & Pump2.Voltage.ToString & "%"
        Else
            MsgBox("Pump not on!")
            TrackBar2.Value = 0
        End If
    End Sub

    Private Sub TrackBar3_Scroll(sender As Object, e As EventArgs) Handles TrackBar3.Scroll
        If (Pump3.State = 1) Then
            Pump3.Voltage = TrackBar3.Value
            Try
                InputPort1.WriteLine(Pump3.Voltage.ToString & "1032") ' The 1,03,2 here indicates that this is a pump(1) numbered 03 with analog control (2)
            Catch
            End Try
            'Now convert the voltage to real output voltage to print'
            Pump3.Voltage = Format(Pump3.Voltage / 9 * 100, 0.0)
            Pump3.StateStr = Pump3.Voltage.ToString
            TextBox3.Text = "Analog" & ", " & Pump3.Voltage.ToString & "%"
        Else
            MsgBox("Pump not on!")
            TrackBar3.Value = 0
        End If
    End Sub

    Private Sub TrackBar4_Scroll(sender As Object, e As EventArgs) Handles TrackBar4.Scroll
        If (Pump4.State = 1) Then
            Pump4.Voltage = TrackBar4.Value
            Try
                InputPort1.WriteLine(Pump4.Voltage.ToString & "1042") ' The 1,04,2 here indicates that this is a pump(1) numbered 04 with analog control (2)
            Catch
            End Try
            'Now convert the voltage to real output voltage to print'
            Pump4.Voltage = Format(Pump4.Voltage / 9 * 100, 0.0)
            Pump4.StateStr = Pump4.Voltage.ToString
            TextBox4.Text = "Analog" & ", " & Pump4.Voltage.ToString & "%"
        Else
            MsgBox("Pump not on!")
            TrackBar4.Value = 0
        End If
    End Sub

    Private Sub TrackBar5_Scroll(sender As Object, e As EventArgs) Handles TrackBar5.Scroll
        If (Pump5.State = 1) Then
            Pump5.Voltage = TrackBar5.Value
            Try
                InputPort1.WriteLine(Pump5.Voltage.ToString & "1052") ' The 1,05,2 here indicates that this is a pump(1) numbered 05 with analog control (2)
            Catch
            End Try
            'Now convert the voltage to real output voltage to print'
            Pump5.Voltage = Format(Pump5.Voltage / 9 * 100, 0.0)
            Pump5.StateStr = Pump5.Voltage.ToString
            TextBox5.Text = "Analog" & ", " & Pump5.Voltage.ToString & "%"
        Else
            MsgBox("Pump not on!")
            TrackBar5.Value = 0
        End If

    End Sub


    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click 'Initialize!'
        InitializeStatus += 1 'Check if this is initialization, or update
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

        portCount = 0

        Try
            Timer1.Interval = 100 * CInt(TextBox17.Text)
            FlashTime = CInt(TextBox16.Text)
        Catch
            MsgBox("Check your input values and update again")
        End Try

        'Initialize the serial port connection with Arduinos'
        For Each sp As String In My.Computer.Ports.SerialPortNames
            comlist.Items.Add(sp)
            Try
                com(portCount) = My.Computer.Ports.OpenSerialPort(sp)
                com(portCount).ReadTimeout = 10000
                com(portCount).Close()
            Catch
            End Try
            portCount += 1
        Next
        'Now, because I was stupid and didn't know these ports object exist...'
        'I only have limited amount of port objects, so lets manually assign them'
        Try
            comlist.SetSelected(0, True)
            InputPort1 = My.Computer.Ports.OpenSerialPort(comlist.SelectedItem.ToString)
            'Add further ports here'
            'comlist.SetSelected(1, True)
            'ReadPort1 = My.Computer.Ports.OpenSerialPort(comlist.SelectedItem.ToString)

        Catch 'if someone clicked it twice, remove the extra instance of port
            MsgBox("Updated!")
            For i = 0 To (portCount - 1)
                comlist.Items.RemoveAt(i)
            Next
        End Try



        'Confirm that the form is initialized'
        If InitializeStatus = 1 Then
            TextBox15.Text = "Initialized!"
            Button24.Text = "Update"
        Else
            TextBox15.Text = "Updated!"
        End If
    End Sub



    Private Sub TextBox16_LostFocus(sender As Object, e As EventArgs) Handles TextBox16.LostFocus
        'Check if the input number is between 0 - 9
        Try
            If (CInt(TextBox16.Text) > 9 Or CInt(TextBox16.Text) < 0) Then
                MsgBox("Flash time should be a number between 0 to 9!")
                TextBox16.Text = ""
                Me.ActiveControl = TextBox16
            End If
        Catch
            If (Not TextBox16.Text = "") Then
                MsgBox("That...was not a number...")
                TextBox16.Text = ""
                Me.ActiveControl = TextBox16
            Else
                TextBox16.Text = "0"
            End If
        End Try
    End Sub

    Private Sub TextBox17_LostFocus(sender As Object, e As EventArgs) Handles TextBox17.LostFocus
        'Check if the input is a number
        Try
            If IsNumeric(TextBox16.Text) = False Then
                MsgBox("Sensor reading interval should be a number!")
                TextBox16.Text = ""
                Me.ActiveControl = TextBox16
            End If
        Catch
            If (Not TextBox16.Text = "") Then
            Else
                TextBox16.Text = "10"
            End If
        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Working for now, but Readport1 needs to be adjusted'
        If Sensor1.Status = 1 Then
            Sensor1.Timer += 1
            Try
                Sensor1.Reading = ReadPort1.ReadLine
                StomachPH.Series(0).Points.AddXY(Sensor1.Timer.ToString, Sensor1.Reading.ToString)
                If StomachPH.Series(0).Points.Count = 20 Then
                    StomachPH.Series(0).Points.RemoveAt(0)
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        'Button for enabling Stomach PH monitor'
        If Sensor1.Status = 1 Then
            Button25.Text = "Enable"
            Sensor1.Status = 0
        Else
            Button25.Text = "Disable"
            Sensor1.Status = 1
        End If
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        'Rest the chart'
        Sensor1.Timer = 0
        StomachPH.Series(0).Points.Clear()
    End Sub
End Class

