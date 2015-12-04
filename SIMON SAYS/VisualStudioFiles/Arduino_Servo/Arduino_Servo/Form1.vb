'created by Laura Boccanfuso 11/2015
' Connect, provide control commands to Arduino .ino
' Provide GUI for game play, dialogue
' Connect to webcam and perform face detection
' Call external function to perform face recognition, collect and store session data for each participant

Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.IO
Imports System.IO.Ports
Imports System.Security.Cryptography.X509Certificates
Imports System.Speech.Synthesis
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Emgu.CV
Imports Emgu.CV.Structure
Imports Emgu.Util

Module GlobalVariables
    Public rect As Rectangle
    Public camcap As Capture = New Capture(0)
    Public camimage As Mat
    Public faceDetector As New CascadeClassifier("haarcascade_frontalface_alt.xml")  ' Load the classifier
End Module

Public Class Form1
    Shared _continue As Boolean
    Shared _serialPort As SerialPort
    WithEvents speaker As New SpeechSynthesizer()
    Public Event VisemeReached As EventHandler(Of VisemeReachedEventArgs)
    Public Event SpeakCompleted As EventHandler(Of SpeakCompletedEventArgs)
    Dim stop_Clicked As Boolean = False
    Dim pause_Clicked As Boolean = False
    Dim myName As String
    Dim failedToConnect As Boolean
    Dim OKAnswer As Boolean = False
    ReadOnly LEVoice As String = "IVONA 2 Ivy OEM" 'Microsoft Anna OR IVONA 2 Ivy OEM
    Dim currentThread As Thread

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Setup Voice
        speaker.Rate = -2
        speaker.Volume = 100
        Try
            speaker.SelectVoice(LEVoice)
        Catch ex As Exception
        End Try

        SerialPort1.Close()

        SerialPort1.PortName = "COM7" 'define your port
        SerialPort1.BaudRate = 9600
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = Encoding.Default
        Threading.Thread.Sleep(1000)
        Try
            SendCommand("2")
        Catch ex As Exception
            If (ex.ToString.Contains("does not exist")) Then
                MsgBox(
                    "Failed to connect to serial port. Please make sure L-E is plugged in and the arduino code has been uploaded.")
                failedToConnect = True
            End If
        End Try

        'ClearAllObject()
        PictureBox1.BackColor = Color.Black
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.Image = Nothing
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.Refresh()
        camcap.Start()
    End Sub

    Private Sub speaker_VisemeReached(sender As Object, e2 As VisemeReachedEventArgs) Handles speaker.VisemeReached
        'Console.WriteLine("Viseme " & e2.Viseme & " was " & e2.Duration.TotalMilliseconds.ToString & " ms. long" & vbNewLine)

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
                SendCommand("4")
            ElseIf (e2.Viseme = 3 Or e2.Viseme = 6 Or e2.Viseme = 10) Then
                SendCommand("5")
            ElseIf (e2.Viseme = 4 Or e2.Viseme = 5 Or e2.Viseme = 7 Or e2.Viseme = 20) Then
                SendCommand("6")
            Else
                SendCommand("7")
            End If
        Catch e As Exception
            Console.WriteLine("exception caught")
        End Try
    End Sub

    Private Sub speaker_SpeakCompleted(sender As Object, e2 As SpeakCompletedEventArgs) Handles speaker.SpeakCompleted
        If (stop_Clicked = True) Then
            Return
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            SpeakString(TextBox1.Text)
        End If
    End Sub

    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        TextBox1.Clear()
    End Sub

#Region "Actions/Body Positions"

    Private Sub blink_Click(sender As Object, e As EventArgs) Handles Timer1.Tick, blink.Click
        SendCommand("3")
    End Sub

    Private Sub HeadLeft_Click(sender As Object, e As EventArgs) Handles Timer2.Tick, HeadLeft.Click
        SendCommand("A")
    End Sub

    Private Sub HeadRight_Click(sender As Object, e As EventArgs) Handles HeadRight.Click
        SendCommand("B", True)
    End Sub

    Private Sub head_center_Click(sender As Object, e As EventArgs) Handles head_center.Click
        SendCommand("F")
    End Sub

    Private Sub Wink_Click(sender As Object, e As EventArgs) Handles Wink.Click
        SendCommand("E", True)
    End Sub

#End Region

