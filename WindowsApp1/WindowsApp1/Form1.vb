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

        If (Pump1.State = 1) Then
            Pump1.StateStr = "On"
        Else
            Pump1.StateStr = "Off"
            TrackBar1.Value = 0 'In this case reset the trackbar'
        End If

        Try
            ListBox1.SetSelected(0, True)
            com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
            com(0).WriteLine(Pump1.State.ToString & "1011") ' The 1,01,1 here indicates that this is a pump(1) numbered 01 with digital control （1）
            ListBox1.SetSelected(0, False)
            com(0).Close()
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
            ListBox1.SetSelected(0, True)
            com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
            com(0).WriteLine(Pump2.State.ToString & "1021") ' The 1,02,1 here indicates that this is a pump(1) numbered 02 with digital control （1）
            ListBox1.SetSelected(0, False)
            com(0).Close()
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
            ListBox1.SetSelected(0, True)
            com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
            com(0).WriteLine(Pump3.State.ToString & "1031") ' The 1,03,1 here indicates that this is a pump(1) numbered 03 with digital control （1）
            ListBox1.SetSelected(0, False)
            com(0).Close()
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
            ListBox1.SetSelected(0, True)
            com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
            com(0).WriteLine(Pump4.State.ToString & "1041") ' The 1,04,1 here indicates that this is a pump(1) numbered 04 with digital control （1）
            ListBox1.SetSelected(0, False)
            com(0).Close()
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
            ListBox1.SetSelected(0, True)
            com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
            com(0).WriteLine(Pump5.State.ToString & "1051") ' The 1,05,1 here indicates that this is a pump(1) numbered 05 with digital control （1）
            ListBox1.SetSelected(0, False)
            com(0).Close()
        Catch
        End Try
        TextBox5.Text = "Digital" & ", " & Pump5.StateStr
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
                com(0).WriteLine(Pump1.Voltage.ToString & "1012") ' The 1,01,2 here indicates that this is a pump(1) numbered 01 with analog control (2)
                ListBox1.SetSelected(0, False)
                com(0).Close()
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
                ListBox1.SetSelected(0, True)
                com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
                com(0).WriteLine(Pump2.Voltage.ToString & "1022") ' The 1,02,2 here indicates that this is a pump(1) numbered 02 with analog control (2)
                ListBox1.SetSelected(0, False)
                com(0).Close()
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
                ListBox1.SetSelected(0, True)
                com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
                com(0).WriteLine(Pump3.Voltage.ToString & "1032") ' The 1,03,2 here indicates that this is a pump(1) numbered 03 with analog control (2)
                ListBox1.SetSelected(0, False)
                com(0).Close()
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
                ListBox1.SetSelected(0, True)
                com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
                com(0).WriteLine(Pump4.Voltage.ToString & "1042") ' The 1,04,2 here indicates that this is a pump(1) numbered 04 with analog control (2)
                ListBox1.SetSelected(0, False)
                com(0).Close()
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
                ListBox1.SetSelected(0, True)
                com(0) = My.Computer.Ports.OpenSerialPort(ListBox1.SelectedItem.ToString)
                com(0).WriteLine(Pump5.Voltage.ToString & "1052") ' The 1,05,2 here indicates that this is a pump(1) numbered 05 with analog control (2)
                ListBox1.SetSelected(0, False)
                com(0).Close()
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

End Class

