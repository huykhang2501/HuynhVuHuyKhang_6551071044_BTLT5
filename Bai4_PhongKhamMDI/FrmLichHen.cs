namespace Bai4_PhongKhamMDI;

public sealed class FrmLichHen : Form
{
    private readonly DateTimePicker dtpNgayGio = new DateTimePicker();
    private readonly TextBox txtTenBenhNhan = new TextBox();
    private readonly ListBox lstLichHen = new ListBox();
    private readonly List<string> appointmentRecords = new List<string>();

    public FrmLichHen(int windowNumber)
    {
        Text = string.Format("Đặt lịch hẹn #{0}", windowNumber);
        ClientSize = new Size(700, 500);
        MinimumSize = new Size(600, 450);
        BackColor = Color.FromArgb(248, 250, 252);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;

        Panel header = new Panel { Dock = DockStyle.Top, Height = 78, BackColor = Color.FromArgb(37, 99, 235) };
        header.Controls.Add(new Label { Text = "ĐẶT LỊCH HẸN KHÁM", AutoSize = true, Location = new Point(25, 20), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 19F) });

        Panel form = new Panel { Dock = DockStyle.Top, Height = 170, BackColor = Color.White };
        form.Controls.Add(LabelFor("Tên bệnh nhân", 25, 35));
        txtTenBenhNhan.SetBounds(170, 27, 475, 34);
        form.Controls.Add(txtTenBenhNhan);
        form.Controls.Add(LabelFor("Ngày giờ hẹn", 25, 92));
        dtpNgayGio.SetBounds(170, 84, 300, 34);
        dtpNgayGio.Format = DateTimePickerFormat.Custom;
        dtpNgayGio.CustomFormat = "dd/MM/yyyy HH:mm";
        dtpNgayGio.Value = DateTime.Now.AddMinutes(30);
        form.Controls.Add(dtpNgayGio);
        Button btnDatLich = new Button { Text = "ĐẶT LỊCH", Location = new Point(495, 82), Size = new Size(150, 40), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 10F), Cursor = Cursors.Hand };
        btnDatLich.FlatAppearance.BorderSize = 0;
        btnDatLich.Click += delegate { SaveAppointment(); };
        form.Controls.Add(btnDatLich);
        AcceptButton = btnDatLich;

        Panel listHost = new Panel { Dock = DockStyle.Fill, Padding = new Padding(25, 45, 25, 25), BackColor = Color.FromArgb(248, 250, 252) };
        listHost.Controls.Add(new Label { Text = "LỊCH HẸN ĐÃ ĐẶT TRONG CỬA SỔ NÀY", AutoSize = true, Location = new Point(25, 14), ForeColor = Color.FromArgb(37, 99, 235), Font = new Font("Segoe UI Semibold", 10F) });
        lstLichHen.Dock = DockStyle.Fill;
        lstLichHen.BorderStyle = BorderStyle.FixedSingle;
        lstLichHen.Font = new Font("Segoe UI", 10.5F);
        listHost.Controls.Add(lstLichHen);
        lstLichHen.BringToFront();

        Controls.Add(listHost);
        Controls.Add(form);
        Controls.Add(header);
        Shown += delegate { txtTenBenhNhan.Focus(); };
    }

    private void SaveAppointment()
    {
        string name = txtTenBenhNhan.Text.Trim();
        if (name.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập tên bệnh nhân.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTenBenhNhan.Focus();
            return;
        }
        string record = string.Format("{0:dd/MM/yyyy HH:mm} | {1}", dtpNgayGio.Value, name);
        appointmentRecords.Add(record);
        lstLichHen.Items.Add(record);
        txtTenBenhNhan.Clear();
        dtpNgayGio.Value = DateTime.Now.AddMinutes(30);
        txtTenBenhNhan.Focus();
    }

    private static Label LabelFor(string text, int x, int y)
    {
        return new Label { Text = text, AutoSize = true, Location = new Point(x, y), Font = new Font("Segoe UI Semibold", 10F) };
    }
}