#Region "Small Talk"

    Private Sub HowAreYou_Click(sender As Object, e As EventArgs) Handles HowAreYou.Click
        SpeakString("How are you?", -2, 100, True)
    End Sub

    Private Sub ImGood_Click(sender As Object, e As EventArgs) Handles ImGood.Click
        SpeakString("I'm good", -2, 100, True)
    End Sub

    Private Sub ImOkay_Click(sender As Object, e As EventArgs) Handles ImOkay.Click
        SpeakString("I'm okay", -2, 100, True)
    End Sub
    Private Sub NotGreat_Click(sender As Object, e As EventArgs) Handles NotGreat.Click
        SpeakString("Not great.  I'm having trouble figuring out some math problems I was assigned", -2, 100, True)
    End Sub
    Private Sub SmallTalkAsk_Click(sender As Object, e As EventArgs) Handles SmallTalkAsk.Click
        Dim message1, message2 As String
        message1 = SmallTalkBox.Text
        If message1 = "Tell me about yourself" Then
            message2 = message1
        ElseIf message1 = "Have any brothers/sisters?" Then
            message2 = "Do you have any brothers or sisters?"
        ElseIf message1 = "Have any pets?" Then
            message2 = "Do you have any pets?"
        ElseIf message1 = "When is your birthday?" Then
            message2 = message1
        ElseIf message1 = "What grade are you in?" Then
            message2 = message1
        ElseIf message1 = "What breakfast today?" Then
            message2 = "What did you have for breakfast today?"
        ElseIf message1 = "What did in school today?" Then
            message2 = "What did you do in school today?"
        End If
        If Not String.IsNullOrEmpty(message2) Then
            SpeakString(message2, -2, 100, True)
        End If
    End Sub

    Private Sub SmallTalkAns_Click(sender As Object, e As EventArgs) Handles SmallTalkAns.Click
        Dim message1, message2 As String
        message1 = SmallTalkBox.Text
        If message1 = "Tell me about yourself" Then
            message2 = "Well, I love reading and watching TV.  I can't do everything humans can do because I'm a robot, but I love playing wit humans like you!"
        ElseIf message1 = "Have any brothers/sisters?" Then
            message2 = "Robots don't really have brothers and sisters, but there are others like me out there! I like to think of them as my brothers and sisters."
        ElseIf message1 = "Have any pets?" Then
            message2 = "I wish! I think dogs are really fun."
        ElseIf message1 = "When is your birthday?" Then
            message2 = "I was created on May 15"
        ElseIf message1 = "What grade are you in?" Then
            message2 = "Robots can go to school with kids from all different grades, but I'm eight!"
        ElseIf message1 = "What breakfast today?" Then
            message2 = "I didn't have breakfast today, but I love doughnuts!"
        ElseIf message1 = "What did in school today?" Then
            message2 = "Today, I learned about the sea.  Did you know that a shark is the only known fish that can blink with both eyes?"
        End If
        If Not String.IsNullOrEmpty(message2) Then
            SpeakString(message2, -2, 100, True)
        End If
    End Sub
    Private Sub TodayQ_Click(sender As Object, e As EventArgs) Handles TodayQ.Click
        SpeakString("What did you do today?", -2, 100, True)
    End Sub
    Private Sub TodayA_Click(sender As Object, e As EventArgs) Handles TodayA.Click
        SpeakString("Today I went to the park.", -2, 100, True)
    End Sub
    Private Sub TodayACont_Click(sender As Object, e As EventArgs) Handles TodayACont.Click
        SpeakString("There was some really cool music playing in the park.  It was fun!", -2, 100, True)
    End Sub
#End Region

#Region "Responses"

    Private Sub DontKnow_Click(sender As Object, e As EventArgs) Handles DontKnow.Click
        SpeakString("I don't know", -2, 100, True)
    End Sub

    Private Sub Yes_Click(sender As Object, e As EventArgs) Handles Yes.Click
        SpeakString("Yes", -2, 100, True)
    End Sub

    Private Sub No_Click(sender As Object, e As EventArgs) Handles No.Click
        SpeakString("No", -2, 100, True)
    End Sub

    Private Sub Sorry_Click(sender As Object, e As EventArgs) Handles Sorry.Click
        SpeakString("Sorry", -2, 100, True)
    End Sub

    Private Sub HBU_Click(sender As Object, e As EventArgs) Handles HBU.Click
        SpeakString("How about you?", -2, 100, True)
    End Sub

    Private Sub Guess_Click(sender As Object, e As EventArgs) Handles MeToo.Click
        SpeakString("Me too", -2, 100, True)
    End Sub

    Private Sub Maybe_Click(sender As Object, e As EventArgs) Handles Maybe.Click
        SpeakString("Maybe", -2, 100, True)
    End Sub

