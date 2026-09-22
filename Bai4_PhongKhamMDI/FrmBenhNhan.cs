namespace Bai4_PhongKhamMDI;

public sealed class FrmBenhNhan : Form
{
    private readonly TextBox txtHoTen = new TextBox();
    private readonly NumericUpDown numTuoi = new NumericUpDown();
    private readonly TextBox txtTrieuChung = new TextBox();
    private readonly ListBox lstBenhNhan = new ListBox();
    private readonly List<string> patientRecords = new List<string>();

    public FrmBenhNhan(int windowNumber)
    {
        Text = string.Format("Thông tin bệnh nhân #{0}", windowNumber);
        ClientSize = new Size(720, 560);
        MinimumSize = new Size(620, 500);
        BackColor = Color.FromArgb(248, 250, 252);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;

        Panel header = new Panel { Dock = DockStyle.Top, Height = 78, BackColor = Color.FromArgb(14, 116, 144) };
        header.Controls.Add(new Label { Text = "THÔNG TIN BỆNH NHÂN", AutoSize = true, Location = new Point(25, 20), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 19F) });

        Panel form = new Panel { Dock = DockStyle.Top, Height = 205, BackColor = Color.White, Padding = new Padding(25) };
        form.Controls.Add(LabelFor("Họ và tên", 25, 30));
        txtHoTen.SetBounds(155, 22, 340, 34);
        form.Controls.Add(txtHoTen);
        form.Controls.Add(LabelFor("Tuổi", 520, 30));
        numTuoi.SetBounds(570, 22, 100, 34);
        numTuoi.Minimum = 1;
        numTuoi.Maximum = 120;
        numTuoi.Value = 18;
        form.Controls.Add(numTuoi);
        form.Controls.Add(LabelFor("Triệu chứng", 25, 85));
        txtTrieuChung.SetBounds(155, 78, 515, 70);
        txtTrieuChung.Multiline = true;
        txtTrieuChung.ScrollBars = ScrollBars.Vertical;
        form.Controls.Add(txtTrieuChung);
        Button btnLuuTam = ActionButton("LƯU TẠM");
        btnLuuTam.SetBounds(520, 158, 150, 40);
        btnLuuTam.Click += delegate { SavePatient(); };
        form.Controls.Add(btnLuuTam);
        AcceptButton = btnLuuTam;

        Panel listHost = new Panel { Dock = DockStyle.Fill, Padding = new Padding(25, 45, 25, 25), BackColor = Color.FromArgb(248, 250, 252) };
        listHost.Controls.Add(new Label { Text = "BỆNH NHÂN ĐÃ NHẬP TRONG CỬA SỔ NÀY", AutoSize = true, Location = new Point(25, 14), ForeColor = Color.FromArgb(14, 116, 144), Font = new Font("Segoe UI Semibold", 10F) });
        lstBenhNhan.Dock = DockStyle.Fill;
        lstBenhNhan.BorderStyle = BorderStyle.FixedSingle;
        lstBenhNhan.Font = new Font("Segoe UI", 10.5F);
        listHost.Controls.Add(lstBenhNhan);
        lstBenhNhan.BringToFront();

        Controls.Add(listHost);
        Controls.Add(form);
        Controls.Add(header);
        Shown += delegate { txtHoTen.Focus(); };
    }

    private void SavePatient()
    {
        string name = txtHoTen.Text.Trim();
        string symptom = txtTrieuChung.Text.Trim();
        if (name.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập họ tên bệnh nhân.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return;
        }
        string record = string.Format("{0} | {1} tuổi | Triệu chứng: {2}", name, numTuoi.Value, symptom.Length == 0 ? "Chưa ghi nhận" : symptom);
        patientRecords.Add(record);
        lstBenhNhan.Items.Add(record);
        txtHoTen.Clear();
        txtTrieuChung.Clear();
        numTuoi.Value = 18;
        txtHoTen.Focus();
    }

    private static Label LabelFor(string text, int x, int y)
    {
        return new Label { Text = text, AutoSize = true, Location = new Point(x, y), Font = new Font("Segoe UI Semibold", 10F) };
    }

    private static Button ActionButton(string text)
    {
        Button button = new Button { Text = text, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(14, 116, 144), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 10F), Cursor = Cursors.Hand };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }
}
