Imports System.Windows.Forms
Public Class Form
    'Declarations'
    Dim InitializeStatus As Integer
    'Pumps'
    Dim Pump1 As New Pump()
    Dim Pump2 As New Pump()
    Dim Pump3 As New Pump()
    Dim Pump4 As New Pump()
    Dim Pump5 As New Pump()
    Dim Pump6 As New Pump()
    Dim Pump7 As New Pump()
    Dim Pump8 As New Pump()
    Dim Pump9 As New Pump()
    Dim Pump10 As New Pump()
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
    Dim Sensor4 As New pHsensor()
    Dim Sensor5 As New pHsensor()
    'Serial Ports'
    Dim com(4) As IO.Ports.SerialPort
    Dim portCount As Integer
    'For choosing sensors to read'
    Dim SensorNumber As Integer = 1
    Dim ArduinoReading As String ' This is the immediate reading (including sensor handle) from Arduino
    Dim VoltageReading As Integer ' This is the actual voltage reading from Arduino
    Dim tick As Integer = 0 'For timer 1'
    Dim tick2 As Integer = 0 'for timer 2'
    'Random stuff for troubleshooting'
    Dim a As Integer

    Private Sub Base_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox15.Text = "Not initialized!"
        InitializeStatus = 0
    End Sub

    'Output string definition: ABCD - A refers to the type (1 for pump, 2 for valve, ...) B and C refers to the device number (starting from 01)
    'D refers to the control type (1 for digital and 2 for analog for pump or flash for valve)

    'Buttons 1 - 5, 33 - 37 are for pumps, ON/OFF operation'
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Pump_OnOff(Pump1, Button1, TrackBar1, TextBox1, OutputPort1, "1011")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call Pump_OnOff(Pump2, Button2, TrackBar2, TextBox2, OutputPort1, "1021")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call Pump_OnOff(Pump3, Button3, TrackBar3, TextBox3, OutputPort1, "1031")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call Pump_OnOff(Pump4, Button4, TrackBar4, TextBox4, OutputPort1, "1041")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call Pump_OnOff(Pump5, Button5, TrackBar5, TextBox5, OutputPort1, "1051")
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Call Pump_OnOff(Pump6, Button37, TrackBar10, TextBox26, OutputPort1, "1061")
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Call Pump_OnOff(Pump7, Button36, TrackBar9, TextBox25, OutputPort1, "1071")
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Call Pump_OnOff(Pump8, Button35, TrackBar8, TextBox24, OutputPort1, "1081")
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Call Pump_OnOff(Pump9, Button34, TrackBar7, TextBox23, OutputPort1, "1091")
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Call Pump_OnOff(Pump10, Button33, TrackBar6, TextBox22, OutputPort1, "1101")
    End Sub

    'Buttons 6 - 17 are for solenoid valves,each 2(e.x. 6 and 7) for 1 valve's ON/OFF and flash operation, respectively'
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click 'Stomach Acid, On/Off'
        Call Valve_ONOFF(Valve1, OutputPort1, TextBox6, "2011")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click 'Stomach Acid, flash'
        Call Valve_Flash(Valve1, OutputPort1, TextBox6, "2012")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click 'Stomach Base, On/Off'
        Call Valve_ONOFF(Valve2, OutputPort1, TextBox7, "2021")
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click 'Stomach Base, flash'
        Call Valve_Flash(Valve2, OutputPort1, TextBox7, "2022")
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click 'Small Intestine Acid, On/Off'
        Call Valve_ONOFF(Valve3, OutputPort1, TextBox8, "2031")
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click 'Small Intestine Acid, flash'
        Call Valve_Flash(Valve3, OutputPort1, TextBox8, "2032")
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click 'Small Intestine Base, On/Off'
        Call Valve_ONOFF(Valve4, OutputPort1, TextBox9, "2041")
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click 'Small Intestine Base, flash'
        Call Valve_Flash(Valve4, OutputPort1, TextBox9, "2042")
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click 'Colon Acid, On/Off'
        Call Valve_ONOFF(Valve5, OutputPort1, TextBox10, "2051")
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click 'Colon Acid, flash'
        Call Valve_Flash(Valve5, OutputPort1, TextBox10, "2052")
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click 'Colon Base, On/Off'
        Call Valve_ONOFF(Valve6, OutputPort1, TextBox11, "2061")
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click 'Colon Base, flash'
        Call Valve_Flash(Valve6, OutputPort1, TextBox11, "2062")
    End Sub

    'Trackbar 1 - 10 are for pumps, analog operation'
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        'scroll bar for pump 1'
        Call Pump_Scroll(Pump1, TrackBar1, TextBox1, OutputPort1, "1012")
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        'scroll bar for pump 2'
        Call Pump_Scroll(Pump2, TrackBar2, TextBox2, OutputPort1, "1022")
    End Sub

    Private Sub TrackBar3_Scroll(sender As Object, e As EventArgs) Handles TrackBar3.Scroll
        'scroll bar for pump 3'
        Call Pump_Scroll(Pump3, TrackBar3, TextBox3, OutputPort1, "1032")
    End Sub

    Private Sub TrackBar4_Scroll(sender As Object, e As EventArgs) Handles TrackBar4.Scroll
        'scroll bar for pump 4'
        Call Pump_Scroll(Pump4, TrackBar4, TextBox4, OutputPort1, "1042")
    End Sub

    Private Sub TrackBar5_Scroll(sender As Object, e As EventArgs) Handles TrackBar5.Scroll
        'scroll bar for pump 5'
        Call Pump_Scroll(Pump5, TrackBar5, TextBox5, OutputPort1, "1052")
    End Sub

    Private Sub TrackBar6_Scroll(sender As Object, e As EventArgs) Handles TrackBar6.Scroll
        'scroll bar for pump 10'
        Call Pump_Scroll(Pump10, TrackBar6, TextBox22, OutputPort1, "1102")
    End Sub

    Private Sub TrackBar7_Scroll(sender As Object, e As EventArgs) Handles TrackBar7.Scroll
        'scroll bar for pump 9'
        Call Pump_Scroll(Pump9, TrackBar7, TextBox23, OutputPort1, "1092")
    End Sub

    Private Sub TrackBar8_Scroll(sender As Object, e As EventArgs) Handles TrackBar8.Scroll
        'scroll bar for pump 8'
        Call Pump_Scroll(Pump8, TrackBar8, TextBox24, OutputPort1, "1082")
    End Sub

    Private Sub TrackBar9_Scroll(sender As Object, e As EventArgs) Handles TrackBar9.Scroll
        'scroll bar for pump 7'
        Call Pump_Scroll(Pump7, TrackBar9, TextBox25, OutputPort1, "1072")
    End Sub

    Private Sub TrackBar10_Scroll(sender As Object, e As EventArgs) Handles TrackBar10.Scroll
        'scroll bar for pump 6'
        Call Pump_Scroll(Pump6, TrackBar10, TextBox26, OutputPort1, "1062")
    End Sub

    Private Sub Initialization_Click(sender As Object, e As EventArgs) Handles Initialization.Click 'Initialize!'
        'This is the initialization button which initializes all variables'
        Pump1.Name = "Food Media"
        Pump2.Name = "Pancreatic"
        Pump3.Name = "Transfer 1"
        Pump4.Name = "Transfer 2"
        Pump5.Name = "Transfer 3"
        Pump6.Name = "Transfer 4"
        Pump7.Name = "Transfer 5"
        Pump8.Name = "Transfer 6"
        Pump9.Name = "Transfer 7"
        Pump10.Name = "Transfer 8"
        Valve1.Name = "Stomach Acid"
        Valve2.Name = "Stomach Base"
        Valve3.Name = "Small Intestine Acid"
        Valve4.Name = "Small Intestine Base"
        Valve5.Name = "Colon1 Acid"
        Valve6.Name = "Colon1 Base"
        Sensor1.Name = "Stomach PH"
        Sensor2.Name = "Small Intestine PH"
        Sensor3.Name = "Colon PH"

        portCount = 0

        If IsNumeric(Sensor_Time_Interval.Text) = True And IsNumeric(Valve_Flash_Time.Text) Then
            Timer1.Interval = 50 * CInt(Sensor_Time_Interval.Text)
            FlashTime = CInt(Valve_Flash_Time.Text)
        Else
            MsgBox("Check your input values again. Defaulted to 1")
            Timer1.Interval = 50
            FlashTime = 1
            Sensor_Time_Interval.Text = 1
            Valve_Flash_Time.Text = 1
        End If

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
            ReadPort1 = My.Computer.Ports.OpenSerialPort("COM6")
            OutputPort1 = My.Computer.Ports.OpenSerialPort("COM4")
            'Add further ports here, this better be done manually because ports aren't always loaded in the same order'
            InitializeStatus += 1 'Check if this is initialization, or update
            'Confirm that the form is initialized'
            If InitializeStatus = 1 Then
                TextBox15.Text = "Initialized!"
                Initialization.Text = "Initialized, don't click again!"
            End If
        Catch 'if someone clicked it twice, remove the extra instances of port
            MsgBox("Initialization Error!" & vbCrLf & "Restart the program!")
        End Try
    End Sub



    Private Sub Valve_Flash_Time_LostFocus(sender As Object, e As EventArgs) Handles Valve_Flash_Time.LostFocus
        'Check if the input number is between 0 - 9
        If IsNumeric(Valve_Flash_Time.Text) = True Then
            If (CInt(Valve_Flash_Time.Text) > 9 Or CInt(Valve_Flash_Time.Text) < 0) Then
                MsgBox("Flash time should be a number between 0 to 9!")
                Valve_Flash_Time.Text = ""
                Me.ActiveControl = Valve_Flash_Time
            End If
        Else
            If (Not Valve_Flash_Time.Text = "") Then
                MsgBox("That...was not a number...")
                Valve_Flash_Time.Text = ""
                Me.ActiveControl = Valve_Flash_Time
            Else
                Valve_Flash_Time.Text = "0"
            End If
        End If
    End Sub

    Private Sub Sensor_Time_Interval_LostFocus(sender As Object, e As EventArgs) Handles Sensor_Time_Interval.LostFocus
        'Check if the input is a number
        Try
            If IsNumeric(Sensor_Time_Interval.Text) = False Then
                MsgBox("Sensor reading interval should be a number!")
                Sensor_Time_Interval.Text = ""
                Me.ActiveControl = Sensor_Time_Interval
            End If
        Catch
            If (Not Sensor_Time_Interval.Text = "") Then
            Else
                Sensor_Time_Interval.Text = "10"
            End If
        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Working for now, but Readports need to be adjusted'
        'This timer deals with reading voltage from PH sensors and plotting them etc'
        tick += 1
        If InitializeStatus = 1 Then
            Try
                If IsNumeric(ReadPort1.ReadLine.ToString) = True Then
                    Try
                        ArduinoReading = ReadPort1.ReadLine.ToString
                        SensorNumber = CLng(ArduinoReading.Substring(0, 1))
                        VoltageReading = CLng(ArduinoReading.Substring(1))
                        Select Case SensorNumber
                            Case 1
                                Call Plot_Chart(Sensor1, ReadPort1, StomachPH, TextBox18, VoltageReading, 0)
                                Call Plot_Chart(Sensor1, ReadPort1, StomachPHAuto, TextBox16, VoltageReading, 0)
                            Case 2
                                Call Plot_Chart(Sensor2, ReadPort1, SmallIntestinePH, TextBox19, VoltageReading, 0)
                                Call Plot_Chart(Sensor2, ReadPort1, SmallIntestinePHAuto, TextBox17, VoltageReading, 0)
                            Case 3
                                Call Plot_Chart(Sensor3, ReadPort1, ColonPH, TextBox20, VoltageReading, 0)
                                Call Plot_Chart(Sensor3, ReadPort1, ColonPHAuto, TextBox21, VoltageReading, 0)
                            Case 4
                                Call Plot_Chart(Sensor4, ReadPort1, Colon2PH, TextBox27, VoltageReading, 0)
                            'Call Plot_Chart(Sensor4, ReadPort1, Colon2PHAuto, TextBox27, VoltageReading, 0)
                            Case 5
                                Call Plot_Chart(Sensor5, ReadPort1, Colon3PH, TextBox28, VoltageReading, 0)
                                'Call Plot_Chart(Sensor3, ReadPort1, Colon3PHAuto, TextBox28, VoltageReading, 0)
                        End Select
                    Catch
                        ArduinoReading = ""
                        SensorNumber = 0
                        VoltageReading = 0
                    End Try
                End If
            Catch
            End Try
        End If
        If tick Mod 50 = 0 Then
            Try
                ReadPort1.DiscardInBuffer() 'This is VERY important! Otherwise readings accumulate in the buffer and you dont get actual real-time reading
                tick = 0
            Catch
            End Try
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'This timer deals with controller and automatic adjustment of PH'
        tick2 += 1
        If tick2 Mod 5 = 0 Then
            Try
                Select Case SensorNumber
                    Case 1
                        If Sensor1.AutoControlStatus = 1 Then
                            If IsNumeric(Setpoint1_Textbox.Text) = False Then
                                MsgBox("Setpoint error!" & vbCrLf & "Defaulted to 7")
                                Setpoint1_Textbox.Text = "7.0"
                            End If
                            Call PID_Controller(VoltageReading, CDbl(Setpoint1_Textbox.Text), Pump1, Pump2, -0.2, 0.2) 'Temporary, for testing only                           
                            'Implement pump discharge
                            Call Pump_Auto_Control(Pump1, OutputPort1, "1011")
                            Call Pump_Auto_Control(Pump2, OutputPort1, "1021")
                        End If
                    Case 2

                    Case 3

                    Case 4

                    Case 5

                End Select
            Catch
            End Try
        End If

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

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        'Button for enabling Stomach PH monitor for auto control'
        Call Plot_ONOFF(Sensor1, Button24)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        'Button for enabling Small Intestine PH monitor for auto control'
        Call Plot_ONOFF(Sensor2, Button31)
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        'Button for enabling Colon PH monitor for auto control'
        Call Plot_ONOFF(Sensor3, Button32)
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        'Button for auto PH control for PH sensor 1'
        If Sensor1.AutoControlStatus = 0 Then
            Sensor1.AutoControlStatus = 1
            Button42.Text = "Disable Controller"
        Else
            Sensor1.AutoControlStatus = 0
            Button42.Text = "Enable Controller"
        End If
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Functions
    Function PH_Calculations(ByVal readings) As Double
        'This is the function to calculate PH from sensor readings
        Dim ActualVoltage As Double
        ActualVoltage = readings / 1024 * 5 * 1000
        PH_Calculations = (620 - ActualVoltage) / 55 'This is subject to change
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

    Function Plot_Chart(input_sensor As pHsensor, input_port As System.IO.Ports.SerialPort, output_chart As System.Windows.Forms.DataVisualization.Charting.Chart, output_textbox As TextBox, input_number As Long, serie_number As Integer)
        'This function plots the computed reading from sensors into charts
        If input_sensor.PlotStatus = 1 Then
            input_sensor.Timer += 1
            Try
                input_sensor.Reading = PH_Calculations(input_number) 'Note that here Arduino directly sent a number, not a string
                output_chart.Series(serie_number).Points.AddXY(input_sensor.Timer.ToString, input_sensor.Reading.ToString)
                If output_chart.Series(serie_number).Points.Count = 20 Then
                    output_chart.Series(serie_number).Points.RemoveAt(0)
                End If
                output_textbox.Text = input_sensor.Reading.ToString
            Catch
            End Try
        End If
        Plot_Chart = True
    End Function

    Function Plot_ONOFF(input_sensor As pHsensor, input_button As Button)
        If input_sensor.PlotStatus = 1 Then
            input_button.Text = "Enable"
            input_sensor.PlotStatus = 0
        Else
            input_button.Text = "Disable"
            input_sensor.PlotStatus = 1
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

    Function PID_Controller(input_voltage As Double, setpoint As Double, output_pump_acid As Pump, output_pump_base As Pump, tolerance_acid As Double, tolerance_base As Double)
        'This PID controller works with pumps, but to use solenoid valves you just need to switch the pump here to valves'
        'Currently it only works with P, because with ON/OFF control PID doesnt really make any sense anyways...'
        'Here setpoint should be in actual PH instead of voltage'
        Dim errorterm As Double
        errorterm = setpoint - PH_Calculations(input_voltage)
        If errorterm < tolerance_acid Then
            output_pump_acid.State = 1
        Else
            output_pump_acid.State = 0
        End If
        If errorterm > tolerance_base Then
            output_pump_base.State = 1
        Else
            output_pump_base.State = 0
        End If
        PID_Controller = True
    End Function

    Function Pump_Auto_Control(input_pump As Pump, output_port As System.IO.Ports.SerialPort, output_text As String)
        'This is to implement a pump discharge/stop command generated by PID controller
        If (input_pump.State = 1) Then
            input_pump.StateStr = "On"
        Else
            input_pump.StateStr = "Off"
        End If
        Try
            output_port.WriteLine(input_pump.State.ToString & output_text) 'The output_text is the device specific code to the serial and Arduino'
        Catch
        End Try
        Pump_Auto_Control = True
    End Function


    Private Sub Random_Testing_Click(sender As Object, e As EventArgs) Handles Random_Testing.Click
        'For some random tests'
        MsgBox((CInt(TestTextbox.Text.Substring(1)) - 5).ToString)
    End Sub


End Class