#End Region

#Region "Speech Controls"

    Private Sub StopButton_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Try
            If currentThread.IsAlive Then
                Try
                    currentThread.Abort()
                Catch ex As ThreadStateException
                    currentThread.Resume()
                End Try
            End If
        Catch ex As Exception

        End Try
        stop_Clicked = True
        Select Case speaker.State
            Case SynthesizerState.Speaking
                speaker.SpeakAsyncCancelAll()
                Exit Select
            Case SynthesizerState.Paused
                speaker.Resume()
                speaker.SpeakAsyncCancelAll()
                Exit Select
        End Select
        speaker.SpeakAsyncCancelAll()
        SendCommand("AA")
    End Sub

    Private Sub Pause_Click(sender As Object, e As EventArgs) Handles Pause.Click
        Try
            If currentThread.IsAlive Then
                currentThread.Suspend()
            End If
        Catch ex As Exception
            Exit Try
        End Try
        Select Case speaker.State
            Case SynthesizerState.Speaking
                speaker.Pause()
                Exit Select
        End Select
    End Sub

    Private Sub ResumeButton_Click(sender As Object, e As EventArgs) Handles ResumeButton.Click
        Try
            If currentThread.IsAlive Then
                Try
                    currentThread.Resume()
                Catch ex As ThreadStateException
                    Exit Try
                End Try
            End If
        Catch ex As NullReferenceException
            Exit Try
        End Try
        Select Case speaker.State
            Case SynthesizerState.Paused
                speaker.Resume()
                Exit Select
        End Select
    End Sub

#End Region

#Region "Intro / Exit"

    Private Sub Hello_Click(sender As Object, e As EventArgs) Handles Hello.Click
        SpeakString("Hi!  My name is L-E and I love to play Simon Says.  Would you like to play with me?", -2, 100, True)
    End Sub

    Private Sub Intro_Click(sender As Object, e As EventArgs) Handles Intro.Click
        SpeakString("Ok great!  I'll be Simon!  When I say Simon Says, you do what I say.  But if I don't say Simon Says, then you shouldn't do it.  Ok?", -2, 100, True)
    End Sub

    Private Sub Bye_Click(sender As Object, e As EventArgs)
        SpeakString("Bye " + myName + ". That was fun!", -2, 100, True)
    End Sub

    Private Sub SeeYou_Click(sender As Object, e As EventArgs) Handles SeeYou.Click
        SpeakString("See you next time" + myName + "!", -2, 100, True)
    End Sub

#End Region

#Region "Story"

    Private Sub Story1_Click_1(sender As Object, e As EventArgs) Handles Story1.Click
        SpeakString("Do you wanna hear a story?", -2, 100, True)
    End Sub

    Private Sub Story2_Click(sender As Object, e As EventArgs) Handles Story2.Click
        SpeakString(
            "One time, I was at a pool party.  It was my friend Jimmy's birthday, and he decided we should play a game. " &
            "He dropped these jewels at the bottom of the pool, and you had to see how many you could collect in thirty seconds. " &
            "Most people got around five.  But my friend Andy?  He was underwater for so long, we weren't sure if something had happened! " &
            "We were about to send someone in to look for him, when all of a sudden, he popped out of the pool.  He said he saw something " &
            "sparkling at the bottom and thought it was a jewel, but it was covered under some dirt at the bottom of the pool, so he had to " &
            "scrape it out.  And guess what?  He found a necklace Jimmy's sister dropped in the pool a long time ago!  We were all looking for " &
            "Jewels, but, really, it was Andy who found the real treasure!", -3)
    End Sub

    Private Sub Story3_Click(sender As Object, e As EventArgs) Handles Story3.Click
        SpeakString("Do you want to hear another story?", -3, 100, True)
    End Sub

    Private Sub Story4_Click(sender As Object, e As EventArgs) Handles Story4.Click
        SpeakString(
            "Once, I had a playdate at my friend Alex's house and we really wanted to bake something. We both liked cookies and pizza, " &
            "so we thought it would be an awesome idea to bake a cookie pizza! I used chocolate chips, mini oreos, marshmallows, and peanut " &
            "butter chips as my toppings. Alex made a crazy pizza with gummy worms, M and M's, and bacon. Those were the weirdest cookie toppings " &
            "I had ever seen. We put our cookie pizzas in the oven, and once they were done, we were so hungry, we ate 4 slices! I tried some of Alex's " &
            "cookie pizza, and it was actually pretty good. Maybe I'll try making a crazy cookie pizza next time. That was one of the best playdates " &
            "I've ever had.")
    End Sub

    Private Sub Story5_Click(sender As Object, e As EventArgs) Handles Story5.Click
        SpeakString("Why don't you tell me a story!", -2, 100, True)
    End Sub

