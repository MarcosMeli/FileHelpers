#define FRAMEWORKMENUS
/*=======================================================================
  Copyright (C) Lyquidity Solutions Limited.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.

  LYQUIDITY SOLUTIONS LIMITED DOES NOT IMPOSE ANY LIMITATION ON THE
  USE OF THIS CODE AND IT AN BE USED IN COMMERCIAL APPLICATIONS.  LYQUIDITY
  ACCEPTS NO RESPONSIBLY FOR ANY LIABILITY WHATEVER AND WILL NOT PROVIDE
  ANY SUPPORT TO USER OR THEIR CLIENTS.
=======================================================================*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#if !FRAMEWORKMENUS
using Lyquidity.UtilityLibrary.MenusA;
using Lyquidity.UtilityLibrary.General;
#endif

namespace MyControls
{

    #region Enumerations

    public enum OrientationMode
    {
        Horizontal = 0,
        Vertical = 1
    }

    public enum ScaleMode
    {
        Points = 0,
        Pixels = 1,
        Centimetres = 2,
        Inches = 3
    }

    public enum RulerAlignment
    {
        TopOrLeft,
        Middle,
        BottomOrRight
    }


    internal enum Msg
    {
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSELEAVE = 0x02A3,
        WM_NCMOUSELEAVE = 0x02A2,
    }

    #endregion

    /// <summary>
    /// Summary description for RulerControl.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof (RulerControl), "Ruler.bmp")]
    public class RulerControl : System.Windows.Forms.Control, IMessageFilter
    {
        #region Internal Variables

#if !FRAMEWORKMENUS
		private PopupMenu			_mnuContext			= null;
#endif
        private int _Scale;
        private int _ScaleStartValue;
        private bool _bDrawLine = false;
        private bool _bInControl = false;
        private int _iMousePosition = 1;
        private int _iOldMousePosition = -1;
        private Bitmap _Bitmap = null;

        #endregion

        #region Property variable

        private OrientationMode _Orientation;
        private ScaleMode _ScaleMode;
        private RulerAlignment _RulerAlignment = RulerAlignment.BottomOrRight;
        private Border3DStyle _i3DBorderStyle = Border3DStyle.Etched;
        private int _iMajorInterval = 100;
        private int _iNumberOfDivisions = 10;
        private int _DivisionMarkFactor = 5;
        private int _MiddleMarkFactor = 3;
        private double _ZoomFactor = 1;
        private double _StartValue = 0;
        private bool _bMouseTrackingOn = false;
        private bool _VerticalNumbers = true;

        #endregion

        #region Event Arguments

        public class ScaleModeChangedEventArgs : EventArgs
        {
            public ScaleMode Mode;

            public ScaleModeChangedEventArgs(ScaleMode Mode)
                : base()
            {
                this.Mode = Mode;
            }
        }

        public class HooverValueEventArgs : EventArgs
        {
            public double Value;

            public HooverValueEventArgs(double Value)
                : base()
            {
                this.Value = Value;
            }
        }

        #endregion

        #region Delegates

        public delegate void ScaleModeChangedEvent(object sender, ScaleModeChangedEventArgs e);

        public delegate void HooverValueEvent(object sender, HooverValueEventArgs e);

        // public delegate void ClickEvent(object sender, ClickEventArgs e);

        #endregion

        #region Events

        public event ScaleModeChangedEvent ScaleModeChanged;
        public event HooverValueEvent HooverValue;

        #endregion

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #region Constructors/Destructors

        public RulerControl()
        {
            base.BackColor = System.Drawing.Color.White;
            base.ForeColor = System.Drawing.Color.Black;

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

#if !FRAMEWORKMENUS

    // Create the popup menu object
			_mnuContext = new PopupMenu();

			// Create the menu objects
			MenuCommand mnuPoints = new MenuCommand("Points", new EventHandler(Popup_Click));
			mnuPoints.Tag = enumScaleMode.smPoints;
			MenuCommand mnuPixels = new MenuCommand("Pixels", new EventHandler(Popup_Click));
			mnuPixels.Tag = enumScaleMode.smPixels;
			MenuCommand mnuCentimetres = new MenuCommand("Centimetres", new EventHandler(Popup_Click));
			mnuCentimetres.Tag = enumScaleMode.smCentimetres;
			MenuCommand mnuInches = new MenuCommand("Inches", new EventHandler(Popup_Click));
			mnuInches.Tag = enumScaleMode.smInches;

			// Define the list of menu commands
			_mnuContext.MenuCommands.AddRange(new MenuCommand[]{mnuPoints, mnuPixels, mnuCentimetres, mnuInches});

			// Define the properties to get appearance to match MenuControl
			_mnuContext.Style = VisualStyle.IDE;

#endif

#if FRAMEWORKMENUS

            System.Windows.Forms.MenuItem mnuPoints = new System.Windows.Forms.MenuItem("Points",
                new EventHandler(Popup_Click));
            System.Windows.Forms.MenuItem mnuPixels = new System.Windows.Forms.MenuItem("Pixels",
                new EventHandler(Popup_Click));
            System.Windows.Forms.MenuItem mnuCentimetres = new System.Windows.Forms.MenuItem("Centimetres",
                new EventHandler(Popup_Click));
            System.Windows.Forms.MenuItem mnuInches = new System.Windows.Forms.MenuItem("Inches",
                new EventHandler(Popup_Click));
            ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {mnuPoints, mnuPixels, mnuCentimetres, mnuInches});
#endif
            ScaleMode = ScaleMode.Points;
        }

        #endregion

        #region Methods

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool PreFilterMessage(ref Message m)
        {
            if (!this._bMouseTrackingOn)
                return false;

            if (m.Msg == (int) Msg.WM_MOUSEMOVE) {
                int X = 0;
                int Y = 0;

                // The mouse coordinate are measured in screen coordinates because thats what 
                // Control.MousePosition returns.  The Message,LParam value is not used because
                // it returns the mouse position relative to the control the mouse is over. 
                // Chalk and cheese.

                Point pointScreen = Control.MousePosition;

                // Get the origin of this control in screen coordinates so that later we can 
                // compare it against the mouse point to determine it we've hit this control.

                Point pointClientOrigin = new Point(X, Y);
                pointClientOrigin = PointToScreen(pointClientOrigin);

                _bDrawLine = false;
                _bInControl = false;

                HooverValueEventArgs eHoover = null;

                // Work out whether the mouse is within the Y-axis bounds of a vertical ruler or 
                // within the X-axis bounds of a horizontal ruler

                if (this.Orientation == OrientationMode.Horizontal) {
                    _bDrawLine = (pointScreen.X >= pointClientOrigin.X) &&
                                 (pointScreen.X <= pointClientOrigin.X + this.Width);
                }
                else {
                    _bDrawLine = (pointScreen.Y >= pointClientOrigin.Y) &&
                                 (pointScreen.Y <= pointClientOrigin.Y + this.Height);
                }

                // If the mouse is in valid position...
                if (_bDrawLine) {
                    // ...workout the position of the mouse relative to the 
                    X = pointScreen.X - pointClientOrigin.X;
                    Y = pointScreen.Y - pointClientOrigin.Y;

                    // Determine whether the mouse is within the bounds of the control itself
                    _bInControl = (this.ClientRectangle.Contains(new Point(X, Y)));

                    // Make the relative mouse position available in pixel relative to this control's origin
                    ChangeMousePosition((this.Orientation == OrientationMode.Horizontal)
                        ? X
                        : Y);
                    eHoover = new HooverValueEventArgs(CalculateValue(_iMousePosition));
                }
                else {
                    ChangeMousePosition(-1);
                    eHoover = new HooverValueEventArgs(_iMousePosition);
                }

                // Paint directly by calling the OnPaint() method.  This way the background is not 
                // hosed by the call to Invalidate() so paining occurs without the hint of a flicker
                PaintEventArgs e = null;
                try {
                    e = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
                    OnPaint(e);
                }
                finally {
                    e.Graphics.Dispose();
                }

                OnHooverValue(eHoover);
            }

            if ((m.Msg == (int) Msg.WM_MOUSELEAVE) ||
                (m.Msg == (int) Msg.WM_NCMOUSELEAVE)) {
                _bDrawLine = false;
                PaintEventArgs paintArgs = null;
                try {
                    paintArgs = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
                    this.OnPaint(paintArgs);
                }
                finally {
                    paintArgs.Graphics.Dispose();
                }
            }

            return false; // Whether or not the message is filtered
        }


        public double PixelToScaleValue(int iOffset)
        {
            return this.CalculateValue(iOffset);
        }

        public int ScaleValueToPixel(double nScaleValue)
        {
            return CalculatePixel(nScaleValue);
        }

        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // RulerControl
            // 
            this.Name = "RulerControl";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RulerControl_MouseUp);

#if FRAMEWORKMENUS
            this.ContextMenu = new ContextMenu();
            this.ContextMenu.Popup += new EventHandler(ContextMenu_Popup);
#endif
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Take private resize actions here
            _Bitmap = null;
            this.Invalidate();
        }

        public override void Refresh()
        {
            base.Refresh();
            this.Invalidate();
        }


        [Description("Draws the ruler marks in the scale requested.")]
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawControl(e.Graphics);
        }


        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            try {
                if (_bMouseTrackingOn)
                    Application.RemoveMessageFilter(this);
            }
            catch {}
        }

        #endregion

        #region Event Handlers

        private void RulerControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
//			if (e.Button.Equals(MouseButtons.Right)) 
        }

        private void RulerControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
#if FRAMEWORKMENUS
            if ((Control.MouseButtons & MouseButtons.Right) != 0) {
                this.ContextMenu.Show(this, PointToClient(Control.MousePosition));
#else
			if ((e.Button & MouseButtons.Right) != 0)
			{
				_mnuContext.TrackPopup(this.PointToScreen(new Point(e.X, e.Y)));
#endif
            }
            else {
                EventArgs eClick = new EventArgs();
                this.OnClick(eClick);
            }
        }

        private void Popup_Click(object sender, EventArgs e)
        {
#if FRAMEWORKMENUS
            System.Windows.Forms.MenuItem item = (System.Windows.Forms.MenuItem) sender;
            ScaleMode = (ScaleMode) item.Index;
#else
			MenuCommand item = (MenuCommand)sender;
			ScaleMode = (enumScaleMode)item.Tag;
#endif
            _Bitmap = null;
            Invalidate();
        }

        protected void OnHooverValue(HooverValueEventArgs e)
        {
            if (HooverValue != null)
                HooverValue(this, e);
        }

        protected void OnScaleModeChanged(ScaleModeChangedEventArgs e)
        {
            if (ScaleModeChanged != null)
                ScaleModeChanged(this, e);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            _bDrawLine = false;
            Invalidate();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            Invalidate();
        }

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Popup");
        }

        #endregion

        #region Properties

        [
            DefaultValue(typeof (Border3DStyle), "Etched"),
            Description("The border style use the Windows.Forms.Border3DStyle type"),
            Category("Ruler"),
        ]
        public Border3DStyle BorderStyle
        {
            get { return _i3DBorderStyle; }
            set
            {
                _i3DBorderStyle = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description("Horizontal or vertical layout")]
        [Category("Ruler")]
        public OrientationMode Orientation
        {
            get { return _Orientation; }
            set
            {
                _Orientation = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description("A value from which the ruler marking should be shown.  Default is zero.")]
        [Category("Ruler")]
        public double StartValue
        {
            get { return _StartValue; }
            set
            {
                _StartValue = value;
                _ScaleStartValue = Convert.ToInt32(value*_Scale/_iMajorInterval); // Convert value to pixels
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description("The scale to use")]
        [Category("Ruler")]
        public ScaleMode ScaleMode
        {
            get { return _ScaleMode; }
            set
            {
                ScaleMode iOldScaleMode = _ScaleMode;
                _ScaleMode = value;

                if (_iMajorInterval == DefaultMajorInterval(iOldScaleMode)) {
                    // Set the default Scale and MajorInterval value
                    _Scale = DefaultScale(_ScaleMode);
                    _iMajorInterval = DefaultMajorInterval(_ScaleMode);
                }
                else
                    MajorInterval = _iMajorInterval;

                // Use the current start value (if there is one)
                this.StartValue = this._StartValue;

                // Setup the menu
                for (int i = 0; i <= 3; i++)
#if FRAMEWORKMENUS
                    ContextMenu.MenuItems[i].Checked = false;

                ContextMenu.MenuItems[(int) value].Checked = true;
#else
					_mnuContext.MenuCommands[i].Checked = false;

				_mnuContext.MenuCommands[(int)value].Checked = true;
#endif

                ScaleModeChangedEventArgs e = new ScaleModeChangedEventArgs(value);
                this.OnScaleModeChanged(e);
            }
        }

        [Description(
            "The value of the major interval.  When displaying inches, 1 is a typical value.  When displaying Points, 36 or 72 might good values."
            )]
        [Category("Ruler")]
        public int MajorInterval
        {
            get { return _iMajorInterval; }
            set
            {
                if (value <= 0)
                    throw new Exception("The major interval value cannot be less than one");
                _iMajorInterval = value;
                _Scale = DefaultScale(_ScaleMode)*_iMajorInterval/DefaultMajorInterval(_ScaleMode);
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description("How many divisions should be shown between each major interval")]
        [Category("Ruler")]
        public int Divisions
        {
            get { return _iNumberOfDivisions; }
            set
            {
                if (value <= 0)
                    throw new Exception("The number of divisions cannot be less than one");
                _iNumberOfDivisions = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description(
            "The height or width of this control multiplied by the reciprocal of this number will be used to compute the height of the non-middle division marks."
            )]
        [Category("Ruler")]
        public int DivisionMarkFactor
        {
            get { return _DivisionMarkFactor; }
            set
            {
                if (value <= 0)
                    throw new Exception("The Division Mark Factor cannot be less than one");
                _DivisionMarkFactor = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description(
            "The height or width of this control multiplied by the reciprocal of this number will be used to compute the height of the middle division mark."
            )]
        [Category("Ruler")]
        public int MiddleMarkFactor
        {
            get { return _MiddleMarkFactor; }
            set
            {
                if (value <= 0)
                    throw new Exception("The Middle Mark Factor cannot be less than one");
                _MiddleMarkFactor = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description(
            "The value of the current mouse position expressed in unit of the scale set (centimetres, inches, etc.")]
        [Category("Ruler")]
        public double ScaleValue
        {
            get { return CalculateValue(_iMousePosition); }
        }

        [Description(
            "TRUE if a line is displayed to track the current position of the mouse and events are generated as the mouse moves."
            )]
        [Category("Ruler")]
        public bool MouseTrackingOn
        {
            get { return _bMouseTrackingOn; }
            set
            {
                if (value == _bMouseTrackingOn)
                    return;

                if (value) {
                    // Tracking is being enabled so add the message filter hook
                    Application.AddMessageFilter(this);
                }
                else {
                    // Tracking is being disabled so remove the message filter hook
                    Application.RemoveMessageFilter(this);
                    ChangeMousePosition(-1);
                }

                _bMouseTrackingOn = value;

                _Bitmap = null;
                Invalidate();
            }
        }

        [Description("The font used to display the division number")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description(
            "Return the mouse position as number of pixels from the top or left of the control.  -1 means that the mouse is positioned before or after the control."
            )]
        [Category("Ruler")]
        public int MouseLocation
        {
            get { return _iMousePosition; }
        }

        [DefaultValue(typeof (Color), "ControlDarkDark")]
        [Description("The color used to lines and numbers on the ruler")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [DefaultValue(typeof (Color), "White")]
        [Description("The color used to paint the background of the ruler")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                _Bitmap = null;
                Invalidate();
            }
        }


        [Description("")]
        [Category("Ruler")]
        public bool VerticalNumbers
        {
            get { return _VerticalNumbers; }
            set
            {
                _VerticalNumbers = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description("A factor between 0.5 and 2 by which the displayed scale will be zoomed.")]
        [Category("Ruler")]
        public double ZoomFactor
        {
            get { return _ZoomFactor; }
            set
            {
                if ((value < 0.5) ||
                    (value > 2))
                    throw new Exception("Zoom factor can be between 50% and 200%");
                if (_ZoomFactor == value)
                    return;
                _ZoomFactor = value;
                this.ScaleMode = _ScaleMode;
                _Bitmap = null;
                Invalidate();
            }
        }

        [Description("Determines how the ruler markings are displayed")]
        [Category("Ruler")]
        public RulerAlignment RulerAlignment
        {
            get { return _RulerAlignment; }
            set
            {
                if (_RulerAlignment == value)
                    return;
                _RulerAlignment = value;
                _Bitmap = null;
                Invalidate();
            }
        }

        #endregion

        #region Private functions

        private double CalculateValue(int iOffset)
        {
            if (iOffset < 0)
                return 0;

            double nValue = ((double) iOffset - Start())/(double) _Scale*(double) _iMajorInterval;
            return nValue + this._StartValue;
        }

        [Description(
            "May not return zero even when a -ve scale number is given as the returned value needs to allow for the border thickness"
            )]
        private int CalculatePixel(double nScaleValue)
        {
            double nValue = nScaleValue - this._StartValue;
            if (nValue < 0)
                return Start(); // Start is the offset to the actual display area to allow for the border (if any)

            int iOffset = Convert.ToInt32(nValue/(double) _iMajorInterval*(double) _Scale);

            return iOffset + Start();
        }

        public void RenderTrackLine(Graphics g)
        {
            if (_bMouseTrackingOn & _bDrawLine) {
                int iOffset = Offset();

                // Optionally render Mouse tracking line
                switch (Orientation) {
                    case OrientationMode.Horizontal:
                        Line(g, _iMousePosition, iOffset, _iMousePosition, Height - iOffset);
                        break;
                    case OrientationMode.Vertical:
                        Line(g, iOffset, _iMousePosition, Width - iOffset, _iMousePosition);
                        break;
                }
            }
        }

        private void DrawControl(Graphics graphics)
        {
            Graphics g = null;

            if (_Bitmap == null) {
                // Create a bitmap
                _Bitmap = new Bitmap(this.Width, this.Height);

                g = Graphics.FromImage(_Bitmap);

                try {
                    // Wash the background with BackColor
                    g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, _Bitmap.Width, _Bitmap.Height);

                    // Paint the lines on the image
                    int iScale = _Scale;

                    int iStart = Start();
                    int iEnd = (this.Orientation == OrientationMode.Horizontal)
                        ? Width
                        : Height;

                    if (this.Orientation == OrientationMode.Vertical)
                        System.Diagnostics.Debug.WriteLine("Vert");

                    for (int j = iStart; j <= iEnd; j += iScale) {
                        int iLeft = _Scale; // Make an assumption that we're starting at zero or on a major increment
                        int jOffset = j + _ScaleStartValue;

                        iScale = ((jOffset - iStart)%_Scale);
                            // Get the mod vale to see if this is "big line" opportunity

                        // If it is, draw big line
                        if (iScale == 0) {
                            if (_RulerAlignment != RulerAlignment.Middle) {
                                if (this.Orientation == OrientationMode.Horizontal)
                                    Line(g, j, 0, j, Height);
                                else
                                    Line(g, 0, j, Width, j);
                            }

                            iLeft = _Scale; // Set the for loop increment
                        }
                        else
                            iLeft = _Scale - iScale; // Set the for loop increment

                        iScale = iLeft;

                        int iValue = (((jOffset - iStart)/_Scale) + 1)*_iMajorInterval;
                        DrawValue(g, iValue, j - iStart, iScale);

                        int iUsed = 0;

                        //Draw small lines
                        for (int i = 0; i < _iNumberOfDivisions; i++) {
                            int iX =
                                Convert.ToInt32(Math.Round(
                                    (double) (_Scale - iUsed)/(double) (_iNumberOfDivisions - i),
                                    0)); // Use a spreading algorithm rather that using expensive floating point numbers
                            iUsed += iX;

                            if (iUsed >= (_Scale - iLeft)) {
                                iX = iUsed + j - (_Scale - iLeft);

                                // Is it an even number and, if so, is it the middle value?
                                bool bMiddleMark = ((_iNumberOfDivisions & 0x1) == 0) & (i + 1 == _iNumberOfDivisions/2);
                                bool bShowMiddleMark = bMiddleMark;
                                bool bLastDivisionMark = (i + 1 == _iNumberOfDivisions);
                                bool bLastAlignMiddleDivisionMark = bLastDivisionMark &
                                                                    (_RulerAlignment == RulerAlignment.Middle);
                                bool bShowDivisionMark = !bMiddleMark & !bLastAlignMiddleDivisionMark;

                                if (bShowMiddleMark)
                                    DivisionMark(g, iX, _MiddleMarkFactor); // Height or Width will be 1/3
                                else if (bShowDivisionMark)
                                    DivisionMark(g, iX, _DivisionMarkFactor); // Height or Width will be 1/5
                            }
                        }
                    }

                    if (_i3DBorderStyle != Border3DStyle.Flat)
                        ControlPaint.DrawBorder3D(g, this.ClientRectangle, this._i3DBorderStyle);
                }
                catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                finally {
                    g.Dispose();
                }
            }

            g = graphics;

            try {
                // Always draw the bitmap
                g.DrawImage(_Bitmap, this.ClientRectangle);

                RenderTrackLine(g);
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally {
                GC.Collect();
            }
        }

        private void DivisionMark(Graphics g, int iPosition, int iProportion)
        {
            // This function is affected by the RulerAlignment setting

            int iMarkStart = 0, iMarkEnd = 0;

            if (this.Orientation == OrientationMode.Horizontal) {
                switch (_RulerAlignment) {
                    case RulerAlignment.BottomOrRight:
                    {
                        iMarkStart = Height - Height/iProportion;
                        iMarkEnd = Height;
                        break;
                    }
                    case RulerAlignment.Middle:
                    {
                        iMarkStart = (Height - Height/iProportion)/2 - 1;
                        iMarkEnd = iMarkStart + Height/iProportion;
                        break;
                    }
                    case RulerAlignment.TopOrLeft:
                    {
                        iMarkStart = 0;
                        iMarkEnd = Height/iProportion;
                        break;
                    }
                }

                Line(g, iPosition, iMarkStart, iPosition, iMarkEnd);
            }
            else {
                switch (_RulerAlignment) {
                    case RulerAlignment.BottomOrRight:
                    {
                        iMarkStart = Width - Width/iProportion;
                        iMarkEnd = Width;
                        break;
                    }
                    case RulerAlignment.Middle:
                    {
                        iMarkStart = (Width - Width/iProportion)/2 - 1;
                        iMarkEnd = iMarkStart + Width/iProportion;
                        break;
                    }
                    case RulerAlignment.TopOrLeft:
                    {
                        iMarkStart = 0;
                        iMarkEnd = Width/iProportion;
                        break;
                    }
                }

                Line(g, iMarkStart, iPosition, iMarkEnd, iPosition);
            }
        }

        private void DrawValue(Graphics g, int iValue, int iPosition, int iSpaceAvailable)
        {
            // The sizing operation is common to all options
            StringFormat format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
            if (_VerticalNumbers)
                format.FormatFlags |= StringFormatFlags.DirectionVertical;

            SizeF size = g.MeasureString((iValue).ToString(), this.Font, iSpaceAvailable, format);

            Point drawingPoint;
            int iX = 0;
            int iY = 0;

            if (this.Orientation == OrientationMode.Horizontal) {
                switch (_RulerAlignment) {
                    case RulerAlignment.BottomOrRight:
                    {
                        iX = iPosition + iSpaceAvailable - (int) size.Width - 2;
                        iY = 2;
                        break;
                    }
                    case RulerAlignment.Middle:
                    {
                        iX = iPosition + iSpaceAvailable - (int) size.Width/2;
                        iY = (Height - (int) size.Height)/2 - 2;
                        break;
                    }
                    case RulerAlignment.TopOrLeft:
                    {
                        iX = iPosition + iSpaceAvailable - (int) size.Width - 2;
                        iY = Height - 2 - (int) size.Height;
                        break;
                    }
                }

                drawingPoint = new Point(iX, iY);
            }
            else {
                switch (_RulerAlignment) {
                    case RulerAlignment.BottomOrRight:
                    {
                        iX = 2;
                        iY = iPosition + iSpaceAvailable - (int) size.Height - 2;
                        break;
                    }
                    case RulerAlignment.Middle:
                    {
                        iX = (Width - (int) size.Width)/2 - 1;
                        iY = iPosition + iSpaceAvailable - (int) size.Height/2;
                        break;
                    }
                    case RulerAlignment.TopOrLeft:
                    {
                        iX = Width - 2 - (int) size.Width;
                        iY = iPosition + iSpaceAvailable - (int) size.Height - 2;
                        break;
                    }
                }

                drawingPoint = new Point(iX, iY);
            }

            // The drawstring function is common to all operations

            g.DrawString(iValue.ToString(), this.Font, new SolidBrush(this.ForeColor), drawingPoint, format);
        }

        private void Line(Graphics g, int x1, int y1, int x2, int y2)
        {
            g.DrawLine(new Pen(new SolidBrush(this.ForeColor)), x1, y1, x2, y2);
        }

        private int DefaultScale(ScaleMode iScaleMode)
        {
            int iScale = 100;

            // Set scaling
            switch (iScaleMode) {
                    // Determines the *relative* proportions of each scale
                case ScaleMode.Points:
                    iScale = 720;
                    break;
                case ScaleMode.Pixels:
                    iScale = 100;
                    break;
                case ScaleMode.Centimetres:
                    iScale = 38;
                    break;
                case ScaleMode.Inches:
                    iScale = 96;
                    break;
            }

            return Convert.ToInt32((double) iScale*_ZoomFactor);
        }

        private int DefaultMajorInterval(ScaleMode iScaleMode)
        {
            int iInterval = 10;

            // Set scaling
            switch (iScaleMode) {
                    // Determines the *relative* proportions of each scale
                case ScaleMode.Points:
                    iInterval = 72;
                    break;
                case ScaleMode.Pixels:
                    iInterval = 100;
                    break;
                case ScaleMode.Centimetres:
                    iInterval = 1;
                    break;
                case ScaleMode.Inches:
                    iInterval = 1;
                    break;
            }

            return iInterval;
        }

        private int Offset()
        {
            int iOffset = 0;

            switch (this._i3DBorderStyle) {
                case Border3DStyle.Flat:
                    iOffset = 0;
                    break;
                case Border3DStyle.Adjust:
                    iOffset = 0;
                    break;
                case Border3DStyle.Sunken:
                    iOffset = 2;
                    break;
                case Border3DStyle.Bump:
                    iOffset = 2;
                    break;
                case Border3DStyle.Etched:
                    iOffset = 2;
                    break;
                case Border3DStyle.Raised:
                    iOffset = 2;
                    break;
                case Border3DStyle.RaisedInner:
                    iOffset = 1;
                    break;
                case Border3DStyle.RaisedOuter:
                    iOffset = 1;
                    break;
                case Border3DStyle.SunkenInner:
                    iOffset = 1;
                    break;
                case Border3DStyle.SunkenOuter:
                    iOffset = 1;
                    break;
                default:
                    iOffset = 0;
                    break;
            }

            return iOffset;
        }

        private int Start()
        {
            int iStart = 0;

            switch (this._i3DBorderStyle) {
                case Border3DStyle.Flat:
                    iStart = 0;
                    break;
                case Border3DStyle.Adjust:
                    iStart = 0;
                    break;
                case Border3DStyle.Sunken:
                    iStart = 1;
                    break;
                case Border3DStyle.Bump:
                    iStart = 1;
                    break;
                case Border3DStyle.Etched:
                    iStart = 1;
                    break;
                case Border3DStyle.Raised:
                    iStart = 1;
                    break;
                case Border3DStyle.RaisedInner:
                    iStart = 0;
                    break;
                case Border3DStyle.RaisedOuter:
                    iStart = 0;
                    break;
                case Border3DStyle.SunkenInner:
                    iStart = 0;
                    break;
                case Border3DStyle.SunkenOuter:
                    iStart = 0;
                    break;
                default:
                    iStart = 0;
                    break;
            }
            return iStart;
        }

        private void ChangeMousePosition(int iNewPosition)
        {
            this._iOldMousePosition = this._iMousePosition;
            this._iMousePosition = iNewPosition;
        }
    }

    #endregion
}