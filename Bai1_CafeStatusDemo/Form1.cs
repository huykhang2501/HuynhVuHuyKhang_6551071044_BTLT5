namespace Bai1_CafeStatusDemo;

public sealed class Form1 : Form
{
    private readonly ToolStripStatusLabel lblGioHienTai = new ToolStripStatusLabel();
    private readonly ToolStripStatusLabel lblTenQuan = new ToolStripStatusLabel();
    private readonly ToolStripStatusLabel lblTrangThai = new ToolStripStatusLabel();
    private readonly Label lblDongHoLon = new Label();
    private readonly Label lblTrangThaiLon = new Label();
    private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

    public Form1()
    {
        Text = "CafeStatusDemo - CAFE ÁNH DƯƠNG";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(850, 520);
        MinimumSize = new Size(700, 450);
        BackColor = Color.FromArgb(255, 247, 237);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;

        MenuStrip menu = new MenuStrip();
        ToolStripMenuItem heThong = new ToolStripMenuItem("&Hệ thống");
        ToolStripMenuItem doiMauNen = new ToolStripMenuItem("Đổi &màu nền...");
        ToolStripMenuItem thoat = new ToolStripMenuItem("&Thoát");
        doiMauNen.Click += DoiMauNen_Click;
        thoat.Click += delegate { Application.Exit(); };
        heThong.DropDownItems.Add(doiMauNen);
        heThong.DropDownItems.Add(new ToolStripSeparator());
        heThong.DropDownItems.Add(thoat);
        menu.Items.Add(heThong);
        MainMenuStrip = menu;

        Panel banner = new Panel
        {
            Dock = DockStyle.Top,
            Height = 105,
            BackColor = Color.FromArgb(180, 83, 9)
        };
        banner.Controls.Add(new Label
        {
            Text = "☕  CAFE ÁNH DƯƠNG",
            AutoSize = true,
            Location = new Point(32, 18),
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 25F)
        });
        banner.Controls.Add(new Label
        {
            Text = "Không gian ấm áp • Phục vụ mỗi ngày từ 06:00 đến 22:00",
            AutoSize = true,
            Location = new Point(38, 70),
            ForeColor = Color.FromArgb(254, 215, 170)
        });

        TableLayoutPanel center = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(30)
        };
        center.RowStyles.Add(new RowStyle(SizeType.Percent, 32F));
        center.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
        center.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
        Label caption = new Label
        {
            Text = "THỜI GIAN HIỆN TẠI",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.BottomCenter,
            ForeColor = Color.FromArgb(120, 53, 15),
            Font = new Font("Segoe UI Semibold", 13F)
        };
        lblDongHoLon.Dock = DockStyle.Fill;
        lblDongHoLon.TextAlign = ContentAlignment.MiddleCenter;
        lblDongHoLon.ForeColor = Color.FromArgb(120, 53, 15);
        lblDongHoLon.Font = new Font("Segoe UI", 46F, FontStyle.Bold);
        lblTrangThaiLon.Dock = DockStyle.Fill;
        lblTrangThaiLon.TextAlign = ContentAlignment.TopCenter;
        lblTrangThaiLon.Font = new Font("Segoe UI Semibold", 18F);
        center.Controls.Add(caption, 0, 0);
        center.Controls.Add(lblDongHoLon, 0, 1);
        center.Controls.Add(lblTrangThaiLon, 0, 2);

        StatusStrip status = new StatusStrip { SizingGrip = false, BackColor = Color.White };
        lblGioHienTai.TextAlign = ContentAlignment.MiddleLeft;
        lblTenQuan.Text = "CAFE ÁNH DƯƠNG";
        lblTenQuan.Spring = true;
        lblTenQuan.TextAlign = ContentAlignment.MiddleCenter;
        lblTenQuan.Font = new Font("Segoe UI Semibold", 9F);
        lblTrangThai.TextAlign = ContentAlignment.MiddleRight;
        status.Items.Add(lblGioHienTai);
        status.Items.Add(lblTenQuan);
        status.Items.Add(lblTrangThai);

        Controls.Add(center);
        Controls.Add(banner);
        Controls.Add(menu);
        Controls.Add(status);

        timer.Interval = 1000;
        timer.Tick += delegate { UpdateStatus(); };
        timer.Start();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        DateTime now = DateTime.Now;
        string time = now.ToString("HH:mm:ss");
        bool isOpen = now.Hour >= 6 && now.Hour < 22;
        string statusText = isOpen ? "Đang mở cửa" : "Đã đóng cửa";
        Color statusColor = isOpen ? Color.Green : Color.Red;

        lblGioHienTai.Text = time;
        lblDongHoLon.Text = time;
        lblTrangThai.Text = statusText;
        lblTrangThai.ForeColor = statusColor;
        lblTrangThaiLon.Text = statusText;
        lblTrangThaiLon.ForeColor = statusColor;
    }

    private void DoiMauNen_Click(object? sender, EventArgs e)
    {
        using ColorDialog dialog = new ColorDialog { Color = BackColor, FullOpen = true };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            BackColor = dialog.Color;
        }
    }
}
