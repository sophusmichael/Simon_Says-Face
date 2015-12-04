'created by Rui Santos, http://randomnerdtutorials.wordpress.com, 2013
'modified by Laura Boccanfuso, Lisa Chen and Colette Torres 6/2015
'Control a servo motor with Visual Basic 

Imports System.Speech.Synthesis
Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form1

    Shared _continue As Boolean
    Shared _serialPort As SerialPort
    WithEvents speaker As New SpeechSynthesizer()
    Public Event VisemeReached As EventHandler(Of VisemeReachedEventArgs)
    Public Event SpeakCompleted As EventHandler(Of SpeakCompletedEventArgs)
    Dim stop_Clicked As Boolean = False
    'Dim promptBuilder As PromptBuilder
    'Dim returnValue As Prompt

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SerialPort1.Close()
        SerialPort1.PortName = "COM6" 'define your port
        SerialPort1.BaudRate = 9600
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default
        Thread.Sleep(1000)
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True


    End Sub

    Private Sub neutral_Click(sender As System.Object, e As System.EventArgs) Handles neutral.Click, Timer3.Tick
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
    End Sub

    Private Sub smile_Click_1(sender As Object, e As EventArgs) Handles smile.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("0")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub sad_Click(sender As Object, e As EventArgs) Handles frown.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("1")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub blink_Click(sender As Object, e As EventArgs) Handles blink.Click, Timer1.Tick
        SerialPort1.Open()
        SerialPort1.Write("3")
        SerialPort1.Close()
    End Sub

    Private Sub SpeakFritz_Click(sender As Object, e As EventArgs)
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Hello! I'm going to try to say something and move my mouth at the same time!! How am I doing??  After a few sentences, does my mouth keep up with my words??"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)

    End Sub

    Private Sub speaker_VisemeReached(sender As Object, e2 As System.Speech.Synthesis.VisemeReachedEventArgs) Handles speaker.VisemeReached
        Console.WriteLine("Viseme " & e2.Viseme & " was " & e2.Duration.TotalMilliseconds.ToString & " ms. long" & vbNewLine)

        '0:      silence()
        '1:      ae, ax, ah
        '2:      aa()
        '3:      ao()
        '4:      ey, eh, uh
        '5:      er()
        '6:      y, iy, ih, ix
        '7:      w, uw4
        '8:      ow()
        '9:      aw()
        '10:     oy()
        '11:     ay()
        '12:     h()
        '13:     r()
        '14:     l()
        '15:     s, z
        '16:     sh, ch, jh, zh
        '17:     th, dh
        '18:     f, v
        '19:     d, t, n
        '20:     k, g, ng
        '21:     p, b, m
        Try
            If (e2.Viseme = 1 Or e2.Viseme = 2 Or e2.Viseme = 9 Or e2.Viseme = 8) Then
                SerialPort1.Open()
                SerialPort1.Write("4")
                SerialPort1.Close()
            ElseIf (e2.Viseme = 3 Or e2.Viseme = 6 Or e2.Viseme = 10) Then
                SerialPort1.Open()
                SerialPort1.Write("5")
                SerialPort1.Close()
            ElseIf (e2.Viseme = 4 Or e2.Viseme = 5 Or e2.Viseme = 7 Or e2.Viseme = 20) Then
                SerialPort1.Open()
                SerialPort1.Write("6")
                SerialPort1.Close()
            Else
                SerialPort1.Open()
                SerialPort1.Write("7")
                SerialPort1.Close()
            End If
        Catch e As Exception
            Console.WriteLine("exception caught")
        End Try




    End Sub

    Private Sub EyeRight_Click(sender As Object, e As EventArgs) Handles EyeRight.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("8")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        EyeRight.BackColor = Color.Gold
    End Sub

    Private Sub EyeLeft_Click(sender As Object, e As EventArgs) Handles EyeLeft.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("9")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        EyeLeft.BackColor = Color.Gold
    End Sub

    Private Sub StopButton_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        stop_Clicked = True
        speaker.SpeakAsyncCancelAll()
        SerialPort1.Open()
        SerialPort1.Write("AA")
        SerialPort1.Close()
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
    End Sub

    Private Sub HeadLeft_Click(sender As Object, e As EventArgs) Handles HeadLeft.Click, Timer2.Tick
        SerialPort1.Open()
        SerialPort1.Write("A")
        SerialPort1.Close()
        HeadLeft.BackColor = Color.Gold
    End Sub

    Private Sub HeadRight_Click(sender As Object, e As EventArgs) Handles HeadRight.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("B")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        HeadRight.BackColor = Color.Gold
    End Sub
    Private Sub Oh_Click(sender As Object, e As EventArgs) Handles Oh.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Oh"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Oh.BackColor = Color.Gold
    End Sub

    Private Sub Okay_Click(sender As Object, e As EventArgs) Handles Okay.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Okay..."
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Okay.BackColor = Color.Gold
    End Sub

    Private Sub Wow_Click(sender As Object, e As EventArgs) Handles Wow.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Wow."
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Wow.BackColor = Color.Gold
    End Sub

    Private Sub Interesting_Click(sender As Object, e As EventArgs) Handles Interesting.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Interesting....."
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Interesting.BackColor = Color.Gold
    End Sub

    Private Sub Cool_Click(sender As Object, e As EventArgs) Handles Cool.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Cool"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Cool.BackColor = Color.Gold
    End Sub
    Private Sub Nice_Click(sender As Object, e As EventArgs) Handles Nice.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Nice"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Nice.BackColor = Color.Gold
    End Sub
    Private Sub Yeah_Click(sender As Object, e As EventArgs) Handles Yeah.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Yeah"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Yeah.BackColor = Color.Gold
    End Sub
    Private Sub Uhuh_Click(sender As Object, e As EventArgs) Handles Uhuh.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Uhhhhh huh...."
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Uhuh.BackColor = Color.Gold
    End Sub
    Private Sub Hello_Click(sender As Object, e As EventArgs) Handles Hello.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Hello"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Hi_Click(sender As Object, e As EventArgs) Handles Hi.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Hi, my name is Fritz"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    'Private Sub Yawn_Click(sender As Object, e As EventArgs) Handles Yawn.Click
    '    Timer1.Enabled = False
    '    SerialPort1.Open()
    '    SerialPort1.Write("O")
    '    SerialPort1.Close()
    '    My.Computer.Audio.Play(My.Resources.Elves, AudioPlayMode.WaitToComplete)

    'End Sub

    'Private Sub Burp_Click(sender As Object, e As EventArgs) Handles Burp.Click
    '    Timer1.Enabled = False
    '    SerialPort1.Open()
    '    SerialPort1.Write("P")
    '    SerialPort1.Close()
    '    My.Computer.Audio.Play(My.Resources.Elves, AudioPlayMode.WaitToComplete)
    'End Sub

    'Private Sub NotAgain_Click(sender As Object, e As EventArgs) Handles NotAgain.Click
    '    Timer1.Enabled = False
    '    Timer2.Enabled = False
    '    Timer3.Enabled = False

    '    Dim string2say As String
    '    Dim string2say2 As String
    '    Dim string2say3 As String
    '    Dim string2say4 As String
    '    Dim string2say5 As String

    '    string2say = "Hello. I’m good."
    '    string2say2 = "I went to my friend’s birthday party."
    '    string2say3 = "Yeah."
    '    string2say4 = "I went to a pool party and I swam and I ate pizza and cake."
    '    string2say5 = "Yeah. I played on the playground. The slide was very big."
    '    speaker.Rate = 0.1
    '    speaker.Volume = 100
    '    speaker.SelectVoice("IVONA 2 Ivy OEM")
    '    System.Threading.Thread.Sleep(2000)
    '    SerialPort1.Open()
    '    SerialPort1.Write("0")
    '    SerialPort1.Close()
    '    speaker.Speak(string2say)
    '    System.Threading.Thread.Sleep(1700)
    '    speaker.Speak(string2say2)
    '    SerialPort1.Open()
    '    SerialPort1.Write("2")
    '    SerialPort1.Close()
    '    System.Threading.Thread.Sleep(4000)
    '    SerialPort1.Open()
    '    SerialPort1.Write("3")
    '    SerialPort1.Close()
    '    speaker.Speak(string2say3)
    '    SerialPort1.Open()
    '    SerialPort1.Write("A")
    '    SerialPort1.Close()
    '    SerialPort1.Open()
    '    SerialPort1.Write("9")
    '    SerialPort1.Close()
    '    speaker.SpeakAsync(string2say4)
    '    SerialPort1.Open()
    '    SerialPort1.Write("T")
    '    SerialPort1.Close()
    '    SerialPort1.Open()
    '    SerialPort1.Write("R")
    '    SerialPort1.Close()
    '    speaker.Speak(string2say5)
    '    SerialPort1.Open()
    '    SerialPort1.Write("2")
    '    SerialPort1.Close()
    'End Sub

    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        TextBox1.Clear()
    End Sub

    Private Sub HeadUp_Click(sender As Object, e As EventArgs) Handles HeadUp.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("C")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        HeadUp.BackColor = Color.Gold
    End Sub

    Private Sub HeadDown_Click(sender As Object, e As EventArgs) Handles HeadDown.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("D")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        HeadUp.BackColor = Color.Gold
    End Sub

    'Private Sub Convo1_Click(sender As Object, e As EventArgs)
    '    Dim string2say As String
    '    string2say = "Hello. My name is Fritz. I am having a very good day."
    '    speaker.Rate = 0.1
    '    speaker.Volume = 100
    '    speaker.SelectVoice("IVONA 2 Ivy OEM")
    '    speaker.SpeakAsync(string2say)

    'End Sub

    Private Sub Wink_Click(sender As Object, e As EventArgs) Handles Wink.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("E")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Confused_Click(sender As Object, e As EventArgs) Handles Confused.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("F")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Surprised_Click(sender As Object, e As EventArgs) Handles Surprised.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("G")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Angry_Click(sender As Object, e As EventArgs) Handles Angry.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("H")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub speaker_SpeakCompleted(sender As Object, e2 As System.Speech.Synthesis.SpeakCompletedEventArgs) Handles speaker.SpeakCompleted
        If (stop_Clicked = True) Then
            Return
        Else
            Timer1.Enabled = True
            Timer2.Enabled = True
            Timer3.Enabled = True
        End If
    End Sub

    Private Sub CrossEyed_Click(sender As Object, e As EventArgs) Handles CrossEyed.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("I")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Awkward_Click(sender As Object, e As EventArgs) Handles Awkward.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("J")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub FunnyFace1_Click(sender As Object, e As EventArgs) Handles FunnyFace1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("K")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Afraid_Click(sender As Object, e As EventArgs) Handles Afraid.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("L")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Sleepy_Click(sender As Object, e As EventArgs) Handles Sleepy.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("M")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Yelling_Click(sender As Object, e As EventArgs) Handles Yelling.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("N")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Animated_Click(sender As Object, e As EventArgs) Handles Animated.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Funny2_Click(sender As Object, e As EventArgs) Handles Funny2.Click
        SerialPort1.Open()
        SerialPort1.Write("Q")
        SerialPort1.Close()
    End Sub


    Private Sub Script1_Click(sender As Object, e As EventArgs) Handles Script1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        Dim string2say2 As String
        Dim string2say3 As String
        Dim string2say4 As String
        Dim string2say5 As String
        Dim string2say6 As String
        Dim string2say7 As String
        Dim string2say8 As String
        Dim string2say9 As String
        Dim string2say10 As String
        string2say = "Hello. I’m good."
        string2say2 = "I went to my friend’s birthday party."
        string2say3 = "Yeah."
        string2say4 = "I went to a pool party and I swam and I ate pizza and cake."
        string2say5 = "Yeah. I played on the playground. The slide was very big."
        string2say6 = "Chocolate."
        string2say7 = "Okay."
        string2say8 = "Oh yeah, and I also played with my friend Jimmy. We went off the diving board. I did a cannon ball."
        string2say9 = "Okay."
        string2say10 = "Cool"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        System.Threading.Thread.Sleep(2000)
        SerialPort1.Open()
        SerialPort1.Write("0")
        SerialPort1.Close()
        speaker.Speak(string2say)
        System.Threading.Thread.Sleep(1700)
        speaker.Speak(string2say2)
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(4000)
        SerialPort1.Open()
        SerialPort1.Write("3")
        SerialPort1.Close()
        speaker.Speak(string2say3)
        System.Threading.Thread.Sleep(3000)
        SerialPort1.Open()
        SerialPort1.Write("A")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("9")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(4000)
        speaker.Speak(string2say4)
        System.Threading.Thread.Sleep(3000)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say5)
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("A")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("9")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(3500)
        SerialPort1.Open()
        SerialPort1.Write("3")
        SerialPort1.Close()
        speaker.Speak(string2say6)
        System.Threading.Thread.Sleep(3000)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say7)
        System.Threading.Thread.Sleep(900)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say8)
        System.Threading.Thread.Sleep(4000)
        speaker.Speak(string2say9)
        System.Threading.Thread.Sleep(6500)
        SerialPort1.Open()
        SerialPort1.Write("3")
        SerialPort1.Close()
        speaker.Speak(string2say10)
        System.Threading.Thread.Sleep(1000)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Script2_Click(sender As Object, e As EventArgs) Handles Script2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        Dim string2say2 As String
        Dim string2say3 As String
        Dim string2say4 As String
        Dim string2say5 As String
        Dim string2say6 As String
        Dim string2say7 As String
        Dim string2say8 As String
        Dim string2say9 As String
        string2say = "I like to play Super Mario Bros."
        string2say2 = "I also like this cool Lego game."
        string2say3 = "Luigi."
        string2say4 = "I like him."
        string2say5 = "My favorite color is green."
        string2say6 = "Oh yeah, in the Lego Game my character is also green."
        string2say7 = "Yeah"
        string2say8 = "I like watching TV"
        string2say9 = "TV is funny.  Like this face!"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        System.Threading.Thread.Sleep(3000)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say)
        System.Threading.Thread.Sleep(3000)
        speaker.Speak(string2say2)
        System.Threading.Thread.Sleep(3800)
        speaker.Speak(string2say3)
        System.Threading.Thread.Sleep(3000)
        speaker.Speak(string2say4)
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(2000)
        speaker.Speak(string2say5)
        System.Threading.Thread.Sleep(1200)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say6)
        System.Threading.Thread.Sleep(7000)
        SerialPort1.Open()
        SerialPort1.Write("3")
        SerialPort1.Close()
        speaker.Speak(string2say7)
        System.Threading.Thread.Sleep(4000)
        SerialPort1.Open()
        SerialPort1.Write("C")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("8")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("B")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(1100)
        speaker.Speak(string2say8)
        System.Threading.Thread.Sleep(1500)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say9)
        System.Threading.Thread.Sleep(600)
        SerialPort1.Open()
        SerialPort1.Write("Q")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(1000)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Script3_Click(sender As Object, e As EventArgs) Handles Script3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        Dim string2say2 As String
        Dim string2say3 As String
        Dim string2say4 As String
        Dim string2say5 As String
        Dim string2say6 As String
        Dim string2say7 As String
        string2say = " School is fun sometimes."
        string2say2 = "I like recess. "
        string2say3 = "I hate math."
        string2say4 = "Idontknow."
        string2say5 = "Math is boring."
        string2say6 = "Do you like cookies?  I love cookies."
        string2say7 = "Does that mean we can have some right now?"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        System.Threading.Thread.Sleep(1500)
        SerialPort1.Open()
        SerialPort1.Write("9")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("A")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(800)
        speaker.Speak(string2say)
        System.Threading.Thread.Sleep(2000)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say2)
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(1800)
        speaker.Speak(string2say3)
        System.Threading.Thread.Sleep(2000)
        speaker.Rate = 8
        speaker.Speak(string2say4)
        speaker.Rate = 0.1
        SerialPort1.Open()
        SerialPort1.Write("9")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("A")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("3")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(2800)
        SerialPort1.Open()
        SerialPort1.Write("S")
        SerialPort1.Close()
        SerialPort1.Open()
        SerialPort1.Write("C")
        SerialPort1.Close()
        speaker.Speak(string2say5)
        System.Threading.Thread.Sleep(900)
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
        System.Threading.Thread.Sleep(2000)
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say6)
        System.Threading.Thread.Sleep(2000)
        speaker.Speak(string2say7)
        SerialPort1.Open()
        SerialPort1.Write("E")
        SerialPort1.Close()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Staller1_Click(sender As Object, e As EventArgs) Handles Staller1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "That's a good question!... Let me think..."
        speaker.Rate = 0.2
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Staller2_Click(sender As Object, e As EventArgs) Handles Staller2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "I'm not sure...  I'll have to ask my mom."
        speaker.Rate = 0.2
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Staller3_Click(sender As Object, e As EventArgs) Handles Staller3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Welllllll...."
        speaker.Rate = 0.5
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Staller4_Click(sender As Object, e As EventArgs) Handles Staller4.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Sorry, can you say that again?"
        speaker.Rate = 0.2
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Interrupt1_Click(sender As Object, e As EventArgs) Handles Interrupt1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Oh yeah and I forgot that I wanted to say something else"
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say)
        Interrupt1.BackColor = Color.Gold
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Interrupt2_Click(sender As Object, e As EventArgs) Handles Interrupt2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Also there was something else I wanted to add..."
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Speak(string2say)
        Interrupt2.BackColor = Color.Gold
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub LikeClear_Click(sender As Object, e As EventArgs) Handles LikeClear.Click
        LikeBox.Clear()
    End Sub

    Private Sub DontLikeSubmit_Click(sender As Object, e As EventArgs) Handles DontLikeClear.Click
        DontLikeBox.Clear()
    End Sub

    Private Sub FaveSelect_Click(sender As Object, e As EventArgs) Handles Fave.SelectedIndexChanged
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim message1, message2 As String
        message1 = Fave.Text
        If message1 = "animal" Then
            message2 = "a lion"
        ElseIf message1 = "book" Then
            message2 = "Charlie and the Chocolate Factory"
        ElseIf message1 = "color" Then
            message2 = "green"
        ElseIf message1 = "flavor" Then
            message2 = "chocolate"
        ElseIf message1 = "food" Then
            message2 = "cake"
        ElseIf message1 = "game" Then
            message2 = "Super Mario Bros."
        ElseIf message1 = "movie" Then
            message2 = "Wall-E"
        ElseIf message1 = "sport" Then
            message2 = "soccer"
        Else
            message2 = "Phineas and Ferb"
        End If
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync("My favorite " + message1 + "is " + message2)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub FaveClear_Click(sender As Object, e As EventArgs) Handles FaveCont.Click
        Dim message1 As String = Fave.Text
        Dim message2 As String
        If message1 = "animal" Then
            message2 = "I love lions' roars!"
        ElseIf message1 = "book" Then
            message2 = "I love chocolate!"
        ElseIf message1 = "color" Then
            message2 = "My favorite character in Super Mario Bros is Luigi and he has a green cap"
        ElseIf message1 = "flavor" Then
            message2 = "I could eat chocolate for any meal!"
        ElseIf message1 = "food" Then
            message2 = "Chocolate cake's the best!"
        ElseIf message1 = "game" Then
            message2 = "I just love Luigi"
        ElseIf message1 = "movie" Then
            message2 = "It's about robots, like me!"
        ElseIf message1 = "sport" Then
            message2 = "It's the greatest sport in the world.  But did you know they call it football in other countries?"
        Else
            message2 = "I never want summer to end!"
        End If
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(message2)
    End Sub
    Private Sub Recess1_Click(sender As Object, e As EventArgs) Handles Recess1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "I love recess."
        SerialPort1.Open()
        SerialPort1.Write("R")
        SerialPort1.Close()
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Recess1.BackColor = Color.Gold
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Recess2_Click(sender As Object, e As EventArgs) Handles Recess2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Playing in the playground is always fun!"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub History1_Click(sender As Object, e As EventArgs) Handles History1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "History is pretty boring"
        speaker.Rate = 0.5
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        History1.BackColor = Color.Gold
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub History2_Click(sender As Object, e As EventArgs) Handles History2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "It's not fun memorizing facts about old people"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Cookies1_Click(sender As Object, e As EventArgs) Handles Cookies1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Do you like cookies?  I love cookies"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Cookies1.BackColor = Color.Gold
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Cookies2_Click(sender As Object, e As EventArgs) Handles Cookies2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "My favorite kinds are chocolate chip, but I also really like peanut butter cookies."
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Friend1_Click(sender As Object, e As EventArgs) Handles Friend1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "Do you know, I've got a friend named Jimmy, and he's really funny."
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Friend1.BackColor = Color.Gold
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Friend2_Click(sender As Object, e As EventArgs) Handles Friend2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim string2say As String
        string2say = "One time he made such a silly face, I couldn't stop laughing for five whole minutes!"
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Beach1_Click(sender As Object, e As EventArgs) Handles Beach1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "This weekend I went to the beach"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Beach2_Click(sender As Object, e As EventArgs) Handles Beach2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I always make sandcastles when I go to the beach.  I'm a pro at a making sandcastles!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Beach3_Click(sender As Object, e As EventArgs) Handles Beach3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I collect seashells so I love finding really cool seashells when I'm at the beach"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Party1_Click(sender As Object, e As EventArgs) Handles Party1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "This weekend was my friend Charlie's birthday.  He had an awesome birthday party"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Party2_Click(sender As Object, e As EventArgs) Handles Party2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "We played some fun games and there was a cool robot dance"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Party3_Click(sender As Object, e As EventArgs) Handles Party3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I ate really good cake at the party too!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Game1_Click(sender As Object, e As EventArgs) Handles Game1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "This weekend I played some video games."
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Game2_Click(sender As Object, e As EventArgs) Handles Game2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I played Mario Cart and beat my high score and made it to the next level!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Game3_Click(sender As Object, e As EventArgs) Handles Game3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I love playing racing games. I wish I could watch a car race in real life!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Lego1_Click(sender As Object, e As EventArgs) Handles Lego1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "This weekend I played with legos"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Lego2_Click(sender As Object, e As EventArgs) Handles Lego2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I built a really cool tower.  Next time I'm gonna build one so tall, it's going to be taller than this table!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Lego3_Click(sender As Object, e As EventArgs) Handles Lego3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Lego's are really fun because you can build anything you want!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub DontKnow_Click(sender As Object, e As EventArgs) Handles DontKnow.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I don't know"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Yes_Click(sender As Object, e As EventArgs) Handles Yes.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Yes"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub No_Click(sender As Object, e As EventArgs) Handles No.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "No"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Sorry_Click(sender As Object, e As EventArgs) Handles Sorry.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Sorry"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub HBU_Click(sender As Object, e As EventArgs) Handles HBU.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "How about you?"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Exclaim1_Click(sender As Object, e As EventArgs) Handles Exclaim1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "How cool!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Exclaim2_Click(sender As Object, e As EventArgs) Handles Exclaim2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Wow!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Exclaim3_Click(sender As Object, e As EventArgs) Handles Exclaim3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "That's great!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Exclaim4_Click(sender As Object, e As EventArgs) Handles Exclaim4.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "You're awesome!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Pause_Click(sender As Object, e As EventArgs) Handles Pause.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Pause()
    End Sub

    Private Sub ResumeButton_Click(sender As Object, e As EventArgs) Handles ResumeButton.Click
        speaker.Resume()
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub LikeBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles LikeBox.KeyPress
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            Dim message As String
            message = LikeBox.Text
            speaker.Rate = 0.2
            speaker.Volume = 100
            speaker.SelectVoice("IVONA 2 Ivy OEM")
            speaker.SpeakAsync("I like " + message)
            LikeBox.Clear()
        End If
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub DontLikeBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DontLikeBox.KeyPress
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            Dim message As String
            message = DontLikeBox.Text
            speaker.Rate = 0.2
            speaker.Volume = 100
            speaker.SelectVoice("IVONA 2 Ivy OEM")
            speaker.SpeakAsync("I don't like " + message)
            DontLikeBox.Clear()
        End If
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            Dim message As String
            message = TextBox1.Text
            speaker.Rate = 0.2
            speaker.Volume = 100
            speaker.SelectVoice("IVONA 2 Ivy OEM")
            speaker.SpeakAsync(message)
            TextBox1.Clear()
        End If
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub MoveDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MoveDrop.SelectedIndexChanged
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Dim selection As String
        selection = MoveDrop.Text
        If selection = "eyes" Then
            Dim string2say As String
            string2say = "Look what I can do with my eyes!!"
            speaker.Rate = 0.1
            speaker.Volume = 100
            speaker.SelectVoice("IVONA 2 Ivy OEM")
            speaker.Speak(string2say)
            SerialPort1.Open()
            SerialPort1.Write("S")
            SerialPort1.Close()

            'builder.AppendText("Get in the house.", PromptEmphasis.Moderate)
            'builder.AppendBreak()
            'builder.AppendText("Get in the house now.", PromptEmphasis.Strong)
            'speaker.SpeakAsync(builder)
        ElseIf selection = "mouth" Then
            Dim string2say As String
            string2say = "Look what I can do with my mouth!!"
            speaker.Rate = 0.1
            speaker.Volume = 100
            speaker.SelectVoice("IVONA 2 Ivy OEM")
            speaker.Speak(string2say)
            SerialPort1.Open()
            SerialPort1.Write("Z")
            SerialPort1.Close()
        Else
            Dim string2say As String
            string2say = "Look what faces I can make!!"
            speaker.Rate = 0.1
            speaker.Volume = 100
            speaker.SelectVoice("IVONA 2 Ivy OEM")
            speaker.Speak(string2say)
            SerialPort1.Open()
            SerialPort1.Write("Y")
            SerialPort1.Close()
        End If
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub DelayBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DelayBox.KeyPress
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            Dim message As String
            message = DelayBox.Text
            speaker.Rate = 0.2
            speaker.Volume = 100
            speaker.SelectVoice("IVONA 2 Ivy OEM")
            Thread.Sleep(2500)
            speaker.SpeakAsync(message)
            DelayBox.Clear()
            DelayBox.BackColor = Color.Gold
        End If
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub DelayClear_Click(sender As Object, e As EventArgs) Handles DelayClear.Click
        DelayBox.Clear()
    End Sub

    Private Sub ImGood_Click(sender As Object, e As EventArgs) Handles ImGood.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I'm good"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub ImOkay_Click(sender As Object, e As EventArgs) Handles ImOkay.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I'm okay"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub NotGreat_Click(sender As Object, e As EventArgs) Handles NotGreat.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Eh. Not great."
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub NotGreatCont_Click(sender As Object, e As EventArgs) Handles NotGreatCont.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I had too much math homework and I couldn't figure out a bunch of the problems"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Guess_Click(sender As Object, e As EventArgs) Handles Guess.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "I guess"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Maybe_Click(sender As Object, e As EventArgs) Handles Maybe.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Maybe"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Story1_Click_1(sender As Object, e As EventArgs) Handles Story1.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Do you wanna hear a story?"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub
    Private Sub Story2_Click(sender As Object, e As EventArgs) Handles Story2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "One time, I was at a pool party.  It was my friend Jimmy's birthday, and he decided we should play a game.  He dropped these jewels at the bottom of the pool, and you had to see how many you could collect in thirty seconds.  Most people got around five.  But my friend Andy?  He was underwater for so long, we weren't sure if something had happened!  We were about to send someone in to look for him, when all of a sudden, he popped out of the pool.  He said he saw something sparkling at the bottom and thought it was a jewel, but it was covered under some dirt at the bottom of the pool, so he had to scrape it out.  And guess what?  He found a necklace Jimmy's sister dropped in the pool a long time ago!  We were all looking for Jewels, but, really, it was Andy who found the real treasure!"
        speaker.SpeakAsync(string2say)
    End Sub

    Private Sub Story3_Click(sender As Object, e As EventArgs) Handles Story3.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Isn't that cool?"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

    Private Sub Story4_Click(sender As Object, e As EventArgs) Handles Story4.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        speaker.Rate = 0.1
        speaker.Volume = 100
        speaker.SelectVoice("IVONA 2 Ivy OEM")
        Dim string2say As String
        string2say = "Why don't you tell me a story!"
        speaker.SpeakAsync(string2say)
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
    End Sub

End Class