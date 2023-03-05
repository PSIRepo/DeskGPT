using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using TextBox = System.Windows.Controls.TextBox;

namespace dGPT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        public MessageCollection messages = new MessageCollection();
        private Storyboard scrollViewerStoryboard;
        private DoubleAnimation scrollViewerScrollToEndAnim;
        private Storyboard MSB, MSB2, C1, C2;
        private MessageSide curside;

        private DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset",
          typeof(double), typeof(MainWindow), new PropertyMetadata(0.0, OnVerticalOffsetChanged));

        private const int HOTKEY_ID = 6326;
        private const uint MOD_WIN = 0x0008;
        private const uint MOD_ALT = 0x0001;
        private const uint MOD_CTRL = 0x0002;
        private const uint MOD_NONE = 0x0000;

        private bool vis = true, Cht = true, clVis = false, startVis = true;

        private DispatcherTimer DT;
        private Stopwatch SW;

        public HwndSource source;

        private bool canceled = false;
        public IntPtr handle;

        private Message T;

        private NotifyIcon nIcon = new NotifyIcon();
        private ContextMenuStrip CM = new ContextMenuStrip();

        private string OPENAI_API_KEY = Settings1.Default.APIKey;
        private OpenAIWrapper OAIW;

        public MainWindow()
        {
            InitializeComponent();

            startVis = Settings1.Default.startupVisible;

            if (!startVis)
            {
                Window1.Opacity = 0;
                vis = false;
            }

            DT = new DispatcherTimer();
            DT.Tick += DT_Tick;
            DT.Interval = new TimeSpan(0, 0, 0, 0, 5);
            SW = new Stopwatch();

            DoubleAnimation DA = new()
            {
                From = 1.0,
                To = 0.0,
                Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(500))
            };
            DA.Completed += DA_Comp;
            MSB = new Storyboard();
            MSB.Children.Add(DA);
            Storyboard.SetTargetName(DA, Name);
            Storyboard.SetTargetProperty(DA, new PropertyPath(Window.OpacityProperty));

            DoubleAnimation DAO = new DoubleAnimation();
            DAO.From = 0.0;
            DAO.To = 1.0;
            DAO.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(500));
            DAO.Completed += DAO_Comp;
            MSB2 = new Storyboard();
            MSB2.Children.Add(DAO);
            Storyboard.SetTargetName(DAO, Name);
            Storyboard.SetTargetProperty(DAO, new PropertyPath(Window.OpacityProperty));

            DoubleAnimationUsingKeyFrames C1D = new DoubleAnimationUsingKeyFrames();
            C1D.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(6500));
            C1D.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero), Value = 0.0 });
            C1D.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)), Value = 154.0 });
            C1D.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)), Value = 154.0 });
            C1D.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(6)), Value = 0.0 });
            C1D.Completed += C1D_Completed;
            C1 = new Storyboard();
            C1.Children.Add(C1D);
            Storyboard.SetTargetName(C1, Confirm.Name);
            Storyboard.SetTargetProperty(C1, new PropertyPath(TextBox.WidthProperty));

            DoubleAnimationUsingKeyFrames C1D2 = new DoubleAnimationUsingKeyFrames();
            C1D2.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(1000));
            //C1D2.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero), Value = Confirm.Width });
            C1D2.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)), Value = 0.0 });
            C1D2.Completed += C1D2_Completed;
            C2 = new Storyboard();
            C2.Children.Add(C1D2);
            Storyboard.SetTargetName(C2, Confirm.Name);
            Storyboard.SetTargetProperty(C2, new PropertyPath(TextBox.WidthProperty));

            ToolStripItem CMI3 = CM.Items.Add("Settings");
            CMI3.Click += CMI3_Click;
            ToolStripItem CMI = CM.Items.Add("Exit");
            CMI.Click += Cls;
            ToolStripItem CMI2 = CM.Items.Add("Restart");
            CMI2.Click += CMI_Click;
            nIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
            nIcon.Visible = true;
            nIcon.Text = "DeskGPT";
            nIcon.DoubleClick += nIcon_DoubleClick;
            nIcon.ContextMenuStrip = CM;

            OAIW = new OpenAIWrapper(OPENAI_API_KEY);
        }

        private void CMI3_Click(object? sender, EventArgs e)
        {
            Settings setWin = new Settings();
            setWin.Show();
        }

        private void CMI_Click(object? sender, EventArgs e)
        {
            App._instanceMutex.ReleaseMutex();
            App._instanceMutex.Close();
            System.Windows.Forms.Application.Restart();
            Environment.Exit(0);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            handle = new WindowInteropHelper(this).Handle;
            source = HwndSource.FromHwnd(handle);
            source.AddHook(HwndHook);

            uint mod = (Settings1.Default.Modifier.Contains("WIN") ? MOD_WIN : (Settings1.Default.Modifier.Contains("ALT") ? MOD_ALT : (Settings1.Default.Modifier.Contains("CTRL") ? MOD_CTRL : MOD_NONE)));
            uint key = KeyDictionary.ParseKey(Settings1.Default.Key);
            MainWindowHelpers.RegisterHotKey(handle, HOTKEY_ID, mod, key);
        }

        private void C1D2_Completed(object? sender, EventArgs e)
        {
            clVis = false;
        }

        private void C1D_Completed(object? sender, EventArgs e)
        {
            clVis = false;
        }

        public async void SendMsg(string sQuestion)
        {
            string sModel = Settings1.Default.Model;
            OpenAIWrapper.APIRequest APIR = new OpenAIWrapper.APIRequest(sModel, sQuestion);
            string resp = OAIW.SendRequest(APIR);

            if (!canceled && !resp.Contains("[ERROR]"))
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    messages.Remove(T);
                    messages.Add(new Message((Settings1.Default.Model.Contains("3.5") ? "GPT 3.5" : "GPT 3.0"))
                    {
                        Side = MessageSide.You,
                        Text = resp
                    });

                    curside = MessageSide.You;

                    this.DataContext = messages;

                    scrollViewerScrollToEndAnim.From = ConversationScrollViewer.VerticalOffset;
                    scrollViewerScrollToEndAnim.To = ConversationContentContainer.ActualHeight;
                    scrollViewerStoryboard.Begin();

                    ChtSnd.Focus();
                    Cht = true;

                    SW.Stop();
                    DT.Stop();
                    CancelB.Visibility = Visibility.Hidden;
                }
                ));
                canceled = false;
                return;
            }
            else
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    messages.Remove(T);
                    messages.Add(new Message((Settings1.Default.Model.Contains("3.5") ? "GPT 3.5" : "GPT 3.0"))
                    {
                        Side = MessageSide.Error,
                        Text = "Error retrieving response. Please check network status, API key, request parameters, and server status and try again."
                    });

                    curside = MessageSide.Error;

                    this.DataContext = messages;

                    scrollViewerScrollToEndAnim.From = ConversationScrollViewer.VerticalOffset;
                    scrollViewerScrollToEndAnim.To = ConversationContentContainer.ActualHeight;
                    scrollViewerStoryboard.Begin();

                    ChtSnd.Focus();
                    Cht = true;

                    SW.Stop();
                    DT.Stop();
                    canceled = false;
                    CancelB.Visibility = Visibility.Hidden;
                }
                ));
            }
        }

        #region VerticalOffset DP

        private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow app = d as MainWindow;
            app.OnVerticalOffsetChanged(e);
        }

        private void OnVerticalOffsetChanged(DependencyPropertyChangedEventArgs e)
        {
            ConversationScrollViewer.ScrollToVerticalOffset((double)e.NewValue);
        }

        #endregion VerticalOffset DP

        private void DAO_Comp(object sender, EventArgs e)
        {
            Visibility = Visibility.Visible;
            vis = true;
            this.Activate();
            this.Focus();
            ChtSnd.Focus();
        }

        private void nIcon_DoubleClick(object sender, EventArgs e)
        {
            if (vis)
            {
                MSB.Begin(this);
            }
            else
            {
                MSB2.Begin(this);
            }
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == (int)KeyDictionary.ParseKey(Settings1.Default.Key))
                            {
                                if (vis)
                                {
                                    MSB.Begin(Window1);
                                    vis = false;
                                }
                                else
                                {
                                    MSB2.Begin(Window1);
                                    this.Activate();
                                    this.Focus();
                                    ChtSnd.Focus();
                                    vis = true;
                                }
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void DA_Comp(object sender, EventArgs e)
        {
            Visibility = Visibility.Hidden;
            vis = false;
        }

        private void Cls(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception a) { }
        }

        private void cm_Opened(object sender, RoutedEventArgs e)
        {
        }

        private void CloseB_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MSB.Begin(this);
        }

        private void CloseB_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            messages.Remove(T);
            messages.Add(new Message((Settings1.Default.Model.Contains("3.5") ? "GPT 3.5" : "GPT 3.0"))
            {
                Side = MessageSide.Error,
                Text = "Request cancelled by user. Be aware you may still be charged for the request."
            });

            curside = MessageSide.Error;

            this.DataContext = messages;

            scrollViewerScrollToEndAnim.From = ConversationScrollViewer.VerticalOffset;
            scrollViewerScrollToEndAnim.To = ConversationContentContainer.ActualHeight;
            scrollViewerStoryboard.Begin();

            ChtSnd.Focus();
            Cht = true;

            SW.Stop();
            DT.Stop();
            canceled = true;
            CancelB.Visibility = Visibility.Hidden;
        }

        private void Window1_Loaded_1(object sender, RoutedEventArgs e)
        {
            Opacity = 0;

            if (startVis)
            {
                MSB2.Begin(this);
            }
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            scrollViewerScrollToEndAnim = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new SineEase()
            };
            Storyboard.SetTarget(scrollViewerScrollToEndAnim, this);
            Storyboard.SetTargetProperty(scrollViewerScrollToEndAnim, new PropertyPath(VerticalOffsetProperty));

            scrollViewerStoryboard = new Storyboard();
            scrollViewerStoryboard.Children.Add(scrollViewerScrollToEndAnim);
            this.Resources.Add("foo", scrollViewerStoryboard);

            ChtSnd.Focus();
        }

        private void CloseB_MouseLeave_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Settings setWin = new Settings();
            setWin.Show();
        }

        private void Clean_LostFocus(object sender, RoutedEventArgs e)
        {
            C2.Begin(Confirm);
            clVis = false;
        }

        private void Clean_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Cht)
            {
                if (!clVis)
                {
                    C1.Begin(Confirm);
                    Clean.Focus();
                    clVis = true;
                }
                else
                {
                    messages.Clear();
                    C2.Begin(Confirm);
                    clVis = false;
                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Cht && !String.IsNullOrEmpty(ChtSnd.Text))
            {
                var txt = ChtSnd.Text;
                ChtSnd.Clear();

                messages.Add(new Message((Settings1.Default.Model.Contains("3.5") ? "GPT 3.5" : "GPT 3.0"))
                {
                    Side = MessageSide.Me,
                    Text = txt
                });

                curside = MessageSide.Me;

                this.DataContext = messages;

                scrollViewerScrollToEndAnim.From = ConversationScrollViewer.VerticalOffset;
                scrollViewerScrollToEndAnim.To = ConversationContentContainer.ActualHeight;
                scrollViewerStoryboard.Begin();

                ChtSnd.Focus();
                T = new Message((Settings1.Default.Model.Contains("3.5") ? "GPT 3.5" : "GPT 3.0"))
                {
                    Side = MessageSide.Typing
                };
                messages.Add(T);

                curside = MessageSide.Typing;

                this.DataContext = messages;

                scrollViewerScrollToEndAnim.From = ConversationScrollViewer.VerticalOffset;
                scrollViewerScrollToEndAnim.To = ConversationContentContainer.ActualHeight;
                scrollViewerStoryboard.Begin();

                ChtSnd.Focus();

                //writeChat("c|" + txt);
                var t = Task.Run(() => { SendMsg(txt); });

                SW.Reset();
                DT.Start();
                SW.Start();

                Cht = false;
            }
        }

        private void DT_Tick(object sender, EventArgs e)
        {
            if (SW.IsRunning)
            {
                TimeSpan ts = SW.Elapsed;
                if (ts.TotalSeconds > 7)
                {
                    CancelB.Visibility = Visibility.Visible;
                    SW.Stop();
                    DT.Stop();
                }
            }
        }
    }
}