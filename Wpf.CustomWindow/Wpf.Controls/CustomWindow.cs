using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shapes;

namespace Wpf.Controls
{
    public class CustomWindow : Window
    {

        public CustomWindow() : base()
        {
            PreviewMouseMove += OnPreviewMouseMove;
        }
        static CustomWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow),
                new FrameworkPropertyMetadata(typeof(CustomWindow)));
        }

        
        #region Click events

        protected void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

       
        protected void RestoreClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
        protected void MaxClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        protected void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void moveRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        #endregion
        
        
        public static readonly DependencyProperty MinimizeProperty =
            DependencyProperty.Register("CanMinimize", typeof(bool), typeof(CustomWindow),
                new PropertyMetadata(true));

        public bool CanMinimize
        {
            get => (bool)GetValue(MinimizeProperty);
            set => SetValue(MinimizeProperty, value);
        }

        public static readonly DependencyProperty ResizeProperty =
            DependencyProperty.Register("CanResize", typeof(bool), typeof(CustomWindow),
                new PropertyMetadata(true));

        public bool CanResize
        {
            get => (bool)GetValue(ResizeProperty);
            set => SetValue(ResizeProperty, value);
        }

        public static readonly DependencyProperty RestoreProperty =
            DependencyProperty.Register("CanRestore", typeof(bool), typeof(CustomWindow),
                new PropertyMetadata(true));

        public bool CanRestore
        {
            get => (bool)GetValue(RestoreProperty);
            set => SetValue(RestoreProperty, value);
        }

        public override void OnApplyTemplate()
        {
            Label WindowTitle = GetTemplateChild("WindowTitle") as Label;
            WindowTitle.Content = Title;
            WindowTitle.MouseDoubleClick += Window_MouseDoubleClick;
            Button minimizeButton = GetTemplateChild("minimizeButton") as Button;
            if (minimizeButton != null & CanMinimize) 
                minimizeButton.Click += MinimizeClick;
            else if (minimizeButton != null & !CanMinimize)
                minimizeButton.Visibility = Visibility.Collapsed;
            


            Button restoreButton = GetTemplateChild("restoreButton") as Button;
            Button restoreButton1 = GetTemplateChild("restoreButton1") as Button;
            

            if (restoreButton1 != null)
            {
                
                restoreButton1.Click += MaxClick;
            }

            if (restoreButton != null & CanMinimize)
            {
                restoreButton.Click += RestoreClick;
            }
            else if (restoreButton != null & !CanMinimize)
            {
                restoreButton.Visibility = Visibility.Collapsed;
            }

            Rectangle rectangle = GetTemplateChild("moveRectangle") as Rectangle;
            if(rectangle != null)
            {
                rectangle.PreviewMouseDown += moveRectangle_PreviewMouseDown;
            }

            Button closeButton = GetTemplateChild("closeButton") as Button;
            if (closeButton != null)
                closeButton.Click += CloseClick;

            


            Grid resizeGrid = GetTemplateChild("resizeGrid") as Grid;
            if (resizeGrid != null & CanMinimize == true)
            {
                foreach (UIElement element in resizeGrid.Children)
                {
                    Rectangle resizeRectangle = element as Rectangle;
                    if (resizeRectangle != null)
                    {
                        resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                        resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                    }
                }
            }

            base.OnApplyTemplate();
            //base.OnApplyTemplate();
        }

        private void Rectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void ResizeRectangle_MouseMove(Object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    break;
            }
        }
        protected void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);
        protected void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
                default:
                    break;
            }
        }
        private HwndSource _hwndSource;

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += OnSourceInitialized;
            base.OnInitialized(e);
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }
        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }
    }
}
