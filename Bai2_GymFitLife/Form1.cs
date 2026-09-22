namespace Bai2_GymFitLife;

public sealed class Form1 : Form
{
    private readonly TextBox txtHoTen = new TextBox();
    private readonly TextBox txtSDT = new TextBox();
    private readonly TextBox txtEmail = new TextBox();
    private readonly DateTimePicker dtpNgaySinh = new DateTimePicker();
    private readonly ComboBox cboGoiTap = new ComboBox();
    private readonly NumericUpDown numSoBuoiTuan = new NumericUpDown();
    private readonly ToolTip toolTip1 = new ToolTip();

    public Form1()
    {
        Text = "FitLife - Đăng ký hội viên";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(760, 690);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        BackColor = Color.FromArgb(240, 253, 244);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;

        Panel header = new Panel { Dock = DockStyle.Top, Height = 105, BackColor = Color.FromArgb(22, 163, 74) };
        header.Controls.Add(new Label { Text = "FITLIFE MEMBERSHIP", AutoSize = true, Location = new Point(35, 18), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 24F) });
        header.Controls.Add(new Label { Text = "Đăng ký hội viên phòng Gym", AutoSize = true, Location = new Point(39, 68), ForeColor = Color.FromArgb(220, 252, 231) });

        Panel card = new Panel { Location = new Point(45, 135), Size = new Size(670, 500), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
        AddField(card, "Họ và tên", txtHoTen, 35, 35);
        AddField(card, "Số điện thoại", txtSDT, 35, 100);
        AddField(card, "Email", txtEmail, 35, 165);
        card.Controls.Add(CreateLabel("Ngày sinh", 35, 235));
        dtpNgaySinh.SetBounds(205, 226, 410, 34);
        dtpNgaySinh.Format = DateTimePickerFormat.Long;
        dtpNgaySinh.MaxDate = DateTime.Today;
        card.Controls.Add(dtpNgaySinh);
        card.Controls.Add(CreateLabel("Gói tập", 35, 300));
        cboGoiTap.SetBounds(205, 292, 410, 34);
        cboGoiTap.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGoiTap.Items.AddRange(new object[] { "Basic", "VIP", "Premium" });
        cboGoiTap.SelectedIndex = 0;
        card.Controls.Add(cboGoiTap);
        card.Controls.Add(CreateLabel("Số buổi/tuần", 35, 365));
        numSoBuoiTuan.SetBounds(205, 357, 410, 34);
        numSoBuoiTuan.Minimum = 1;
        numSoBuoiTuan.Maximum = 7;
        numSoBuoiTuan.Value = 3;
        card.Controls.Add(numSoBuoiTuan);
        Button btnDangKy = new Button { Text = "ĐĂNG KÝ HỘI VIÊN", Location = new Point(205, 425), Size = new Size(260, 48), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(22, 163, 74), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 11F), Cursor = Cursors.Hand };
        btnDangKy.FlatAppearance.BorderSize = 0;
        btnDangKy.Click += BtnDangKy_Click;
        card.Controls.Add(btnDangKy);
        AcceptButton = btnDangKy;

        toolTip1.AutoPopDelay = 5000;
        toolTip1.InitialDelay = 500;
        toolTip1.ReshowDelay = 100;
        toolTip1.ShowAlways = true;
        toolTip1.SetToolTip(txtHoTen, "Nhập đầy đủ họ và tên hội viên");
        toolTip1.SetToolTip(txtSDT, "Nhập đúng 10 chữ số, không chứa khoảng trắng hay ký tự đặc biệt");
        toolTip1.SetToolTip(txtEmail, "Email dùng để nhận thông báo lịch tập và khuyến mãi");
        toolTip1.SetToolTip(dtpNgaySinh, "Chọn ngày sinh của hội viên");
        toolTip1.SetToolTip(cboGoiTap, "Gói VIP và Premium có kèm huấn luyện viên riêng");
        toolTip1.SetToolTip(numSoBuoiTuan, "Chọn số buổi tập mỗi tuần, từ 1 đến 7 buổi");
        toolTip1.SetToolTip(btnDangKy, "Nhấn để kiểm tra và hoàn tất đăng ký");

        txtSDT.MaxLength = 10;
        txtSDT.KeyPress += delegate (object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        };
        Controls.Add(card);
        Controls.Add(header);
        Shown += delegate { txtHoTen.Focus(); };
    }

    private static void AddField(Control parent, string label, TextBox textBox, int x, int y)
    {
        parent.Controls.Add(CreateLabel(label, x, y + 8));
        textBox.SetBounds(205, y, 410, 34);
        textBox.Font = new Font("Segoe UI", 11F);
        textBox.BorderStyle = BorderStyle.FixedSingle;
        parent.Controls.Add(textBox);
    }

    private static Label CreateLabel(string text, int x, int y)
    {
        return new Label { Text = text, AutoSize = true, Location = new Point(x, y), Font = new Font("Segoe UI Semibold", 10F) };
    }

    private void InitializeComponent()
    {

    }

    private void BtnDangKy_Click(object? sender, EventArgs e)
    {
        string hoTen = txtHoTen.Text.Trim();
        string sdt = txtSDT.Text.Trim();
        if (hoTen.Length == 0 || sdt.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Số điện thoại", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            (hoTen.Length == 0 ? txtHoTen : txtSDT).Focus();
            return;
        }
        if (sdt.Length != 10 || !sdt.All(char.IsDigit))
        {
            MessageBox.Show("Số điện thoại phải gồm đúng 10 chữ số.", "Số điện thoại không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSDT.Focus();
            return;
        }

        string summary = string.Format(
            "Họ tên: {0}\nSĐT: {1}\nGói tập: {2}\nSố buổi/tuần: {3}",
            hoTen, sdt, cboGoiTap.Text, numSoBuoiTuan.Value);
        MessageBox.Show(summary, "Đăng ký thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
