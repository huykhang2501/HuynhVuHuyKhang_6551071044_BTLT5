namespace Bai1_FormBanHang;

public sealed class Form1 : Form
{
    private readonly TextBox txtMaSP = new TextBox();
    private readonly TextBox txtSoLuong = new TextBox();
    private readonly TextBox txtDonGia = new TextBox();
    private readonly ListBox lstKetQua = new ListBox();
    private bool allowClose;

    public Form1()
    {
        Text = "FormBanHang - Nhập liệu siêu thị";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(820, 610);
        MinimumSize = new Size(840, 650);
        BackColor = Color.FromArgb(241, 245, 249);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;
        KeyPreview = true;

        Panel header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 98,
            BackColor = Color.FromArgb(5, 150, 105)
        };
        header.Controls.Add(new Label
        {
            Text = "NHẬP LIỆU BÁN HÀNG",
            AutoSize = true,
            Location = new Point(30, 17),
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 21F)
        });
        header.Controls.Add(new Label
        {
            Text = "F2: Thêm sản phẩm   •   F5: Xóa trắng   •   Esc: Thoát",
            AutoSize = true,
            Location = new Point(33, 61),
            ForeColor = Color.FromArgb(209, 250, 229)
        });

        TableLayoutPanel content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(30, 26, 30, 28)
        };
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 225F));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        Panel inputCard = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Margin = new Padding(0, 0, 0, 18)
        };
        inputCard.Controls.Add(CreateLabel("Mã sản phẩm", 25, 28));
        inputCard.Controls.Add(CreateLabel("Số lượng", 25, 83));
        inputCard.Controls.Add(CreateLabel("Đơn giá", 25, 138));

        ConfigureInput(txtMaSP, 165, 22, 360, 0);
        ConfigureInput(txtSoLuong, 165, 77, 360, 1);
        ConfigureInput(txtDonGia, 165, 132, 360, 2);
        txtSoLuong.KeyPress += NumericTextBox_KeyPress;
        txtDonGia.KeyPress += NumericTextBox_KeyPress;
        txtSoLuong.TextChanged += NumericTextBox_TextChanged;
        txtDonGia.TextChanged += NumericTextBox_TextChanged;
        inputCard.Controls.Add(txtMaSP);
        inputCard.Controls.Add(txtSoLuong);
        inputCard.Controls.Add(txtDonGia);

        Button btnThem = CreateButton("Thêm (F2)", true);
        btnThem.SetBounds(555, 42, 170, 48);
        btnThem.TabIndex = 3;
        btnThem.Click += delegate { AddProduct(); };
        inputCard.Controls.Add(btnThem);

        Button btnXoaTrang = CreateButton("Xóa trắng (F5)", false);
        btnXoaTrang.SetBounds(555, 112, 170, 48);
        btnXoaTrang.TabIndex = 4;
        btnXoaTrang.Click += delegate { ClearInputs(); };
        inputCard.Controls.Add(btnXoaTrang);

        Panel resultCard = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Margin = Padding.Empty,
            Padding = new Padding(22, 55, 22, 20)
        };
        Label resultTitle = new Label
        {
            Text = "DANH SÁCH SẢN PHẨM ĐÃ THÊM",
            AutoSize = true,
            Location = new Point(22, 18),
            ForeColor = Color.FromArgb(4, 120, 87),
            Font = new Font("Segoe UI Semibold", 12F)
        };
        resultCard.Controls.Add(resultTitle);
        lstKetQua.Dock = DockStyle.Fill;
        lstKetQua.BorderStyle = BorderStyle.FixedSingle;
        lstKetQua.Font = new Font("Consolas", 11F);
        lstKetQua.TabIndex = 5;
        resultCard.Controls.Add(lstKetQua);
        lstKetQua.BringToFront();

        content.Controls.Add(inputCard, 0, 0);
        content.Controls.Add(resultCard, 0, 1);
        Controls.Add(content);
        Controls.Add(header);

        KeyDown += Form1_KeyDown;
        FormClosing += Form1_FormClosing;
        Shown += delegate { txtMaSP.Focus(); };
    }

    private static Label CreateLabel(string text, int x, int y)
    {
        return new Label
        {
            Text = text,
            AutoSize = true,
            Location = new Point(x, y),
            Font = new Font("Segoe UI Semibold", 10F)
        };
    }

    private static void ConfigureInput(TextBox textBox, int x, int y, int width, int tabIndex)
    {
        textBox.SetBounds(x, y, width, 34);
        textBox.Font = new Font("Segoe UI", 11F);
        textBox.BorderStyle = BorderStyle.FixedSingle;
        textBox.TabIndex = tabIndex;
    }

    private static Button CreateButton(string text, bool primary)
    {
        Button button = new Button
        {
            Text = text,
            FlatStyle = FlatStyle.Flat,
            BackColor = primary ? Color.FromArgb(5, 150, 105) : Color.White,
            ForeColor = primary ? Color.White : Color.FromArgb(15, 23, 42),
            Font = new Font("Segoe UI Semibold", 10F),
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderColor = primary
            ? Color.FromArgb(5, 150, 105)
            : Color.FromArgb(203, 213, 225);
        return button;
    }

    private static void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
        {
            e.Handled = true;
        }
    }

    private static void NumericTextBox_TextChanged(object? sender, EventArgs e)
    {
        TextBox? textBox = sender as TextBox;
        if (textBox == null)
        {
            return;
        }

        string filtered = new string(textBox.Text.Where(char.IsDigit).ToArray());
        if (filtered == textBox.Text)
        {
            return;
        }

        int caretPosition = Math.Min(textBox.SelectionStart, filtered.Length);
        textBox.Text = filtered;
        textBox.SelectionStart = caretPosition;
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F2)
        {
            AddProduct();
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.F5)
        {
            ClearInputs();
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Escape)
        {
            RequestClose();
            e.SuppressKeyPress = true;
        }
    }

    private void AddProduct()
    {
        string maSP = txtMaSP.Text.Trim();
        string soLuong = txtSoLuong.Text.Trim();
        string donGia = txtDonGia.Text.Trim();

        if (maSP.Length == 0)
        {
            ShowInputError("Bạn chưa nhập mã sản phẩm.", txtMaSP);
            return;
        }
        if (soLuong.Length == 0)
        {
            ShowInputError("Bạn chưa nhập số lượng.", txtSoLuong);
            return;
        }
        if (donGia.Length == 0)
        {
            ShowInputError("Bạn chưa nhập đơn giá.", txtDonGia);
            return;
        }

        lstKetQua.Items.Add(string.Format("{0} | {1} | {2}", maSP, soLuong, donGia));
        ClearInputs();
    }

    private static void ShowInputError(string message, Control control)
    {
        MessageBox.Show(message, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        control.Focus();
    }

    private void ClearInputs()
    {
        txtMaSP.Clear();
        txtSoLuong.Clear();
        txtDonGia.Clear();
        txtMaSP.Focus();
    }

    private void RequestClose()
    {
        DialogResult result = MessageBox.Show(
            "Bạn có muốn thoát?",
            "Xác nhận thoát",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

        if (result == DialogResult.Yes)
        {
            allowClose = true;
            Close();
        }
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (allowClose)
        {
            return;
        }

        DialogResult result = MessageBox.Show(
            "Bạn có muốn thoát?",
            "Xác nhận thoát",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

        if (result == DialogResult.No)
        {
            e.Cancel = true;
        }
        else
        {
            allowClose = true;
        }
    }
}