#End Region

#Region "General Functions"
    Private Sub SendCommand(cmd As String, Optional resetTimers As Boolean = False)
        If failedToConnect Then Exit Sub
        Try
            SerialPort1.Open()
            SerialPort1.Write(cmd)
            SerialPort1.Close()
        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Private Sub SpeakString(txt As String, Optional rate As Double = -2, Optional volume As Double = 100,
                            Optional resetTimers As Boolean = False)
        speaker.Rate = rate
        speaker.Volume = volume
        speaker.SpeakAsync(txt)
    End Sub

    Private Sub SpeakStringSync(txt As String, Optional rate As Double = -2, Optional volume As Double = 100,
                            Optional resetTimers As Boolean = False)
        speaker.Rate = rate
        speaker.Volume = volume
        speaker.Speak(txt)
    End Sub

#End Region

#Region "Conversation"
    Private Sub Why_Click(sender As Object, e As EventArgs) Handles Why.Click
        SpeakString("Why", -2, 100, True)
    End Sub

    Private Sub Ok_lets_play_Click(sender As Object, e As EventArgs) Handles Ok_lets_play.Click
        SpeakString("Alrighty then.  Let's PLAY!", -2, 100, True)
    End Sub

    Private Sub Maybe_next_time_Click(sender As Object, e As EventArgs) Handles Maybe_next_time.Click
        SpeakString("Ok, maybe we can play next time.  See you later!", -2, 100, True)
    End Sub
#End Region

#Region "Simon Didn't Say"
    Private Sub Hands_head_Click(sender As Object, e As EventArgs) Handles Hands_head.Click
        SendCommand("B")
        SpeakString("Put your hands on your head!", -2, 100, True)
    End Sub
    Private Sub Hands_hip_Click(sender As Object, e As EventArgs) Handles Hands_hip.Click
        SendCommand("F")
        SpeakString("Put your hands on your hips!", -2, 100, True)
    End Sub
    Private Sub Hands_high_Click(sender As Object, e As EventArgs) Handles Hands_high.Click
        SendCommand("A")
        SpeakString("Hold your hands up high!", -2, 100, True)
    End Sub
    Private Sub clap_Click(sender As Object, e As EventArgs) Handles clap.Click
        SendCommand("A")
        SpeakString("Clap your hands!", -2, 100, True)
    End Sub
    Private Sub Touch_nose_Click(sender As Object, e As EventArgs) Handles Touch_nose.Click
        SendCommand("F")
        SpeakString("Touch your nose!", -2, 100, True)
    End Sub
    Private Sub Touch_knees_Click(sender As Object, e As EventArgs) Handles Touch_knees.Click
        SendCommand("B")
        SpeakString("Touch your nose!", -2, 100, True)
    End Sub
    Private Sub Touch_toes_Click(sender As Object, e As EventArgs) Handles Touch_toes.Click
        SendCommand("A")
        SpeakString("Now, touch your toes!", -2, 100, True)
    End Sub
    Private Sub Run_Click(sender As Object, e As EventArgs) Handles Run.Click
        SendCommand("A")
        SpeakString("Ok, run in place!", -2, 100, True)
    End Sub
    Private Sub Spin_Click(sender As Object, e As EventArgs) Handles Spin.Click
        SendCommand("F")
        SpeakString("Turn all the way around!", -2, 100, True)
    End Sub
    Private Sub Jump_Click(sender As Object, e As EventArgs) Handles Jump.Click
        SendCommand("A")
        SpeakString("Now, jump three times!", -2, 100, True)
    End Sub
    Private Sub Rub_tummy_Click(sender As Object, e As EventArgs) Handles Rub_tummy.Click
        SendCommand("B")
        SpeakString("Everyone rub your tummy!", -2, 100, True)
    End Sub
    Private Sub Stomp_Click(sender As Object, e As EventArgs) Handles Stomp.Click
        SendCommand("F")
        SpeakString("Let's stomp your feet!", -2, 100, True)
    End Sub
