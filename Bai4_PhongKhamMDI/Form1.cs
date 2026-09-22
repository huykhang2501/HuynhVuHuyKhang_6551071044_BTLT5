namespace Bai4_PhongKhamMDI;

public sealed class Form1 : Form
{
    private int patientWindowNumber;
    private int appointmentWindowNumber;
    private readonly ToolStripStatusLabel lblWindowCount = new ToolStripStatusLabel();

    public Form1()
    {
        Text = "Phòng khám An Tâm - Quản lý MDI";
        StartPosition = FormStartPosition.CenterScreen;
        WindowState = FormWindowState.Maximized;
        MinimumSize = new Size(900, 620);
        IsMdiContainer = true;
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;

        MenuStrip menu = new MenuStrip { BackColor = Color.White };
        ToolStripMenuItem nghiepVu = new ToolStripMenuItem("&Nghiệp vụ");
        ToolStripMenuItem thongTinBenhNhan = new ToolStripMenuItem("Thông tin &bệnh nhân");
        ToolStripMenuItem datLichHen = new ToolStripMenuItem("Đặt &lịch hẹn");
        ToolStripMenuItem cuaSo = new ToolStripMenuItem("&Cửa sổ");
        ToolStripMenuItem heThong = new ToolStripMenuItem("&Hệ thống");
        ToolStripMenuItem thoat = new ToolStripMenuItem("&Thoát");
        thongTinBenhNhan.ShortcutKeys = Keys.Control | Keys.B;
        datLichHen.ShortcutKeys = Keys.Control | Keys.L;
        thongTinBenhNhan.Click += delegate { OpenPatientForm(); };
        datLichHen.Click += delegate { OpenAppointmentForm(); };
        thoat.Click += delegate { Close(); };
        nghiepVu.DropDownItems.Add(thongTinBenhNhan);
        nghiepVu.DropDownItems.Add(datLichHen);
        heThong.DropDownItems.Add(thoat);
        menu.Items.Add(nghiepVu);
        menu.Items.Add(cuaSo);
        menu.Items.Add(heThong);
        menu.MdiWindowListItem = cuaSo;
        MainMenuStrip = menu;

        ToolStrip toolBar = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden, BackColor = Color.FromArgb(239, 246, 255), Padding = new Padding(8, 5, 8, 5) };
        ToolStripButton btnPatient = new ToolStripButton("+ Bệnh nhân mới");
        ToolStripButton btnAppointment = new ToolStripButton("+ Lịch hẹn mới");
        btnPatient.Click += delegate { OpenPatientForm(); };
        btnAppointment.Click += delegate { OpenAppointmentForm(); };
        toolBar.Items.Add(btnPatient);
        toolBar.Items.Add(new ToolStripSeparator());
        toolBar.Items.Add(btnAppointment);

        StatusStrip status = new StatusStrip { BackColor = Color.FromArgb(219, 234, 254), SizingGrip = false };
        status.Items.Add(new ToolStripStatusLabel("PHÒNG KHÁM AN TÂM"));
        status.Items.Add(new ToolStripStatusLabel { Spring = true });
        status.Items.Add(lblWindowCount);
        lblWindowCount.Text = "0 cửa sổ đang mở";

        Controls.Add(status);
        Controls.Add(toolBar);
        Controls.Add(menu);
        MdiChildActivate += delegate { UpdateWindowCount(); };
        FormClosing += Form1_FormClosing;
    }

    private void OpenPatientForm()
    {
        patientWindowNumber++;
        FrmBenhNhan child = new FrmBenhNhan(patientWindowNumber)
        {
            MdiParent = this
        };
        child.FormClosed += delegate { UpdateWindowCount(); };
        child.Show();
        UpdateWindowCount();
    }

    private void OpenAppointmentForm()
    {
        appointmentWindowNumber++;
        FrmLichHen child = new FrmLichHen(appointmentWindowNumber)
        {
            MdiParent = this
        };
        child.FormClosed += delegate { UpdateWindowCount(); };
        child.Show();
        UpdateWindowCount();
    }

    private void UpdateWindowCount()
    {
        lblWindowCount.Text = string.Format("{0} cửa sổ đang mở", MdiChildren.Length);
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có chắc muốn đóng phần mềm phòng khám?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (result == DialogResult.No) e.Cancel = true;
    }
}
