using System.Drawing.Drawing2D;

namespace Bai2_BangVeMini;

public sealed class Form1 : Form
{
    private readonly DoubleBufferedPanel pnlCanvas = new DoubleBufferedPanel();
    private readonly Label lblViTri = new Label();
    private readonly Label lblTrangThai = new Label();
    private Bitmap? drawingBitmap;
    private bool isDrawing;
    private Point previousPoint;

    public Form1()
    {
        Text = "Bảng vẽ mini - Sự kiện chuột";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(980, 700);
        MinimumSize = new Size(760, 560);
        BackColor = Color.FromArgb(241, 245, 249);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;

        Panel header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100,
            BackColor = Color.FromArgb(37, 99, 235)
        };
        header.Controls.Add(new Label
        {
            Text = "BẢNG VẼ MINI",
            AutoSize = true,
            Location = new Point(30, 16),
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 22F)
        });
        header.Controls.Add(new Label
        {
            Text = "Giữ chuột trái để vẽ  •  Nhấn chuột phải để xóa toàn bộ",
            AutoSize = true,
            Location = new Point(33, 62),
            ForeColor = Color.FromArgb(219, 234, 254)
        });

        Panel footer = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 58,
            BackColor = Color.White,
            Padding = new Padding(24, 0, 24, 0)
        };
        lblTrangThai.Text = "Sẵn sàng";
        lblTrangThai.AutoSize = true;
        lblTrangThai.Location = new Point(25, 18);
        lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74);
        lblTrangThai.Font = new Font("Segoe UI Semibold", 11F);

        lblViTri.Text = "X: 0, Y: 0";
        lblViTri.AutoSize = true;
        lblViTri.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblViTri.Location = new Point(820, 18);
        lblViTri.Font = new Font("Consolas", 11F);
        footer.Resize += delegate
        {
            lblViTri.Left = footer.ClientSize.Width - lblViTri.Width - 25;
        };
        footer.Controls.Add(lblTrangThai);
        footer.Controls.Add(lblViTri);

        Panel canvasHost = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28),
            BackColor = Color.FromArgb(241, 245, 249)
        };
        pnlCanvas.Dock = DockStyle.Fill;
        pnlCanvas.BackColor = Color.White;
        pnlCanvas.BorderStyle = BorderStyle.FixedSingle;
        pnlCanvas.Cursor = Cursors.Cross;
        pnlCanvas.MouseDown += PnlCanvas_MouseDown;
        pnlCanvas.MouseMove += PnlCanvas_MouseMove;
        pnlCanvas.MouseUp += PnlCanvas_MouseUp;
        pnlCanvas.MouseClick += PnlCanvas_MouseClick;
        pnlCanvas.Paint += PnlCanvas_Paint;
        pnlCanvas.Resize += PnlCanvas_Resize;
        canvasHost.Controls.Add(pnlCanvas);

        Controls.Add(canvasHost);
        Controls.Add(footer);
        Controls.Add(header);
        Shown += delegate { EnsureBitmap(); };
        FormClosed += delegate { drawingBitmap?.Dispose(); };
    }

    private void PnlCanvas_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        isDrawing = true;
        previousPoint = e.Location;
        lblTrangThai.Text = "Đang vẽ...";
        lblTrangThai.ForeColor = Color.FromArgb(220, 38, 38);
        pnlCanvas.Capture = true;
    }

    private void PnlCanvas_MouseMove(object? sender, MouseEventArgs e)
    {
        lblViTri.Text = string.Format("X: {0}, Y: {1}", e.X, e.Y);

        if (!isDrawing || e.Button != MouseButtons.Left)
        {
            return;
        }

        EnsureBitmap();
        if (drawingBitmap == null)
        {
            return;
        }

        using (Graphics graphics = Graphics.FromImage(drawingBitmap))
        using (Pen pen = new Pen(Color.FromArgb(15, 23, 42), 3F))
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            graphics.DrawLine(pen, previousPoint, e.Location);
        }

        Rectangle changedArea = GetChangedArea(previousPoint, e.Location, 8);
        previousPoint = e.Location;
        pnlCanvas.Invalidate(changedArea);
    }

    private void PnlCanvas_MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        isDrawing = false;
        pnlCanvas.Capture = false;
        lblTrangThai.Text = "Sẵn sàng";
        lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74);
    }

    private void PnlCanvas_MouseClick(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
        {
            return;
        }

        EnsureBitmap();
        if (drawingBitmap != null)
        {
            using (Graphics graphics = Graphics.FromImage(drawingBitmap))
            {
                graphics.Clear(Color.White);
            }
        }

        isDrawing = false;
        lblTrangThai.Text = "Sẵn sàng";
        lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74);
        pnlCanvas.Invalidate();
    }

    private void PnlCanvas_Paint(object? sender, PaintEventArgs e)
    {
        if (drawingBitmap != null)
        {
            e.Graphics.DrawImageUnscaled(drawingBitmap, Point.Empty);
        }
    }

    private void PnlCanvas_Resize(object? sender, EventArgs e)
    {
        if (pnlCanvas.ClientSize.Width <= 0 || pnlCanvas.ClientSize.Height <= 0)
        {
            return;
        }

        Bitmap resizedBitmap = new Bitmap(pnlCanvas.ClientSize.Width, pnlCanvas.ClientSize.Height);
        using (Graphics graphics = Graphics.FromImage(resizedBitmap))
        {
            graphics.Clear(Color.White);
            if (drawingBitmap != null)
            {
                graphics.DrawImageUnscaled(drawingBitmap, Point.Empty);
            }
        }

        drawingBitmap?.Dispose();
        drawingBitmap = resizedBitmap;
        pnlCanvas.Invalidate();
    }

    private void EnsureBitmap()
    {
        if (drawingBitmap != null || pnlCanvas.ClientSize.Width <= 0 || pnlCanvas.ClientSize.Height <= 0)
        {
            return;
        }

        drawingBitmap = new Bitmap(pnlCanvas.ClientSize.Width, pnlCanvas.ClientSize.Height);
        using (Graphics graphics = Graphics.FromImage(drawingBitmap))
        {
            graphics.Clear(Color.White);
        }
    }

    private static Rectangle GetChangedArea(Point start, Point end, int padding)
    {
        int left = Math.Min(start.X, end.X) - padding;
        int top = Math.Min(start.Y, end.Y) - padding;
        int right = Math.Max(start.X, end.X) + padding;
        int bottom = Math.Max(start.Y, end.Y) + padding;
        return Rectangle.FromLTRB(left, top, right, bottom);
    }
}

internal sealed class DoubleBufferedPanel : Panel
{
    public DoubleBufferedPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
    }
}