#End Region

#Region "UhOh Responses"
    Private Sub Uh_oh_Click(sender As Object, e As EventArgs) Handles Uh_oh.Click
        SendCommand("1")
        SpeakStringSync("Uh-Oh... I didn't say Simon Says!", -2, 100, True)
        Threading.Thread.Sleep(800)
        SpeakString("... ...Let's try again!", -2, 100, True)
        SendCommand("0") ' happy
        SendCommand("3")

    End Sub
    Private Sub im_sorry_Click(sender As Object, e As EventArgs) Handles im_sorry.Click
        SendCommand("0") ' happy
        SendCommand("8") ' shake head
        Threading.Thread.Sleep(1800)
        SendCommand("3")
        SpeakString("I'm sorry but I didn't say Simon Says!... make sure you listen closely!", -2, 100, True)
    End Sub
    Private Sub Too_bad_Click(sender As Object, e As EventArgs) Handles Too_bad.Click
        SendCommand("9")  ' JAW DROP
        Threading.Thread.Sleep(1300)
        SpeakString("Oh no!... you moved but I never said Simon Says!  Ok, here we go again...", -2, 100, True)
        SendCommand("3")
    End Sub
    Private Sub got_you_Click(sender As Object, e As EventArgs) Handles got_you.Click
        SendCommand("C")  ' Nod
        Threading.Thread.Sleep(1800)
        SpeakString("I got you!  I didn't say Simon Says this time...  this is fun!  Let's keep going!", -2, 100, True)
        SendCommand("3")
    End Sub
    Private Sub No_no_Click(sender As Object, e As EventArgs) Handles No_no.Click
        SendCommand("D")
        Threading.Thread.Sleep(600)
        SendCommand("A")
        SpeakString("No-No-No", -2, 100, True)
        SendCommand("B")
        SendCommand("A")
        SendCommand("B")
        SendCommand("A")
        Threading.Thread.Sleep(1200)
        SendCommand("G")
        SpeakString("I didn't say it!  That's ok though, you are doing great!  Let's play again!", -2, 100, True)
    End Sub
    Private Sub saw_you_Click(sender As Object, e As EventArgs) Handles saw_you.Click
        SendCommand("9")  ' JAW DROP
        Threading.Thread.Sleep(1100)
        SpeakString("HA-ha-ha!  I saw you!  You moved but I didn't say Simon Says!", -2, 100, True)
        SendCommand("E")
        SpeakString("This is so much fun!  Let's play again!", -2, 100, True)
    End Sub
    Private Sub Didnt_say_Click(sender As Object, e As EventArgs) Handles Didnt_say.Click
        SendCommand("0") ' happy
        SendCommand("8") ' shake head
        Threading.Thread.Sleep(1800)
        SendCommand("3")
        SpeakString("OOPS!  I think you moved but I didn't say Simon Says!  Ok, let's keep going!", -2, 100, True)
    End Sub
    Private Sub oh_no_Click(sender As Object, e As EventArgs) Handles oh_no.Click
        SpeakString("Oh-oh-oh!  Too bad, so sad.  I got you!!  NExt time, wait until I say Simon Says before you move!", -2, 100, True)
    End Sub
#End Region

