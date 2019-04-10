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

    'Output string definition: ABCD - A refers to the type (1 for pump, 2 for valve, ...) B and C refers to the device number (starting from 01)
    'D refers to the control type (1 for digital and 2 for analog for pump or flash for valve)

    'Buttons 1 - 5 are for pumps, ON/OFF operation'
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Pump_OnOff(Pump1, Button1, TrackBar1, TextBox1, InputPort1, "1011")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call Pump_OnOff(Pump2, Button2, TrackBar2, TextBox2, InputPort1, "1021")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call Pump_OnOff(Pump3, Button3, TrackBar3, TextBox3, InputPort1, "1031")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call Pump_OnOff(Pump4, Button4, TrackBar4, TextBox4, InputPort1, "1041")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call Pump_OnOff(Pump5, Button5, TrackBar5, TextBox5, InputPort1, "1051")
    End Sub

    'Buttons 6 - 17 are for solenoid valves,each 2(e.x. 6 and 7) for 1 valve's ON/OFF and flash operation, respectively'
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click 'Stomach Acid, On/Off'
        Call Valve_ONOFF(Valve1, InputPort1, TextBox6, "2011")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click 'Stomach Acid, flash'
        Call Valve_Flash(Valve1, InputPort1, TextBox6, "2012")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click 'Stomach Base, On/Off'
        Call Valve_ONOFF(Valve2, InputPort1, TextBox7, "2021")
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click 'Stomach Base, flash'
        Call Valve_Flash(Valve2, InputPort1, TextBox7, "2022")
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click 'Small Intestine Acid, On/Off'
        Call Valve_ONOFF(Valve3, InputPort1, TextBox8, "2031")
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click 'Small Intestine Acid, flash'
        Call Valve_Flash(Valve3, InputPort1, TextBox8, "2032")
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click 'Small Intestine Base, On/Off'
        Call Valve_ONOFF(Valve4, InputPort1, TextBox9, "2041")
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click 'Small Intestine Base, flash'
        Call Valve_Flash(Valve4, InputPort1, TextBox9, "2042")
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click 'Colon Acid, On/Off'
        Call Valve_ONOFF(Valve5, InputPort1, TextBox10, "2051")
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click 'Colon Acid, flash'
        Call Valve_Flash(Valve5, InputPort1, TextBox10, "2052")
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click 'Colon Base, On/Off'
        Call Valve_ONOFF(Valve6, InputPort1, TextBox11, "2061")
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click 'Colon Base, flash'
        Call Valve_Flash(Valve6, InputPort1, TextBox11, "2062")
    End Sub

    'Trackbar 1 - 5 are for pumps, analog operation'
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        'scroll bar for pump 1'
        Call Pump_Scroll(Pump1, TrackBar1, TextBox1, InputPort1, "1012")
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        'scroll bar for pump 2'
        Call Pump_Scroll(Pump2, TrackBar2, TextBox2, InputPort1, "1022")
    End Sub

    Private Sub TrackBar3_Scroll(sender As Object, e As EventArgs) Handles TrackBar3.Scroll
        'scroll bar for pump 3'
        Call Pump_Scroll(Pump3, TrackBar3, TextBox3, InputPort1, "1032")
    End Sub

    Private Sub TrackBar4_Scroll(sender As Object, e As EventArgs) Handles TrackBar4.Scroll
        'scroll bar for pump 4'
        Call Pump_Scroll(Pump4, TrackBar4, TextBox4, InputPort1, "1042")
    End Sub

    Private Sub TrackBar5_Scroll(sender As Object, e As EventArgs) Handles TrackBar5.Scroll
        'scroll bar for pump 5'
        Call Pump_Scroll(Pump5, TrackBar5, TextBox5, InputPort1, "1052")
    End Sub


    Private Sub Initialization_Click(sender As Object, e As EventArgs) Handles Initialization.Click 'Initialize!'
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
            Timer1.Interval = 100 * CInt(Sensor_Time_Interval.Text)
            FlashTime = CInt(Valve_Flash_Time.Text)
        Catch
            MsgBox("Check your input values again. Defaulted to 1")
            Timer1.Interval = 100
            FlashTime = 1
            Sensor_Time_Interval.Text = 1
            Valve_Flash_Time.Text = 1
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
        Catch 'if someone clicked it twice, remove the extra instances of port
            MsgBox("Updated!")
            For i = 0 To (portCount - 1)
                comlist.Items.RemoveAt(i)
            Next
        End Try



        'Confirm that the form is initialized'
        If InitializeStatus = 1 Then
            TextBox15.Text = "Initialized!"
            Initialization.Text = "Update"
        Else
            TextBox15.Text = "Updated!"
        End If
    End Sub



    Private Sub Valve_Flash_Time_LostFocus(sender As Object, e As EventArgs) Handles Valve_Flash_Time.LostFocus
        'Check if the input number is between 0 - 9
        Try
            If (CInt(Valve_Flash_Time.Text) > 9 Or CInt(Valve_Flash_Time.Text) < 0) Then
                MsgBox("Flash time should be a number between 0 to 9!")
                Valve_Flash_Time.Text = ""
                Me.ActiveControl = Valve_Flash_Time
            End If
        Catch
            If (Not Valve_Flash_Time.Text = "") Then
                MsgBox("That...was not a number...")
                Valve_Flash_Time.Text = ""
                Me.ActiveControl = Valve_Flash_Time
            Else
                Valve_Flash_Time.Text = "0"
            End If
        End Try
    End Sub

    Private Sub Sensor_Time_Interval_LostFocus(sender As Object, e As EventArgs) Handles Sensor_Time_Interval.LostFocus
        'Check if the input is a number
        Try
            If IsNumeric(Valve_Flash_Time.Text) = False Then
                MsgBox("Sensor reading interval should be a number!")
                Valve_Flash_Time.Text = ""
                Me.ActiveControl = Valve_Flash_Time
            End If
        Catch
            If (Not Valve_Flash_Time.Text = "") Then
            Else
                Valve_Flash_Time.Text = "10"
            End If
        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Working for now, but Readports need to be adjusted'
        Call Plot_Chart(Sensor1, ReadPort1, StomachPH, TextBox18)
        Call Plot_Chart(Sensor2, ReadPort2, SmallIntestinePH, TextBox19)
        Call Plot_Chart(Sensor3, ReadPort3, ColonPH, TextBox20)
    End Sub

    'The following buttons handle the three charts'
    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        'Button for enabling Stomach PH monitor'
        Call Plot_ONOFF(Sensor1, Button25)
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        'Rest the chart'
        Call Plot_Reset(Sensor1, TextBox18, StomachPH)
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        'Button for enabling Small Intestione PH monitor'
        Call Plot_ONOFF(Sensor2, Button26)
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        'Rest the chart'
        Call Plot_Reset(Sensor2, TextBox19, SmallIntestinePH)
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        'Button for enabling Colon PH monitor'
        Call Plot_ONOFF(Sensor3, Button27)
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        'Rest the chart'
        Call Plot_Reset(Sensor3, TextBox20, ColonPH)
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Functions
    Function PH_Calculations(ByVal readings) As Double
        'This is the function to calculate PH from sensor readings
        'Pending for actual calculations'
        PH_Calculations = readings / 1024 * 5 * 1000
    End Function

    Function Pump_Scroll(input_pump As Pump, input_scroll As TrackBar, output_textbox As TextBox, output_port As System.IO.Ports.SerialPort, output_text As String) As Boolean
        'This function handles the Trackbar of each pump'
        If (input_pump.State = 1) Then
            input_pump.Voltage = input_scroll.Value
            Try
                output_port.WriteLine(input_pump.Voltage.ToString & output_text) 'The output_text is the device specific code to the serial and Arduino'
            Catch
            End Try
            'Now convert the voltage to real output voltage to print'
            input_pump.Voltage = Format(input_pump.Voltage / 9 * 100, 0.0)
            input_pump.StateStr = input_pump.Voltage.ToString
            output_textbox.Text = "Analog" & ", " & input_pump.Voltage.ToString & "%"
        Else
            MsgBox("Pump not on!")
            input_scroll.Value = 0
        End If
        Pump_Scroll = True
    End Function

    Function Pump_OnOff(input_pump As Pump, input_button As Button, input_scroll As TrackBar, output_textbox As TextBox, output_port As System.IO.Ports.SerialPort, output_text As String)
        'This function handles the On/Off of each pump'
        If (input_pump.State = 0) Then
            input_pump.State += 1
        Else
            input_pump.State *= 0
        End If

        If (input_pump.State = 1) Then
            input_pump.StateStr = "On"
        Else
            input_pump.StateStr = "Off"
            input_scroll.Value = 0 'In this case reset the trackbar'
        End If

        Try
            output_port.WriteLine(input_pump.State.ToString & output_text) 'The output_text is the device specific code to the serial and Arduino'
        Catch
        End Try
        output_textbox.Text = "Digital" & ", " & input_pump.StateStr
        Pump_OnOff = True
    End Function

    Function Valve_ONOFF(input_valve As Valve, input_port As System.IO.Ports.SerialPort, output_textbox As TextBox, output_text As String)
        'This function handles the On/Off button of each valve'
        If (input_valve.State = 0) Then
            input_valve.State += 1
        Else
            input_valve.State *= 0
        End If

        If (input_valve.State = 1) Then
            input_valve.StateStr = "On"
        Else
            input_valve.StateStr = "Off"
        End If

        Try
            input_port.WriteLine(input_valve.State.ToString & output_text) 'The output_text is the device specific code to the serial and Arduino'
        Catch
        End Try
        output_textbox.Text = input_valve.StateStr
        Valve_ONOFF = True
    End Function

    Function Valve_Flash(input_valve As Valve, input_port As System.IO.Ports.SerialPort, output_textbox As TextBox, output_text As String)
        'This function handles the flash button of each valve'
        If (input_valve.State = 0) Then
            Try
                input_port.WriteLine(FlashTime.ToString & output_text) 'The output_text is the device specific code to the serial and Arduino'
            Catch
            End Try
            input_valve.StateStr = CDbl(FlashTime.ToString) * 0.5
            output_textbox.Text = input_valve.StateStr & "s"
        Else
            MsgBox("Valve already on!")
        End If
        Valve_Flash = True
    End Function

    Function Plot_Chart(input_sensor As pHsensor, input_port As System.IO.Ports.SerialPort, output_chart As System.Windows.Forms.DataVisualization.Charting.Chart, output_textbox As TextBox)
        'This function plots the computed reading from sensors into charts
        If input_sensor.Status = 1 Then
            input_sensor.Timer += 1
            Try
                input_sensor.Reading = PH_Calculations(input_port.ReadLine)
                output_chart.Series(0).Points.AddXY(input_sensor.Timer.ToString, input_sensor.Reading.ToString)
                If output_chart.Series(0).Points.Count = 20 Then
                    output_chart.Series(0).Points.RemoveAt(0)
                End If
                output_textbox.Text = input_sensor.Reading.ToString
            Catch
            End Try
        End If
        Plot_Chart = True
    End Function

    Function Plot_ONOFF(input_sensor As pHsensor, input_button As Button)
        If input_sensor.Status = 1 Then
            input_button.Text = "Enable"
            input_sensor.Status = 0
        Else
            input_button.Text = "Disable"
            input_sensor.Status = 1
        End If
        Plot_ONOFF = True
    End Function

    Function Plot_Reset(input_sensor As pHsensor, output_textbox As TextBox, output_chart As System.Windows.Forms.DataVisualization.Charting.Chart)
        'Rest the chart'
        input_sensor.Timer = 0
        output_chart.Series(0).Points.Clear()
        output_textbox.Text = ""
        Plot_Reset = True
    End Function

    Private Sub Random_Testing_Click(sender As Object, e As EventArgs) Handles Random_Testing.Click
        'For some random tests'
        MsgBox("a")
    End Sub

End Class