#Region "Simon Says"
    Private Sub SS_Hands_head_Click(sender As Object, e As EventArgs) Handles SS_Hands_head.Click
        SendCommand("A")
        SpeakString("Simon says, put your hands on your head!", -2, 100, True)
    End Sub
    Private Sub SS_Hands_hip_Click(sender As Object, e As EventArgs) Handles SS_Hands_hip.Click
        SendCommand("F")
        SpeakString("Simon says, place your hands on your hips!", -2, 100, True)
    End Sub
    Private Sub SS_Hands_high_Click(sender As Object, e As EventArgs) Handles SS_Hands_high.Click
        SendCommand("A")
        SpeakString("Ok, Simon says, Hold your hands up high!", -2, 100, True)
    End Sub
    Private Sub SS_clap_Click(sender As Object, e As EventArgs) Handles SS_clap.Click
        SendCommand("B")
        SpeakString("Simon says, Clap your hands!", -2, 100, True)
    End Sub
    Private Sub SS_Touch_nose_Click(sender As Object, e As EventArgs) Handles SS_Touch_nose.Click
        SendCommand("A")
        SpeakString("Simon says, Touch your nose!", -2, 100, True)
    End Sub
    Private Sub SS_Touch_knees_Click(sender As Object, e As EventArgs) Handles SS_Touch_knees.Click
        SendCommand("F")
        SpeakString("Now Simon says, Touch your knees!", -2, 100, True)
    End Sub
    Private Sub SS_Touch_toes_Click(sender As Object, e As EventArgs) Handles SS_Touch_toes.Click
        SendCommand("A")
        SpeakString("Simon says, touch your toes!", -2, 100, True)
    End Sub
    Private Sub SS_Run_Click(sender As Object, e As EventArgs) Handles SS_run.Click
        SendCommand("B")
        SpeakString("And Simon says, run in place!", -2, 100, True)
    End Sub
    Private Sub SS_Spin_Click(sender As Object, e As EventArgs) Handles SS_spin.Click
        SendCommand("F")
        SpeakString("Simon says, turn all the way around!", -2, 100, True)
    End Sub
    Private Sub SS_Jump_Click(sender As Object, e As EventArgs) Handles SS_Jump.Click
        SendCommand("B")
        SpeakString("Simon says, jump three times!", -2, 100, True)
    End Sub
    Private Sub SS_Rub_tummy_Click(sender As Object, e As EventArgs) Handles SS_Rub_tummy.Click
        SendCommand("A")
        SpeakString("Simon says, rub your tummy!", -2, 100, True)
    End Sub
    Private Sub SS_Stomp_Click(sender As Object, e As EventArgs) Handles SS_stomp.Click
        SendCommand("F")
        SpeakString("Simon says, stomp your feet!", -2, 100, True)
    End Sub
#End Region

#Region "Positive responses"
    Private Sub way_2_go_Click(sender As Object, e As EventArgs) Handles way_2_go.Click
        SpeakString("Way to go!  That was perfect!  Let's play again!", -2, 100, True)
    End Sub
    Private Sub awesome_Click(sender As Object, e As EventArgs) Handles awesome.Click
        SpeakString("You are awesome!  Great listening!  Let's keep going!", -2, 100, True)
    End Sub
    Private Sub you_did_it_Click(sender As Object, e As EventArgs) Handles you_did_it.Click
        SpeakString("You did it!  Fabulous!  Should we keep playing?", -2, 100, True)
    End Sub
    Private Sub excellent_Click(sender As Object, e As EventArgs) Handles excellent.Click
        SpeakString("Excellent!  You are listening so well and did it perfectly! Let's keep playing!", -2, 100, True)
    End Sub
    Private Sub smart_Click(sender As Object, e As EventArgs) Handles smart.Click
        SpeakString("You are very smart!  I couldn't trick you once!  Let's play again!", -2, 100, True)
    End Sub
    Private Sub terrific_Click(sender As Object, e As EventArgs) Handles terrific.Click
        SpeakString("That was terrific!  Another amazing performance!  Do you want to keep playing with me?", -2, 100, True)
    End Sub
    Private Sub the_best_Click(sender As Object, e As EventArgs) Handles the_best.Click
        SpeakString("You are the best!  Incredible!  Let's do it again!", -2, 100, True)
    End Sub
    Private Sub great_Click(sender As Object, e As EventArgs) Handles great.Click
        SpeakString("Great!  You are truly awesome.  Here we go again!  Are you ready?", -2, 100, True)
    End Sub
#End Region

#Region "Camera+face detection"
    Private Sub OpenPreviewWindow() Handles Timer4.Tick
        camimage = camcap.QueryFrame
        PictureBox1.Image = camimage.Bitmap()

        '' OPENCV EMGU WRAPPER
        Dim imgGray As New UMat()
        CvInvoke.CvtColor(camimage, imgGray, CvEnum.ColorConversion.Bgr2Gray)   ' Convert RGB to grayscale for faster frame processing
        For Each face As Rectangle In faceDetector.DetectMultiScale( _
                  camimage, _
                  1.1, _
                  10, _
                  New Size(20, 20), _
                  Size.Empty)
            CvInvoke.Rectangle(camimage, face, New MCvScalar(255, 255, 255))
        Next
        PictureBox1.Invalidate()
        'DestroyWindow(hHwnd)
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles PictureBox1.Paint
        'refresh picbox
    End Sub
#End Region
End Class
